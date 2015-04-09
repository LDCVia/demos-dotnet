using RestSharp;
using System;

namespace TestViaApi
{
    class Program
    {
        // Uses RestSharp: http://restsharp.org/
        public static void Main(string[] args)
        {
            var client = new RestClient("http://eu.ldcvia.com");
            var request = new RestRequest("/1.0/databases", Method.GET);

            // Add HTTP header for the API key
            request.AddHeader("apikey", "MAGIC API KEY HERE!");

            IRestResponse response = client.Execute(request);
            // response.Content; // Will give you the raw content (string)

            // Iterate the response, which is an array of Database object references
            IRestResponse<DatabaseList> dbList = client.Execute<DatabaseList>(request);
            Console.WriteLine("There are {0} databases detailed in the response", dbList.Data.Count);

            int i = 0;
            foreach (Database db in dbList.Data)
            {
                i++;
                Console.WriteLine("Database #{0} is called {1}", i, db.title);
            }

            // Other stuff to add:
            // 1) Login - post username and password, get back api key
            // 2) get database list
            // 3) get collections for a database
            // 4) get documents in a collection
            // 5) get document

        }

    }
}
