using System.Collections.Generic;
using System.Xml.Serialization;

namespace AutoMerger.Shared.Types
{
	public class EmailSettings<T> where T : Email
	{
		[XmlElement("FromAddress")]
		public T FromAddress { get; set; }

		[XmlElement("ToAddress")]
		public List<T> ToAddresses { get; set; }

		[XmlElement("CcAddress")]
		public List<T> CcAddresses { get; set; }

		[XmlElement("BccAddress")]
		public List<T> BccAddresses { get; set; }
	}
}
