namespace BranchManager.Core.Types
{
	public class Branch
	{
		private readonly bool _enabled;
		private readonly Node _child;
		private readonly long? _unmergedRevisions;

		public Branch(Node child, bool enabled, long? unmergedRevisions)
		{
			_child = child;
			_enabled = enabled;
			_unmergedRevisions = unmergedRevisions;
		}

		public Node Child
		{
			get { return _child; }
		}

		public bool Enabled
		{
			get { return _enabled; }
		}

		public long? UnmergedRevisions
		{
			get { return _unmergedRevisions; }
		}
	}
}
