using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Requests
    {
        private static HttpClient createJsonHttpClient()
        {
            HttpClient client = new HttpClient();

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        public static Response<T> post<T>(string url, FormUrlEncodedContent parameters)
        {
            HttpClient client = createJsonHttpClient();

            //HttpResponseMessage response = client.GetAsync(url).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            HttpResponseMessage response = client.PostAsync(url, parameters).Result;
            
            Response<T> result = new Response<T>(response);

            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of;
            // for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();

            return result;
        }

        public static Response<T> get<T>(string url)
        {
            HttpClient client = createJsonHttpClient();

            // Blocking call! Program will wait here until a response is received or a timeout occurs.
            HttpResponseMessage response = client.GetAsync(url).Result;
            
            Response<T> result = new Response<T>(response);

            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of;
            // for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();

            return result;
        }
    }
}
