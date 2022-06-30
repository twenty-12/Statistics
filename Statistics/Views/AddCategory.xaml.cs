using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Statistics.Models;

namespace Statistics.Views
{
    public partial class AddCategory : ContentPage
    {
        public AddCategory()
        {
            InitializeComponent();
            BindingContext = new Categories();
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            Categories category = (Categories)BindingContext;
            App.ReportsDB.SaveCategoryAsync(category);
            Shell.Current.GoToAsync("..");
        }

        private async void OperationButton_Clicked(object sender, EventArgs e)
        {
            Categories category = (Categories)BindingContext;
            Button send = (Button)sender;
            if (category.Operations == Operations.plus)
            {
                category.Operations = Operations.minus;
                send.Text = "minus";
                send.BackgroundColor = Color.Red;
            }
            else
            {
                send.Text = "plus";
                category.Operations = Operations.plus;
                send.BackgroundColor = Color.Green;
            }
            BindingContext = category;
        }
    }
}