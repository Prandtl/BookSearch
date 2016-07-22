using System;
using System.Runtime.InteropServices.ComTypes;

namespace BookSearch.Core.Services
{
	public interface ISimpleRestService
	{
		void MakeRequest<T>(string url, string verb, Action<T> successAction, Action<Exception> errorAction);
	}
}
