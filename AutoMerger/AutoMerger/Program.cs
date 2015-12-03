using AutoMerger.Core;
using AutoMerger.Ninject;
using AutoMerger.Shared.Ninject;
using Ninject;

namespace AutoMerger
{
	class Program
	{
		static void Main(string[] args)
		{
			var kernel = new StandardKernel(new CoreModule(), new SharedModule(args));

			var mergeRunner = kernel.Get<IMergeRunner>();
			mergeRunner.Run();
		}
	}
}
