using System.Collections.Generic;
using System.Xml.Serialization;

namespace BranchManager.Core.Types
{
	public class Project
	{
		private string _projectUrl;

		[XmlAttribute("ProjectUrl")]
		public string ProjectUrl
		{
			get { return _projectUrl.TrimEnd('/'); }
			set { _projectUrl = value; }
		}

		[XmlElement("Merge")]
		public List<Merge> Merges { get; set; }
	}
}
