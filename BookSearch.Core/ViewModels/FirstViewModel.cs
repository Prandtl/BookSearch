using System.Collections.Generic;
using BookSearch.Core.Services;
using MvvmCross.Core.ViewModels;

namespace BookSearch.Core.ViewModels
{
	public class FirstViewModel
		: MvxViewModel
	{
		private IBooksService _booksService;

		public FirstViewModel(IBooksService booksService)
		{
			_booksService = booksService;
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

		

		private void Update()
		{
			
		}
	}
}
