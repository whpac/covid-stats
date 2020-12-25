using CovidStats.Countries;
using CovidStats.Data;
using CovidStats.Data.Loaders;
using CovidStats.Data.Sources;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CovidStats
{
    public partial class Form1 : Form
    {
        private Group[] Groups;

        public Form1()
        {
            InitializeComponent();
            this.Groups = new Group[] { };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataLoader loader;
            DataSource source;
            GroupSource group_source;
            try
            {
                loader = new NetworkDownloader(textBox1.Text);
                loader.Load();

                source = new CSVSource(loader.GetData());
                source.Load();

                group_source = new GroupSource(textBox2.Text);
                group_source.Load();
            }catch(Exception ex)
            {
                MessageBox.Show("Unable to download data: " + ex.Message);
                return;
            }

            listView1.Items.Clear();
            this.Groups = group_source.Groups;
            CovidDataRow total = new CovidDataRow("Total");
            foreach(var group in group_source.Groups)
            {
                var last_date = dateTimePicker1.Value;
                var data = group.GetDataForDate(last_date);
                this.DisplayData(group.Name, data);
                
                total += data;
            }
            this.DisplayData("Total:", total);
        }

        private void DisplayData(string code, CovidDataRow data)
        {
            var listItem = new ListViewItem(code);
            listItem.SubItems.Add(data.TotalCases.ToString());
            listItem.SubItems.Add(data.TotalDeaths.ToString());
            listView1.Items.Add(listItem);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var cf = new ChartForm();
            cf.Show(this.Groups);
        }
    }
}
