﻿using HabitCracker.Model.Contexts;
using HabitCracker.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Documents;
using HabitCracker.View.MainWindow;
using Microsoft.EntityFrameworkCore;
using static HabitCracker.Model.Entities.CourseworkDbContext;

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

        //foreach (var item in CourseworkDbContext.GetInstance().Auths)
        //{
        //    if (id == item.Id)
        //    {
        //        return false;
        //    }
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
                    UserWallet.Idwallet = GetUniqueId();
                    UserWallet.Balance = 0;
                    UserWallet.Hash = "";

                    currentPerson.Idwallet = UserWallet.Idwallet;

                    currentPerson.Name = Name;
                    currentPerson.Lastname = Lastname;
                    var id = GetUniqueId();
                    SignAuth.Login = Login;
                    SignAuth.Salt = Hasher.GetSalt();
                    SignAuth.Password = Hasher.Encrypt(Password, SignAuth.Salt);
                    SignAuth.Id = id;
                    currentPerson.Id = SignAuth.Id;
                    SignAuth.Person = currentPerson;
                    currentPerson.IdNavigation = SignAuth;

                    UserWallet.People.Add(currentPerson);

                    GetInstance().Auths.Add(SignAuth);
                    GetInstance().Wallets.Add(UserWallet);
                    currentPerson.Id = SignAuth.Id;
                    SignAuth = null;
                    GetInstance().SaveChanges();

                    Passed();
                }
                catch (InvalidOperationException exception)
                {
                    MessageBox.Show("Скорее ");
                    //!need to add exception and try catch
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
                    MessageBox.Show("Что-то пошло не так");
                }
            }
        );

        public void Passed()
        {
            Window window = null;

            if (UserContext.GetInstance().UserPerson.Role == "Администратор" || UserContext.GetInstance().UserPerson.Role == "Модератор")
            {
                window = new AdminMainWindow();
            }
            else
            {
                window = new MainWindow();
            }
            window?.Show();

            if (Application.Current.MainWindow != null) Application.Current.MainWindow.Close();

            MessageBox.Show(nameof(Model.Entities.Auth));

            //AuthWindow authWindow = new AuthWindow();
            //window.Close();
            //authWindow.Show();
        }
    }
}