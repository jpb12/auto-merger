using System.Collections.Generic;
using System.Xml.Serialization;

namespace BranchManager.Core.Types
{
	public class MergeConfig
	{
		[XmlElement("Project")]
		public List<Project> Projects { get; set; }
	}
}
