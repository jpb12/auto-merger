using BranchManager.Core.Types;
using System.Collections.Generic;
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

			var tasks = new List<Task>();

			foreach (var merge in rootMerges)
			{
				tasks.Add(Task.Factory.StartNew(() => HandleMerge(project.ProjectUrl, merge, enabledMerges)));
			}

			tasks.ForEach(t => t.Wait());
		}

		private void HandleMerge(string projectUrl, Merge merge, ReadOnlyCollection<Merge> merges)
		{
			while (!_threadManager.TryStartThread())
			{
				Thread.Sleep(1000);
			}

			_merger.Merge(projectUrl, merge.Parent, merge.Child);

			var childMerges = merges.Where(m => merge.Child == m.Parent);

			var tasks = new List<Task>();

			foreach (var childMerge in childMerges)
			{
				tasks.Add(Task.Factory.StartNew(() => HandleMerge(projectUrl, childMerge, merges)));
			}

			tasks.ForEach(t => t.Wait());
		}
	}
}
