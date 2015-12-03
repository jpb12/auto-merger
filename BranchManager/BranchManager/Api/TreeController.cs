using AutoMerger.Shared.Types;
using System.Collections.Generic;
using System.Web.Http;

namespace BranchManager.Api
{
	[RoutePrefix("api/tree")]
	public class TreeController : ApiController
	{
		[Route("")]
		public MergeConfig Get()
		{
			return new MergeConfig
			{
				Projects = new List<Project>
				{
					new Project
					{
						ProjectUrl = "http://temp/one",
						Merges = new List<Merge>
						{
							new Merge
							{
								Child = "trunk",
								Parent = "1.0",
								Enabled = true
							},
							new Merge
							{
								Child = "child-one",
								Parent = "trunk",
								Enabled = true
							},
							new Merge
							{
								Child = "child-two",
								Parent = "trunk",
								Enabled = true
							}
						}
					}
				}
			};
		}
	}
}
