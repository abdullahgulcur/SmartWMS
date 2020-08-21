using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartWMS.OperationViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartWMS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuView : MasterDetailPage
    {
        List<SmartWMSPage> pages = new List<SmartWMSPage>();

        public MainMenuView()
        {
            pages.Add(new SmartWMSPage("Ürün Alım", typeof(GoodsReceiptView)));

            InitializeComponent();
            BindingContext = this;
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var selectedItem = pages.FirstOrDefault();

            var view = Activator.CreateInstance(selectedItem.OperationView);
            //GoodsReceiptView view1 = new GoodsReceiptView();
            Navigation.PushAsync(view as ContentPage);
        }
    }

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