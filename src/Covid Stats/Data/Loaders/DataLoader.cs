using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidStats.Data.Loaders
{
    interface DataLoader
    {
        void Load();
        string GetData();
    }
}
