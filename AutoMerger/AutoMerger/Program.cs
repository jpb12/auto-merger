using AutoMerger.Core;
using AutoMerger.Ninject;
using Ninject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoMerger
{
	class Program
	{
		static void Main(string[] args)
		{
			var kernel = new StandardKernel(new CoreModule());

			var projectMerger = kernel.Get<IProjectMerger>();
			var configGetter = kernel.Get<IConfigGetter>();

			var config = configGetter.GetConfig();

			var tasks = new List<Task>();

			foreach(var project in config.Projects)
			{
				tasks.Add(Task.Factory.StartNew(() => projectMerger.MergeProject(project)));
			}

			tasks.ForEach(t => t.Wait());
		}
	}
}
