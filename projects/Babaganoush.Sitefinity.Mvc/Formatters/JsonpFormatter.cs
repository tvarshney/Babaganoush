using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Babaganoush.Sitefinity.Mvc.Formatters
{
    /// <summary>
    /// Custom WebAPI media type formatter for the streaming JSONP data to the API consumer
    /// http://www.west-wind.com/weblog/posts/2012/Apr/02/Creating-a-JSONP-Formatter-for-ASPNET-Web-
    /// API.
    /// </summary>
    public class JsonpFormatter : JsonMediaTypeFormatter
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public JsonpFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/javascript"));

            JsonpParameterName = "callback";
        }

        /// <summary>
        /// Name of the query string parameter to look for the jsonp function name.
        /// </summary>
        ///
        /// <value>
        /// The name of the jsonp parameter.
        /// </value>
        public string JsonpParameterName { get; set; }

        /// <summary>
        /// Captured name of the Jsonp function that the JSON call is wrapped in. Set in
        /// GetPerRequestFormatter Instance.
        /// </summary>
        private string JsonpCallbackFunction;

        /// <summary>
        /// Determine if we can write type.
        /// </summary>
        ///
        /// <param name="type">The type.</param>
        ///
        /// <returns>
        /// true if we can write type, false if not.
        /// </returns>
        public override bool CanWriteType(Type type)
        {
            return true;
        }

        /// <summary>
        /// Override this method to capture the Request object.
        /// </summary>
        ///
        /// <param name="type">.</param>
        /// <param name="request">.</param>
        /// <param name="mediaType">.</param>
        ///
        /// <returns>
        /// The per request formatter instance.
        /// </returns>
        public override MediaTypeFormatter GetPerRequestFormatterInstance(Type type, HttpRequestMessage request, MediaTypeHeaderValue mediaType)
        {
            var formatter = new JsonpFormatter()
            {
                JsonpCallbackFunction = GetJsonCallbackFunction(request)
            };

            // this doesn't work unfortunately
            //formatter.SerializerSettings = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;

            // You have to reapply any JSON.NET default serializer Customizations here    
            formatter.SerializerSettings.Converters.Add(new StringEnumConverter());
            formatter.SerializerSettings.Formatting = Formatting.Indented;

            return formatter;
        }

        /// <summary>
        /// Writes to stream asynchronous.
        /// </summary>
        ///
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <param name="stream">The stream.</param>
        /// <param name="content">The content.</param>
        /// <param name="transportContext">Context for the transport.</param>
        ///
        /// <returns>
        /// A Task.
        /// </returns>
        public override Task WriteToStreamAsync(Type type, object value,
                                        Stream stream,
                                        HttpContent content,
                                        TransportContext transportContext)
        {
            if (string.IsNullOrEmpty(JsonpCallbackFunction))
                return base.WriteToStreamAsync(type, value, stream, content, transportContext);

            StreamWriter writer = null;

            // write the pre-amble
            try
            {
                writer = new StreamWriter(stream);
                writer.Write(JsonpCallbackFunction + "(");
                writer.Flush();
            }
            catch (Exception ex)
            {
                try
                {
                    if (writer != null)
                        writer.Dispose();
                }
                catch { }

                var tcs = new TaskCompletionSource<object>();
                tcs.SetException(ex);
                return tcs.Task;
            }

            return base.WriteToStreamAsync(type, value, stream, content, transportContext)
                       .ContinueWith(innerTask =>
                       {
                           if (innerTask.Status == TaskStatus.RanToCompletion)
                           {
                               writer.Write(")");
                               writer.Flush();
                           }

                       }, TaskContinuationOptions.ExecuteSynchronously)
                        .ContinueWith(innerTask =>
                        {
                            writer.Dispose();
                            return innerTask;

                        }, TaskContinuationOptions.ExecuteSynchronously)
                        .Unwrap();
        }

        /// <summary>
        /// Retrieves the Jsonp Callback function from the query string.
        /// </summary>
        ///
        /// <param name="request">.</param>
        ///
        /// <returns>
        /// The JSON callback function.
        /// </returns>
        private string GetJsonCallbackFunction(HttpRequestMessage request)
        {
            if (request.Method != HttpMethod.Get)
                return null;

            var query = HttpUtility.ParseQueryString(request.RequestUri.Query);
            var queryVal = query[JsonpParameterName];

            if (string.IsNullOrEmpty(queryVal))
                return null;

            return queryVal;
        }
    }
}
