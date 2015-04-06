using System;
using System.Web;

namespace Trunc.Data
{
    public static class UrlUtilities
    {
        public static string ToAbsoluteUrl(this string relativeUrl)
        {
            if (string.IsNullOrWhiteSpace(relativeUrl))
            {
                return relativeUrl;
            }

            if (HttpContext.Current == null)
            {
                return relativeUrl;
            }

            if (relativeUrl.StartsWith("/"))
            {
                relativeUrl = relativeUrl.Insert(0, "~");
            }

            if (!relativeUrl.StartsWith("~/"))
            {
                relativeUrl = relativeUrl.Insert(0, "~/");
            }

            Uri url = HttpContext.Current.Request.Url;
            string port = url.Port != 80 ? (":" + url.Port) : string.Empty;

            return string.Format("{0}://{1}{2}{3}", url.Scheme, url.Host, port, VirtualPathUtility.ToAbsolute(relativeUrl));
        }
    }
}