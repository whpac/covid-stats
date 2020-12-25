using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidStats.Data
{
    class CovidDataRow
    {
        public int NewCases = 0;
        public int TotalCases = 0;
        public int NewDeaths = 0;
        public int TotalDeaths = 0;
        public string Name = "";

        public CovidDataRow(string name = "")
        {
            this.Name = name;
        }

        public static CovidDataRow operator +(CovidDataRow a, CovidDataRow b)
        {
            CovidDataRow result = new CovidDataRow
            {
                NewCases = a.NewCases + b.NewCases,
                TotalCases = a.TotalCases + b.TotalCases,
                NewDeaths = a.NewDeaths + b.NewDeaths,
                TotalDeaths = a.TotalDeaths + b.TotalDeaths
            };

            return result;
        }
    }
}
