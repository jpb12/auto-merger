using Ninject.Modules;
using AutoMerger.Core;
using AutoMerger.Shared.Core;
using AutoMerger.Types;

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
			Bind<IConfigurationManager<ConfigKey>>().ToMethod(c => new ConfigurationManager<ConfigKey>(_args));
			Bind<IEmailSender>().To<EmailSender>();
			Bind<IMerger>().To<Merger>();
			Bind<IMergeRunner>().To<MergeRunner>();
			Bind<IProjectMerger>().To<ProjectMerger>();
			Bind<IReportGenerator>().To<ReportGenerator>();
			Bind<IThreadManager>().To<ThreadManager>();
		}
	}
}
