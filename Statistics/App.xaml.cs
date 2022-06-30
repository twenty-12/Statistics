using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Statistics.Data;
using System.IO;

namespace Statistics
{
    public partial class App : Application
    {
        static ReportDB reportsDB;

        public static ReportDB ReportsDB
        {
            get
            {
                if (reportsDB == null)
                {
                    reportsDB = new ReportDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "StatisticDatabase.db3"));
                }
                return reportsDB;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }


    }
}
