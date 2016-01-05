namespace BranchManager.Core.Types
{
	public class Branch
	{
		private readonly bool _enabled;
		private readonly Node _child;
		private readonly int _unmergedRevisions;

		public Branch(bool enabled, Node child, int unmergedRevisions)
		{
			_enabled = enabled;
			_child = child;
			_unmergedRevisions = unmergedRevisions;
		}

		public bool Enabled
		{
			get { return _enabled; }
		}

		public Node Child
		{
			get { return _child; }
		}

		public int UnmergedRevisions
		{
			get { return _unmergedRevisions; }
		}
	}
}
