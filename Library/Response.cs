using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Response<T>
    {
        private HttpResponseMessage response;
        private string text;
        private T? data;

        public HttpStatusCode StatusCode { get => response.StatusCode; }
        public bool Ok { get => response.IsSuccessStatusCode; }
        public string? Reason { get => response.ReasonPhrase; }
        public string Text { get => text; }
        public T? Data { get => data; }

        public Response(HttpResponseMessage response)
        {
            this.response = response;
            this.text = response.Content.ReadAsStringAsync().Result;
            this.data = response.Content.ReadFromJsonAsync<T>().Result;
        }

    }
}
