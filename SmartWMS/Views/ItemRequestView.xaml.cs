using SmartWMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartWMS.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemRequestView : ContentPage
    {

        #region

        private string userInput;
        public string UserInput
        {
            get => userInput;
            set
            {
                userInput = value;
                OnPropertyChanged(nameof(UserInput));
            }
        }

        private string itemBarcode;
        public string ItemBarcode
        {
            get => itemBarcode;
            set
            {
                itemBarcode = value;
                OnPropertyChanged(nameof(ItemBarcode));
            }
        }

        private string locationBarcode;
        public string LocationBarcode
        {
            get => locationBarcode;
            set
            {
                locationBarcode = value;
                OnPropertyChanged(nameof(LocationBarcode));
            }
        }

        private int count;
        public int Count
        {
            get => count;
            set
            {
                count = value;
                OnPropertyChanged(nameof(Count));
            }
        }

        #endregion

        private RequestedStock stock;
        public RequestedStock Stock
        {
            get => stock;
            set
            {
                stock = value;
                OnPropertyChanged(nameof(Stock));
            }
        }

        private int currentOperation; // 1: location 2: count


        public ItemRequestView(RequestedStock stock)
        {
            Stock = stock;

            ItemBarcode = Stock.Barcode;
            LocationBarcode = Stock.Location;
            Count = Stock.Count;

            currentOperation = 0;

            InitializeComponent();
            BindingContext = this;
        }

        private void Keyboard_Tapped(object sender, EventArgs e)
        {

        }

        private void Microphone_Tapped(object sender, EventArgs e)
        {

        }

        private void UserInput_Completed(object sender, EventArgs e)
        {
            //Task.Run(async () => { await UserInput_CompletedAsync(); }).Wait();

            UserInput_CompletedAsync();
        }

        private async void UserInput_CompletedAsync()
        {

            switch (currentOperation)
            {
                case 0: // input location barcode

                    if (GetSimpleLocation(UserInput).Equals(GetSimpleLocation(Stock.Location)))
                    {
                        UserInputEntry.Placeholder = "Enter Item Barcode";
                        UserInputEntry.Text = "";
                        UserInputEntry.Focus();
                        currentOperation = 1;
                    }
                    else
                    {
                        await DisplayAlert("Location Barcode", "You entered invalid location barcode.", "OK");

                        // alert msg
                    }

                    break;
                case 1: // input item barcode

                    if (GetSimpleItem(UserInput).Equals(GetSimpleItem(Stock.Barcode)))
                    {
                        UserInputEntry.Placeholder = "Enter Amount";
                        UserInputEntry.Text = "";

                        UserInputEntry.Focus();

                        currentOperation = 2;
                    }
                    else
                    {
                        await DisplayAlert("Item Barcode", "You entered invalid item barcode.", "OK");

                        // alert msg
                    }

                    break;
                case 2: // input amount

                    if (Stock.Count >= GetSimpleAmount(UserInput))
                    {
                        Stock.Count = Stock.Count - GetSimpleAmount(UserInput);
                        UserInputEntry.Text = "";

                        UserInputsSucceededEventArgs eventArgs = new UserInputsSucceededEventArgs();
                        eventArgs.Amount = Stock.Count;
                        OnUserInputsSucceeded(eventArgs);

                        // alert msg

                        await DisplayAlert("Stock Unit", "Stock unit is collected succesfully", "OK");

                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Item Amount", "Please enter no more than existing amount", "OK");

                        // alert msg
                    }

                    break;
            }

        }

        public event EventHandler<UserInputsSucceededEventArgs> UserInputsSucceeded;

        private void OnUserInputsSucceeded(UserInputsSucceededEventArgs e)
        {
            EventHandler<UserInputsSucceededEventArgs> handler = UserInputsSucceeded;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        

        private string GetSimpleLocation(string locationUserInput)
        {
            Regex rgx = new Regex("[^a-zA-Z1-9]");
            locationUserInput = rgx.Replace(locationUserInput, "");
            return locationUserInput;
        }

        private string GetSimpleItem(string itemUserInput)
        {
            Regex rgx = new Regex("[^0-9]");
            itemUserInput = rgx.Replace(itemUserInput, "");
            return itemUserInput;
        }

        private int GetSimpleAmount(string amountUserInput)
        {
            Regex rgx = new Regex("[^0-9]");
            amountUserInput = rgx.Replace(amountUserInput, "");
            return int.Parse(amountUserInput);
        }

        public class UserInputsSucceededEventArgs : EventArgs
        {
            public int Amount { get; set; }
        }
    }
}