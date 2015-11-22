using AutoMerger.Core;
using BranchManager.Core.Types;

namespace AutoMerger
{
	class Program
	{
		static void Main(string[] args)
		{
			// TODO: Get config
			var config = new MergeConfig();

			// TODO: Ninject
			var configManager = new ConfigManager();
			var svnInterface = new SvnInterface(configManager);
			var merger = new Merger(svnInterface, configManager);
			var projectMerger = new ProjectMerger(merger);

			foreach(var project in config.Projects)
			{
				projectMerger.MergeProject(project);
			}
		}
	}
}
