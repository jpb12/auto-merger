using System.Collections.Generic;
using System.Xml.Serialization;

namespace AutoMerger.Types
{
	public class MergeConfig
	{
		[XmlElement("Project")]
		public List<Project> Projects { get; set; }

		[XmlElement("EmailSettings")]
		public EmailSettings EmailSettings { get; set; }
	}
}
