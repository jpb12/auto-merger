using AutoMerger.Shared.Core;
using BranchManager.Core.Types;
using SharpSvn;
using System.Collections.ObjectModel;
using System.Linq;

namespace BranchManager.Core.Tree
{
	public interface IBranchInfoGetter
	{
		BranchInfo Get(string projectUrl, string name);
	}

	class BranchInfoGetter : IBranchInfoGetter
	{
		private readonly ISvnInterface _svnInterface;

		public BranchInfoGetter(ISvnInterface svnInterface) {
			_svnInterface = svnInterface;
		}

		public BranchInfo Get(string projectUrl, string name)
		{
			Collection<SvnLogEventArgs> logs;
			_svnInterface.Log(projectUrl, name, null, out logs);

			var initialCommit = logs.Last();

			foreach(var change in initialCommit.ChangedPaths)
			{
				switch (change.Action)
				{
					case SvnChangeAction.Add:
					case SvnChangeAction.Replace:
						if (!change.Path.EndsWith(name)) {
							continue;
						}
						return new BranchInfo(
							change.CopyFromPath.Substring(change.CopyFromPath.LastIndexOf('/') + 1),
							initialCommit.Time,
							initialCommit.Revision);
				}
			}

			return new BranchInfo(
				null,
				initialCommit.Time,
				initialCommit.Revision);
		}
	}
}
