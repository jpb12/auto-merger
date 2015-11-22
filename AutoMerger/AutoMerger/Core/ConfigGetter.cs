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

	class ConfigGetter
	{
		private readonly IConfigurationManager _configManager;

		public ConfigGetter(IConfigurationManager configManager)
		{
			_configManager = configManager;
		}

		public MergeConfig GetConfig()
		{
			var configLocation = _configManager.GetStringValue(ConfigKey.MergeConfig);

			if (_configManager.GetBoolValue(ConfigKey.ConfigIsInSvn))
			{
				// TODO
			}

			var xmlSerializer = new XmlSerializer(typeof(MergeConfig));

			using (var reader = new XmlTextReader(configLocation))
			{
				return (MergeConfig)xmlSerializer.Deserialize(reader);
			}
		}
	}
}
