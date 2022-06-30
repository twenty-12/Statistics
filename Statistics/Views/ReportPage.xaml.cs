using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Statistics.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace Statistics.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportPage : ContentPage
    {
        public ReportPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            double summ = 0;
            foreach (Report r in await App.ReportsDB.GetReportsAsync())
            {
                if(r.Operation == Operations.plus)
                {
                    summ += r.Summ;
                }
                else
                {
                    summ -= r.Summ;
                }
            }
            plusCollectionView.ItemsSource = await App.ReportsDB.GetReportsAsync(Operations.plus);
            minusCollectionView.ItemsSource = await App.ReportsDB.GetReportsAsync(Operations.minus);
            Balance.Text = Convert.ToString(summ);
            base.OnAppearing();
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ReportAddingPage));
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Report report = (Report)e.CurrentSelection.FirstOrDefault();

                await Shell.Current.GoToAsync($"{nameof(ReportAddingPage)}?{nameof(ReportAddingPage.ItemID)}={report.Id.ToString()}");
            }
        }

        private async void OrderButton_Clicked(object sender, EventArgs e)
        {
            OrderButton.TextColor = Color.Black;
            OrderButton.BackgroundColor = Color.Gray;
            DateButton.TextColor = Color.Black;
            DateButton.BackgroundColor = Color.Gray;
            SumButton.TextColor = Color.Black;
            SumButton.BackgroundColor = Color.Gray;
            Button button = (Button)sender;
            button.BackgroundColor = Color.Green;
            button.TextColor = Color.White;
            plusCollectionView.ItemsSource = await App.ReportsDB.GetReportsAsync(Operations.plus, button.Text);
            minusCollectionView.ItemsSource = await App.ReportsDB.GetReportsAsync(Operations.minus, button.Text);

        }
    }
}
