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

        public Country(string code)
        {
            this.Code = code;
        }
    }
}
