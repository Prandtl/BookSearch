using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MvvmCross.Platform;

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
			BookSearchResult result = null;

			try
			{
				string address = $"https://www.googleapis.com/books/v1/volumes?q={Uri.EscapeDataString(whatFor)}";
				result = await _restService.MakeRequest<BookSearchResult>(address, "GET", error);
				
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
			}

			return result;
		}
	}
}