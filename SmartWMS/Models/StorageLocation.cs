using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace SmartWMS.Models
{
    [Table("StorageLocations")]
    public class StorageLocation // Adres 
    {
        [PrimaryKey, AutoIncrement]
        public int StorageLocationId { get; set; }

        public string StorageLocationBarcode { get; set; }

    }
}
