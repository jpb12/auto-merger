using AutoMerger.Shared.Core;
using SharpSvn;
using SharpSvn.Security;
using System.IO;

namespace BranchManager.Core.Svn
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

			client.LoadConfiguration(Path.Combine(Path.GetTempPath(), "Svn"), true);

			client.Authentication.Clear();
			client.Authentication.SslServerTrustHandlers +=
				delegate (object sender, SvnSslServerTrustEventArgs e)
				{
					e.AcceptedFailures = e.Failures;
					e.Save = true;
				};

			return client;
		}
	}
}
