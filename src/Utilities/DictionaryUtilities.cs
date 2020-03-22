using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    public static class DictionaryUtilities
    {
        /// <summary>
        /// Increment dictionary counter
        /// </summary>
        /// <typeparam name="T">Type of the key</typeparam>
        /// <param name="dictionary">Dictionary</param>
        /// <param name="key">Key</param>
        public static void Inc<T>(this Dictionary<T, int> dictionary, T key)
        {
            Guard.Against.Null(dictionary, nameof(dictionary));

            if (!dictionary.ContainsKey(key))
                dictionary.Add(key, 1);
            else
                dictionary[key] += 1;
        }
    }
}
