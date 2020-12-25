using CovidStats.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace CovidStats.Countries
{
    public class Country
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
            // This is done to strip the time part from the index
            int day_number = ParseDayIntoNumber(day);
            this.DataRows[day_number] = data;
        }

        /// <summary>
        /// Returns data for a given date. If there is no data, returns last knows values
        /// </summary>
        /// <param name="date">Requested date</param>
        /// <returns>Data about the country</returns>
        public CovidDataRow GetDataForDate(DateTime date)
        {
            int day_number = ParseDayIntoNumber(date);
            if(this.DataRows.ContainsKey(day_number)) return this.DataRows[day_number];

            // Read last known
            var dates = this.GetAvailableDates();
            Array.Sort(dates, 0, dates.Length, new DateComparer());
            for(int i = dates.Length - 1; i >= 0; i--)
            {
                if(dates[i] < date)
                {
                    return this.DataRows[ParseDayIntoNumber(dates[i])];
                }
            }

            return new CovidDataRow(this.Code);
        }

        /// <summary>
        /// Returns array of dates for which there's data
        /// </summary>
        /// <returns>Array of dates</returns>
        public DateTime[] GetAvailableDates()
        {
            var keys = this.DataRows.Keys;
            var dates = new List<DateTime>();
            foreach(var key in keys)
            {
                dates.Add(ParseDayNumberIntoDate(key));
            }
            return dates.ToArray();
        }

        /// <summary>
        /// Strips the time part from date and calculates the number of days since Jan 1, 2020
        /// </summary>
        /// <param name="day">Date</param>
        /// <returns>Number of whole days since Jan 1, 2020</returns>
        protected int ParseDayIntoNumber(DateTime day)
        {
            DateTime first_day = new DateTime(2020, 1, 1);
            return (int)(day - first_day).TotalDays;
        }

        /// <summary>
        /// Calculates a date from the day number
        /// </summary>
        /// <param name="day_number">Day number</param>
        /// <returns>Calculated date</returns>
        protected DateTime ParseDayNumberIntoDate(int day_number)
        {
            return new DateTime(2020, 1, 1).AddDays(day_number);
        }

        class DateComparer : IComparer<DateTime>
        {
            public int Compare(DateTime a, DateTime b)
            {
                return (int)(a - b).TotalSeconds;
            }
        }
    }
}
