using System.Web.Optimization;

namespace BranchManager
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/*.css"));

			bundles.Add(new ScriptBundle("~/Scripts/libraries").Include(
				"~/Scripts/jquery.js",
				"~/Scripts/d3.js",
				"~/Scripts/react.js",
				"~/Scripts/react-dom.js",
				"~/Scripts/redux.js",
				"~/Scripts/react-redux.js"));

			bundles.Add(new ScriptBundle("~/Scripts/app").Include(
				"~/JS/Actions.js",
				"~/JS/Reducers/*.js",
				"~/JS/Store.js",
				"~/JS/Components/Main.js",
				"~/JS/Components/LeftPanel/*.js",
				"~/JS/Components/RightPanel/*.js",
				"~/JS/Components/Tree/*.js",
				"~/JS/Components/Misc/*.js",
				"~/JS/App.js"));
		}
	}
}
