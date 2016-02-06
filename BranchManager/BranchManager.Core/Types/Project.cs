using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BranchManager.Core.Types
{
	public class Project
	{
		public Project(string projectUrl, IEnumerable<Node> roots)
		{
			ProjectUrl = projectUrl;
			Roots = roots.ToList().AsReadOnly();
		}

		public string ProjectUrl { get; }

		public ReadOnlyCollection<Node> Roots { get; }

		public string Name
		{
			get { return ProjectUrl.Substring(ProjectUrl.LastIndexOf('/') + 1); }
		}
	}
}
