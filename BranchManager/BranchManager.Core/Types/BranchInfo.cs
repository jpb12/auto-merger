using System;

namespace BranchManager.Core.Types
{
	public class BranchInfo
	{
		private readonly string _creationBranch;
		private readonly DateTime _creationTime;
		private readonly long _creationRevision;

		public BranchInfo(
			string creationBranch,
			DateTime creationTime,
			long creationRevision)
		{
			_creationBranch = creationBranch;
			_creationTime = creationTime;
			_creationRevision = creationRevision;
		}

		public string CreationBranch
		{
			get { return _creationBranch; }
		}

		public DateTime CreationTime
		{
			get { return _creationTime; }
		}

		public long CreationRevision
		{
			get { return _creationRevision; }
		}
	}
}
