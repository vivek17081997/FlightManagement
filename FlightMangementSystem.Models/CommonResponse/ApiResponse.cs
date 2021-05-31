using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace FlightMangementSystem.Models.CommonResponse
{
	public class ApiResponse<T>
	{
		public ApiResponse()
		{

		}
        public ApiResponse(HttpStatusCode statusCode, string message, T data)
        {
            ResponseStatusCode = statusCode;
            ResponseMessage = message;
            ResponseData = data;
        }

        public HttpStatusCode ResponseStatusCode { get; set; }
        public string ResponseMessage { get; set; }
        public T ResponseData { get; set; }
    }
}
