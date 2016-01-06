using AutoMerger.Shared.Core;
using BranchManager.Core.Types;
using SharpSvn;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ConfigProject = AutoMerger.Shared.Types.Project;
using Merge = AutoMerger.Shared.Types.Merge;

namespace BranchManager.Core.Tree
{
	public interface IMergeTreeGetter
	{
		IEnumerable<Project> GetTree();
	}

	class MergeTreeGetter : IMergeTreeGetter
	{
		private readonly IConfigGetter _configGetter;
		private readonly ISvnInterface _svnInterface;
		private readonly IUnmergedRevisionGetter _unmergedRevisionGetter;

		public MergeTreeGetter(
			IConfigGetter configGetter,
			ISvnInterface svnInterface,
			IUnmergedRevisionGetter unmergedRevisionGetter)
		{
			_configGetter = configGetter;
			_svnInterface = svnInterface;
			_unmergedRevisionGetter = unmergedRevisionGetter;
		}

		public IEnumerable<Project> GetTree()
		{
			var config = _configGetter.GetConfig();

			return config.Projects.Select(GetProjectTree);
		}

		private Project GetProjectTree(ConfigProject project)
		{
			Collection<SvnListEventArgs> svnBranches;
			_svnInterface.List(project.ProjectUrl, out svnBranches);

			var rootMerges = project
				.Merges
				.Where(m1 => project.Merges.All(m2 => m1.Parent != m2.Child))
				.Select(m => m.Parent)
				.Distinct();

			return new Project(
				project.ProjectUrl,
				rootMerges.Select(r => GetNodeTree(
					r,
					project.ProjectUrl,
					project.Merges,
					svnBranches.Skip(1).Select(b => b.Name).Concat(new List<string> { "trunk" }).ToList())));
		}

		private Node GetNodeTree(string name, string projectUrl, IEnumerable<Merge> merges, IList<string> svnBranches)
		{
			var childMerges = merges.Where(m => m.Parent == name);

			return new Node(
				name,
				svnBranches.Contains(name),
				childMerges.Select(m =>
					new Branch(
						GetNodeTree(m.Child, projectUrl, merges, svnBranches),
						m.Enabled,
						svnBranches.Contains(name) && svnBranches.Contains(m.Child)
							? _unmergedRevisionGetter.GetUnmergedRevisions(projectUrl, m.Child, name)
							: (long?) null)));
		}
	}
}
