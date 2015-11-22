using System.Configuration;

namespace AutoMerger.Core
{
	enum ConfigKey
	{
		MergesFolder
	}

	interface IConfigManager
	{
		string GetConfigValue(ConfigKey configKey);
	}

	class ConfigManager : IConfigManager
	{
		public string GetConfigValue(ConfigKey configKey)
		{
			return ConfigurationManager.AppSettings[configKey.ToString()];
		}
	}
}
