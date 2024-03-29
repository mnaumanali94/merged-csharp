/*
 * CustomHeaderSignature.Standard
 *
 * This file was automatically generated by APIMATIC v2.0 ( https://apimatic.io ).
 */

using Merged.Standard.Utilities;
using Merged.Standard.Http.Client;
using Merged.Standard.Http.Response;
using Merged.Standard.Exceptions;

namespace Merged.Standard
{
    public class BaseController
    {
        protected ArrayDeserialization ArrayDeserializationFormat = ArrayDeserialization.Indexed;
        protected static char ParameterSeparator = '&';

        /// <summary>
        /// Configuration instance
        /// </summary>
        protected Configuration config;
        
        /// <summary>
        /// HttpCallBack instance
        /// </summary>
        protected HttpCallBack HttpCallBack { get; }
        
        /// <summary>
        /// Contructor to initialize the controller with the specified configuration
        /// </summary>
        /// <param name="config">Configuration for the API</param>
        protected BaseController(Configuration config)
        {
            this.config = config;
        }

        /// <summary>
        /// Contructor to initialize the controller with the specified configuration and HTTP callback
        /// </summary>
        /// <param name="config">Configuration for the API</param>
        /// <param name="httpCallBack">HTTP callback to catch before/after HTTP request/response events</param>
        protected BaseController(Configuration config, HttpCallBack httpCallBack)
        {
            this.config = config;
            this.HttpCallBack = httpCallBack;
        }
        /// <summary>
        /// Get default HTTP client instance
        /// </summary>
        protected IHttpClient GetClientInstance()
        {
            return config.HttpClient;
        }

        /// <summary>
        /// Validates the response against HTTP errors defined at the API level
        /// </summary>
        /// <param name="_response">The response recieved</param>
        /// <param name="_context">Context of the request and the recieved response</param>
        protected void ValidateResponse(HttpResponse _response, HttpContext _context)
        {
            if ((_response.StatusCode < 200) || (_response.StatusCode > 208)) //[200,208] = HTTP OK
            {
                throw new APIException(@"HTTP Response Not OK", _context);
            }

        }
    }
} 