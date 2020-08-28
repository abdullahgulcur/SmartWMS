using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartWMS.Models;
using SQLite;

namespace SmartWMS.Repository
{
    public class StorageLocationRepository
    {
        /*
         * All Methods run perfectly...
         * It includes storage locations and items table 
         * Insert methods can be done in different parameter types
         */

        readonly SQLiteAsyncConnection _database;
        public StorageLocationRepository(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);

            _database.CreateTableAsync<StorageLocation>().Wait();
            _database.CreateTableAsync<Item>().Wait();
        }

        /* STORAGE LOCATIONS PART */

        public Task<List<StorageLocation>> GetStorageLocationsAsync()
        {
            return _database.Table<StorageLocation>().ToListAsync();
        }

        public Task<StorageLocation> ReadStorageLocationAsync(int id)
        {
            return _database.Table<StorageLocation>().Where(p => p.StorageLocationId == id).FirstOrDefaultAsync();
        }

        public Task<StorageLocation> ReadStorageLocationBarcodeAsync(string barcode)
        {
            return _database.Table<StorageLocation>().Where(p => p.StorageLocationBarcode == barcode).FirstOrDefaultAsync();
        }

        public Task<int> SaveStorageLocationAsync(StorageLocation location)
        {
            if (location.StorageLocationId != 0)
                return _database.UpdateAsync(location);
            else
                return _database.InsertAsync(location);
        }

        public Task<int> SaveStorageLocationWithItemAsync(StorageLocation location)
        {
            if (location.StorageLocationId != 0)
            {
                foreach (Item item in location.Items)
                {
                    item.StorageLocationId = location.StorageLocationId;
                    SaveItemAsync(item);
                }

                return _database.UpdateAsync(location);
            }
            else
                return _database.InsertAsync(location);

        }

        public Task<int> DeleteStorageLocationAsync(StorageLocation location)
        {
            return _database.DeleteAsync(location);
        }

        public void DropStorageLocationTable()
        {
            _database.DropTableAsync<StorageLocation>().Wait();
        }

        /*
         *  This is a new method for adding locations to location repository
         *  If location is not found in repo, then add it.
         */
        public async void SaveUniqueLocationAsync(StorageLocation location)
        {
            var locationToBeCreated = _database.Table<StorageLocation>().Where(p => p.StorageLocationBarcode == location.StorageLocationBarcode).FirstOrDefaultAsync().Result;

            if (locationToBeCreated == null)
                await SaveStorageLocationAsync(location);
        }


        /* ITEMS PART */

        public Task<List<Item>> GetItemsAsync()
        {
            return _database.Table<Item>().ToListAsync();
        }

        public Task<Item> ReadItemAsync(int id)
        {
            return _database.Table<Item>().Where(p => p.ItemId == id).FirstOrDefaultAsync();
        }

        public Task<Item> ReadItemBarcodeAsync(string barcode)
        {
            return _database.Table<Item>().Where(p => p.ItemBarcode == barcode).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Item item)
        {
            if (item.ItemId != 0)
                return _database.UpdateAsync(item);
            else
                return _database.InsertAsync(item);

        }

        public Task<int> DeleteItemAsync(Item item)
        {
            return _database.DeleteAsync(item);
        }

        public void DropItemTable()
        {
            _database.DropTableAsync<Item>().Wait();
        }

        /*
         *  This is a new method for adding unique item to table
         *  If item is not found in repo, then add it.
         */
        public async void SaveUniqueItemAsync(Item item)
        {
           
            var itemToBeCreated = _database.Table<Item>().Where(p => p.ItemBarcode == item.ItemBarcode).FirstOrDefaultAsync().Result;

            if (itemToBeCreated == null)
                await SaveItemAsync(item);
        }

        public void UpdateLocationWithItem(StorageLocation storageLocation)
        {

        }
    }
}
