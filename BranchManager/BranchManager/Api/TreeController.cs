using BranchManager.Core.Tree;
using System.Collections.Generic;
using System.Web.Http;

namespace BranchManager.Api
{
	[RoutePrefix("api/tree")]
	public class TreeController : ApiController
	{
		private readonly IMergeTreeGetter _mergeTreeGetter;

		public TreeController(IMergeTreeGetter mergeTreeGetter)
		{
			_mergeTreeGetter = mergeTreeGetter;
		}

		[Route("")]
		public IEnumerable<Project> Get()
		{
			return _mergeTreeGetter.GetTree();
		}
	}
}
