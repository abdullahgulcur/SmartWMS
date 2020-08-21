using SQLite;

namespace SmartWMS.Models
{
    public class StorageLocation // Adres 
    {
        [PrimaryKey, AutoIncrement]
        public int StorageLocaitonId { get; set; }
        public string StorageLocationBarcode { get; set; }
    }
}

// 54-0A1-001-01 54 => depolama alani 0A1 => Koridor 001 => raf 01 => kat
// 54-0A1-001-02
// 54-0A1-001-03

// 54-0A5-005-03
