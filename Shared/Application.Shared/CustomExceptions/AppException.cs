using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared.CustomExceptions
{
    public sealed class AppException : ApplicationException
    {
        public AppException(string message, HttpStatusCode code)
        {
            StatusCode = code;
            ErrorMessage = message;
        }
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage {  get; set; }
    }
}
