using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace BranchManager
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.Formatters.Remove(config.Formatters.XmlFormatter);

#if DEBUG
			config.Formatters.JsonFormatter.Indent = true;
#endif

			config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			config.MapHttpAttributeRoutes();
		}
	}
}
