using System;
using System.Threading.Tasks;

namespace BookSearch.Core.Services
{
	public interface IAsyncBooksService
	{
		Task<BookSearchResult> StartSearchAsync(string whatFor, Action<Exception> error);
	}
}
