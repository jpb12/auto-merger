using System;

namespace BranchManager.Core.Types
{
	public class BranchInfo
	{
		private readonly string _name;
		private readonly string _projectUrl;
		private readonly string _creationBranch;
		private readonly DateTime _creationTime;
		private readonly long _creationRevision;

		public BranchInfo(
			string name,
			string projectUrl,
			string creationBranch,
			DateTime creationTime,
			long creationRevision)
		{
			_name = name;
			_projectUrl = projectUrl;
			_creationBranch = creationBranch;
			_creationTime = creationTime;
			_creationRevision = creationRevision;
		}

		public string Name
		{
			get { return _name; }
		}

		public string ProjectUrl
		{
			get { return _projectUrl; }
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
