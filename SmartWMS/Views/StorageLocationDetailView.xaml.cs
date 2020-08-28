using SmartWMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartWMS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StorageLocationDetailView : ContentPage
    {
        #region

        private List<Item> items;
        public List<Item> Items
        {
            get => items;
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }


        #endregion

        public StorageLocationDetailView(StorageLocation location)
        {
            Items = location.Items;
            InitializeComponent();
            BindingContext = this;
        }

    }
}