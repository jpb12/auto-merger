using System.Collections.Generic;
using System.Xml.Serialization;

namespace BranchManager.Core.Types
{
	public class MergeConfig
	{
		[XmlElement("Peoject")]
		public IList<Project> Projects { get; set; }
	}
}
