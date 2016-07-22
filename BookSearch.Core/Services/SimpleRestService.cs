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
			var request = (HttpWebRequest) WebRequest.Create(url);
			request.Method = verb;
			request.Accept = "application/json";
			
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
				}, null);
		}
	}
}