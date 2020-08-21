using SmartWMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartWMS
{

    public partial class UserListView : ContentPage
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
        public UserListView()
        {
            InitializeComponent();
            BindingContext = this;
            ListUsers();
        }

        private async void ListUsers()
        {
            Users = await App.UserDB.GetUsersAsync();
        }
    }
}