using System.Collections.Generic;
using BookSearch.Core.Services;
using MvvmCross.Core.ViewModels;

namespace BookSearch.Core.ViewModels
{
	public class FirstViewModel
		: MvxViewModel
	{
		private readonly IBooksService _booksService;

		public FirstViewModel(IBooksService booksService)
		{
			_booksService = booksService;
			//_sw = new Stopwatch();
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
		/*
		 * Обновление не сразу а по истечении некоторого времени с последнего обновления Query
		private Stopwatch _sw;
		private const int _waitMs = 300;//Количество милисекунд после истечения которых отправляется пакет в Google

		private void OnQueryUpdate()
		{
			_sw.Stop();

			
			
		}
		*/
		private void Update()
		{
			_booksService.StartSearchAsync(Query,
				result => Results = result.items,
				error => { });
		}
	}
}
