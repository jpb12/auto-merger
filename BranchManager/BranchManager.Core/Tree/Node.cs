using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BranchManager.Core.Tree
{
	public class Node
	{
		private readonly string _name;
		private readonly ReadOnlyCollection<Branch> _branches;

		public Node(string name, IEnumerable<Branch> branches)
		{
			_name = name;
			_branches = branches.ToList().AsReadOnly();
		}

		public string Name
		{
			get { return _name; }
		}

		public ReadOnlyCollection<Branch> Branches
		{
			get { return _branches; }
		}
	}
}
