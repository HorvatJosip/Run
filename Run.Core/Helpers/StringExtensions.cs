using System.ComponentModel;

namespace Run.Core
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str)
            => string.IsNullOrEmpty(str);

        public static bool IsNullOrWhiteSpace(this string str)
            => string.IsNullOrWhiteSpace(str);

        public static T Parse<T>(this string str, T defaultReplacement = default(T))
        {
            var val = default(T);
            try
            {
                var method = typeof(T).GetMethod("Parse", new[] { typeof(string) });
                val = (T)method.Invoke(null, new[] { str });
            }
            catch
            {
                try
                {
                    var converter = TypeDescriptor.GetConverter(typeof(T));
                    val = (T)converter.ConvertFromInvariantString(str);
                }
                catch { }
            }

            if (Equals(val, default(T)))
                return defaultReplacement;

            return val;
        }
    }
}
