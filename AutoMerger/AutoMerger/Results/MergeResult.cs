using System;

namespace AutoMerger.Results
{
	class MergeResult
	{
		private readonly string _parent;
		private readonly string _child;
		private readonly bool _success;
		private readonly string _message;
		private readonly Exception _exception;

		public MergeResult(
			string parent,
			string child,
			bool success,
			string message,
			Exception exception = null)
		{
			_parent = parent;
			_child = child;
			_success = success;
			_message = message;
			_exception = exception;
		}

		public string Parent
		{
			get { return _parent; }
		}

		public string Child
		{
			get { return _child; }
		}

		public bool Success
		{
			get { return _success; }
		}

		public string Message
		{
			get { return _message; }
		}

		public Exception Exception
		{
			get { return _exception; }
		}
	}
}
