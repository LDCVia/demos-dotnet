
namespace TestViaApi
{
    // This corresponds to the structure of the response to a /1.0/login POST request
    public class Login
    {
        public string apikey { get; set; }
        public string email { get; set; }

        // NOTE: there are other attributes in a login response, such as databases array, 
        // notesnames array, etc. See http://api.ldcvia.com/#login
    }
}
