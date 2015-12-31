using AutoMerger.Shared.Core;
using SharpSvn;

namespace AutoMerger.Core
{
	class SvnInterface : BaseSvnInterface
	{
		private readonly string _userName;
		private readonly string _password;

		public SvnInterface(IConfigurationManager<SharedConfigKey> configManager)
		{
			_userName = configManager.GetStringValue(SharedConfigKey.UserName);
			_password = configManager.GetStringValue(SharedConfigKey.Password);
		}

		public override SvnClient CreateSvnClient()
		{
			var client = new SvnClient();

			if (!string.IsNullOrEmpty(_userName) && !string.IsNullOrEmpty(_password))
			{
				client.Authentication.ForceCredentials(_userName, _password);
			}

			return client;
		}
	}
}
