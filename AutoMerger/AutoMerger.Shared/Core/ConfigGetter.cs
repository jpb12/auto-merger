using AutoMerger.Shared.Types;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
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
		private readonly IConfigurationManager<SharedConfigKey> _configManager;

		public ConfigGetter(ISvnInterface svnInterface, IConfigurationManager<SharedConfigKey> configManager)
		{
			_svnInterface = svnInterface;
			_configManager = configManager;
		}

		public MergeConfig GetConfig()
		{
			var xml = GetXml();
			var schema = GetSchema();

			xml.Validate(schema, (o, e) => { throw e.Exception; });

			var xmlSerializer = new XmlSerializer(typeof(MergeConfig));

			return (MergeConfig)xmlSerializer.Deserialize(xml.CreateReader());
		}

		private XDocument GetXml()
		{
			var configLocation = _configManager.GetStringValue(SharedConfigKey.MergeConfig);

			if (_configManager.GetBoolValue(SharedConfigKey.ConfigIsInSvn))
			{
				var stream = _svnInterface.Cat(configLocation);
				stream.Position = 0;
				return XDocument.Load(stream);
			}

			using (var reader = new XmlTextReader(configLocation))
			{
				return XDocument.Load(reader);
			}
		}

		private XmlSchemaSet GetSchema()
		{
			var assembly = Assembly.GetExecutingAssembly();
			var resourceName = "AutoMerger.Shared.Schemas.Merges.xsd";

			using (var stream = assembly.GetManifestResourceStream(resourceName))
			using (var reader = new StreamReader(stream))
			{
				var schemas = new XmlSchemaSet();
				schemas.Add("", XmlReader.Create(reader));
				return schemas;
			}
		}
	}
}
