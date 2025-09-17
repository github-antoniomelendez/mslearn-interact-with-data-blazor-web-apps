using System.Globalization;

namespace BlazingPizza.Services
{
    public static class CurrencyHelper
    {
        // Replace "es-MX" with your local culture code
        private static readonly CultureInfo culture = new CultureInfo("es-MX");

        public static string Format(decimal amount)
        {
            return string.Format(culture, "{0:C}", amount);
        }
    }
}
