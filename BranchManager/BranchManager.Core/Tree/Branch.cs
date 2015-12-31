namespace BranchManager.Core.Tree
{
	public class Branch
	{
		private readonly bool _enabled;
		private readonly Node _child;

		public Branch(bool enabled, Node child)
		{
			_enabled = enabled;
			_child = child;
		}

		public bool Enabled
		{
			get { return _enabled; }
		}

		public Node Child
		{
			get { return _child; }
		}
	}
}
