using Ardalis.GuardClauses;
using System;
using System.Linq;

namespace Utilities
{
    public static class EnumUtilities
    {
        public static TMapTo MapTo<T, TMapTo>(this T thisValue)
            where T : notnull
            where TMapTo : notnull
        {
            Guard.Against.Null(thisValue, nameof(thisValue));

            var optionsName = Enum.GetName(typeof(T), thisValue)!.ToUpperInvariant();
            var button = ((TMapTo[])Enum
                .GetValues(typeof(TMapTo)))
                .Where(val => Enum.GetName(typeof(TMapTo), val!)!.ToUpperInvariant() == optionsName)
                .Single();
            return button;
        }
    }
}
