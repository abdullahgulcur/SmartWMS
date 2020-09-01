using System;
using System.Collections.Generic;
using System.Text;

namespace SmartWMS.Models
{
    public class RequestedStock
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string Location { get; set; }
        public int Count { get; set; }
    }
}
