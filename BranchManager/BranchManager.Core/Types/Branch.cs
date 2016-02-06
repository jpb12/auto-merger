namespace BranchManager.Core.Types
{
	public class Branch
	{
		public Branch(Node child, bool enabled, long? unmergedRevisions)
		{
			Child = child;
			Enabled = enabled;
			UnmergedRevisions = unmergedRevisions;
		}

		public Node Child { get; }

		public bool Enabled { get; }

		public long? UnmergedRevisions { get; }
	}
}
