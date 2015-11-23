using AutoMerger.Core;
using System.Threading.Tasks;

namespace AutoMerger
{
	class Program
	{
		static void Main(string[] args)
		{
			// TODO: Ninject
			var configManager = new ConfigurationManager();
			var svnInterface = new SvnInterface(configManager);
			var configGetter = new ConfigGetter(svnInterface, configManager);
			var merger = new Merger(svnInterface, configManager);
			var threadManager = new ThreadManager(configManager);
			var projectMerger = new ProjectMerger(merger, threadManager);

			var config = configGetter.GetConfig();

			foreach(var project in config.Projects)
			{
				Task.Factory.StartNew(() => projectMerger.MergeProject(project));
			}
		}
	}
}
