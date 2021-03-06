﻿using SharpSvn;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace AutoMerger.Shared.Core
{
	public interface ISvnInterface
	{
		Stream Cat(string path);
		bool Checkout(string projectUrl, string branch, string folderPath);
		bool CheckForConflicts(string folderPath);
		bool CheckForModifications(string folderPath);
		bool Commit(string folderPath, string message);
		SvnRevisionRange GetMergeInfo(SvnTarget path, string branch, string parent);
		bool List(string projectUrl, out Collection<SvnListEventArgs> result);
		bool Log(string projectUrl, string branch, long? start, out Collection<SvnLogEventArgs> result);
		bool Merge(string projectUrl, string parentBranch, string folderPath);
		bool Update(string folderPath);
	}

	public abstract class BaseSvnInterface : ISvnInterface
	{
		public abstract SvnClient CreateSvnClient();

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

		public virtual SvnRevisionRange GetMergeInfo(SvnTarget target, string branch, string parent)
		{
			string value;

			using (var svnClient = CreateSvnClient())
			{
				svnClient.GetProperty(
					target,
					"svn:mergeinfo",
					out value);
			}

			var branchPath = "/" + GetBranchPath(parent) + ":";
			var branchRow = value?.Split('\n').SingleOrDefault(r => r.StartsWith(branchPath));

			if (branchRow == null)
			{
				return null;
			}

			var rangeSplit = branchRow.Replace(branchPath, "").Split('-');
			return new SvnRevisionRange(long.Parse(rangeSplit[0]), long.Parse(rangeSplit[1]));
		}

		public bool List(string projectUrl, out Collection<SvnListEventArgs> result)
		{
			var svnPath = new SvnUriTarget(UriCombine(projectUrl, "branches"));

			var args = new SvnListArgs
			{
				Depth = SvnDepth.Children,
				IncludeExternals = false
			};

			using (var svnClient = CreateSvnClient())
			{
				return svnClient.GetList(svnPath.Uri, args, out result);
			}
		}

		public bool Log(string projectUrl, string branch, long? start, out Collection<SvnLogEventArgs> result)
		{
			var svnPath = GetSvnPath(projectUrl, branch);

			var args = start.HasValue
				? new SvnLogArgs
				{
					End = new SvnRevision(start.Value),
					StrictNodeHistory = true
				}
				: new SvnLogArgs
				{
					StrictNodeHistory = true
				};

			using (var svnClient = CreateSvnClient())
			{
				return svnClient.GetLog(svnPath.Uri, args, out result);
			}
		}

		public bool Merge(string projectUrl, string parentBranch, string folderPath)
		{
			var svnPath = GetSvnPath(projectUrl, parentBranch);

			var args = new SvnMergeArgs
			{
				Force = true
			};

			using (var svnClient = CreateSvnClient())
			{
				SvnInfoEventArgs info;

				svnClient.GetInfo(svnPath, out info);

				var range = new SvnRevisionRange(new SvnRevision(1), new SvnRevision(info.LastChangeRevision));

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

		protected SvnUriTarget GetSvnPath(string projectUrl, string branch)
		{
			return new SvnUriTarget(UriCombine(projectUrl, GetBranchPath(branch)));
		}

		private string GetBranchPath(string branch)
		{
			return branch == "trunk" ? branch : "branches/" + branch;
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
