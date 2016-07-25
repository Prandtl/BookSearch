using System;
using System.Collections.Generic;
using System.Diagnostics;
using BookSearch.Core.Services;
using MvvmCross.Core.ViewModels;
using System.Threading;

namespace BookSearch.Core.ViewModels
{
	public class FirstViewModel
		: MvxViewModel
	{
		private readonly IAsyncBooksService _booksService;
		public FirstViewModel(IAsyncBooksService booksService)
		{
			_booksService = booksService;

			_lockObject = new object();
		}

		private string _query;
		public string Query
		{
			get { return _query; }
			set { SetProperty(ref _query, value); ScheduleUpdate(); }
		}

		private List<BookSearchItem> _results;
		public List<BookSearchItem> Results
		{
			get { return _results; }
			set { SetProperty(ref _results, value); }
		}

		private bool _isUpdating;

		public bool IsUpdating
		{
			get { return _isUpdating; }
			set { SetProperty(ref _isUpdating, value); }
		}


		private void ScheduleUpdate()
		{
			lock (_lockObject)
			{
				if (_timer == null)
				{
					_timer = new Timer(OnTimerTick, null, _timeToThink, TimeSpan.Zero);
				}
				else
				{
					_timer.Change(_timeToThink, TimeSpan.Zero);
				}
			}
		}

		private void OnTimerTick(object state)
		{
			lock (_lockObject)
			{
				_timer?.Dispose();
				_timer = null;
				Update();
			}
		}

		private async void Update()
		{
			IsUpdating = true;
			try
			{
				var result = await _booksService.StartSearchAsync(Query, error => { IsUpdating = false; });
				Results = result?.items;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
			}
			IsUpdating = false;
		}

		private Timer _timer;
		private readonly object _lockObject;

		private readonly TimeSpan _timeToThink = TimeSpan.FromMilliseconds(300);//Время которое должно пройти с последнего обновления чтобы обновляться снова;
	}
}
