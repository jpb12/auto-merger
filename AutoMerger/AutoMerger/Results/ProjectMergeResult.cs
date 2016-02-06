using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AutoMerger.Results
{
	class ProjectMergeResult
	{
		public ProjectMergeResult(string projectUrl, IEnumerable<MergeResult> results)
		{
			ProjectUrl = projectUrl;
			Results = results.ToList().AsReadOnly();
		}

		public string ProjectUrl { get; }

		public ReadOnlyCollection<MergeResult> Results { get; }
	}
}
