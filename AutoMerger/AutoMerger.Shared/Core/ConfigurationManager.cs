using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutoMerger.Shared.Core
{
	public enum SharedConfigKey
	{
		ConfigIsInSvn,
		MergeConfig,
		Password,
		UserName
	}

	public interface IConfigurationManager<T> where T : struct, IConvertible
	{
		bool GetBoolValue(T configKey);
		int GetIntValue(T configKey);
		string GetStringValue(T configKey);
	}

	public class ConfigurationManager<T> : IConfigurationManager<T> where T : struct, IConvertible
	{
		private readonly string[] _args;

		public ConfigurationManager(string[] args)
		{
			_args = args;
		}

		public bool GetBoolValue(T configKey)
		{
			bool value;

			if (!bool.TryParse(GetStringValue(configKey), out value))
			{
				throw new InvalidOperationException("Config key " + configKey + " was not a valid boolean");
			}

			return value;
		}

		public int GetIntValue(T configKey)
		{
			int value;

			if (!int.TryParse(GetStringValue(configKey), out value))
			{
				throw new InvalidOperationException("Config key " + configKey + " was not a valid integer");
			}

			return value;
		}

		public string GetStringValue(T configKey)
		{
			string value;

			if (TryGetFromArguments(configKey, out value))
			{
				return value;
			}

			return System.Configuration.ConfigurationManager.AppSettings[configKey.ToString()];
		}

		private bool TryGetFromArguments(T configKey, out string value)
		{
			var argKey = "--" + ToKebabCase(configKey) + "=";
			var arg = _args.SingleOrDefault(a => a.StartsWith(argKey));
			value = arg?.Replace(argKey, "");
			return value != null;
		}

		private string ToKebabCase(IConvertible key)
		{
			return Regex.Replace(key.ToString(), "[a-z][A-Z]", m => m.Value[0] + "-" + m.Value[1]).ToLowerInvariant();
		}
	}
}
