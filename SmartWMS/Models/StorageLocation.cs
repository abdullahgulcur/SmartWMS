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

        /*
        public override string ToString()
        {
            return StorageLocationId + " " + StorageLocationBarcode;
        }
        */
    }
}

// 54-0A1-001-01 54 => depolama alani 0A1 => Koridor 001 => raf 01 => kat
// 54-0A1-001-02
// 54-0A1-001-03

// 54-0A5-005-03
