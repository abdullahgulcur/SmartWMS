﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartWMS
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            //App.UserDB.GetAllUsers();

            //var response = App.UserDB.Login("", "");

            //App.UserDB.SaveUserAsyn(new Models.User()
            //{
            //    Password = "asd",
            //    Username = "Abdullah",
            //});
        }
    }
}