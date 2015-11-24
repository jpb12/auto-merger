using AutoMerger.Core;
using AutoMerger.Ninject;
using AutoMerger.Results;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
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
			var reportGenerator = kernel.Get<IReportGenerator>();

			var config = configGetter.GetConfig();

			var tasks = new List<Task<ProjectMergeResult>>();

			foreach (var project in config.Projects)
			{
				tasks.Add(Task<ProjectMergeResult>.Factory.StartNew(() => projectMerger.MergeProject(project)));
			}

			var results = tasks
				.Select(t => t.Result)
				.OrderBy(r => r.ProjectUrl)
				.ToList();

			var report = reportGenerator.Generate(results);

			Console.Write(report);
		}
	}
}
