using System.Collections.Generic;
using System.Xml.Serialization;

namespace BranchManager.Core.Types
{
	public class Project
	{
		[XmlAttribute("ProjectUrl")]
		public string ProjectUrl { get; set; }

		[XmlElement("Merge")]
		public List<Merge> Merges { get; set; }
	}
}
