using HabbitCracker.Model.Contexts;
using HabbitCracker.Model.Entities;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace HabbitCracker.ViewModel.AuthViewModel
{
    internal class SignViewModel : BaseViewModel
    {
        public Model.Entities.Auth SignAuth { get; set; } = new();

        private string name;

        public string Name
        {
            get => name;
            set => name = value;
        }

        private string lastname;

        public bool IsUserIdUnique(int id)
        {
            foreach (var item in CourseworkDbContext.GetInstance().Auths)
            {
                if (id == item.Id)
                {
                    return false;
                }
            }
            return true;
        }

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
                    currentPerson.Name = Name;
                    currentPerson.Lastname = Lastname;
                    var id = GetUniqueId();

                    SignAuth.Id = id;
                    currentPerson.Id = SignAuth.Id;
                    SignAuth.Person = currentPerson;
                    currentPerson.IdNavigation = SignAuth;
                    CourseworkDbContext.GetInstance().Auths.Add(SignAuth);
                    currentPerson.Id = SignAuth.Id;

                    CourseworkDbContext.GetInstance().SaveChanges();
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
                    var i = CourseworkDbContext.GetInstance().Auths
                        .Where(p => p.Login == SignAuth.Login && p.Password == SignAuth.Password);
                    var q = i.Single().Id;
                    if (i.Count() == 0)
                        MessageBox.Show("Такого пользователя не существует");
                    else
                    {
                        UserContext.GetInstance().UserPerson = CourseworkDbContext.GetInstance().People.Where(p => p.Id == q).Single();
                        Passed();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Что-то пошло не так");
                    throw;
                }
            }
        );

        public void Passed()
        {
            Window window = new MainWindow();
            window.Show();

            if (Application.Current.MainWindow != null) Application.Current.MainWindow.Close();

            MessageBox.Show(UserContext.GetInstance().UserPerson.ToString());
        }
    }
}