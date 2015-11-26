using System.Xml.Serialization;

namespace AutoMerger.Types
{
	public class SummaryEmail : Email
	{
		[XmlAttribute("InheritInMergeEmails")]
		public bool InheritInMergeEmails { get; set; }
	}
}
