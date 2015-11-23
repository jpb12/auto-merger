namespace AutoMerger.Core
{
	interface IThreadManager
	{
		bool TryStartThread();
		void FinishThred();
	}

	class ThreadManager : IThreadManager
	{
		private readonly int? _maxThreads;
		private readonly object _lock;

		private int _runningThreads;

		public ThreadManager(IConfigurationManager configManager)
		{
			var threads = configManager.GetIntValue(ConfigKey.Threads);
			_maxThreads = threads > 0 ? threads : (int?)null;
			_runningThreads = 0;
			_lock = new object();
		}

		public bool TryStartThread()
		{
			lock (_lock)
			{
				if (!_maxThreads.HasValue || _runningThreads < _maxThreads)
				{
					_runningThreads++;
					return true;
				}

				return false;
			}
		}

		public void FinishThred()
		{
			lock (_lock)
			{
				_runningThreads--;
			}
		}
	}
}
