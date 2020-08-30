using SmartWMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartWMS.OperationViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TablesView : ContentPage
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

        private List<Stock> stocks;
        public List<Stock> Stocks
        {
            get => stocks;
            set
            {
                stocks = value;
                OnPropertyChanged(nameof(Stocks));
            }
        }

        private List<StorageLocation> storageLocations;
        public List<StorageLocation> StorageLocations
        {
            get => storageLocations;
            set
            {
                storageLocations = value;
                OnPropertyChanged(nameof(StorageLocations));
            }
        }

        private List<Item> items;
        public List<Item> Items
        {
            get => items;
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        
        private List<Models.Stock> stockUnits;

        public List<Models.Stock> StockUnits
        {
            get => stockUnits;
            set
            {
                stockUnits = value;
                OnPropertyChanged(nameof(StockUnits));
            }
        }
        

        #endregion

        public TablesView()
        {
            ListAllStorageLocations();

            Task.Run(async () => { await SetStocks(); }).Wait();

            //SetStocks();
            InitializeComponent();
            BindingContext = this;
        }

        public async void ListAllStorageLocations()
        {
            StorageLocations = await App.StockManagementDB.GetStorageLocationsAsync();
            Items = await App.StockManagementDB.GetItemsAsync();
            StockUnits = await App.StockManagementDB.GetStockUnitsAsync();
        }

        public async Task SetStocks()
        {
            List<Models.Stock> stockUnits = await App.StockManagementDB.GetStockUnitsAsync();

            Stocks = new List<Stock>();

            foreach(Models.Stock stock in stockUnits)
            {
                string itemBarcode = await GetItemBarcodeAsync(stock.ItemId);
                string locationBarcode = await GetLocationBarcodeAsync(stock.StorageLocationId);


                Stocks.Add(new Stock(stock.StockId, itemBarcode, locationBarcode, stock.Count));
            }

            //List<Item> items1 = await App.StockManagementDB.GetItemsAsync();
            //List<StorageLocation> locations1 = await App.StockManagementDB.GetStorageLocationsAsync();
            //List<Models.Stock> stockUnits1 = await App.StockManagementDB.GetStockUnitsAsync();
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

    }
}