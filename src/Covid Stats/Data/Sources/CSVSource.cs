using CovidStats.Countries;

using Microsoft.SqlServer.Server;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidStats.Data.Sources
{
    class CSVSource : DataSource
    {
        protected string FilePath;

        public CSVSource(string path)
        {
            this.FilePath = path;
        }

        public void Load()
        {
            string[] data_rows = System.IO.File.ReadAllText(this.FilePath).Split('\n');
            for(int row_number = 0; row_number < data_rows.Length; row_number++)
            {
                // Skip header row
                if(row_number == 0) continue;

                string[] row = data_rows[row_number].Split(',');
                DateTime date_reported = DateTime.ParseExact(row[0], "yyyy-MM-dd", null);
                string country_code = row[1];

                CovidDataRow data = new CovidDataRow
                {
                    NewCases = int.Parse(row[4]),
                    TotalCases = int.Parse(row[5]),
                    NewDeaths = int.Parse(row[6]),
                    TotalDeaths = int.Parse(row[7])
                };

                CountryManager.GetCountry(country_code).AddDataRow(date_reported, data);
            }
        }
    }
}
