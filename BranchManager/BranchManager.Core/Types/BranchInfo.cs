using System;

namespace BranchManager.Core.Types
{
	public class BranchInfo
	{
		public BranchInfo(
			string name,
			string projectUrl,
			string creationBranch,
			DateTime creationTime,
			long creationRevision)
		{
			Name = name;
			ProjectUrl = projectUrl;
			CreationBranch = creationBranch;
			CreationTime = creationTime;
			CreationRevision = creationRevision;
		}

		public string Name { get; }

		public string ProjectUrl { get; }

		public string CreationBranch { get; }

		public DateTime CreationTime { get; }

		public long CreationRevision { get; }
	}
}
