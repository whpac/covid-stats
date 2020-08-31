using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CovidStats.Data.Loaders
{
    class NetworkDownloader : DataLoader
    {
        protected string Url;
        protected string Data;

        public NetworkDownloader(string url)
        {
            this.Url = url;
        }

        public void Load()
        {
            using(var client = new WebClient())
            {
                var content = client.DownloadData(this.Url);
                this.Data = Encoding.UTF8.GetString(content, 0, content.Length);
            }
        }

        public string GetData() => this.Data;
    }
}
