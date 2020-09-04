using Microcharts;
using SkiaSharp;
using SmartWMS.OperationViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Entry = Microcharts.ChartEntry;

namespace SmartWMS.OperationViews
{
    public partial class MainPageView : ContentPage
    {
        
        public Chart PieChart => new PieChart()
        {
            Entries = new[]
            {
                new Entry(200)
                {
                Color = SKColor.Parse("#6FAFB0"),
                Label = "Works to be done",

                },
                new Entry(400)
                {
                Color = SKColor.Parse("#D197B0"),
                Label = "Cancelled works"
                },

                new Entry(550)
                {
                Color = SKColor.Parse("#E8BB8E"),
                Label = "Works done"
                }
            },
            LabelTextSize = 20,
            LabelMode = LabelMode.LeftAndRight,
        };
        

        public MainPageView()
        {
            InitializeComponent();

            BindingContext = this;
        }

    }
}