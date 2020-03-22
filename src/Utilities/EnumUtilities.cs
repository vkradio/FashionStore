using System;
using System.Linq;

namespace Utilities
{
    public static class EnumUtilities
    {
        public static TMapTo MapTo<T, TMapTo>(this T thisValue)
        {
            var optionsName = Enum.GetName(typeof(T), thisValue).ToUpperInvariant();
            var button = ((TMapTo[])Enum.GetValues(typeof(TMapTo)))
                .Where(v => Enum.GetName(typeof(TMapTo), v).ToUpperInvariant() == optionsName)
                .Single();
            return button;
        }
    }
}
