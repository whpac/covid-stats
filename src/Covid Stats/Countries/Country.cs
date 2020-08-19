using CovidStats.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidStats.Countries
{
    class Country
    {
        public string Code { get; private set; }
        protected Dictionary<int, CovidDataRow> DataRows = new Dictionary<int, CovidDataRow>();

        public Country(string code)
        {
            this.Code = code;
        }

        /// <summary>
        /// Adds data row to the country
        /// </summary>
        /// <param name="day">The date of the data</param>
        /// <param name="data">Actual data</param>
        public void AddDataRow(DateTime day, CovidDataRow data)
        {
            // The first day when pandemic was registered
            DateTime first_day = new DateTime(2020, 1, 21);
            int day_of_pandemic = (int)(day - first_day).TotalDays;
            this.DataRows[day_of_pandemic] = data;
        }
    }
}
