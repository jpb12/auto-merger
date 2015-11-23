using BranchManager.Core.Types;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace AutoMerger.Core
{
	interface IConfigGetter
	{
		MergeConfig GetConfig();
	}

	class ConfigGetter : IConfigGetter
	{
		private readonly ISvnInterface _svnInterface;
		private readonly IConfigurationManager _configManager;

		public ConfigGetter(ISvnInterface svnInterface, IConfigurationManager configManager)
		{
			_svnInterface = svnInterface;
			_configManager = configManager;
		}

		public MergeConfig GetConfig()
		{
			var configLocation = _configManager.GetStringValue(ConfigKey.MergeConfig);

			var xmlSerializer = new XmlSerializer(typeof(MergeConfig));

			if (_configManager.GetBoolValue(ConfigKey.ConfigIsInSvn))
			{
				var stream = _svnInterface.Cat(configLocation);
				using (var reader = new XmlTextReader(stream))
				{
					return (MergeConfig)xmlSerializer.Deserialize(reader);
				}
			}

			using (var reader = new XmlTextReader(configLocation))
			{
				return (MergeConfig)xmlSerializer.Deserialize(reader);
			}
		}
	}
}
