using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidStats.Countries
{
    static class CountryManager
    {
        private static Dictionary<string, Country> ExistingCountries = new Dictionary<string, Country>();

        /// <summary>
        /// Returns a country corresponding to give code
        /// </summary>
        /// <param name="code">Country code</param>
        /// <returns>Country</returns>
        public static Country GetCountry(string code)
        {
            if(!ExistingCountries.ContainsKey(code))
            {
                ExistingCountries[code] = new Country(code);
            }
            return ExistingCountries[code];
        }
    }
}
