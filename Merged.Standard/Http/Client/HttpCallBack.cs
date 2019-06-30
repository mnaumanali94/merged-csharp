using Merged.Standard.Http.Request;
using Merged.Standard.Http.Response;

namespace Merged.Standard.Http.Client
{
    public sealed class HttpCallBack
    {
        public HttpRequest Request { get; private set; }

        public HttpResponse Response { get; private set; }

        public void OnBeforeHttpRequestEventHandler(IHttpClient source, HttpRequest request)
        {
            this.Request = request;
        }

        public void OnAfterHttpResponseEventHandler(IHttpClient source, HttpResponse response)
        {
            this.Response = response;
        }
    }
}