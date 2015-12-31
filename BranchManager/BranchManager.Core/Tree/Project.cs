using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BranchManager.Core.Tree
{
	public class Project
	{
		private readonly string _projectUrl;
		private readonly ReadOnlyCollection<Node> _roots;

		public Project(string projectUrl, IEnumerable<Node> roots)
		{
			_projectUrl = projectUrl;
			_roots = roots.ToList().AsReadOnly();
		}

		public string ProjectUrl
		{
			get { return _projectUrl; }
		}

		public ReadOnlyCollection<Node> Roots
		{
			get { return _roots; }
		}

		public string Name
		{
			get { return _projectUrl.Substring(_projectUrl.LastIndexOf('\\') + 1); }
		}
	}
}
