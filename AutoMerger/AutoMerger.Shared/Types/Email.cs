using System.Xml.Serialization;

namespace AutoMerger.Shared.Types
{
	public class Email
	{
		[XmlAttribute("Email")]
		public string Value { get; set; }
	}
}
