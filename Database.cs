using System.Collections.Generic;

namespace TestViaApi
{
    // This corresponds to the structure of the response to a /1.0/databases GET request
    public class DatabaseList : List<Database> { }

    public class Database
    {
        public string name { get; set; }
        public string title { get; set; }
    }
}
