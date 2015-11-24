using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AutoMerger.Results
{
	class ProjectMergeResult
	{
		private readonly string _projectUrl;
		private readonly ReadOnlyCollection<MergeResult> _results;

		public ProjectMergeResult(string projectUrl, IEnumerable<MergeResult> results)
		{
			_projectUrl = projectUrl;
			_results = results.ToList().AsReadOnly();
		}

		public string ProjectUrl
		{
			get { return _projectUrl; }
		}

		public ReadOnlyCollection<MergeResult> Results
		{
			get { return _results; }
		}
	}
}
