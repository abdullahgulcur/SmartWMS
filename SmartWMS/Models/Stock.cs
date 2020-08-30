using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartWMS.Models
{
    [Table("Stocks")]
    public class Stock
    {
        [PrimaryKey, AutoIncrement]
        public int StockId { get; set; }
        public int ItemId { get; set; }
        public int StorageLocationId { get; set; }
        public int Count { get; set; }
    }
}
