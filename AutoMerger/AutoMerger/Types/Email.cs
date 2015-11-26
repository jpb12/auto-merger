using System.Xml.Serialization;

namespace BranchManager.Core.Types
{
	public class Email
	{
		[XmlAttribute("Email")]
		public string Value { get; set; }
	}
}
