using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace RestApi.Client
{    
    /*
     *  Return json string of result
        received from server after sending request.

     *  Has two type result of request is :
        #1 : result without status code.
        #2 : result with status code.
     */
    public class RestClient
    {
        private string _baseUrl = ConfigurationManager.AppSettings["MainAPI"].ToString();

        public string BaseUrl
        {
            get
            {
                return _baseUrl;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value) == false)
                {
                    _baseUrl = value;
                }
            }
        }

        public string EndPoint { get; set; }

        /*
            convert C# object to json object.
         */
        private string ObjectToJson(object obj)
        {
            return obj != null ? JsonConvert.SerializeObject(obj) : string.Empty;
        }

        /*
            support create body content for post and put method.
         */
        private HttpContent CreateHttpContent(object obj)
        {
            return new StringContent(ObjectToJson(obj), Encoding.UTF8, "application/json");
        }

        /*
            get result from rest server after sending request.
         */
        private HttpResponseMessage GetResponse(RequestType type, object obj)
        {
            HttpClient client = new HttpClient { BaseAddress = new Uri(BaseUrl) };

            switch (type)
            {
                case RequestType.GET: return client.GetAsync(EndPoint).Result;
                case RequestType.POST: return client.PostAsync(EndPoint, CreateHttpContent(obj)).Result;
                case RequestType.PUT: return client.PutAsync(EndPoint, CreateHttpContent(obj)).Result;
                case RequestType.DELETE: return client.DeleteAsync(EndPoint).Result;
                default: throw new NotSupportedException();
            }
        }

        /* generic method request : start. */
        /*
            #1 : result without status code.
         */
        public string Request(RequestType type, object obj)
        {
            var message = GetResponse(type, obj);

            return message.IsSuccessStatusCode
                ? message.Content.ReadAsStringAsync().Result
                : string.Empty;
        }

        public string Request(RequestType _RequestType)
        {
            return Request(_RequestType, null);
        }

        /*
            #2 : result with status code.
         */
        public string Request(RequestType type, object obj, out object statusCode)
        {
            var message = GetResponse(type, obj);

            statusCode = message.StatusCode.ToString();

            return message.IsSuccessStatusCode
                ? message.Content.ReadAsStringAsync().Result
                : string.Empty;
        }

        public string Request(RequestType _RequestType, out object statuscode)
        {
            return Request(_RequestType, null, out statuscode);
        }
        /*
            :: end.
         */

        /* request http method get : start. */
        /*
            #1 ::
         */
        public string Get()
        {
            return Request(RequestType.GET);
        }

        /*
            #2 ::
         */
        public string Get(out object statuscode)
        {
            return Request(RequestType.GET, out statuscode);
        }
        /*
            :: end.
         */

        /* request http method post : start. */
        /*
            #1 ::
         */
        public string Post(object obj)
        {
            return Request(RequestType.POST, obj);
        }

        public string Post()
        {
            return Post(null);
        }

        /*
            #2 ::
         */
        public string Post(object obj, out object statuscode)
        {
            return Request(RequestType.POST, obj, out statuscode);
        }

        public string Post(out object statuscode)
        {
            return Post(null, out statuscode);
        }
        /*
            :: end.
         */

        /* request http method put : start. */
        /*
            #1 ::
         */
        public string Put(object obj)
        {
            return Request(RequestType.PUT, obj);
        }

        public string Put()
        {
            return Put(null);
        }

        /*
            #2 ::
         */
        public string Put(object obj, out object statuscode)
        {
            return Request(RequestType.PUT, obj, out statuscode);
        }

        public string Put(out object statuscode)
        {
            return Put(null, out statuscode);
        }
        /*
            :: end.
         */

        /* request http method delete : start. */
        /*
            #1 ::
         */
        public string Delete()
        {
            return Request(RequestType.DELETE, null);
        }

        /*
            #2 ::
         */
        public string Delete(out object statuscode)
        {
            return Request(RequestType.DELETE, out statuscode);
        }

        /*
            :: end.
         */
    }

    /*
     *  Support 4 request type : get, post, put, delete.
     */
    public enum RequestType
    {
        GET, POST, PUT, DELETE
    }

    public class RestParser
    {
        /*
            Generic method parse : start.
         */
        /*
            result hasn't status code.
         */
        public static T ParseTo<T>(RestClient rest, RequestType type, object obj)
        {
            string json = rest.Request(type, obj);

            if (string.IsNullOrWhiteSpace(json) == true)
            {
                json = string.Empty;
            }

            return JsonConvert.DeserializeObject<T>(json);
        }

        /*
            result has status code.
         */
        public static T ParseTo<T>(RestClient rest, RequestType type, object obj, out object statuscode)
        {
            string json = rest.Request(type, obj, out statuscode);

            if (string.IsNullOrWhiteSpace(json) == true)
            {
                json = string.Empty;
            }

            return JsonConvert.DeserializeObject<T>(json);
        }
        /*
            :: end.
         */

        /*
            method parse get : start.
         */
        /*
            result hasn't status code.
         */
        public static T ParseGetTo<T>(RestClient rest)
        {
            return ParseTo<T>(rest, RequestType.GET, null);
        }

        /*
            result has status code.
         */
        public static T ParseGetTo<T>(RestClient rest, out object statuscode)
        {
            return ParseTo<T>(rest, RequestType.GET, null, out statuscode);
        }
        /*
            :: end.
         */

        /*
            method parse post : start.
         */
        /*
            result hasn't status code.
         */
        public static T ParsePostTo<T>(RestClient rest, object obj)
        {
            return ParseTo<T>(rest, RequestType.POST, obj);
        }

        public static T ParsePostTo<T>(RestClient rest)
        {
            return ParsePostTo<T>(rest, null);
        }

        /*
            result has status code.
         */
        public static T ParsePostTo<T>(RestClient rest, object obj, out object statuscode)
        {
            return ParseTo<T>(rest, RequestType.POST, obj, out statuscode);
        }

        public static T ParsePostTo<T>(RestClient rest, out object statuscode)
        {
            return ParsePostTo<T>(rest, null, out statuscode);
        }
        /*
            :: end.
         */

        /*
            method parse put : start.
         */
        /*
            result hasn't status code.
         */
        public static T ParsePutTo<T>(RestClient rest, object obj)
        {
            return ParseTo<T>(rest, RequestType.PUT, obj);
        }

        public static T ParsePutTo<T>(RestClient rest)
        {
            return ParsePutTo<T>(rest, null);
        }

        /*
            result has status code.
         */
        public static T ParsePutTo<T>(RestClient rest, object obj, out object statuscode)
        {
            return ParseTo<T>(rest, RequestType.PUT, obj, out statuscode);
        }

        public static T ParsePutTo<T>(RestClient rest, out object statuscode)
        {
            return ParsePutTo<T>(rest, null, out statuscode);
        }
        /*
            :: end.
         */

        /*
            method parse delete : start.
         */
        /*
            result hasn't status code.
         */
        public static T ParseDeleteTo<T>(RestClient rest)
        {
            return ParseTo<T>(rest, RequestType.DELETE, null);
        }

        /*
            result has status code.
         */
        public static T ParseDeleteTo<T>(RestClient rest, out object statuscode)
        {
            return ParseTo<T>(rest, RequestType.DELETE, null, out statuscode);
        }
        /*
            :: end.
         */
    }

}