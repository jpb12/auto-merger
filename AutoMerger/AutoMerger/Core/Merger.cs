using System;
using System.IO;

namespace AutoMerger.Core
{
	class Merger
	{
		private readonly ISvnInterface _svnInterface;
		private readonly string _mergesFolder;

		public Merger(ISvnInterface svnInterface, IConfigManager configManager)
		{
			_svnInterface = svnInterface;
			_mergesFolder = configManager.GetConfigValue(ConfigKey.MergesFolder);
		}

		public void Merge(string projectUrl, string parent, string child)
		{
			var folderPath = Path.Combine(
				_mergesFolder,
				projectUrl.Replace('/', '_'),
				child);

			PrepareWorkingCopy(projectUrl, child, folderPath);

			PerformMerge(projectUrl, parent, folderPath);
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

		private void PerformMerge(string projectUrl, string parent, string folderPath)
		{
			if (!_svnInterface.Merge(projectUrl, parent, folderPath))
			{
				throw new InvalidOperationException("Unable to merge into working copy.");
			}

			if (!_svnInterface.CheckForConflicts(folderPath))
			{
				throw new InvalidOperationException("Working copy contained conflicts after merge.");
			}

			if (!_svnInterface.Commit(folderPath))
			{
				throw new InvalidOperationException("Unable to commit changes from working copy.");
			}
		}
	}
}
