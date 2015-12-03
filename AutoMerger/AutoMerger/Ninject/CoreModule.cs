using Ninject.Modules;
using AutoMerger.Core;

namespace AutoMerger.Ninject
{
	class CoreModule : NinjectModule
	{
		private readonly string[] _args;

		public CoreModule(string[] args)
		{
			_args = args;
		}

		public override void Load()
		{
			Bind<IConfigGetter>().To<ConfigGetter>();
			Bind<IConfigurationManager>().ToMethod(c => new ConfigurationManager(_args));
			Bind<IEmailSender>().To<EmailSender>();
			Bind<IMerger>().To<Merger>();
			Bind<IMergeRunner>().To<MergeRunner>();
			Bind<IProjectMerger>().To<ProjectMerger>();
			Bind<IReportGenerator>().To<ReportGenerator>();
			Bind<ISvnInterface>().To<SvnInterface>();
			Bind<IThreadManager>().To<ThreadManager>();
		}
	}
}
