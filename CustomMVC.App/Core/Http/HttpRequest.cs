using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Collections.Specialized;

namespace CustomMVC.App.Core.Http
{
    public class HttpRequest
    {
        private readonly HttpListenerRequest _inner;
        public Uri? Uri => _inner.Url;
        public Uri? UriReferer => _inner.UrlReferrer;
        public string Method => _inner.HttpMethod;
        public Stream Body => _inner.InputStream;
        public NameValueCollection Headers => _inner.Headers;
        public NameValueCollection Query => _inner.QueryString;
        public string[]? Language => _inner.UserLanguages;

        public HttpRequest(HttpListenerRequest inner)
        {
            _inner = inner;
        }
    }
}
