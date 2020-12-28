using CovidStats.Countries;

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
    public partial class ChartForm : Form
    {
        public ChartForm()
        {
            InitializeComponent();
        }

        public void Show(Group[] groups)
        {
            string[] series = new string[groups.Length + 1];
            List<string> dates = new List<string>();
            List<int>[] data = new List<int>[groups.Length + 1];

            for(int i = 0; i < data.Length; i++) data[i] = new List<int>();
            for(int i = 0; i < groups.Length; i++) series[i+1] = groups[i].Name;
            series[0] = "Świat (w tym Chiny)";

            DateTime date = new DateTime(2020, 1, 3);
            while(date < DateTime.Now)
            {
                dates.Add(this.DateToString(date));

                int sum = 0;
                for(int i = 0; i < groups.Length; i++)
                {
                    int d = groups[i].GetDataForDate(date).TotalCases;
                    data[i+1].Add(d);
                    sum += d;
                }
                data[0].Add(sum);

                date = date.AddDays(7);
            }

            string chart_code = "|x=";
            
            for(int i = 0; i < dates.Count; i++)
            {
                if(i > 0) chart_code += ",";
                chart_code += dates[i];
            }
            chart_code += "\r\n\r\n";

            for(int i = 0; i < series.Length; i++)
            {
                chart_code += $"|y{i + 1}Title={series[i]}\r\n";
                chart_code += $"|y{i + 1}=";

                for(int j = 0; j < data[i].Count; j++)
                {
                    if(j > 0) chart_code += ",";
                    chart_code += data[i][j];
                }
                chart_code += "\r\n\r\n";
            }
            textBox1.Text = chart_code;
            this.Show();
        }

        protected string DateToString(DateTime date)
        {
            string res = date.Day + " ";
            switch(date.Month)
            {
                case 1: res += "sty"; break;
                case 2: res += "lut"; break;
                case 3: res += "mar"; break;
                case 4: res += "kwi"; break;
                case 5: res += "maj"; break;
                case 6: res += "cze"; break;
                case 7: res += "lip"; break;
                case 8: res += "sie"; break;
                case 9: res += "wrz"; break;
                case 10: res += "paź"; break;
                case 11: res += "lis"; break;
                case 12: res += "gru"; break;
            }
            if(date.DayOfYear <= 7) res += " " + date.Year;
            return res;
        }
    }
}
