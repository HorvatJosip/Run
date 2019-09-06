using System.Collections.Generic;
using System.Linq;

namespace Run.Core
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
            => collection == null || collection.Count() == 0;

        public static T RandomElement<T>(this IEnumerable<T> collection)
            => collection.IsNullOrEmpty() 
                ? default(T) 
                : collection.ElementAt(Utils.Rng.Next(collection.Count()));
    }
}
