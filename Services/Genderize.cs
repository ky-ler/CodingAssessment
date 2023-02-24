using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CodingAssessment.Services
{
    class Genderize
    {
        // Makes an API Request to guess the customers gender based on their first name
        // And then Console.WriteLine the customer's info

        // Originally, I had it setup so it would return gender as Task<string>, and do the Console.WriteLine in Program.cs
        // But I ran into a hiccup where the variable's value was "System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1+AsyncStateMachineBox`1..."
        // So I just moved everything into here
        public async void GetGender(Model.Customer customer)
        {
            string baseUrl = $"https://api.genderize.io/?name={customer.FirstName}";

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
                                Console.WriteLine($"{customer.LastName}, {customer.FirstName}, {DateTime.Now.Year - customer.BirthDate?.Year}, {dataObj["gender"]}");
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
        }
    }
}
