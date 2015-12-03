using AutoMerger.Shared.Core;
using Ninject.Modules;

namespace AutoMerger.Shared.Ninject
{
	public class SharedModule : NinjectModule
	{
		private readonly string[] _args;

		public SharedModule(string[] args)
		{
			_args = args;
		}

		public override void Load()
		{
			Bind<IConfigGetter>().To<ConfigGetter>();
			Bind<IConfigurationManager>().ToMethod(c => new ConfigurationManager(_args));
			Bind<ISvnInterface>().To<SvnInterface>();
		}
	}
}
