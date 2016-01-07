using AutoMerger.Shared.Core;
using SharpSvn;
using System.Collections.ObjectModel;

namespace BranchManager.Core.Tree
{
	interface IUnmergedRevisionGetter
	{
		int GetUnmergedRevisions(string projectUrl, string child, string parent);
	}

	class UnmergedRevisionGetter : IUnmergedRevisionGetter
	{
		private readonly ISvnInterface _svnInterface;

		public UnmergedRevisionGetter(ISvnInterface svnInterface)
		{
			_svnInterface = svnInterface;
		}

		public int GetUnmergedRevisions(string projectUrl, string child, string parent)
		{
			// TODO: Edge cases

			long start;

			var revisions = _svnInterface.GetMergeInfo(projectUrl, child, parent);
			if (revisions == null)
			{
				Collection<SvnLogEventArgs> parentLogs;
				_svnInterface.Log(projectUrl, parent, null, out parentLogs);
				start = parentLogs[0].Revision + 1;
			}
			else
			{
				start = revisions.EndRevision.Revision + 1;
			}

			Collection<SvnLogEventArgs> logs;
			_svnInterface.Log(projectUrl, parent, start, out logs);
			
			return logs.Count;
		}
	}
}
