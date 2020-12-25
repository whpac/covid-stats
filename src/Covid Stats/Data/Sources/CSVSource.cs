using CovidStats.Countries;

using Microsoft.SqlServer.Server;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CovidStats.Data.Sources
{
    class CSVSource : DataSource
    {
        protected string Data;

        public CSVSource(string data)
        {
            this.Data = data;
        }

        public void Load()
        {
            string[] data_rows = this.Data.Split('\n');
            for(int row_number = 0; row_number < data_rows.Length; row_number++)
            {
                // Skip header row
                if(row_number == 0) continue;
                if(data_rows[row_number] == "") continue;

                string[] row = SplitByCommas(data_rows[row_number]);
                DateTime date_reported = DateTime.ParseExact(row[0], "yyyy-MM-dd", null);
                string country_code = row[1];

                CovidDataRow data = new CovidDataRow(row[2])
                {
                    NewCases = int.Parse(row[4]),
                    TotalCases = int.Parse(row[5]),
                    NewDeaths = int.Parse(row[6]),
                    TotalDeaths = int.Parse(row[7])
                };

                CountryManager.GetCountry(country_code).AddDataRow(date_reported, data);
            }
        }

        protected string[] SplitByCommas(string row)
        {
            var columns = new List<string>();
            var current_column = "";
            bool is_in_quotes = false;
            bool is_escaped = false;

            foreach(char c in row)
            {
                if(c == ',' && !is_in_quotes)
                {
                    columns.Add(current_column);
                    current_column = "";
                    continue;
                }

                
                if(is_escaped)
                {
                    is_escaped = false;
                    current_column += c;
                    continue;
                }

                if(c == '\\')
                {
                    is_escaped = true;
                    continue;
                }

                if(c == '"')
                {
                    is_in_quotes = !is_in_quotes;
                    continue;
                }

                current_column += c;
            }

            if(current_column != "") columns.Add(current_column);

            return columns.ToArray();
        }
    }
}
