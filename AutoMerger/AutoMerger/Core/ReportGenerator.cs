using AutoMerger.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoMerger.Core
{
	interface IReportGenerator
	{
		string Generate(IEnumerable<ProjectMergeResult> projects);
	}

	class ReportGenerator : IReportGenerator
	{
		public string Generate(IEnumerable<ProjectMergeResult> projects)
		{
			var stringBuilder = new StringBuilder();

			foreach(var project in projects.OrderBy(p => p.ProjectUrl))
			{
				GenerateForProject(stringBuilder, project);
			}

			return stringBuilder.ToString();
		}

		private void GenerateForProject(StringBuilder stringBuilder, ProjectMergeResult project)
		{
			stringBuilder.AppendLine("Project: " + project.ProjectUrl);

			if (project.Results.Any(r => r.Success))
			{
				stringBuilder.AppendLine("\tSucceeded:");

				foreach (var result in project.Results.Where(r => r.Success))
				{
					GenerateForMerge(stringBuilder, result);
				}

				stringBuilder.AppendLine();
			}

			if (project.Results.Any(r => !r.Success))
			{
				stringBuilder.AppendLine("\tFailed:");

				foreach (var result in project.Results.Where(r => !r.Success))
				{
					GenerateForMerge(stringBuilder, result);
				}

				stringBuilder.AppendLine();
			}
		}

		private void GenerateForMerge(StringBuilder stringBuilder, MergeResult result)
		{
			stringBuilder.AppendFormat(
				"\t{0} to {1}: {2}{3}",
				result.Parent,
				result.Child,
				result.Message,
				Environment.NewLine);
		}
	}
}
