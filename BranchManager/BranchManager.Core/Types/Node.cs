using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BranchManager.Core.Types
{
	public class Node
	{
		public Node(string name, bool exists, IEnumerable<Branch> branches)
		{
			Name = name;
			Exists = exists;
			Branches = branches.ToList().AsReadOnly();
		}

		public string Name { get; }

		public bool Exists { get; }

		public ReadOnlyCollection<Branch> Branches { get; }
	}
}
