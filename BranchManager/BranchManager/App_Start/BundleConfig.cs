using System.Web.Optimization;

namespace BranchManager
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/Site.css"));

			bundles.Add(new ScriptBundle("~/Scripts/libraries").Include(
				"~/Scripts/d3.js",
				"~/Scripts/react.js",
				"~/Scripts/react-dom.js"));

			bundles.Add(new ScriptBundle("~/Scripts/app").Include(
				"~/JS/Components/*.js",
				"~/JS/App.js"));
		}
	}
}
