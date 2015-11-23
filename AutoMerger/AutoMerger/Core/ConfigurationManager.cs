namespace AutoMerger.Core
{
	enum ConfigKey
	{
		ConfigIsInSvn,
		MergeConfig,
		MergesFolder,
		Password,
		Threads,
		UserName
	}

	interface IConfigurationManager
	{
		bool GetBoolValue(ConfigKey configKey);
		int GetIntValue(ConfigKey configKey);
		string GetStringValue(ConfigKey configKey);
	}

	class ConfigurationManager : IConfigurationManager
	{
		public bool GetBoolValue(ConfigKey configKey)
		{
			return GetStringValue(configKey) == "true";
		}

		public int GetIntValue(ConfigKey configKey)
		{
			return int.Parse(GetStringValue(configKey));
		}

		public string GetStringValue(ConfigKey configKey)
		{
			return System.Configuration.ConfigurationManager.AppSettings[configKey.ToString()];
		}
	}
}
