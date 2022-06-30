using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Statistics.Models;
using Xamarin.Forms.Xaml;

namespace Statistics.Views
{
    [QueryProperty(nameof(ItemID), nameof(ItemID))]
    public partial class ReportAddingPage : ContentPage
    {
        public List<string> Categories { get; set; }
        public string ItemID
        {
            set
            {
                LoadNote(value);
            }
        }
        public ReportAddingPage()
        {
            InitializeComponent();
            BindingContext = new Report()
            {
                Date = DateTime.Now
            };
            Changed.BackgroundColor = Color.Green;

        }
        protected override async void OnAppearing()
        {
            Report r = (Report)BindingContext;
            SelectPicker.Items.Clear();

            foreach(Categories c in await App.ReportsDB.GetCategoriesAsync(r.Operation))
            {
                SelectPicker.Items.Add(c.Category);
            }

            SelectPicker.Items.Add("Добавить...");
            base.OnAppearing();
        }
        public ReportAddingPage(Operations operations)
        {
            InitializeComponent();
            BindingContext = new Report()
            {
                Date = DateTime.Now,
                Operation = operations
            };
            if(operations == Operations.minus)
            {
                Changed.BackgroundColor = Color.Red;
            }
            else
            {
                Changed.BackgroundColor = Color.Green;
            }
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            Report report = (Report)BindingContext;
            if (report.Summ != 0 && report.Category != null)
            {
                await App.ReportsDB.SaveReportAsync(report);
                await Shell.Current.GoToAsync("..");
            }
        }
        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            Report report = (Report)BindingContext;

            await App.ReportsDB.DeleteReportAsync(report);

            await Shell.Current.GoToAsync("..");
        }

        private async void OperationButton_Clicked(object sender, EventArgs e)
        {
            Report report = (Report)BindingContext;
            Button send = (Button)sender;
            if (report.Operation == Operations.plus)
            {
                report.Operation = Operations.minus;
                send.Text = "minus";
                send.BackgroundColor = Color.Red;
            }
            else
            {
                send.Text = "plus";
                report.Operation = Operations.plus;
                send.BackgroundColor = Color.Green;
            }
            BindingContext = report;
            OnAppearing();
        }
        private async void LoadNote(string value)
        {
            try
            {

                BindingContext = await App.ReportsDB.GetReportAsync(Convert.ToInt32(value));
            }
            catch { }
        }


        private void CategorySelected(object sender, EventArgs e)
        {
            Report report = (Report)BindingContext;
            Picker s = (Picker)sender;
            if ("Добавить..." == (string)s.SelectedItem)
            {
                Shell.Current.GoToAsync(nameof(AddCategory));
            }
            report.Category = (string)s.SelectedItem;
        }
    }
}