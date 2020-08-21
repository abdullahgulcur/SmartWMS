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

    public partial class LoginView : ContentPage
    {
        #region

        private string username;
        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        #endregion
        public LoginView()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            Response response = App.UserDB.LoginAsync(Username, Password);

            if (response.Success)
            {
                MainMenuView view = new MainMenuView();
                await Navigation.PushAsync(view);
            }
            else
                await DisplayAlert("Warning", "The username or password you entered is incorrect", "OK");
        }

        private void SignUpLabelTap_Tapped(object sender, EventArgs e)
        {
            SignUpView view = new SignUpView();
            Navigation.PushAsync(view);
        }

        private void UsernameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password))
                LoginButton.IsEnabled = false;
            else
                LoginButton.IsEnabled = true;

        }

        private void PasswordEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password))
                LoginButton.IsEnabled = false;
            else
                LoginButton.IsEnabled = true;
        }
    }
}