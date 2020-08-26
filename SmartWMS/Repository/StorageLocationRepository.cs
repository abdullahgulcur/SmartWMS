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
         */

        readonly SQLiteAsyncConnection _database;
        public StorageLocationRepository(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<StorageLocation>().Wait();
        }

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

        public Task<int> DeleteStorageLocationAsync(StorageLocation location)
        {
            return _database.DeleteAsync(location);
        }

        /*
         *  This is a new method for adding locations to location repository
         *  If location is not found in repo, then add it.
         */
        public async void SaveLocationAsync(StorageLocation location)
        {
            var locationToBeCreated = _database.Table<StorageLocation>().Where(p => p.StorageLocationBarcode == location.StorageLocationBarcode).FirstOrDefaultAsync().Result;

            if (locationToBeCreated == null)
                await SaveStorageLocationAsync(location);
        }

    }
}
