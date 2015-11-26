using System.Xml.Serialization;

namespace AutoMerger.Types
{
	public class Merge
	{
		[XmlAttribute("Parent")]
		public string Parent { get; set; }

		[XmlAttribute("Child")]
		public string Child { get; set; }

		[XmlAttribute("Enabled")]
		public bool Enabled { get; set; }
	}
}
