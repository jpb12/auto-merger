using SharpSvn;
using System;
using System.IO;

namespace AutoMerger.Core
{
	interface ISvnInterface
	{
		bool Checkout(string projectUrl, string branch, string folderPath);
		bool CheckForConflicts(string folderPath);
		bool CheckForModifications(string folderPath);
		bool Commit(string folderPath);
		bool Merge(string projectUrl, string parentBranch, string folderPath);
		bool Update(string folderPath);
	}

	class SvnInterface : ISvnInterface
	{
		private readonly string _userName;
		private readonly string _password;

		public SvnInterface(IConfigManager configManager)
		{
			_userName = configManager.GetConfigValue(ConfigKey.UserName);
			_password = configManager.GetConfigValue(ConfigKey.Password);
		}

		public bool Checkout(string projectUrl, string branch, string folderPath)
		{
			var svnPath = GetSvnPath(projectUrl, branch);

			var args = new SvnCheckOutArgs
			{
				IgnoreExternals = true
			};
			using(var svnClient = CreateSvnClient())
			{
				return svnClient.CheckOut(svnPath, folderPath, args);
			}
		}

		public bool CheckForConflicts(string folderPath)
		{
			var conflicted = false;

			var args = new SvnStatusArgs
			{
				IgnoreExternals = true
			};

			using (var svnClient = CreateSvnClient())
			{
				svnClient.Status(
					folderPath,
					args,
					(sender, e) =>
					{
						if (e.Conflicted)
						{
							conflicted = true;
							e.Cancel = true;
						}
					});
			}

			return conflicted;
		}

		public bool CheckForModifications(string folderPath)
		{
			var modified = false;

			var args = new SvnStatusArgs
			{
				IgnoreExternals = true
			};

			using (var svnClient = CreateSvnClient())
			{
				svnClient.Status(
					folderPath,
					args,
					(sender, e) =>
					{
						modified = true;
						e.Cancel = true;
					});
			}

			return modified;
		}

		public bool Commit(string folderPath)
		{
			using (var svnClient = CreateSvnClient())
			{
				return svnClient.Commit(folderPath);
			}
		}

		public bool Merge(string projectUrl, string parentBranch, string folderPath)
		{
			var svnPath = GetSvnPath(projectUrl, parentBranch);

			var args = new SvnMergeArgs
			{
				Force = true
			};

			var range = new SvnRevisionRange(new SvnRevision(1), new SvnRevision(DateTime.Now));

			using (var svnClient = CreateSvnClient())
			{
				return svnClient.Merge(folderPath, svnPath, range, args);
			}
		}

		public bool Update(string folderPath)
		{
			var args = new SvnUpdateArgs
			{
				IgnoreExternals = true
			};

			using (var svnClient = CreateSvnClient())
			{
				return svnClient.Update(folderPath, args);
			}
		}

		private SvnUriTarget GetSvnPath(string projectUrl, string branch)
		{
			if (branch == "trunk")
			{
				return new SvnUriTarget(Path.Combine(projectUrl, branch));
			}

			return new SvnUriTarget(Path.Combine(projectUrl, "branches", branch));
		}

		private SvnClient CreateSvnClient()
		{
			var client = new SvnClient();
			client.Authentication.ForceCredentials(_userName, _password);
			return client;
		}
	}
}
