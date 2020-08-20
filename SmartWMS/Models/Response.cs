using System;
namespace SmartWMS.Models
{
    public class Response
    {
        public bool Success { get; set; } = true;
        public string ExceptionMessage { get; set; }
    }
}
