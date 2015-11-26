using System.Collections.Generic;
using System.Xml.Serialization;

namespace BranchManager.Core.Types
{
	public class EmailSettings
	{
		[XmlElement("FromAddress")]
		public Email FromAddress { get; set; }

		[XmlElement("ToAddress")]
		public List<Email> ToAddresses { get; set; }

		[XmlElement("CcAddress")]
		public List<Email> CcAddresses { get; set; }

		[XmlElement("BccAddress")]
		public List<Email> BccAddresses { get; set; }
	}
}
