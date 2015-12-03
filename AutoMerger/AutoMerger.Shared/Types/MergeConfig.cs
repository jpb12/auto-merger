using System.Collections.Generic;
using System.Xml.Serialization;

namespace AutoMerger.Shared.Types
{
	public class MergeConfig
	{
		[XmlElement("Project")]
		public List<Project> Projects { get; set; }

		[XmlElement("EmailSettings")]
		public EmailSettings<SummaryEmail> EmailSettings { get; set; }
	}
}
