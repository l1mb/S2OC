using HabitCracker.Model.Contexts;
using HabitCracker.Model.Entities;
using HabitCracker.View.MainWindow;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using static HabitCracker.Model.Entities.CoolerContext;

namespace HabitCracker.ViewModel.AuthViewModel
{
    internal class SignViewModel : BaseViewModel
    {
        public Model.Entities.Auth SignAuth { get; set; } = new();
        public Wallet UserWallet { get; set; } = new();

        private string _login;

        public string Login
        {
            get => _login;
            set => _login = value;
        }

        private string _password;

        public String Password
        {
            get => _password;
            set => _password = value;
        }

        private string name;

        public string Name
        {
            get => name;
            set => name = value;
        }

        private string lastname;

        public string Lastname
        {
            get => lastname;
            set => lastname = value;
        }

        public Person CurrentPerson = UserContext.GetInstance().UserPerson;

        public RelayCommand SignUpButtonClickCommand => new RelayCommand(PasswordBox =>
            {
                try
                {

                    
                    CurrentPerson.Wallet = UserWallet;
                    UserWallet.Balance = 0;

                    CurrentPerson.Name = Name;
                    CurrentPerson.Lastname = Lastname;
                    SignAuth.Login = Login;
                    SignAuth.Salt = Hasher.GetSalt();
                    if (PasswordBox is PasswordBox passwordBox)
                    {
                        SignAuth.Password = Hasher.Encrypt(passwordBox.Password, SignAuth.Salt);
                    }
                    SignAuth.Person = CurrentPerson;
                    CurrentPerson.Auth = SignAuth;

                    UserWallet.Owner = CurrentPerson;

                    GetInstance().Auths.Add(SignAuth);
                    GetInstance().Wallets.Add(UserWallet);
                    SignAuth = null;
                    GetInstance().SaveChanges();

                    Passed();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        );

        public RelayCommand SignInButtonClickCommand => new RelayCommand(PasswordBox =>
            {
                try
                {
                    var eAuth = new Model.Entities.Auth();

                    if ((PasswordBox is PasswordBox passwordBox))
                    {
                        foreach (var item in GetInstance().Auths)
                        {
                            if ((item == null) || ((SignAuth.Login != item.Login) ||
                                                   (item.Password != Hasher.Encrypt(passwordBox.Password, item.Salt))))
                                continue;
                            eAuth = item;
                            break;
                        }
                    }
                    
                    
                    if (CoolerContext.GetInstance().People.FirstOrDefault(p => p.AuthRef == eAuth.Id)==null)
                    {
                        MessageBox.Show("Такого пользователя не существует");
                        return;
                    }
                    UserContext.GetInstance().UserPerson = GetInstance().People.Include(p => p.Wallet).Include(p => p.Habits).ThenInclude(p => p.HabitProgress).FirstOrDefault(p => p.AuthRef == eAuth.Id);
                    UserContext.GetInstance().UserPerson.Wallet = GetInstance().Wallets.First(p => p.PersonRef == UserContext.GetInstance().UserPerson.Id);
                    //currentPerson = null;
                    //SignAuth = null;
                    Passed();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        );

        public void Passed()
        {
            Window window = null;

            if (UserContext.GetInstance().UserPerson.Role == "Администратор" || UserContext.GetInstance().UserPerson.Role == "Модератор")
                window = new AdminMainWindow();
            else
                window = new MainWindow();
            window?.Show();

            Application.Current.MainWindow?.Close();

            //AuthWindow authWindow = new AuthWindow();
            //window.Close();
            //authWindow.Show();
        }
    }
}