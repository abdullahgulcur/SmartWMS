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
        /*
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

        private static StorageLocationRepository storageLocaitonDB;
        public static StorageLocationRepository StorageLocationDB
        {
            get
            {
                if (storageLocaitonDB == null)
                {
                    storageLocaitonDB = new StorageLocationRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "User.db3"));
                }
                return storageLocaitonDB;
            }
        }
        */

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
        
        private static StorageRepository storageRepository;
        public static StorageRepository StorageRepository
        {
            get
            {
                if(storageRepository == null)
                {
                    storageRepository = new StorageRepository();
                }
                return storageRepository;
            }
        }

        /*
        private async void DeleteSomeRows()
        {
            //StorageLocation loc = await StorageLocationDB.ReadStorageLocationAsync(46);
            //await StorageLocationDB.DeleteStorageLocationAsync(loc);

            StorageLocation loc = await StorageLocationDB.ReadStorageLocationBarcodeAsync("54-0A1-001-01");
            await StorageLocationDB.DeleteStorageLocationAsync(loc);

            List<StorageLocation> locs = await StorageLocationDB.GetStorageLocationsAsync();
        }

        private void AddStorageLocation()
        {
            //DeleteSomeRows();

            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A1-001-01" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A1-001-02" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A1-001-03" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A1-002-01" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A1-002-02" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A1-002-03" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A1-003-01" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A1-003-02" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A1-003-03" });

            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A2-001-01" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A2-001-02" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A2-001-03" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A2-002-01" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A2-002-02" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A2-002-03" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A2-003-01" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A2-003-02" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A2-003-03" });

            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A3-001-01" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A3-001-02" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A3-001-03" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A3-002-01" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A3-002-02" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A3-002-03" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A3-003-01" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A3-003-02" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A3-003-03" });

            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A4-001-01" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A4-001-02" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A4-001-03" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A4-002-01" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A4-002-02" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A4-002-03" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A4-003-01" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A4-003-02" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A4-003-03" });

            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A5-001-01" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A5-001-02" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A5-001-03" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A5-002-01" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A5-002-02" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A5-002-03" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A5-003-01" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A5-003-02" });
            StorageLocationDB.SaveUniqueLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A5-003-03" });
        }

        private void AddItem()
        {
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "868138123742", ItemName = "SWT,ZARS" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "868138123759", ItemName = "SWT,ZARS" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "868138123793", ItemName = "SWT,ZARS" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "868138123809", ItemName = "SWT,ZARS" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "868138123773", ItemName = "SWT,ZARS" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "868138123729", ItemName = "SWT,ZARS" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "868138123712", ItemName = "SWT,ZARS" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "868138123743", ItemName = "SWT,ZARS" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "868138123750", ItemName = "SWT,ZARS" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "868138123728", ItemName = "ŞRT,FILLO-0S" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "299997123396", ItemName = "PNT,ARS / CRP-NAVY" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "299997123433", ItemName = "PNT,ARS / FGP-BEIGE" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "299997123457", ItemName = "PNT,ARS / JTK-ANTHRACITE" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "299997123518", ItemName = "SWT,FORGAN / CVP-GREEN" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "299997123833", ItemName = "UK.BDY,MASLAK / Q0G-NEON GREEN" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "299997123600", ItemName = "UK.ELB,KANSU / D6Z-DARK YELLOW" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "299997123697", ItemName = "TYT,BRONZ-A-20S / CT3-GREY MELANGE" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "299997123703", ItemName = "TYT,BRONZ-A-20S / FTG-PINK" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "299997123633", ItemName = "UK.TSH,BS.STARWA / JYX-BRILLIANT WH" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "299997123626", ItemName = "UK.TSH,BS.STARWA / JYX-BRILLIANT WH" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "868138123132", ItemName = "UK.TSH,BS.STARWARS-3" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "868138123034", ItemName = "PNT,SLIM-MOM-MID-L-YD" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "868138123027", ItemName = "PNT,SLIM-MOM-MID-L-YD" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "868138123065", ItemName = "PNT,SLIM-MOM-MID-L-YD" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "868138123058", ItemName = "PNT,SLIM-MOM-MID-L-YD" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "299996123735", ItemName = "PNT,MOM-ANTRACID / GRD-GREY RODEO" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "299996123161", ItemName = "UK.ELB,BUZY-0W / MHS-BLACK JACQUA" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "299996123154", ItemName = "UK.ELB,BUZY-0W / MHH-ANTHRACITE J" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "299996123185", ItemName = "UK.ELB,BUZY-0W / MHS-BLACK JACQUA" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "868138123965", ItemName = "UK.ELB,BUZY-0W" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "868138123972", ItemName = "UK.ELB,BUZY-0W" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "258805123571", ItemName = "TERE,ROSES-YD" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "258805123588", ItemName = "TERE,ROSES-YD" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "258805123595", ItemName = "TERE,ROSES-YD" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "258805123601", ItemName = "TERE,SASHA-YD" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "258805123618", ItemName = "TERE,SASHA-YD" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "258805123663", ItemName = "KBN,CIBUTI-R" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "258805123670", ItemName = "KBN,CIBUTI-R" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "258805123687", ItemName = "KBN,CIBUTI-R" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "258805123694", ItemName = "KBN,CIBUTI-R" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "258805123700", ItemName = "KBN,CIBUTI-R" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "258805123717", ItemName = "KBN,CIBUTI-R" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "868138123600", ItemName = "KK.TSH,DUBAR" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "868138123617", ItemName = "KK.TSH,DUBAR" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "868138123049", ItemName = "KK.BDY,WEFSE" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "868138123056", ItemName = "KK.BDY,WEFSE" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "868138123087", ItemName = "KK.BDY,WEFSE" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "868138123946", ItemName = "KK.TSH,DUBAR" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "868138123953", ItemName = "KK.TSH,DUBAR" });
            StorageLocationDB.SaveItemAsync(new Item { ItemBarcode = "299997123726", ItemName = "KK.TSH,B,Y.LIVE- / NOO-DEEP NAVY" });


        }

        */

        public App()
        {

            Task.Run(async () => { await AddSomeLocation(); }).Wait();
            Task.Run(async () => { await AddSomeItem(); }).Wait();

            VisitAndAddItemsToStorageLocations();

            //Task.Run(async () => { await DeleteSomeLocation(); }).Wait();

            //Task.Run(async () => { await StorageRepository.DropLocations(); }).Wait();
            //Task.Run(async () => { await StorageRepository.DropItems(); }).Wait();

            //ForDebug();

            InitializeComponent();

            MainPage = new NavigationPage(new LoginView());
        }

        public async Task DeleteSomeLocation()
        {
            //await StorageRepository.DeleteLocationWithBarcode("54-0A1-001-01");
            await StorageRepository.DeleteLocationWithId(7);

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
                /*
                new StorageLocation() { StorageLocationBarcode = "" },
                new StorageLocation() { StorageLocationBarcode = "" },
                new StorageLocation() { StorageLocationBarcode = "" },
                new StorageLocation() { StorageLocationBarcode = "" },
                new StorageLocation() { StorageLocationBarcode = "" },
                new StorageLocation() { StorageLocationBarcode = "" },
                new StorageLocation() { StorageLocationBarcode = "" },
                new StorageLocation() { StorageLocationBarcode = "" },
                new StorageLocation() { StorageLocationBarcode = "" },

                new StorageLocation() { StorageLocationBarcode = "" },
                new StorageLocation() { StorageLocationBarcode = "" },
                new StorageLocation() { StorageLocationBarcode = "" },
                new StorageLocation() { StorageLocationBarcode = "" },
                new StorageLocation() { StorageLocationBarcode = "" },
                new StorageLocation() { StorageLocationBarcode = "" },
                new StorageLocation() { StorageLocationBarcode = "" },
                new StorageLocation() { StorageLocationBarcode = "" },
                new StorageLocation() { StorageLocationBarcode = "" },

                new StorageLocation() { StorageLocationBarcode = "" },
                new StorageLocation() { StorageLocationBarcode = "" },
                new StorageLocation() { StorageLocationBarcode = "" },
                new StorageLocation() { StorageLocationBarcode = "" },
                new StorageLocation() { StorageLocationBarcode = "" },
                new StorageLocation() { StorageLocationBarcode = "" },
                new StorageLocation() { StorageLocationBarcode = "" },
                new StorageLocation() { StorageLocationBarcode = "" },
                new StorageLocation() { StorageLocationBarcode = "" }
                */
            };

            var dataset = (await StorageRepository.GetAllLocations()).Select(x => x.StorageLocationId);

            foreach (var location in locations)
            {
                if (!dataset.Contains(location.StorageLocationId))
                {
                    await StorageRepository.AddLocation(location);
                }
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

            };

            var dataset = (await StorageRepository.GetAllItems()).Select(x => x.ItemId);

            foreach (var item in items)
            {
                if (!dataset.Contains(item.ItemId))
                {
                    await StorageRepository.AddItem(item);
                }
            }
        }

        public async void ForDebug()
        {
            List<Item> items = await StorageRepository.GetAllItems();
            List<StorageLocation> locations = await StorageRepository.GetAllLocations();
        }

        public async void VisitAndAddItemsToStorageLocations()
        {
            /* These are local variables */
            List<Item> items = await StorageRepository.GetAllItems();
            List<StorageLocation> locations = await StorageRepository.GetAllLocations();

            for (int i = 0; i < locations.Count; i++)
            {
                locations[i].Items = null;
                await StorageRepository.UpdateLocation(locations[i]);
            }

            for (int i = 0; i < items.Count; i++)
            {
                items[i].StorageLocationId = 0;
                await StorageRepository.UpdateItem(items[i]);
            }
            
            int locationCapacity = 3;

            for(int i = 0; i < locations.Count; i++)
            {
                // Create memory location for storage locations 
                locations[i].Items = new List<Item>();

                // Add some items to locations
                for (int j = 0; j < locationCapacity; j++)
                {
                    int randomItem = GetRandomNumber(0, items.Count);
                    Item item = items[randomItem];

                    if(items[randomItem].StorageLocationId == 0)
                    {
                        locations[i].Items.Add(item);
                    }

                }

                // save them
                await StorageRepository.UpdateLocation(locations[i]);
            }
            //List<Item> items1 = await StorageRepository.GetAllItems();
            //List<StorageLocation> locations1 = await StorageRepository.GetAllLocations();
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

        private static int GetRandomNumber(int min, int max)
        {
            Random random = new Random();
            int randomNumber = random.Next(min, max);
            return randomNumber;
        }
    }
}
