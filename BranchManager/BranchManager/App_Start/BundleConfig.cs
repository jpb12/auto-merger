using System.Web.Optimization;

namespace BranchManager
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/Site.css"));

			bundles.Add(new ScriptBundle("~/Scripts/react").Include(
				"~/Scripts/react.js",
				"~/Scripts/react-dom.js"));

			bundles.Add(new ScriptBundle("~/Scripts/app").Include("~/JS/App.js"));
		}
	}
}
