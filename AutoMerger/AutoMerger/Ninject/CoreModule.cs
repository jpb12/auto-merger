using Ninject.Modules;
using AutoMerger.Core;

namespace AutoMerger.Ninject
{
	class CoreModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IEmailSender>().To<EmailSender>();
			Bind<IMerger>().To<Merger>();
			Bind<IMergeRunner>().To<MergeRunner>();
			Bind<IProjectMerger>().To<ProjectMerger>();
			Bind<IReportGenerator>().To<ReportGenerator>();
			Bind<IThreadManager>().To<ThreadManager>();
		}
	}
}
