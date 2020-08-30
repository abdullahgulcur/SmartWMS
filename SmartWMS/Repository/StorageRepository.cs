using SmartWMS.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWMS.Repository
{
    public class StorageRepository
    {
        readonly SQLiteConnection connection;

        public StorageRepository()
        {
            connection = SqliteExtension.GetConnection();
        }

        public Task<bool> DropLocations()
        {
            connection.DropTable<StorageLocation>();

            return Task.FromResult(true);
        }

        public Task<bool> DropItems()
        {
            connection.DropTable<Item>();

            return Task.FromResult(true);
        }

        public Task<List<StorageLocation>> GetAllLocations()
        {
            List<StorageLocation> data = connection.GetAllWithChildren<StorageLocation>();

            return Task.FromResult(data);
        }

        public Task<List<Item>> GetAllItems()
        {
            List<Item> data = connection.GetAllWithChildren<Item>();

            return Task.FromResult(data);
        }

        public Task<bool> DeleteLocationWithId(int locationId)
        {
            var data = connection.Get<StorageLocation>(locationId);
            connection.Delete(data);

            return Task.FromResult(true);
        }

        public Task<bool> DeleteLocationWithBarcode(string barcode)
        {
            var locationToBeDeleted = connection.GetAllWithChildren<StorageLocation>()
                .Where(p => p.StorageLocationBarcode == barcode).FirstOrDefault();
            connection.Delete(locationToBeDeleted);

            return Task.FromResult(true);
        }

        public Task<bool> DeleteItem(int ItemId)
        {
            var data = connection.Get<StorageLocation>(ItemId);
            connection.Delete(data);

            return Task.FromResult(true);
        }

        public Task<bool> AddLocation(StorageLocation location)
        {
            StorageLocation locationToBeCreated = connection.GetAllWithChildren<StorageLocation>()
                .Where(p => p.StorageLocationBarcode == location.StorageLocationBarcode).FirstOrDefault();

            if (locationToBeCreated == null)
                connection.Insert(location);

            return Task.FromResult(true);
        }

        public Task<bool> AddItem(Item item)
        {
            Item itemToBeCreated = connection.GetAllWithChildren<Item>()
                .Where(p => p.ItemBarcode == item.ItemBarcode).FirstOrDefault();

            if(itemToBeCreated == null)
                connection.Insert(item);

            return Task.FromResult(true);
        }

        public Task<bool> UpdateLocation(StorageLocation location)
        {
            connection.UpdateWithChildren(location);
            return Task.FromResult(true);
        }

        public Task<bool> UpdateItem(Item item)
        {
            connection.Update(item);

            return Task.FromResult(true);
        }

    }
}
