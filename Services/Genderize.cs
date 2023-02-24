using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CodingAssessment.Services
{
	class Genderize
	{
		// Makes an API Request to guess the customers gender based on their first name
		public async Task<string> GetGender(string firstName)
		{
			string baseUrl = $"https://api.genderize.io/?name={firstName}";

			try
			{
				using (HttpClient client = new HttpClient())
				{
					using (HttpResponseMessage res = await client.GetAsync(baseUrl))
					{
						using (HttpContent content = res.Content)
						{
							var data = await content.ReadAsStringAsync();
							if (data != null)
							{
								var dataObj = JObject.Parse(data);
								return (string)dataObj["gender"];
							}
							else
							{
								Console.WriteLine("No data returned");
							}
						}
					}
				}
			}
			catch (Exception exception)
			{
				Console.WriteLine($"Error occurred: {exception}");
				throw;
			}

			return "Gender Unknown";
		}
	}
}
