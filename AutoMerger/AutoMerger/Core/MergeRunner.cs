using AutoMerger.Results;
using AutoMerger.Shared.Core;
using AutoMerger.Shared.Types;
using AutoMerger.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMerger.Core
{
	interface IMergeRunner
	{
		void Run();
	}

	class MergeRunner : IMergeRunner
	{
		private readonly IProjectMerger _projectMerger;
		private readonly IConfigGetter _configGetter;
		private readonly IConfigurationManager<ConfigKey> _configManager;
		private readonly IReportGenerator _reportGenerator;
		private readonly IEmailSender _emailSender;

		public MergeRunner(
			IProjectMerger projectMerger,
			IConfigGetter configGetter,
			IConfigurationManager<ConfigKey> configManager,
			IReportGenerator reportGenerator,
			IEmailSender emailSender)
		{
			_projectMerger = projectMerger;
			_configGetter = configGetter;
			_configManager = configManager;
			_reportGenerator = reportGenerator;
			_emailSender = emailSender;
		}

		public void Run()
		{
			var config = _configGetter.GetConfig();

			IEnumerable<ProjectMergeResult> results;

			var projectUrl = _configManager.GetStringValue(ConfigKey.ProjectUrl);

			if (string.IsNullOrEmpty(projectUrl))
			{
				results = RunAllMerges(config);
			}
			else
			{
				var parent = _configManager.GetStringValue(ConfigKey.Parent);
				var child = _configManager.GetStringValue(ConfigKey.Child);

				results = new List<ProjectMergeResult>
				{
					RunSpecificMerges(config, projectUrl.TrimEnd('/'), parent, child)
				};
			}

			var report = _reportGenerator.Generate(results);

			Console.Write(report);

			_emailSender.SendSummaryEmail(report);
		}

		private IEnumerable<ProjectMergeResult> RunAllMerges(MergeConfig config)
		{
			return config
				.Projects
				.AsParallel()
				.Select(p => _projectMerger.MergeProject(p.ProjectUrl, p.Merges))
				.OrderBy(r => r.ProjectUrl);
		}

		private ProjectMergeResult RunSpecificMerges(MergeConfig config, string projectUrl, string parent, string child)
		{
			var project = config.Projects.SingleOrDefault(p => p.ProjectUrl == projectUrl);

			if (project == null)
			{
				throw new InvalidOperationException("Config did not contain a project with url: " + projectUrl);
			}

			// If both a parent and child are specified, we merge from the parent into the child, via any
			// intermediate branches if necessary
			if (!string.IsNullOrEmpty(parent) && !string.IsNullOrEmpty(child))
			{
				if (parent == child)
				{
					throw new InvalidOperationException("Parent and child were both " + parent);
				}

				var merges = GetMergePath(project.Merges, parent, child);

				if (!merges.Any())
				{
					throw new InvalidOperationException(string.Format(
						"Unable to find path from {0} to {1} in project with url {2}",
						parent,
						child,
						projectUrl));
				}

				return new ProjectMergeResult(
					projectUrl,
					merges.Select(m => _projectMerger.MergeIndividualMerge(projectUrl, m)));
			}
			// If just the parent is specified, we do a full, recursive merge starting from the parent
			else if (!string.IsNullOrEmpty(parent))
			{
				var merges = project.Merges.Where(m => m.Parent == parent && m.Enabled);
				return Task<ProjectMergeResult>.Factory.StartNew(
					() => _projectMerger.MergeProject(project.ProjectUrl, merges)).Result;
			}
			// If just the child is specified, we merge into the child
			else if (!string.IsNullOrEmpty(child))
			{
				var merge = project.Merges.SingleOrDefault(m => m.Child == child);

				if (merge == null)
				{
					throw new InvalidOperationException(string.Format(
						"Unable to find a merge into {0} in project with url {1}",
						child,
						projectUrl));
				}

				return new ProjectMergeResult(
					projectUrl,
					new List<MergeResult>
					{
						_projectMerger.MergeIndividualMerge(projectUrl, merge)
					});
			}
			// If neither are specified, we do a full, recursive merge of the whole project
			else
			{
				return Task<ProjectMergeResult>.Factory.StartNew(
					() => _projectMerger.MergeProject(project.ProjectUrl, project.Merges)).Result;
			}
		}

		/// <summary>
		/// Recursively searches for a route through the merge tree from parent to child
		/// </summary>
		private IList<Merge> GetMergePath(IList<Merge> merges, string parent, string child)
		{
			var parentMerges = merges.Where(m => m.Parent == parent).ToList();

			if (parentMerges.Any(m => m.Child == child))
			{
				return new List<Merge> { parentMerges.Single(m => m.Child == child) };
			}

			foreach (var merge in parentMerges)
			{
				var path = GetMergePath(merges, merge.Child, child);

				if (path.Any())
				{
					return new List<Merge> { merge }.Concat(path).ToList();
				}
			}

			return new List<Merge>();
		}
	}
}
