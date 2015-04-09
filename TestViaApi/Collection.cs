using System.Collections.Generic;

namespace TestViaApi
{
    // This corresponds to the structure of the response to a /1.0/collections GET request
    public class CollectionList : List<Collection> { }

    public class Collection
    {
        public string collection { get; set; }
        public int count { get; set; }
    }

}
