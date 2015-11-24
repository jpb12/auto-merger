using AutoMerger.Results;
using SharpSvn;
using System;
using System.IO;

namespace AutoMerger.Core
{
	interface IMerger
	{
		MergeResult Merge(string projectUrl, string parent, string child);
	}

	class Merger : IMerger
	{
		private readonly ISvnInterface _svnInterface;
		private readonly string _mergesFolder;

		public Merger(ISvnInterface svnInterface, IConfigurationManager configManager)
		{
			_svnInterface = svnInterface;
			_mergesFolder = configManager.GetStringValue(ConfigKey.MergesFolder);
		}

		public MergeResult Merge(string projectUrl, string parent, string child)
		{
			var folderPath = Path.Combine(
				_mergesFolder,
				projectUrl.Substring(projectUrl.LastIndexOf('/') + 1),
				child);

			try {
				PrepareWorkingCopy(projectUrl, child, folderPath);

				return PerformMerge(projectUrl, parent, child, folderPath);
			}
			catch (Exception e)
			{
				return new MergeResult(
					parent,
					child,
					false,
					e.Message,
					e);
			}
		}

		private void PrepareWorkingCopy(string projectUrl, string child, string folderPath)
		{
			if (Directory.Exists(folderPath))
			{
				if (_svnInterface.CheckForModifications(folderPath))
				{
					throw new InvalidOperationException("Working copy contained local modifications.");
				}

				if (!_svnInterface.Update(folderPath))
				{
					throw new InvalidOperationException("Unable to update working copy.");
				}
			}
			else
			{
				if (!_svnInterface.Checkout(projectUrl, child, folderPath))
				{
					throw new InvalidOperationException("Unable to checkout working copy.");
				}
			}
		}

		private MergeResult PerformMerge(string projectUrl, string parent, string child, string folderPath)
		{
			var initialRange = _svnInterface.GetMergeInfo(folderPath, parent);

			if (!_svnInterface.Merge(projectUrl, parent, folderPath))
			{
				throw new InvalidOperationException("Unable to merge into working copy.");
			}

			if (!_svnInterface.CheckForModifications(folderPath))
			{
				return new MergeResult(
					parent,
					child,
					true,
					"No merge required");
			}

			if (_svnInterface.CheckForConflicts(folderPath))
			{
				throw new InvalidOperationException("Working copy contained conflicts after merge.");
			}

			var newRange = _svnInterface.GetMergeInfo(folderPath, parent);

			var mergedRevisions = GetMergedRevisions(initialRange, newRange);

			var commitMessage = GetCommitMessage(parent, child, mergedRevisions);

			if (!_svnInterface.Commit(folderPath, commitMessage))
			{
				throw new InvalidOperationException("Unable to commit changes from working copy.");
			}

			return new MergeResult(
				parent,
				child,
				true,
				GetSuccessMessage(mergedRevisions));
		}

		private string GetSuccessMessage(SvnRevisionRange mergedRevisions)
		{
			return string.Format(
				"Merged revisions r{0}-r{1}",
				mergedRevisions.StartRevision.Revision,
				mergedRevisions.EndRevision.Revision);
		}

		private string GetCommitMessage(string parent, string child, SvnRevisionRange mergedRevisions)
		{
			return string.Format(
				"{0} from {1} to {2}",
				GetSuccessMessage(mergedRevisions),
				parent,
				child);
		}

		private SvnRevisionRange GetMergedRevisions(SvnRevisionRange initialRange, SvnRevisionRange newRange)
		{
			if (initialRange != null)
			{
				return new SvnRevisionRange(
					initialRange.EndRevision.Revision + 1,
					newRange.EndRevision);
			}

			return newRange;
		}
	}
}
