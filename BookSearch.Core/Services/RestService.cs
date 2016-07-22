using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;

namespace BookSearch.Core.Services
{
	public class RestService : IRestService
	{
		private readonly IMvxJsonConverter _jsonConverter;

		public RestService(IMvxJsonConverter jsonConverter)
		{
			_jsonConverter = jsonConverter;
		}

		async public Task<T> MakeRequest<T>(string url, string verb, Action<Exception> errorAction)
		{
			var request = (HttpWebRequest) WebRequest.Create(url);
			request.Method = verb;
			request.Accept = "application/json";
			try
			{
				using (var response = (HttpWebResponse)await request.GetResponseAsync())
				using (var stream = response.GetResponseStream())
				using (var reader = new StreamReader(stream))
				{
					var responseText = await reader.ReadToEndAsync();
					var result = Deserialize<T>(responseText);
					return result;
				}
			}
			catch (WebException e)
			{
				Mvx.Error("Error: {0} when making {1} request to {2}", e.Message, request.Method, request.RequestUri.AbsoluteUri);
			}
			return default(T);
		}

		private T Deserialize<T>(string responseBody)
		{
			var toReturn = _jsonConverter.DeserializeObject<T>(responseBody);
			return toReturn;
		}
	}
}