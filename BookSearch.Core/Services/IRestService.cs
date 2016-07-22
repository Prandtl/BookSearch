using System;
using System.Threading.Tasks;

namespace BookSearch.Core.Services
{
	public interface IRestService
	{
		Task<T> MakeRequest<T>(string url, string verb, Action<Exception> errorAction);
	}
}
