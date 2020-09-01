using SmartWMS.Models;
using SmartWMS.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SmartWMS.Views.ItemRequestView;

namespace SmartWMS.OperationViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrolleyTourView : ContentPage
    {

        #region

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

        private ObservableCollection<Models.Stock> stockUnitRequests;
        public ObservableCollection<Models.Stock> StockUnitRequests
        {
            get => stockUnitRequests;
            set
            {
                stockUnitRequests = value;
                OnPropertyChanged(nameof(StockUnitRequests));
            }
        }

        private int currentStockId;
        public int CurrentStockId
        {
            get => currentStockId;
            set
            {
                currentStockId = value;
                OnPropertyChanged(nameof(CurrentStockId));
            }
        }

        private string currentStockItemBarcode;
        public string CurrentStockItemBarcode
        {
            get => currentStockItemBarcode;
            set
            {
                currentStockItemBarcode = value;
                OnPropertyChanged(nameof(CurrentStockItemBarcode));
            }
        }

        private string currentStockLocationBarcode;
        public string CurrentStockLocationBarcode
        {
            get => currentStockLocationBarcode;
            set
            {
                currentStockLocationBarcode = value;
                OnPropertyChanged(nameof(CurrentStockLocationBarcode));
            }
        }

        private int currentStockCount;
        public int CurrentStockCount
        {
            get => currentStockCount;
            set
            {
                currentStockCount = value;
                OnPropertyChanged(nameof(CurrentStockCount));
            }
        }

        #endregion

        private ISpeechToText _speechRecongnitionInstance;
        private int currentOperation; // 1: location 2: count
        private int stockIndex = 0;

        public TrolleyTourView()
        {
            InitializeComponent();

            try
            {
                _speechRecongnitionInstance = DependencyService.Get<ISpeechToText>();
            }
            catch (Exception ex)
            {
                //recon.Text = ex.Message;
            }
        
            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            MessagingCenter.Subscribe<IMessageSender, string>(this, "STT", (sender, args) =>
            {
                if (App.speechView == this.GetType().Name)
                {
                    SpeechToTextFinalResultRecieved(args);
                }

            });

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            //MessagingCenter.Unsubscribe<IMessageSender, string>(this, "STT");
            base.OnDisappearing();
        }

        private void SpeechToTextFinalResultRecieved(string args)
        {
            //recon.Text = args;

            /*
            Stock stock = Stocks[stockIndex];

            // if current operation is going location
            if(currentOperation == 0)
            {
                if (GetSimpleLocation(args).Equals(GetSimpleLocation(stock.Location)))
                {
                    GoLocationLabel.Text = "PICK " + stock.Count + " OF ITEM " + stock.Barcode;
                    currentOperation = 1;
                }
            }
            else // current operation is picking
            {
                if(stock.Count >= GetSimpleAmount(args))
                {
                    stock.Count = stock.Count - GetSimpleAmount(args);

                    GoLocationLabel.Text = "GO TO LOCATION " + stock.Location;
                    stockIndex++;
                    currentOperation = 0;
                }

            }
            */
        }

        private async Task RequestItemsRandomly(int itemCount)
        {
            List<Models.Stock> stockUnits = await App.StockManagementDB.GetStockUnitsAsync();

            // Empty stocks are deleted here
            for(int i = stockUnits.Count - 1; i >= 0; i--)
            {
                if (stockUnits[i].Count == 0)
                    stockUnits.Remove(stockUnits[i]);
            }

            StockUnitRequests = new ObservableCollection<Models.Stock>();

            while(itemCount > 0 && stockUnits.Count > 0)
            {
                Models.Stock randomStockUnit = stockUnits[App.GetRandomNumber(0, stockUnits.Count)];

                if(!StockUnitRequests.Contains(randomStockUnit) && randomStockUnit.Count != 0)
                {
                    StockUnitRequests.Add(randomStockUnit);
                    stockUnits.Remove(randomStockUnit);
                    itemCount--;
                }
            }

            foreach (Models.Stock stock in StockUnitRequests)
            {
                stock.Count = App.GetRandomNumber(1, stock.Count + 1);
            }

            Stocks = new ObservableCollection<RequestedStock>();

            foreach (Models.Stock stock in StockUnitRequests)
            {
                string itemBarcode = await GetItemBarcodeAsync(stock.ItemId);
                string locationBarcode = await GetLocationBarcodeAsync(stock.StorageLocationId);


                Stocks.Add(new RequestedStock { Id = stock.StockId, Barcode = itemBarcode,
                    Location = locationBarcode, Count = stock.Count });
            }
        }

        /*
        private async Task<Item> GetItemId(string itemBarcode)
        {
            Item item = await App.StockManagementDB.ReadItemBarcodeAsync(itemBarcode);
            return item;
        }*/

        private string GetSimpleLocation(string locationUserInput)
        {
            Regex rgx = new Regex("[^a-zA-Z1-9]");
            locationUserInput = rgx.Replace(locationUserInput, "");
            return locationUserInput;
        }

        private int GetSimpleAmount(string amountUserInput)
        {
            Regex rgx = new Regex("[^0-9]");
            amountUserInput = rgx.Replace(amountUserInput, "");
            return int.Parse(amountUserInput);
        }

        private void RequestItemsButton_Clicked(object sender, EventArgs e)
        {
            currentOperation = 0;

            Task.Run(async () => { await RequestItemsRandomly(10); }).Wait();

            RequestedStock stock = Stocks[stockIndex];

            PickButton.IsVisible = true;
            //Stocks.Remove(stock);

            /*
            GoLocationLabel.IsVisible = true;
            GoLocationLabel.Text = "GO TO LOCATION " + stock.Location;
            */

            /*
            PickButton.IsVisible = true;
            LabelItemBarcode.IsVisible = true;
            LabelLocationBarcode.IsVisible = true;
            LabelId.IsVisible = true;
            LabelCount.IsVisible = true;
            */

        }

        /*
        public async Task SetStocks()
        {
            List<Models.Stock> stockUnits = await App.StockManagementDB.GetStockUnitsAsync();

            Stocks = new ObservableCollection<Stock>();

            foreach (Models.Stock stock in stockUnits)
            {
                string itemBarcode = await GetItemBarcodeAsync(stock.ItemId);
                string locationBarcode = await GetLocationBarcodeAsync(stock.StorageLocationId);


                Stocks.Add(new Stock(stock.StockId, itemBarcode, locationBarcode, stock.Count));
            }

            //List<Item> items1 = await App.StockManagementDB.GetItemsAsync();
            //List<StorageLocation> locations1 = await App.StockManagementDB.GetStorageLocationsAsync();
            //List<Models.Stock> stockUnits1 = await App.StockManagementDB.GetStockUnitsAsync();
        }*/

        private async Task<string> GetItemBarcodeAsync(int itemId)
        {
            Item item = await App.StockManagementDB.ReadItemAsync(itemId);
            return item.ItemBarcode;
        }

        private async Task<string> GetLocationBarcodeAsync(int locationId)
        {
            StorageLocation location = await App.StockManagementDB.ReadStorageLocationAsync(locationId);
            return location.StorageLocationBarcode;
        }

        /*
        private void PickButton_Clicked(object sender, EventArgs e)
        {
            Stock stock = Stocks[0];
            Stocks.Remove(stock);
            
            LabelCurrentStockId.IsVisible = true;
            LabelCurrentStockItemBarcode.IsVisible = true;
            LabelCurrentStockLocationBarcode.IsVisible = true;
            LabelCurrentStockCount.IsVisible = true;
            
            CurrentStockId = stock.Id;
            CurrentStockItemBarcode = stock.Barcode;
            CurrentStockLocationBarcode = stock.Location;
            CurrentStockCount = stock.Count;
        }*/

        private void Microphone_Tapped(object sender, EventArgs e)
        {
            App.speechView = this.GetType().Name;
            StartRecording();
        }

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

            if (Device.RuntimePlatform == Device.iOS)
            {
                //start.IsEnabled = false;
            }
        }

        private void HandleInputsSucceeded(object sender, UserInputsSucceededEventArgs e)
        {
            Stocks[stockIndex].Count = e.Amount;
            stockIndex++;
        }

        private void PickButton_Clicked(object sender, EventArgs e)
        {
            ItemRequestView view = new ItemRequestView(Stocks[stockIndex]);
            view.UserInputsSucceeded += HandleInputsSucceeded;
            Navigation.PushAsync(view);
        }
    }
}