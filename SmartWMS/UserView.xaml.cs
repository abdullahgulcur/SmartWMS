using SmartWMS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartWMS
{
    
    public partial class UserView : ContentPage
    {

        List<User> users;
        public List<User> Users
        {
            get => users;
            set
            {
                users = value;
                OnPropertyChanged(nameof(Users));
            }
        }
        public UserView()
        {
            //Users = App.UserDB.GetUsersAsync();



            InitializeComponent();
            BindingContext = this;

            //UsersList = (System.Collections.IEnumerable)App.UserDB.GetUsersAsync();
        }
    }
}