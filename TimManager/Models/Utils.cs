using System.Collections.Generic;

namespace TimManager.Models
{
    public static class Utils
    {
        public static bool Has(this string str) => !string.IsNullOrWhiteSpace(str);
        
        public static bool Has(this object str) => str != null;

        public static bool Has<T>(this ICollection<T> collection) => collection.Count > 0;
    }
}
