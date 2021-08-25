using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace TextEmAll
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            var jsonFormatter = new JsonMediaTypeFormatter();
            GlobalConfiguration.Configuration.Services.Replace(typeof(IContentNegotiator), new JsonContentNegotiator(jsonFormatter));
        }
    }
    internal class JsonContentNegotiator : IContentNegotiator
    {
        private readonly JsonMediaTypeFormatter _jsonFormatter;

        public JsonContentNegotiator(JsonMediaTypeFormatter formatter)
        {
            _jsonFormatter = formatter;
            _jsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
        }

        public ContentNegotiationResult Negotiate(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
        {
            return new ContentNegotiationResult(_jsonFormatter, new MediaTypeHeaderValue("application/json"));
        }
    }
}
