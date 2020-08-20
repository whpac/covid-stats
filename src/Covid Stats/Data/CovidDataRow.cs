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

        public static CovidDataRow operator +(CovidDataRow a, CovidDataRow b)
        {
            CovidDataRow result = new CovidDataRow();

            result.NewCases = a.NewCases + b.NewCases;
            result.TotalCases = a.TotalCases + b.TotalCases;
            result.NewDeaths = a.NewDeaths + b.NewDeaths;
            result.TotalDeaths = a.TotalDeaths + b.TotalDeaths;

            return result;
        }
    }
}
