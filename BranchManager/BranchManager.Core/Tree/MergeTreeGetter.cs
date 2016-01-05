using AutoMerger.Shared.Core;
using BranchManager.Core.Types;
using System.Collections.Generic;
using System.Linq;
using Merge = AutoMerger.Shared.Types.Merge;
using ConfigProject = AutoMerger.Shared.Types.Project;

namespace BranchManager.Core.Tree
{
	public interface IMergeTreeGetter
	{
		IEnumerable<Project> GetTree();
	}

	class MergeTreeGetter : IMergeTreeGetter
	{
		private readonly IConfigGetter _configGetter;
		private readonly IUnmergedRevisionGetter _unmergedRevisionGetter;

		public MergeTreeGetter(IConfigGetter configGetter, IUnmergedRevisionGetter unmergedRevisionGetter)
		{
			_configGetter = configGetter;
			_unmergedRevisionGetter = unmergedRevisionGetter;
		}

		public IEnumerable<Project> GetTree()
		{
			var config = _configGetter.GetConfig();

			return config.Projects.Select(GetProjectTree);
		}

		private Project GetProjectTree(ConfigProject project)
		{
			var rootMerges = project
				.Merges
				.Where(m1 => project.Merges.All(m2 => m1.Parent != m2.Child))
				.Select(m => m.Parent)
				.Distinct();

			return new Project(
				project.ProjectUrl,
				rootMerges.Select(r => GetNodeTree(r, project.ProjectUrl, project.Merges)));
		}

		private Node GetNodeTree(string name, string projectUrl, IEnumerable<Merge> merges)
		{
			var childMerges = merges.Where(m => m.Parent == name);

			return new Node(
				name,
				childMerges.Select(m =>
					new Branch(
						m.Enabled,
						GetNodeTree(m.Child, projectUrl, merges),
						_unmergedRevisionGetter.GetUnmergedRevisions(projectUrl, m.Child, name))));
		}
	}
}
