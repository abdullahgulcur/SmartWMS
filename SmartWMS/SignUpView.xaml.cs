using SmartWMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartWMS
{
    public partial class SignUpView : ContentPage
    {
        #region

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string surname;
        public string Surname
        {
            get => surname;
            set
            {
                surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

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

        private string confirmPassword;
        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        #endregion

        public SignUpView()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private void createAccount_Clicked(object sender, EventArgs e)
        {
            CreateAccount();
        }

        private void AccountsLabelTap_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserListView());
        }

        private void NameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            NameValidationCheck();
        }

        private void SurnameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            SurnameValidationCheck();
        }

        private void EmailEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            EmailValidationCheck();
        }

        private void UsernameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserNameValidationCheck();
        }

        private void PasswordEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            PasswordValidationCheck();
            PasswordConfirmValidationCheck();
        }

        private void ConfirmPasswordEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            PasswordConfirmValidationCheck();
        }

        private void NameEntry_Focused(object sender, FocusEventArgs e)
        {
            NameValidationCheck();
        }

        private void SurnameEntry_Focused(object sender, FocusEventArgs e)
        {
            SurnameValidationCheck();
        }

        private void EmailEntry_Focused(object sender, FocusEventArgs e)
        {
            EmailValidationCheck();
        }

        private void UsernameEntry_Focused(object sender, FocusEventArgs e)
        {
            UserNameValidationCheck();
        }

        private void PasswordEntry_Focused(object sender, FocusEventArgs e)
        {
            PasswordValidationCheck();
            PasswordConfirmValidationCheck();
        }

        private void ConfirmPasswordEntry_Focused(object sender, FocusEventArgs e)
        {
            PasswordConfirmValidationCheck();
        }

        private bool NameValidationCheck()
        {
            if (String.IsNullOrEmpty(Name))
            {
                NameLabel.Text = "Please enter a name.";
                NameLabel.TextColor = Color.Red;
                return false;
            }
            else
            {
                NameLabel.Text = "Name is fine.";
                NameLabel.TextColor = Color.Green;
                return true;
            }
        }

        private bool SurnameValidationCheck()
        {
            if (String.IsNullOrEmpty(Surname))
            {
                SurnameLabel.Text = "Please enter a surname.";
                SurnameLabel.TextColor = Color.Red;
                return false;
            }
            else
            {
                SurnameLabel.Text = "Surname is fine.";
                SurnameLabel.TextColor = Color.Green;
                return true;
            }
        }

        private bool EmailValidationCheck()
        {
            if (String.IsNullOrEmpty(Email))
            {
                EmailLabel.Text = "Please enter an email.";
                EmailLabel.TextColor = Color.Red;
                return false;
            }
            else if (IsValidEmail(Email))
            {
                EmailLabel.Text = "Email is fine.";
                EmailLabel.TextColor = Color.Green;
                return true;
            }
            else
            {
                EmailLabel.Text = "Please enter valid Email.";
                EmailLabel.TextColor = Color.Red;
                return false;
            }
        }

        private bool UserNameValidationCheck()
        {
            if (String.IsNullOrEmpty(Username))
            {
                UsernameLabel.Text = "Please enter a Username.";
                UsernameLabel.TextColor = Color.Red;
                return false;
            }
            else
            {
                UsernameLabel.Text = "Username is fine.";
                UsernameLabel.TextColor = Color.Green;
                return true;
            }
        }

        private bool PasswordValidationCheck()
        {
            if (String.IsNullOrEmpty(Password))
            {
                PasswordLabel.Text = "Please enter a password.";
                PasswordLabel.TextColor = Color.Red;
                ConfirmPasswordLabel.Text = "";
                return false;
            }
            else if (!IsValidPassword(Password))
            {
                PasswordLabel.Text = "Password between 4 and 15 characters; must contain at least one lowercase letter, one uppercase letter, one numeric digit, and one special character.";
                ConfirmPasswordLabel.Text = "";
                PasswordLabel.TextColor = Color.Red;
                return false;
            }
            else
            {
                ConfirmPasswordLabel.Text = "Please confirm password.";
                PasswordLabel.Text = "Password is fine.";
                PasswordLabel.TextColor = Color.Green;
                return true;
            }
        }

        private bool PasswordConfirmValidationCheck()
        {
            if (!String.IsNullOrEmpty(Password) && IsValidPassword(Password))
            {
                if (String.IsNullOrEmpty(ConfirmPassword))
                {
                    ConfirmPasswordLabel.Text = "Please confirm password.";
                    ConfirmPasswordLabel.TextColor = Color.Red;
                    return false;

                }
                else
                {
                    if (!Password.Equals(ConfirmPassword))
                    {
                        ConfirmPasswordLabel.Text = "Please confirm password.";
                        ConfirmPasswordLabel.TextColor = Color.Red;
                        return false;

                    }
                    else
                    {
                        ConfirmPasswordLabel.Text = "Password is confirmed.";
                        ConfirmPasswordLabel.TextColor = Color.Green;
                        return true;

                    }
                }
            }
            return false;
        }

        private bool IsValidEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            return match.Success;
        }

        private bool IsValidPassword(string password)
        {
            Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{4,15}$");
            Match match = regex.Match(password);
            return match.Success;
        }

        private async void CreateAccount()
        {
            bool validation = true;

            if (String.IsNullOrEmpty(Name))
            {
                await DisplayAlert("Name", "Please enter a name.", "OK");
                validation = false;
            }
            else if (String.IsNullOrEmpty(Surname))
            {
                await DisplayAlert("Surname", "Please enter a surname.", "OK");
                validation = false;
            }
            else if (String.IsNullOrEmpty(Email))
            {
                await DisplayAlert("Email", "Please enter an email.", "OK");
                validation = false;
            }
            else if (!IsValidEmail(Email))
            {
                await DisplayAlert("Email", "Please enter a valid email.", "OK");
                validation = false;
            }
            else if (String.IsNullOrEmpty(Username))
            {
                await DisplayAlert("User Name", "Please enter a user name.", "OK");
                validation = false;
            }
            else if (String.IsNullOrEmpty(Password))
            {
                await DisplayAlert("Password", "Please enter a password.", "OK");
                validation = false;
            }
            else if (!IsValidPassword(Password))
            {
                await DisplayAlert("Password", "Password between 4 and 15 characters; must contain at least one lowercase letter, one uppercase letter, one numeric digit, and one special character.", "OK");
                validation = false;
            }
            else if (String.IsNullOrEmpty(ConfirmPassword))
            {
                await DisplayAlert("Confirm Password", "Please confirm password.", "OK");
                validation = false;
            }
            else if (!Password.Equals(ConfirmPassword))
            {
                await DisplayAlert("Inconsistent Passwords", "Passwords are not matched.", "OK");
                validation = false;
            }

            if (validation)
            {

                User user = new User();

                user.Name = Name;
                user.Surname = Surname;
                user.Username = Username;
                user.UserEmail = Email;
                user.Password = Password;

                await App.UserDB.SaveUserAsync(user);

                await DisplayAlert("New User", "New user is created", "OK");

                await Navigation.PopAsync();
            }
        }

        private void NameEntry_Completed(object sender, EventArgs e)
        {
            if(NameValidationCheck())
                SurmameEntry.Focus();
        }

        private void SurnameEntry_Completed(object sender, EventArgs e)
        {
            if (SurnameValidationCheck())
                EmailEntry.Focus();
        }

        private void EmailEntry_Completed(object sender, EventArgs e)
        {
            if (EmailValidationCheck())
                UsernameEntry.Focus();
        }

        private void UsernameEntry_Completed(object sender, EventArgs e)
        {
            if (UserNameValidationCheck())
                PasswordEntry.Focus();
        }

        private void PasswordEntry_Completed(object sender, EventArgs e)
        {
            if (PasswordValidationCheck())
                ConfirmPasswordEntry.Focus();
        }

        private void ConfirmPasswordEntry_Completed(object sender, EventArgs e)
        {
            if (PasswordConfirmValidationCheck())
                CreateAccount();
        }
    }
}