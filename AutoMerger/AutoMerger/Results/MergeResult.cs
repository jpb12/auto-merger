using System;

namespace AutoMerger.Results
{
	class MergeResult
	{
		public MergeResult(
			string parent,
			string child,
			bool success,
			string message,
			Exception exception = null)
		{
			Parent = parent;
			Child = child;
			Success = success;
			Message = message;
			Exception = exception;
		}

		public string Parent { get; }

		public string Child { get; }

		public bool Success { get; }

		public string Message { get; }

		public Exception Exception { get; }
	}
}
