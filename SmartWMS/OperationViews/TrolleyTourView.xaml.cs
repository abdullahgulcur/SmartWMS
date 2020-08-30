using SmartWMS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartWMS.OperationViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrolleyTourView : ContentPage
    {

        #region

        public struct Stock
        {
            public Stock(int id, string barcode, string location, int count)
            {
                Id = id;
                Barcode = barcode;
                Location = location;
                Count = count;
            }
            public int Id { get; }
            public string Barcode { get; }
            public string Location { get; }
            public int Count { get; }
        }

        private ObservableCollection<Stock> stocks;
        public ObservableCollection<Stock> Stocks
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

            MessagingCenter.Subscribe<ISpeechToText, string>(this, "STT", (sender, args) =>
            {
                SpeechToTextFinalResultRecieved(args);
            });

            MessagingCenter.Subscribe<ISpeechToText>(this, "Final", (sender) =>
            {
                //start.IsEnabled = true;
            });

            MessagingCenter.Subscribe<IMessageSender, string>(this, "STT", (sender, args) =>
            {
                SpeechToTextFinalResultRecieved(args);
            });

            BindingContext = this;
        }

        private void SpeechToTextFinalResultRecieved(string args)
        {
            //recon.Text = args;
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

            Stocks = new ObservableCollection<Stock>();

            foreach (Models.Stock stock in StockUnitRequests)
            {
                string itemBarcode = await GetItemBarcodeAsync(stock.ItemId);
                string locationBarcode = await GetLocationBarcodeAsync(stock.StorageLocationId);


                Stocks.Add(new Stock(stock.StockId, itemBarcode, locationBarcode, stock.Count));
            }
        }

        /*
        private async Task<Item> GetItemId(string itemBarcode)
        {
            Item item = await App.StockManagementDB.ReadItemBarcodeAsync(itemBarcode);
            return item;
        }*/

        private void RequestItemsButton_Clicked(object sender, EventArgs e)
        {
            Task.Run(async () => { await RequestItemsRandomly(10); }).Wait();

            PickButton.IsVisible = true;
            LabelItemBarcode.IsVisible = true;
            LabelLocationBarcode.IsVisible = true;
            LabelId.IsVisible = true;
            LabelCount.IsVisible = true;
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
        }

        private void Microphone_Tapped(object sender, EventArgs e)
        {
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
    }
}