using AutoMerger.Results;
using AutoMerger.Shared.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace AutoMerger.Core
{
	interface IProjectMerger
	{
		ProjectMergeResult MergeProject(string projectUrl, IEnumerable<Merge> merges);
		MergeResult MergeIndividualMerge(string projectUrl, Merge merge);
	}

	class ProjectMerger : IProjectMerger
	{
		private readonly IMerger _merger;
		private readonly IThreadManager _threadManager;
		private readonly IEmailSender _emailSender;

		public ProjectMerger(IMerger merger, IThreadManager threadManager, IEmailSender emailSender)
		{
			_merger = merger;
			_threadManager = threadManager;
			_emailSender = emailSender;
		}

		public ProjectMergeResult MergeProject(string projectUrl, IEnumerable<Merge> merges)
		{
			var enabledMerges = merges.Where(m => m.Enabled).ToList().AsReadOnly();

			var rootMerges = enabledMerges.Where(m1 => enabledMerges.All(m2 => m1.Parent != m2.Child));

			var results = rootMerges
				.AsParallel()
				.Select(m => HandleMerge(projectUrl, m, enabledMerges))
				.SelectMany(t => t)
				.OrderBy(r => r.Parent)
				.ThenBy(r => r.Child);

			return new ProjectMergeResult(projectUrl, results);
		}

		public MergeResult MergeIndividualMerge(string projectUrl, Merge merge)
		{
			var result = _merger.Merge(projectUrl, merge.Parent, merge.Child);

			try
			{
				_emailSender.SendIndividualMergeEmail(merge, result);
			}
			catch (Exception e)
			{
				result = new MergeResult(
					merge.Parent,
					merge.Child,
					false,
					string.Format("{0} - {1}", result.Message, e.Message),
					e);
			}

			return result;
		}

		private IEnumerable<MergeResult> HandleMerge(string projectUrl, Merge merge, ReadOnlyCollection<Merge> merges)
		{
			while (!_threadManager.TryStartThread())
			{
				Thread.Sleep(1000);
			}

			var result = MergeIndividualMerge(projectUrl, merge);

			var childMerges = merges.Where(m => merge.Child == m.Parent);

			var results = childMerges
				.AsParallel()
				.Select(m => HandleMerge(projectUrl, m, merges))
				.SelectMany(t => t);

			return new List<MergeResult> { result }.Concat(results);
		}
	}
}
