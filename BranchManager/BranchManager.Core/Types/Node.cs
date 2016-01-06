﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BranchManager.Core.Types
{
	public class Node
	{
		private readonly string _name;
		private readonly bool _exists;
		private readonly ReadOnlyCollection<Branch> _branches;

		public Node(string name, bool exists, IEnumerable<Branch> branches)
		{
			_name = name;
			_exists = exists;
			_branches = branches.ToList().AsReadOnly();
		}

		public string Name
		{
			get { return _name; }
		}

		public bool Exists
		{
			get { return _exists; }
		}

		public ReadOnlyCollection<Branch> Branches
		{
			get { return _branches; }
		}
	}
}
