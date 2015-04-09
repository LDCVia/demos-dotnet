using RestSharp;
using System;

namespace TestViaApi
{
    class Program
    {
        private static string usr = "USER EMAIL HERE";
        private static string pwd = "USER PASSWORD HERE";

        // LDC Via endpoint here
        const string BASE_URI = "https://eu.ldcvia.com";

        /// <summary>
        /// Main entrypoint: get a specified user's API key, then use that to make a load of other calls 
        /// to the LDC Via REST API. This code makes use of the RESTSharp library: http://restsharp.org/
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var apiKey = GetUserKey();
            Console.WriteLine("Retrieved API key for '{0}': {1}", usr, apiKey);

            var databases = GetDatabaseList(apiKey);
            if (databases.Count > 0)
            {
                var db = databases[1];
                GetCollectionsForDatabase(db, apiKey);
            }
        }

        /// <summary>
        /// The initial call, which retrieves a user's API key.
        /// </summary>
        /// <returns>string containing LDC Via API key</returns>
        private static string GetUserKey()
        {
            var client = new RestClient(BASE_URI);
            var request = new RestRequest("/1.0/login", Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("username", usr);
            request.AddParameter("password", pwd);

            IRestResponse<Login> response = client.Execute<Login>(request);
            return response.Data.apikey;
        }

        /// <summary>
        /// Given a valid API key, retrieves a database list from the LDC Via API.
        /// </summary>
        /// <param name="key">API key to use for call</param>
        /// <returns>DatabaseList from response</returns>
        private static DatabaseList GetDatabaseList(string key)
        {
            var client = new RestClient(BASE_URI);
            var request = new RestRequest("/1.0/databases", Method.GET);
            // Add HTTP header for the API key
            request.AddHeader("apikey", key);

            // Iterate the response, which is an array of Database object references
            IRestResponse<DatabaseList> response = client.Execute<DatabaseList>(request);
            Console.WriteLine("There are {0} databases detailed in the response", response.Data.Count);

            int i = 0;
            foreach (Database db in response.Data)
            {
                i++;
                Console.WriteLine("Database #{0} is called {1}", i, db.title);
            }

            return response.Data;
        }

        /// <summary>
        /// Given a database reference and a valid API key, retrieves a collection list from the LDC Via API
        /// </summary>
        /// <param name="db">Database reference to query</param>
        /// <param name="key">API key to use for call</param>
        /// <returns>CollectionList from Database</returns>
        private static CollectionList GetCollectionsForDatabase(Database db, string key)
        {
            var client = new RestClient(BASE_URI);
            var request = new RestRequest("/1.0/collections/" + db.name, Method.GET);
            // Add HTTP header for the API key
            request.AddHeader("apikey", key);

            // Iterate the response, which is an array of Database object references
            IRestResponse<CollectionList> response = client.Execute<CollectionList>(request);
            Console.WriteLine("There are {0} collections detailed in the response for {1}", response.Data.Count, db.name);

            int i = 0;
            foreach (Collection c in response.Data)
            {
                i++;
                Console.WriteLine("Collection #{0} is called {1}", i, c.collection);
            }

            return response.Data;
        }

    }

}
