using System;
using System.Threading.Tasks;

namespace BookSearch.Core.Services
{
	public class AsyncBooksService : IAsyncBooksService
	{
		private readonly IRestService _restService;

		public AsyncBooksService(IRestService restService)
		{
			_restService = restService;
		}

		public async Task<BookSearchResult> StartSearchAsync(string whatFor, Action<Exception> error)
		{
			string address = $"https://www.googleapis.com/books/v1/volumes?q={Uri.EscapeDataString(whatFor)}";
			var result = await _restService.MakeRequest<BookSearchResult>(address, "GET", error);
			return result;
		}
	}
}