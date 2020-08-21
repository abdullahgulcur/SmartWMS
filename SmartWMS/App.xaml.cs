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

        private void AddStorageLocaiton()
        {

            // adres ekli mi kontrolü
            // true return;

            //false 
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A1-001-01" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A1-001-02" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A1-001-03" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A1-002-01" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A1-002-02" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A1-002-03" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A1-003-01" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A1-003-02" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A1-003-03" });

            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A2-001-01" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A2-001-02" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A2-001-03" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A2-002-01" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A2-002-02" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A2-002-03" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A2-003-01" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A2-003-02" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A2-003-03" });

            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A3-001-01" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A3-001-02" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A3-001-03" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A3-002-01" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A3-002-02" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A3-002-03" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A3-003-01" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A3-003-02" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A3-003-03" });

            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A4-001-01" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A4-001-02" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A4-001-03" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A4-002-01" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A4-002-02" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A4-002-03" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A4-003-01" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A4-003-02" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A4-003-03" });

            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A5-001-01" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A5-001-02" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A5-001-03" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A5-002-01" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A5-002-02" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A5-002-03" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A5-003-01" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A5-003-02" });
            UserDB.SaveUserAsync(new Models.User() { Name = "54-0A5-003-03" });
        }

        public App()
        {
            //AddStorageLocaiton();

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
