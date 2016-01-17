using AutoMerger.Shared.Core;
using BranchManager.Core.Svn;
using BranchManager.Core.Tree;
using Ninject.Modules;

namespace BranchManager.Core.Ninject
{
	public class CoreModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IBranchInfoGetter>().To<BranchInfoGetter>();
			Bind<IMergeTreeGetter>().To<MergeTreeGetter>();
			Bind<IUnmergedRevisionGetter>().To<UnmergedRevisionGetter>();
			Bind<ISvnInterface>().To<SvnInterface>();
		}
	}
}
