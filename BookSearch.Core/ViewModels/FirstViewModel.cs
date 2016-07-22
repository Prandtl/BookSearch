using System.Collections.Generic;
using System.Diagnostics;
using BookSearch.Core.Services;
using MvvmCross.Core.ViewModels;

namespace BookSearch.Core.ViewModels
{
	public class FirstViewModel
		: MvxViewModel
	{
		private readonly IAsyncBooksService _booksService;

		public FirstViewModel(IAsyncBooksService booksService)
		{
			_booksService = booksService;
			_sw = new Stopwatch();
		}


		private string _query;

		public string Query
		{
			get { return _query; }
			set { SetProperty(ref _query, value); }
		}

		private List<BookSearchItem> _results;

		public List<BookSearchItem> Results
		{
			get { return _results; }
			set { SetProperty(ref _results, value); }
		}

		async private void Update()
		{
			if (_sw.ElapsedMilliseconds > _timeToThink)
			{
				_sw.Reset();
				_updatedRecently = false;
			}
			if (_updatedRecently)
				return;

			var results =  await _booksService.StartSearchAsync(Query, error => { });
			Results = results.items;
			_sw.Start();
			_updatedRecently = true;
		}

		private bool _updatedRecently;
		private Stopwatch _sw;
		private const int _timeToThink = 300;//Время которое должно пройти с последнего обновления чтобы обновляться снова;
	}
}
