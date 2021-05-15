﻿using HabitCracker.Model.Contexts;
using HabitCracker.Model.Entities;
using HabitCracker.View.MainWindow;
using System;
using System.Linq;
using System.Windows;
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

        public bool IsUserIdUnique(int id) => GetInstance().Auths.All(p => p.Id != id);

        //foreach (var item in CoolerContext.GetInstance().Auths)
        //{
        //    if (id == item.Id)
        //    {
        //        return false;
        //    }s
        //}
        //return true;

        public int GetUniqueId()
        {
            Random random = new Random();

            int Id = random.Next();
            while (!IsUserIdUnique(Id))
            {
                Id = random.Next();
            }

            return Id;
        }

        public string Lastname
        {
            get => lastname;
            set => lastname = value;
        }

        public Person currentPerson = UserContext.GetInstance().UserPerson;

        public RelayCommand SignUpButtonClickCommand => new RelayCommand(obj =>
            {
                try
                {
                    //UserWallet.Id = GetUniqueId();

                    currentPerson.Wallet = UserWallet;
                    UserWallet.Balance = 0;

                    //currentPerson.Id = UserWallet.Id;

                    currentPerson.Name = Name;
                    currentPerson.Lastname = Lastname;
                    //var id = GetUniqueId();
                    SignAuth.Login = Login;
                    SignAuth.Salt = Hasher.GetSalt();
                    SignAuth.Password = Hasher.Encrypt(Password, SignAuth.Salt);
                    //SignAuth.Id = id;
                    //currentPerson.Id = SignAuth.Id;
                    SignAuth.Person = currentPerson;
                    currentPerson.Auth = SignAuth;

                    UserWallet.Owner = currentPerson;

                    GetInstance().Auths.Add(SignAuth);
                    GetInstance().Wallets.Add(UserWallet);
                    //currentPerson.Id = SignAuth.Id;
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

        public RelayCommand SignInButtonClickCommand => new RelayCommand(obj =>
            {
                try
                {
                    var eAuth = new Model.Entities.Auth();
                    //todo GET MATCHES WITH AUTH.LOGIN => GET PERSON FROM DB WITH SIGN.AUTH ID

                    //!tried using linq to ef, but it cant resolve my hasher methods
                    foreach (var item in GetInstance().Auths)
                    {
                        if ((item == null) || ((SignAuth.Login != item.Login) ||
                                               (item.Password != Hasher.Encrypt(SignAuth.Password, item.Salt))))
                            continue;
                        eAuth = item;
                        break;
                    }

                    UserContext.GetInstance().UserPerson = GetInstance().People
                        .Single(p => p.Id == eAuth.Id);
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