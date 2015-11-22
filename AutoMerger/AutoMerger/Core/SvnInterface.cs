using SharpSvn;
using System;
using System.IO;
using System.Linq;

namespace AutoMerger.Core
{
	interface ISvnInterface
	{
		Stream Cat(string path);
		bool Checkout(string projectUrl, string branch, string folderPath);
		bool CheckForConflicts(string folderPath);
		bool CheckForModifications(string folderPath);
		bool Commit(string folderPath, string message);
		bool Merge(string projectUrl, string parentBranch, string folderPath);
		bool Update(string folderPath);
	}

	class SvnInterface : ISvnInterface
	{
		private readonly string _userName;
		private readonly string _password;

		public SvnInterface(IConfigurationManager configManager)
		{
			_userName = configManager.GetStringValue(ConfigKey.UserName);
			_password = configManager.GetStringValue(ConfigKey.Password);
		}

		public Stream Cat(string path)
		{
			var stream = new MemoryStream();

			using(var svnClient = CreateSvnClient())
			{
				svnClient.Write(path, stream);
			}

			return stream;
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

		public bool Commit(string folderPath, string message)
		{
			var args = new SvnCommitArgs
			{
				LogMessage = message
			};

			using (var svnClient = CreateSvnClient())
			{
				return svnClient.Commit(folderPath, args);
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
				return new SvnUriTarget(UriCombine(projectUrl, branch));
			}

			return new SvnUriTarget(UriCombine(projectUrl, "branches", branch));
		}

		private SvnClient CreateSvnClient()
		{
			var client = new SvnClient();

			if (!string.IsNullOrEmpty(_userName) && !string.IsNullOrEmpty(_password))
			{
				client.Authentication.ForceCredentials(_userName, _password);
			}

			return client;
		}

		private string UriCombine(params string[] uriStrings)
		{
			var uri = uriStrings[0];

			foreach(var uriString in uriStrings.Skip(1))
			{
				if (!uri.EndsWith("/"))
				{
					uri += '/';
				}

				uriString.TrimEnd('/');

				uri += uriString;
			}

			return uri;
		}
	}
}
