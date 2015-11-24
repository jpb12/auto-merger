using Ninject.Modules;
using AutoMerger.Core;

namespace AutoMerger.Ninject
{
	class CoreModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IConfigGetter>().To<ConfigGetter>();
			Bind<IConfigurationManager>().To<ConfigurationManager>();
			Bind<IMerger>().To<Merger>();
			Bind<IProjectMerger>().To<ProjectMerger>();
			Bind<IReportGenerator>().To<ReportGenerator>();
			Bind<ISvnInterface>().To<SvnInterface>();
			Bind<IThreadManager>().To<ThreadManager>();
		}
	}
}
