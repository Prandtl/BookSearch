using System;

namespace BookSearch.Core.Services
{
	public interface IBooksService
	{
		void StartSearchAsync(string whatFor, Action<BookSearchResult> success, Action<Exception> error);
	}

	public class BooksService : IBooksService
	{
		private readonly ISimpleRestService _simpleRestService;

		public BooksService(ISimpleRestService restService)
		{
			_simpleRestService = restService;
		}
		public void StartSearchAsync(string whatFor, Action<BookSearchResult> success, Action<Exception> error)
		{
			string address = $"https://www.googleapis.com/books/v1/volumes?q={Uri.EscapeDataString(whatFor)}";
			_simpleRestService.MakeRequest(address, "GET", success, error);
		}
	}
}
