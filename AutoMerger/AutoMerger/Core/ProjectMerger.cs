using BranchManager.Core.Types;
using System.Collections.ObjectModel;
using System.Linq;

namespace AutoMerger.Core
{
	interface IProjectMerger
	{
		void MergeProject(Project project);
	}

	class ProjectMerger : IProjectMerger
	{
		private readonly IMerger _merger;

		public ProjectMerger(IMerger merger)
		{
			_merger = merger;
		}

		public void MergeProject(Project project)
		{
			var enabledMerges = project.Merges.Where(m => m.Enabled).ToList().AsReadOnly();

			var rootMerges = enabledMerges.Where(m1 => enabledMerges.All(m2 => m1.Parent != m2.Child));

			foreach(var merge in rootMerges)
			{
				HandleMerge(project.ProjectUrl, merge, enabledMerges);
			}
		}

		private void HandleMerge(string projectUrl, Merge merge, ReadOnlyCollection<Merge> merges)
		{
			_merger.Merge(projectUrl, merge.Parent, merge.Child);

			var childMerges = merges.Where(m => merge.Child == m.Parent);

			foreach(var childMerge in childMerges)
			{
				HandleMerge(projectUrl, childMerge, merges);
			}
		}
	}
}
