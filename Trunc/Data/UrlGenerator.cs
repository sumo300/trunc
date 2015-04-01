using System;
using System.Text;
using System.Web;

namespace Trunc.Data {
    public static class UrlGenerator {
        private const string RandomList = "0123456789abcdefghijklmnopqrstuwxyz";

        public static string GetRandomUrl(int length) {
            var sb = new StringBuilder(length);
            var random = new Random();
            for (int i = 0; i < length; i++) {
                int index = random.Next(0, RandomList.Length);
                sb.Append(RandomList[index]);
            }
            return sb.ToString();
        }
    }

    public static class UrlUtilities {
        public static string GetShortUrl(string path) {
            var url = HttpContext.Current.Request.Url;
            var builder = new UriBuilder(url.Scheme, url.Host, url.Port, path);
            return builder.ToString();
        }
    }
}
