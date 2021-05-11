using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabitCracker.Model.Entities;

namespace HabitCracker.Model.Contexts
{
    internal class UserContext : IDisposable
    {
        public static readonly Lazy<UserContext> Instance = new(() => new UserContext());

        public Person UserPerson;

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