using HabitCracker.Model.Entities;
using System;

namespace HabitCracker.Model.Contexts
{
    internal class UserContext : IDisposable
    {
        private static readonly Lazy<UserContext> Instance = new(() => new UserContext());

        public Person UserPerson { get; set; }

        public static UserContext GetInstance()
        {
            return Instance.Value;
        }

        public UserContext()
        {
            UserPerson = new Person();
        }

        public void Dispose()
        {
        }
    }
}