using System;
using System.IO;
using System.Net;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;

namespace BookSearch.Core.Services
{
	class SimpleRestService : ISimpleRestService
	{
		private readonly IMvxJsonConverter _jsonConverter;

		public SimpleRestService(IMvxJsonConverter jsonConverter)
		{
			_jsonConverter = jsonConverter;
		}

		public void MakeRequest<T>(string url, string verb, Action<T> successAction, Action<Exception> errorAction)
		{
			var request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = verb;
			request.Accept = "application/json";

			MakeRequest(request,
				response =>
				{
					if (successAction == null) return;
					T toReturn;
					try
					{
						toReturn = Deserialize<T>(response);
					}
					catch (Exception e)
					{
						errorAction(e);
						return;
					}
					successAction(toReturn);
				},
				error =>
				{
					errorAction?.Invoke(error);
				});
		}

		private void MakeRequest(HttpWebRequest request, Action<string> successAction, Action<Exception> errorAction)
		{
			request.BeginGetResponse(
				ar =>
				{
					try
					{
						using (var response = request.EndGetResponse(ar))
						{
							using (var stream = response.GetResponseStream())
							{
								var reader = new StreamReader(stream);
								successAction(reader.ReadToEnd());
							}
						}
					}
					catch (WebException e)
					{
						Mvx.Error("Error: {0} when making {1} request to {2}", e.Message, request.Method, request.RequestUri.AbsoluteUri);
					}
				},
				null);
		}

		private T Deserialize<T>(string responseBody)
		{
			var toReturn = _jsonConverter.DeserializeObject<T>(responseBody);
			return toReturn;
		}
	}
}