using System;
using System.Linq;
using System.Collections.Generic;

namespace AdvancedDictionary.AdditionalClasses
{
    static class Extensions
    {
        public static IList<T> Clone<T>(this IEnumerable<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
}
