using System.Collections.Generic;

namespace TestViaApi
{
    // This corresponds to the structure of the response to a /1.0/collections/:dbName/:collectionname GET request
    public class DocumentList : List<Document> { }

    public class Document
    {
    }

}
