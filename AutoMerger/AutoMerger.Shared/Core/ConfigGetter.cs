﻿using AutoMerger.Shared.Types;
using System.Xml;
using System.Xml.Serialization;

namespace AutoMerger.Shared.Core
{
	public interface IConfigGetter
	{
		MergeConfig GetConfig();
	}

	class ConfigGetter : IConfigGetter
	{
		private readonly ISvnInterface _svnInterface;
		private readonly IConfigurationManager<ConfigKey> _configManager;

		public ConfigGetter(ISvnInterface svnInterface, IConfigurationManager<ConfigKey> configManager)
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
				stream.Position = 0;
				return (MergeConfig)xmlSerializer.Deserialize(stream);
			}

			using (var reader = new XmlTextReader(configLocation))
			{
				return (MergeConfig)xmlSerializer.Deserialize(reader);
			}
		}
	}
}
