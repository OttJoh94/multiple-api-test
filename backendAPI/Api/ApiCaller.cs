using backendAPI.Models;
using Newtonsoft.Json;
using System.Text;

namespace backendAPI.Api
{
	public class ApiCaller
	{
		public HttpClient Client { get; set; }

		public ApiCaller()
		{
			Client = new();

			Client.BaseAddress = new Uri("http://127.0.0.1:5000/");
		}

		public async Task<PromptModel> GetAsync(string url)
		{
			HttpResponseMessage response = await Client.GetAsync(url);

			if (response.IsSuccessStatusCode)
			{
				string json = await response.Content.ReadAsStringAsync();

				PromptModel? result = JsonConvert.DeserializeObject<PromptModel>(json);

				if (result != null)
				{
					return result;
				}
			}

			throw new HttpRequestException();
		}

		public async Task<PromptModel> PostAsync(string url, object data)
		{
			string jsonData = JsonConvert.SerializeObject(data);

			StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await Client.PostAsync(url, content);

			if (response.IsSuccessStatusCode)
			{
				string json = await response.Content.ReadAsStringAsync();

				PromptModel? result = JsonConvert.DeserializeObject<PromptModel>(json);

				if (result != null)
				{
					return result;
				}
			}

			throw new HttpRequestException();
		}
	}
}
