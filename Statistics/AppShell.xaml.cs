using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Statistics.Views;

namespace Statistics
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ReportAddingPage), typeof(ReportAddingPage));
            Routing.RegisterRoute(nameof(AddCategory), typeof(AddCategory));
        }
    }
}