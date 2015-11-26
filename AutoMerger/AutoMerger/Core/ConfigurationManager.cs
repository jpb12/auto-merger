using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutoMerger.Core
{
	enum ConfigKey
	{
		ConfigIsInSvn,
		MergeConfig,
		MergesFolder,
		Password,
		SendEmails,
		Threads,
		UserName
	}

	static class ConfigKeyExtensions
	{
		public static string ToKebabCase(this ConfigKey key)
		{
			return Regex.Replace(key.ToString(), "[a-z][A-Z]", m => m.Value[0] + "-" + m.Value[1]).ToLowerInvariant();
		}
	}

	interface IConfigurationManager
	{
		bool GetBoolValue(ConfigKey configKey);
		int GetIntValue(ConfigKey configKey);
		string GetStringValue(ConfigKey configKey);
	}

	class ConfigurationManager : IConfigurationManager
	{
		private readonly string[] _args;

		public ConfigurationManager(string[] args)
		{
			_args = args;
		}

		public bool GetBoolValue(ConfigKey configKey)
		{
			bool value;

			if (!bool.TryParse(GetStringValue(configKey), out value))
			{
				throw new InvalidOperationException("Config key " + configKey + " was not a valid boolean");
			}

			return value;
		}

		public int GetIntValue(ConfigKey configKey)
		{
			int value;

			if (!int.TryParse(GetStringValue(configKey), out value))
			{
				throw new InvalidOperationException("Config key " + configKey + " was not a valid integer");
			}

			return value;
		}

		public string GetStringValue(ConfigKey configKey)
		{
			string value;

			if (TryGetFromArguments(configKey, out value))
			{
				return value;
			}

			return System.Configuration.ConfigurationManager.AppSettings[configKey.ToString()];
		}

		private bool TryGetFromArguments(ConfigKey configKey, out string value)
		{
			var argKey = "--" + configKey.ToKebabCase() + "=";

			var arg = _args.SingleOrDefault(a => a.StartsWith(argKey));

			if (arg == null)
			{
				value = null;
				return false;
			}

			value =  arg.Replace(argKey, "");
			return true;
		}
	}
}
