using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SmartWMS.Models;
using SmartWMS.Repository;
using SQLite;
using SQLitePCL;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartWMS
{
    public partial class App : Application
    {

        private static StockManagementDatabase stockManagementDB;
        public static StockManagementDatabase StockManagementDB
        {
            get
            {
                if (stockManagementDB == null)
                {
                    stockManagementDB= new StockManagementDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "User.db3"));
                }
                return stockManagementDB;
            }
        }
        
        private static UserDatabase userDB;
        public static UserDatabase UserDB
        {
            get
            {
                if(userDB == null)
                {
                    userDB = new UserDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "User.db3"));
                }
                return userDB;
            }
        }

        public static string speechView;
        
        public App()
        {
            //DropAllTables();

            //Task.Run(async () => { await AddSomeLocation(); }).Wait();
            //Task.Run(async () => { await AddSomeItem(); }).Wait();
            //Task.Run(async () => { await AddSomeStock(); }).Wait();

            //DeleteSomeRows();

            //AddSomeLocation();
            //AddSomeItem();


            //ForDebug();

            InitializeComponent();

            MainPage = new NavigationPage(new MainMenuView());
            //MainPage = new NavigationPage(new Views.ItemRequestView());

        }

        private void DropAllTables()
        {
            //StockManagementDB.DropStorageLocationTable();
            //StockManagementDB.DropItemTable();
            StockManagementDB.DropStockUnitTable();

        }

        private async void DeleteSomeRows()
        {
            //StorageLocation loc = await StorageLocationDB.ReadStorageLocationAsync(46);
            //await StorageLocationDB.DeleteStorageLocationAsync(loc);

            StorageLocation loc = await StockManagementDB.ReadStorageLocationBarcodeAsync("54-0A5-003-03");
            await StockManagementDB.DeleteStorageLocationAsync(loc);

            /*
            loc = await StockManagementDB.ReadStorageLocationBarcodeAsync("54-0A5-003-02");
            await StockManagementDB.DeleteStorageLocationAsync(loc);

            loc = await StockManagementDB.ReadStorageLocationBarcodeAsync("54-0A5-003-03");
            await StockManagementDB.DeleteStorageLocationAsync(loc);
            */
            List<StorageLocation> locs = await StockManagementDB.GetStorageLocationsAsync();
        }

        public async Task AddSomeLocation()
        {
            List<StorageLocation> locations = new List<StorageLocation>
            {
                new StorageLocation() { StorageLocationBarcode = "54-0A1-001-01" },
                new StorageLocation() { StorageLocationBarcode = "54-0A1-001-02" },
                new StorageLocation() { StorageLocationBarcode = "54-0A1-001-03" },
                new StorageLocation() { StorageLocationBarcode = "54-0A1-002-01" },
                new StorageLocation() { StorageLocationBarcode = "54-0A1-002-02" },
                new StorageLocation() { StorageLocationBarcode = "54-0A1-002-03" },
                new StorageLocation() { StorageLocationBarcode = "54-0A1-003-01" },
                new StorageLocation() { StorageLocationBarcode = "54-0A1-003-02" },
                new StorageLocation() { StorageLocationBarcode = "54-0A1-003-03" },
                
                new StorageLocation() { StorageLocationBarcode = "54-0A2-001-01" },
                new StorageLocation() { StorageLocationBarcode = "54-0A2-001-02" },
                new StorageLocation() { StorageLocationBarcode = "54-0A2-001-03" },
                new StorageLocation() { StorageLocationBarcode = "54-0A2-002-01" },
                new StorageLocation() { StorageLocationBarcode = "54-0A2-002-02" },
                new StorageLocation() { StorageLocationBarcode = "54-0A2-002-03" },
                new StorageLocation() { StorageLocationBarcode = "54-0A2-003-01" },
                new StorageLocation() { StorageLocationBarcode = "54-0A2-003-02" },
                new StorageLocation() { StorageLocationBarcode = "54-0A2-003-03" },
                
                new StorageLocation() { StorageLocationBarcode = "54-0A3-001-01" },
                new StorageLocation() { StorageLocationBarcode = "54-0A3-001-02" },
                new StorageLocation() { StorageLocationBarcode = "54-0A3-001-03" },
                new StorageLocation() { StorageLocationBarcode = "54-0A3-002-01" },
                new StorageLocation() { StorageLocationBarcode = "54-0A3-002-02" },
                new StorageLocation() { StorageLocationBarcode = "54-0A3-002-03" },
                new StorageLocation() { StorageLocationBarcode = "54-0A3-003-01" },
                new StorageLocation() { StorageLocationBarcode = "54-0A3-003-02" },
                new StorageLocation() { StorageLocationBarcode = "54-0A3-003-03" },

                new StorageLocation() { StorageLocationBarcode = "54-0A4-001-01" },
                new StorageLocation() { StorageLocationBarcode = "54-0A4-001-02" },
                new StorageLocation() { StorageLocationBarcode = "54-0A4-001-03" },
                new StorageLocation() { StorageLocationBarcode = "54-0A4-002-01" },
                new StorageLocation() { StorageLocationBarcode = "54-0A4-002-02" },
                new StorageLocation() { StorageLocationBarcode = "54-0A4-002-03" },
                new StorageLocation() { StorageLocationBarcode = "54-0A4-003-01" },
                new StorageLocation() { StorageLocationBarcode = "54-0A4-003-02" },
                new StorageLocation() { StorageLocationBarcode = "54-0A4-003-03" },

                new StorageLocation() { StorageLocationBarcode = "54-0A5-001-01" },
                new StorageLocation() { StorageLocationBarcode = "54-0A5-001-02" },
                new StorageLocation() { StorageLocationBarcode = "54-0A5-001-03" },
                new StorageLocation() { StorageLocationBarcode = "54-0A5-002-01" },
                new StorageLocation() { StorageLocationBarcode = "54-0A5-002-02" },
                new StorageLocation() { StorageLocationBarcode = "54-0A5-002-03" },
                new StorageLocation() { StorageLocationBarcode = "54-0A5-003-01" },
                new StorageLocation() { StorageLocationBarcode = "54-0A5-003-02" },
                new StorageLocation() { StorageLocationBarcode = "54-0A5-003-03" }
                
            };

            foreach (var location in locations)
            {
                await StockManagementDB.SaveUniqueLocationAsync(location);
            }
        }

        public async Task AddSomeItem()
        {
            List<Item> items = new List<Item>
            {
                new Item { ItemBarcode = "868138123742", ItemName = "SWT,ZARS" },
                new Item { ItemBarcode = "868138123759", ItemName = "SWT,ZARS" },
                new Item { ItemBarcode = "868138123793", ItemName = "SWT,ZARS" },
                new Item { ItemBarcode = "868138123809", ItemName = "SWT,ZARS" },
                new Item { ItemBarcode = "868138123773", ItemName = "SWT,ZARS" },
                new Item { ItemBarcode = "868138123729", ItemName = "SWT,ZARS" },
                new Item { ItemBarcode = "868138123712", ItemName = "SWT,ZARS" },
                new Item { ItemBarcode = "868138123743", ItemName = "SWT,ZARS" },
                new Item { ItemBarcode = "868138123750", ItemName = "SWT,ZARS" },
                new Item { ItemBarcode = "868138123728", ItemName = "ŞRT,FILLO-0S" },
                new Item { ItemBarcode = "299997123396", ItemName = "PNT,ARS / CRP-NAVY" },
                new Item { ItemBarcode = "299997123433", ItemName = "PNT,ARS / FGP-BEIGE" },
                new Item { ItemBarcode = "299997123457", ItemName = "PNT,ARS / JTK-ANTHRACITE" },
                new Item { ItemBarcode = "299997123518", ItemName = "SWT,FORGAN / CVP-GREEN" },
                new Item { ItemBarcode = "299997123833", ItemName = "UK.BDY,MASLAK / Q0G-NEON GREEN" },
                new Item { ItemBarcode = "299997123600", ItemName = "UK.ELB,KANSU / D6Z-DARK YELLOW" },
                new Item { ItemBarcode = "299997123697", ItemName = "TYT,BRONZ-A-20S / CT3-GREY MELANGE" },
                new Item { ItemBarcode = "299997123703", ItemName = "TYT,BRONZ-A-20S / FTG-PINK" },
                new Item { ItemBarcode = "299997123633", ItemName = "UK.TSH,BS.STARWA / JYX-BRILLIANT WH" },
                new Item { ItemBarcode = "299997123626", ItemName = "UK.TSH,BS.STARWA / JYX-BRILLIANT WH" },
                new Item { ItemBarcode = "868138123132", ItemName = "UK.TSH,BS.STARWARS-3" },
                new Item { ItemBarcode = "868138123034", ItemName = "PNT,SLIM-MOM-MID-L-YD" },
                new Item { ItemBarcode = "868138123027", ItemName = "PNT,SLIM-MOM-MID-L-YD" },
                new Item { ItemBarcode = "868138123065", ItemName = "PNT,SLIM-MOM-MID-L-YD" },
                new Item { ItemBarcode = "868138123058", ItemName = "PNT,SLIM-MOM-MID-L-YD" },
                new Item { ItemBarcode = "299996123735", ItemName = "PNT,MOM-ANTRACID / GRD-GREY RODEO" },
                new Item { ItemBarcode = "299996123161", ItemName = "UK.ELB,BUZY-0W / MHS-BLACK JACQUA" },
                new Item { ItemBarcode = "299996123154", ItemName = "UK.ELB,BUZY-0W / MHH-ANTHRACITE J" },
                new Item { ItemBarcode = "299996123185", ItemName = "UK.ELB,BUZY-0W / MHS-BLACK JACQUA" },
                new Item { ItemBarcode = "868138123965", ItemName = "UK.ELB,BUZY-0W" },
                new Item { ItemBarcode = "868138123972", ItemName = "UK.ELB,BUZY-0W" },
                new Item { ItemBarcode = "258805123571", ItemName = "TERE,ROSES-YD" },
                new Item { ItemBarcode = "258805123588", ItemName = "TERE,ROSES-YD" },
                new Item { ItemBarcode = "258805123595", ItemName = "TERE,ROSES-YD" },
                new Item { ItemBarcode = "258805123601", ItemName = "TERE,SASHA-YD" },
                new Item { ItemBarcode = "258805123618", ItemName = "TERE,SASHA-YD" },
                new Item { ItemBarcode = "258805123663", ItemName = "KBN,CIBUTI-R" },
                new Item { ItemBarcode = "258805123670", ItemName = "KBN,CIBUTI-R" },
                new Item { ItemBarcode = "258805123687", ItemName = "KBN,CIBUTI-R" },
                new Item { ItemBarcode = "258805123694", ItemName = "KBN,CIBUTI-R" },
                new Item { ItemBarcode = "258805123700", ItemName = "KBN,CIBUTI-R" },
                new Item { ItemBarcode = "258805123717", ItemName = "KBN,CIBUTI-R" },
                new Item { ItemBarcode = "868138123600", ItemName = "KK.TSH,DUBAR" },
                new Item { ItemBarcode = "868138123617", ItemName = "KK.TSH,DUBAR" },
                new Item { ItemBarcode = "868138123049", ItemName = "KK.BDY,WEFSE" },
                new Item { ItemBarcode = "868138123056", ItemName = "KK.BDY,WEFSE" },
                new Item { ItemBarcode = "868138123087", ItemName = "KK.BDY,WEFSE" },
                new Item { ItemBarcode = "868138123946", ItemName = "KK.TSH,DUBAR" },
                new Item { ItemBarcode = "868138123953", ItemName = "KK.TSH,DUBAR" },
                new Item { ItemBarcode = "299997123726", ItemName = "KK.TSH,B,Y.LIVE- / NOO-DEEP NAVY" }
        };

            foreach (var item in items)
            {
                await StockManagementDB.SaveUniqueItemAsync(item);
            }
        }

        public async Task AddSomeStock()
        {
            List<Stock> stockUnits = new List<Stock>
            {
                new Stock{ ItemId = 1,  StorageLocationId = 1,  Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 2,  StorageLocationId = 2,  Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 3,  StorageLocationId = 3,  Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 4,  StorageLocationId = 4,  Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 5,  StorageLocationId = 5,  Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 6,  StorageLocationId = 6,  Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 7,  StorageLocationId = 7,  Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 8,  StorageLocationId = 8,  Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 9,  StorageLocationId = 9,  Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 10, StorageLocationId = 10, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 11, StorageLocationId = 11, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 12, StorageLocationId = 12, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 13, StorageLocationId = 13, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 14, StorageLocationId = 14, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 15, StorageLocationId = 15, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 16, StorageLocationId = 16, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 17, StorageLocationId = 17, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 18, StorageLocationId = 18, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 19, StorageLocationId = 19, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 20, StorageLocationId = 20, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 21, StorageLocationId = 21, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 22, StorageLocationId = 22, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 23, StorageLocationId = 23, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 24, StorageLocationId = 24, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 25, StorageLocationId = 25, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 26, StorageLocationId = 26, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 27, StorageLocationId = 27, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 28, StorageLocationId = 28, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 29, StorageLocationId = 29, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 30, StorageLocationId = 30, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 31, StorageLocationId = 31, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 32, StorageLocationId = 32, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 33, StorageLocationId = 33, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 34, StorageLocationId = 34, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 35, StorageLocationId = 35, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 36, StorageLocationId = 36, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 37, StorageLocationId = 37, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 38, StorageLocationId = 38, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 39, StorageLocationId = 39, Count = GetRandomNumber(0,4) },
                new Stock{ ItemId = 40, StorageLocationId = 40, Count = GetRandomNumber(0,4) }

            };

            foreach (var stock in stockUnits)
            {
                await StockManagementDB.SaveStockUnitAsync(stock);
            }
        }

        public async void ForDebug()
        {
            List<Item> items = await StockManagementDB.GetItemsAsync();
            List<StorageLocation> locations = await StockManagementDB.GetStorageLocationsAsync();
            List<Stock> stockUnits = await StockManagementDB.GetStockUnitsAsync();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static int GetRandomNumber(int min, int max)
        {
            Random random = new Random();
            int randomNumber = random.Next(min, max);
            return randomNumber;
        }

    }
}
