using HabitCracker.Model.Contexts;
using HabitCracker.Model.Entities;
using HabitCracker.View.MainWindow;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static HabitCracker.Model.Entities.CoolerContext;

namespace HabitCracker.ViewModel.AuthViewModel
{
    internal class SignViewModel : BaseViewModel
    {
        public Model.Entities.Auth SignAuth { get; set; } = new();
        public Wallet UserWallet { get; set; } = new();
        public string Login { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public Person CurrentPerson = UserContext.GetInstance().UserPerson;

        public RelayCommand SignUpButtonClickCommand => new(passwordBox =>
            {
                try
                {
                    if (CoolerContext.GetInstance().Auths.FirstOrDefault(p => p.Login == Login) != null)
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует");
                        return;
                    }

                    CurrentPerson.Wallet = UserWallet;
                    UserWallet.Balance = 0;

                    CurrentPerson.Name = Name;
                    CurrentPerson.Lastname = Lastname;
                    SignAuth.Login = Login;
                    SignAuth.Salt = Hasher.GetSalt();
                    if (passwordBox is PasswordBox pBox)
                    {
                        SignAuth.Password = Hasher.Encrypt(pBox.Password, SignAuth.Salt);
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

        public RelayCommand SignInButtonClickCommand => new RelayCommand(passwordBox =>
            {
                try
                {
                    var eAuth = new Model.Entities.Auth();

                    if ((passwordBox is PasswordBox box))
                    {
                        foreach (var item in GetInstance().Auths)
                        {
                            if ((item == null) || ((SignAuth.Login != item.Login) ||
                                                   (item.Password != Hasher.Encrypt(box.Password, item.Salt))))
                                continue;
                            eAuth = item;
                            break;
                        }
                    }

                    if (GetInstance().People.FirstOrDefault(p => p.AuthRef == eAuth.Id) == null)
                    {
                        MessageBox.Show("Такого пользователя не существует");
                        return;
                    }
                    UserContext.GetInstance().UserPerson = GetInstance().People.Include(p => p.Wallet).Include(p => p.Habits).ThenInclude(p => p.HabitProgress).FirstOrDefault(p => p.AuthRef == eAuth.Id);
                    UserContext.GetInstance().UserPerson.Wallet = GetInstance().Wallets.First(p => p.PersonRef == UserContext.GetInstance().UserPerson.Id);
                    Passed();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        );

        private static void Passed()
        {
            Window window;

            if (UserContext.GetInstance().UserPerson.Role is "Администратор" or "Модератор")
                window = new AdminMainWindow();
            else
                window = new MainWindow();
            window?.Show();

            Application.Current.MainWindow?.Close();
        }
    }
}