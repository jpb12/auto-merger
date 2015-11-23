using SharpSvn;
using System;
using System.IO;

namespace AutoMerger.Core
{
	interface IMerger
	{
		void Merge(string projectUrl, string parent, string child);
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

		public void Merge(string projectUrl, string parent, string child)
		{
			var folderPath = Path.Combine(
				_mergesFolder,
				projectUrl.Substring(projectUrl.LastIndexOf('/') + 1),
				child);

			PrepareWorkingCopy(projectUrl, child, folderPath);

			PerformMerge(projectUrl, parent, child, folderPath);
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

		private void PerformMerge(string projectUrl, string parent, string child, string folderPath)
		{
			var initialRange = _svnInterface.GetMergeInfo(folderPath, parent);

			if (!_svnInterface.Merge(projectUrl, parent, folderPath))
			{
				throw new InvalidOperationException("Unable to merge into working copy.");
			}

			if (!_svnInterface.CheckForModifications(folderPath))
			{
				return;
			}

			if (_svnInterface.CheckForConflicts(folderPath))
			{
				throw new InvalidOperationException("Working copy contained conflicts after merge.");
			}

			var newRange = _svnInterface.GetMergeInfo(folderPath, parent);
			var commitMessage = GetCommitMessage(parent, child, initialRange, newRange);

			if (!_svnInterface.Commit(folderPath, commitMessage))
			{
				throw new InvalidOperationException("Unable to commit changes from working copy.");
			}
		}

		private string GetCommitMessage(string parent, string child, SvnRevisionRange initialRange, SvnRevisionRange newRange)
		{
			SvnRevisionRange revisonsMerged;

			if (initialRange != null)
			{
				revisonsMerged = new SvnRevisionRange(
					initialRange.EndRevision.Revision + 1,
					newRange.EndRevision);
			}
			else
			{
				revisonsMerged = newRange;
			}

			return string.Format(
				"Merging revisions r{0}-r{1} from {2} to {3}",
				revisonsMerged.StartRevision.Revision,
				revisonsMerged.EndRevision.Revision,
				parent,
				child);
		}
	}
}
