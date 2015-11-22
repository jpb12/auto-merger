using AutoMerger.Core;

namespace AutoMerger
{
	class Program
	{
		static void Main(string[] args)
		{
			// TODO: Ninject
			var configManager = new ConfigurationManager();
			var configGetter = new ConfigGetter(configManager);
			var svnInterface = new SvnInterface(configManager);
			var merger = new Merger(svnInterface, configManager);
			var projectMerger = new ProjectMerger(merger);

			var config = configGetter.GetConfig();

			foreach(var project in config.Projects)
			{
				projectMerger.MergeProject(project);
			}
		}
	}
}
