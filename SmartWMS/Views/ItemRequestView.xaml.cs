using SmartWMS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

        private ObservableCollection<RequestedStock> stocks;
        public ObservableCollection<RequestedStock> Stocks
        {
            get => stocks;
            set
            {
                stocks = value;
                OnPropertyChanged(nameof(Stocks));
            }
        }

        private bool isMicrophoneOpen = false;
        private int currentOperation; // 1: location 2: count
        //private int currentIndex = 0;

        public ItemRequestView(ObservableCollection<RequestedStock> stocks)
        {
            Stocks = stocks;

            Stock = Stocks[0];

            ItemBarcode = Stock.Barcode;
            LocationBarcode = Stock.Location;
            Count = Stock.Count;

            currentOperation = 0;

            InitializeComponent();
            BindingContext = this;

            try
            {
                _speechRecongnitionInstance = DependencyService.Get<ISpeechToText>();
            }
            catch (Exception ex)
            {
                //recon.Text = ex.Message;
            }

            MessagingCenter.Instance.Subscribe<IMessageSender, string>(this, "STT", (sender, args) =>
            {
                //if (App.speechView == this.GetType().Name)
                //{

                //}
                isMicrophoneOpen = false;
                SpeechToTextFinalResultRecieved(args);
            });
        }

        private async void SpeechToTextFinalResultRecieved(string UserInput)
        {
            UserInput = UserInput.ToUpper(new CultureInfo("tr-TR", false));

            UserInput = UserInput.Replace("BİR", "1");
            UserInput = UserInput.Replace("İKİ", "2");
            UserInput = UserInput.Replace("ÜÇ", "3");
            UserInput = UserInput.Replace("DÖRT", "4");
            UserInput = UserInput.Replace("BEŞ", "5");
            UserInput = UserInput.Replace("ALTI", "6");
            UserInput = UserInput.Replace("YEDİ", "7");
            UserInput = UserInput.Replace("SEKİZ", "8");
            UserInput = UserInput.Replace("DOKUZ", "9");
            UserInput = UserInput.Replace("SIFIR", "0");

            string command = "OPERASYONU BİTİR";
            bool operationEnded = false;

            if (LevenshteinDistance.Compute(UserInput, command) < command.Length / 3)
            {
                operationEnded = true;
                //UserInputsSucceededEventArgs eventArgs = new UserInputsSucceededEventArgs();
                //OnUserInputsSucceeded(eventArgs);

                await DisplayAlert("Operation", "Operation is ended", "OK");
                await Navigation.PopAsync();
                MessagingCenter.Instance.Unsubscribe<IMessageSender, string>(this, "STT");

            }

            switch (currentOperation)
            {
                case 0: // input location barcode

                    if (string.Equals(GetSimpleLocation(UserInput), GetSimpleLocation(Stock.Location)))
                    {
                        UserInputEntry.Placeholder = "Enter Item Barcode";
                        UserInputEntry.Text = "";
                        currentOperation = 1;
                    }
                    else
                    {
                        if(!operationEnded)
                            await DisplayAlert("Location Barcode", "You entered invalid location barcode.", "OK");
                    }

                    break;
                case 1: // input item barcode

                    if (GetSimpleItem(UserInput).Equals(GetSimpleItem(Stock.Barcode)))
                    {
                        UserInputEntry.Placeholder = "Enter Amount";
                        UserInputEntry.Text = "";
                        currentOperation = 2;
                    }
                    else
                    {
                        if (!operationEnded)
                            await DisplayAlert("Item Barcode", "You entered invalid item barcode.", "OK");
                    }

                    break;
                case 2: // input amount

                    if (!IsEmptyAmountString(UserInput))
                    {
                        if (Stock.Count >= GetSimpleAmount(UserInput))
                        {
                            UserInputEntry.Text = "";

                            

                            UserInputEntry.Placeholder = "Enter Location";

                            Stocks.Remove(Stocks[0]);

                            if(Stocks.Count != 0)
                                Stock = Stocks[0];

                            currentOperation = 0;

                            UpdateRequestedItemProperties();

                            await DisplayAlert("Stock Unit", "Stock unit is collected succesfully", "OK");
                            
                            if(Stocks.Count == 0)
                            {
                                UserInputsSucceededEventArgs eventArgs = new UserInputsSucceededEventArgs();
                                OnUserInputsSucceeded(eventArgs);

                                await DisplayAlert("Stock Unit", "All Stock units are collected succesfully", "OK");
                                await Navigation.PopAsync();
                                MessagingCenter.Instance.Unsubscribe<IMessageSender, string>(this, "STT");

                            }
                        }
                        else
                        {
                            await DisplayAlert("Item Amount", "Please enter no more than existing amount", "OK");
                        }
                    }
                    else
                    {
                        if (!operationEnded)
                            await DisplayAlert("Invalid Input", "You entered invalid amount", "OK");
                    }

                    break;
            }
        }

        protected override void OnDisappearing()
        {
            if (!isMicrophoneOpen)
            {
                MessagingCenter.Instance.Unsubscribe<IMessageSender, string>(this, "STT");
            }
            base.OnDisappearing();
        }

        private void UpdateRequestedItemProperties()
        {
            if(Stocks.Count != 0)
            {
                ItemBarcode = Stock.Barcode;
                LocationBarcode = Stock.Location;
                Count = Stock.Count;
            }
        }

        private void Keyboard_Tapped(object sender, EventArgs e)
        {

        }

        private void Microphone_Tapped(object sender, EventArgs e)
        {
            isMicrophoneOpen = true;
            StartRecording();
        }

        private void UserInput_Completed(object sender, EventArgs e)
        {
            UserInput_CompletedAsync();
        }

        private async void UserInput_CompletedAsync()
        {
            UserInput = UserInput.ToUpper(new CultureInfo("tr-TR", false));

            UserInput = UserInput.Replace("BİR", "1");
            UserInput = UserInput.Replace("İKİ", "2");
            UserInput = UserInput.Replace("ÜÇ", "3");
            UserInput = UserInput.Replace("DÖRT", "4");
            UserInput = UserInput.Replace("BEŞ", "5");
            UserInput = UserInput.Replace("ALTI", "6");
            UserInput = UserInput.Replace("YEDİ", "7");
            UserInput = UserInput.Replace("SEKİZ", "8");
            UserInput = UserInput.Replace("DOKUZ", "9");
            UserInput = UserInput.Replace("SIFIR", "0");

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
                    }

                    break;
                case 2: // input amount

                    if (!IsEmptyAmountString(UserInput))
                    {
                        if (Stock.Count >= GetSimpleAmount(UserInput))
                        {
                            UserInputEntry.Text = "";

                            UserInputsSucceededEventArgs eventArgs = new UserInputsSucceededEventArgs();
                            OnUserInputsSucceeded(eventArgs);

                            UserInputEntry.Placeholder = "Enter Location";


                            Stocks.Remove(Stocks[0]);

                            if (Stocks.Count != 0)
                                Stock = Stocks[0];

                            currentOperation = 0;

                            UpdateRequestedItemProperties();

                            await DisplayAlert("Stock Unit", "Stock unit is collected succesfully", "OK");

                            if (Stocks.Count == 0)
                            {
                                await DisplayAlert("Stock Unit", "All Stock units are collected succesfully", "OK");
                                await Navigation.PopAsync();
                                MessagingCenter.Instance.Unsubscribe<IMessageSender, string>(this, "STT");

                            }
                        }
                        else
                        {
                            await DisplayAlert("Item Amount", "Please enter no more than existing amount", "OK");

                        }
                    }
                    else
                    {
                        await DisplayAlert("Invalid Input", "You entered invalid amount", "OK");
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

        private bool IsEmptyAmountString(string amountUserInput)
        {
            Regex rgx = new Regex("[^0-9]");
            amountUserInput = rgx.Replace(amountUserInput, "");

            if (String.IsNullOrEmpty(amountUserInput))
                return true;
            else
                return false;
        }

        private int GetSimpleAmount(string amountUserInput)
        {
            Regex rgx = new Regex("[^0-9]");
            amountUserInput = rgx.Replace(amountUserInput, "");
            return int.Parse(amountUserInput);
        }
        
        public class UserInputsSucceededEventArgs : EventArgs
        {
        }
        
        private ISpeechToText _speechRecongnitionInstance;


        private void StartRecording()
        {
            try
            {
                _speechRecongnitionInstance.StartSpeechToText();
            }
            catch (Exception ex)
            {
                //recon.Text = ex.Message;
            }

        }
    }
}