using System.Globalization;
using System.Linq;

namespace Trunc.Data {
    /// <summary>
    /// Uses a bijective function ala http://stackoverflow.com/questions/742013/how-to-code-a-url-shortener
    /// to generate a small string from an integral identifier.
    /// </summary>
    public static class UrlGenerator {
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyz0123456789";
        private static readonly int Base = Alphabet.Length;

        public static string Encode(long i) {
            if (i == 0) {
                return Alphabet[0].ToString(CultureInfo.InvariantCulture);
            }

            string s = string.Empty;

            while (i > 0) {
                s += Alphabet[(int) (i % Base)];
                i = i / Base;
            }

            return string.Join(string.Empty, s.Reverse());
        }

        public static int Decode(string s) {
            return s.Aggregate(0, (current, c) => (current * Base) + Alphabet.IndexOf(c));
        }
    }
}
