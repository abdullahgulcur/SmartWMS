using SmartWMS.Models;
using SmartWMS.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private int stockIndex = 0;
        private bool isMicrophoneOpen = false;

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
            
            MessagingCenter.Instance.Subscribe<IMessageSender, string>(this, "STT", (sender, args) =>
            {
                if (App.speechView == this.GetType().Name)
                {
                    isMicrophoneOpen = false;
                    SpeechToTextFinalResultRecieved(args);
                }
            });

            BindingContext = this;
        }

        protected override void OnDisappearing()
        {
            if (!isMicrophoneOpen)
            {
                MessagingCenter.Instance.Unsubscribe<IMessageSender, string>(this, "STT");
            }

            base.OnDisappearing();
        }

        private void SpeechToTextFinalResultRecieved(string args)
        {

            string text = "Başla";

            if (LevenshteinDistance.Compute(args, text) < text.Length / 2)
            {
                PickItems();
            }

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

        private void RequestItemsButton_Clicked(object sender, EventArgs e)
        {
            Task.Run(async () => { await RequestItemsRandomly(3); }).Wait();

            RequestedStock stock = Stocks[stockIndex];

            PickButton.IsVisible = true;
        }

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

        private void Microphone_Tapped(object sender, EventArgs e)
        {
            isMicrophoneOpen = true;

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
        }
        
        
        private void HandleInputsSucceeded(object sender, UserInputsSucceededEventArgs e)
        {
            PickButton.IsVisible = false;
        }

        private void PickButton_Clicked(object sender, EventArgs e)
        {
            PickItems();
        }

        private void PickItems()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if(Stocks == null)
                    await DisplayAlert("Empty Stock Unit", "Stock unit table is empty", "OK");
                else
                {
                    ItemRequestView view = new ItemRequestView(Stocks);
                    view.UserInputsSucceeded += HandleInputsSucceeded;
                    await Navigation.PushAsync(view);
                }
            });
        }
    }
}