using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SmartWMS.Models;
using SmartWMS.Repository;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartWMS
{
    public partial class App : Application
    {
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

        private async void DeleteSomeRows()
        {
            //StorageLocation loc = await StorageLocationDB.ReadStorageLocationAsync(46);
            //await StorageLocationDB.DeleteStorageLocationAsync(loc);

            StorageLocation loc = await StorageLocationDB.ReadStorageLocationBarcodeAsync("54-0A1-001-01");
            await StorageLocationDB.DeleteStorageLocationAsync(loc);

            List<StorageLocation> locs = await StorageLocationDB.GetStorageLocationsAsync();
        }

        /*
         * StorageLocation is tested, methods run well
         * Existing location is not added again.
         */
        private void AddStorageLocation()
        {
            //DeleteSomeRows();

            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A1-001-01" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A1-001-02" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A1-001-03" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A1-002-01" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A1-002-02" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A1-002-03" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A1-003-01" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A1-003-02" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A1-003-03" });

            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A2-001-01" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A2-001-02" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A2-001-03" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A2-002-01" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A2-002-02" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A2-002-03" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A2-003-01" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A2-003-02" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A2-003-03" });

            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A3-001-01" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A3-001-02" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A3-001-03" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A3-002-01" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A3-002-02" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A3-002-03" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A3-003-01" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A3-003-02" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A3-003-03" });

            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A4-001-01" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A4-001-02" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A4-001-03" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A4-002-01" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A4-002-02" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A4-002-03" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A4-003-01" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A4-003-02" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A4-003-03" });

            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A5-001-01" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A5-001-02" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A5-001-03" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A5-002-01" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A5-002-02" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A5-002-03" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A5-003-01" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A5-003-02" });
            StorageLocationDB.SaveLocationAsync(new StorageLocation() { StorageLocationBarcode = "54-0A5-003-03" });
        }

        public App()
        {
            AddStorageLocation();
            InitializeComponent();

            MainPage = new NavigationPage(new LoginView());
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
    }
}
