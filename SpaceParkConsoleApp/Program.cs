
using SpaceParkConsoleApp.Helper;
using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SpaceParkConsoleApp
{
    class Program
    {
        private static string APIUrl = "http://localhost:44382/api/";

        static void Main(string[] args)
        {
            //Added this line to Parse double values to not mix "." and ","
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            Console.WriteLine("Welcome dear traveller!\n");

            
        }

        public static async void GetDataWithoutAuthentication()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(APIUrl);

                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var rawResponse = readTask.GetAwaiter().GetResult();
                    Console.WriteLine(rawResponse);
                }
                Console.WriteLine("Complete");
            }
        }
    }
}

