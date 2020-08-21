using System;
using System.IO;
using SmartWMS.Repository;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartWMS
{
    public partial class App : Application
    {
        private static UserDatabase userDB;
        public static UserDatabase UserDB
        {
            get
            {
                if(userDB == null)
                {
                    userDB = new UserDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "User.db3"));
                }
                return userDB;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginView());

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
