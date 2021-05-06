using HabbitCracker.Model.Contexts;
using HabbitCracker.Model.Entities;
using System;
using System.Windows;

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
                    Window window = new MainWindow();
                    window.Show();

                    if (Application.Current.MainWindow != null) Application.Current.MainWindow.Close();

                    MessageBox.Show(UserContext.GetInstance().UserPerson.ToString());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        );

        public RelayCommand SignInButtonClickCommand => new RelayCommand(obj => { }
        );
    }
}