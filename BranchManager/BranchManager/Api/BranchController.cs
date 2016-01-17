using BranchManager.Core.Tree;
using BranchManager.Core.Types;
using System.Web.Http;

namespace BranchManager.Api
{
	[RoutePrefix("api/branch")]
	public class BranchController : ApiController
	{
		private readonly IBranchInfoGetter _branchInfoGetter;

		public BranchController(IBranchInfoGetter branchInfoGetter)
		{
			_branchInfoGetter = branchInfoGetter;
		}

		[Route("{name}")]
		public BranchInfo Get(string projectUrl, string name)
		{
			return _branchInfoGetter.Get(projectUrl, name);
		}
	}
}
