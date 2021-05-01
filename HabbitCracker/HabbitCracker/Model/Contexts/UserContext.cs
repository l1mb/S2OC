﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabbitCracker.Model.Entities;

namespace HabbitCracker.Model.Contexts
{
    internal class UserContext
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
    }
}