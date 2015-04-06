using System.Web;

namespace Trunc.Data {
    public static class UrlUtilities {
        public static string GetShortUrl(string path) {
            var url = HttpContext.Current.Request.Url;
            string port = null;
            if (url.Port != 80 || url.Port != 443)
            {
                port = ":" + url.Port;
            }

            return string.Format("{0}://{1}{2}/{3}", url.Scheme, url.Host, port, path);
        }
    }
}