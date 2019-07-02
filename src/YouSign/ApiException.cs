using System;
using System.Net;

namespace YouSign
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public ErrorOutput Content { get; set; }
        public string ErrorMessage { get; set; }
    }
}
