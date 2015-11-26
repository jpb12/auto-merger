using System.Xml.Serialization;

namespace AutoMerger.Types
{
	public class Email
	{
		[XmlAttribute("Email")]
		public string Value { get; set; }
	}
}
