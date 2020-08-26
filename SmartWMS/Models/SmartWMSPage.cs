using System;
using System.Collections.Generic;
using System.Text;

namespace SmartWMS.Models
{
    public class SmartWMSPage
    {
        public string ViewName { get; set; }
        public Type OperationView { get; set; }

        public SmartWMSPage(string viewName, Type operationPage)
        {
            ViewName = viewName;
            OperationView = operationPage;
        }
    }
}
