using CovidStats.Countries;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidStats.Data.Sources
{
    class GroupSource : DataSource
    {
        protected string FilePath;
        public Group[] Groups { get; protected set; }

        public GroupSource(string path)
        {
            if(path.StartsWith("\"") && path.EndsWith("\"")) path = path.Substring(1, path.Length - 2);
            this.FilePath = path;
        }

        public void Load()
        {
            var file_content = System.IO.File.ReadAllText(this.FilePath);
            var lines = file_content.Split('\n');

            var group_list = new List<Group>();
            Group current_group = null;

            for(int i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Trim();
                if(line == "") continue;
                if(line.StartsWith("#")) continue;

                if(line.StartsWith(":"))
                {
                    current_group = new Group(line.Substring(1));
                    group_list.Add(current_group);
                    continue;
                }

                current_group.AddCountry(CountryManager.GetCountry(line));
            }

            this.Groups = group_list.ToArray();
        }
    }
}
