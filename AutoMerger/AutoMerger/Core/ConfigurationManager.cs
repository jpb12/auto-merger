namespace AutoMerger.Core
{
	enum ConfigKey
	{
		ConfigIsInSvn,
		MergeConfig,
		MergesFolder,
		Password,
		UserName
	}

	interface IConfigurationManager
	{
		bool GetBoolValue(ConfigKey configKey);
		string GetStringValue(ConfigKey configKey);
	}

	class ConfigurationManager : IConfigurationManager
	{
		public bool GetBoolValue(ConfigKey configKey)
		{
			return System.Configuration.ConfigurationManager.AppSettings[configKey.ToString()] == "true";
		}

		public string GetStringValue(ConfigKey configKey)
		{
			return System.Configuration.ConfigurationManager.AppSettings[configKey.ToString()];
		}
	}
}
