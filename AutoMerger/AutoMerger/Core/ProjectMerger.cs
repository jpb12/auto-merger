using BranchManager.Core.Types;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AutoMerger.Core
{
	interface IProjectMerger
	{
		void MergeProject(Project project);
	}

	class ProjectMerger : IProjectMerger
	{
		private readonly IMerger _merger;
		private readonly IThreadManager _threadManager;

		public ProjectMerger(IMerger merger, IThreadManager threadManager)
		{
			_merger = merger;
			_threadManager = threadManager;
		}

		public void MergeProject(Project project)
		{
			var enabledMerges = project.Merges.Where(m => m.Enabled).ToList().AsReadOnly();

			var rootMerges = enabledMerges.Where(m1 => enabledMerges.All(m2 => m1.Parent != m2.Child));

			foreach(var merge in rootMerges)
			{
				Task.Factory.StartNew(() => HandleMerge(project.ProjectUrl, merge, enabledMerges));
			}
		}

		private void HandleMerge(string projectUrl, Merge merge, ReadOnlyCollection<Merge> merges)
		{
			while (!_threadManager.TryStartThread())
			{
				Thread.Sleep(1000);
			}

			_merger.Merge(projectUrl, merge.Parent, merge.Child);

			var childMerges = merges.Where(m => merge.Child == m.Parent);

			foreach(var childMerge in childMerges)
			{
				Task.Factory.StartNew(() => HandleMerge(projectUrl, childMerge, merges));
			}
		}
	}
}
