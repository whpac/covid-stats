using CovidStats.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidStats.Countries
{
    class Group
    {
        public readonly string Name;
        protected GroupDataInterpolation DataInterpolation;
        protected HashSet<Country> Countries = new HashSet<Country>();

        public Group(string name, GroupDataInterpolation interpolation = GroupDataInterpolation.LAST_KNOWN)
        {
            this.Name = name;
            this.DataInterpolation = interpolation;
        }

        public void AddCountry(Country country)
        {
            this.Countries.Add(country);
        }

        public void AddCountry(Country[] countries)
        {
            foreach(var country in countries) this.Countries.Add(country);
        }

        public CovidDataRow GetDataForDate(DateTime date)
        {
            var joint_data = new CovidDataRow();
            foreach(var country in this.Countries)
            {
                joint_data += country.GetDataForDate(date);
            }
            return joint_data;
        }

        public DateTime[] GetAvailableDates()
        {
            if(this.Countries.Count == 0) return new DateTime[] { };

            HashSet<DateTime> dates = new HashSet<DateTime>(this.Countries.First().GetAvailableDates());
            
            if(this.DataInterpolation == GroupDataInterpolation.NONE)
            {
                foreach(var country in this.Countries)
                {
                    dates.IntersectWith(country.GetAvailableDates());
                }
            }
            else
            {
                foreach(var country in this.Countries)
                {
                    dates.UnionWith(country.GetAvailableDates());
                }
            }
            return dates.ToArray();
        }
    }

    enum GroupDataInterpolation
    {
        NONE,
        LAST_KNOWN
    }
}
