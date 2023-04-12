using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Nodes;
using System.Net.Http.Headers;

namespace Library
{
    public class Response<T>
    {
        private HttpResponseMessage? response;
        private string? text = "";
        private T? data;
        private JsonObject? json;
        private HttpContentHeaders? headers;
        private string? contentType;
        

        public HttpStatusCode StatusCode { get => (response != null) ? response.StatusCode : HttpStatusCode.BadRequest; }
        public bool Ok { get => (response != null) ? response.IsSuccessStatusCode : false; }
        public string? Reason { get => (response != null) ? response.ReasonPhrase : ""; }
        public string Text { get => (text != null) ? text : ""; }
        public T? Data { get => data; }
        public JsonObject? JSON { get => json; }
        public HttpContentHeaders? Headers { get => headers; }
        public string? ContentType { get => contentType; }


        public Response(Task<HttpResponseMessage> task)
        {
            try { this.response = task.Result; } catch { }

            if (this.response != null)
            {
                this.headers = this.response.Content.Headers;
                this.contentType = (this.headers.ContentType != null) ? this.headers.ContentType.MediaType : null;
                this.text = this.response.Content.ReadAsStringAsync().Result;

                try { 
                    this.data = this.response.Content.ReadFromJsonAsync<T>().Result; 
                } catch { }

                if (this.contentType == "application/json")
                {
                    JsonNode? node = JsonObject.Parse(this.text);
                    if (node != null)
                        this.json = node.AsObject();
                }
            }
        }
    }
}
