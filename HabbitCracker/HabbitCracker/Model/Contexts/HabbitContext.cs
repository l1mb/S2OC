﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabbitCracker.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace HabbitCracker.Model.Contexts
{
    internal class HabbitContext : DbContext
    {
        public static readonly Lazy<HabbitContext> Instance = new(() => new HabbitContext());

        public static HabbitContext GetInstance()
        {
            return Instance.Value;
        }

        public ObservableCollection<Habbit> Habbits = new();

        private readonly CourseworkDbContext _dbContext;

        public HabbitContext()
        {
            _dbContext = CourseworkDbContext.GetInstance();
            Habbits = GetHabbits();
        }

        public void GetCurrentContext()
        {
        }

        private ObservableCollection<Habbit> GetHabbits()
        {
            var habbits = new ObservableCollection<Habbit>();

            foreach (var item in _dbContext.Habbits.Where(p =>
                p.Userid == UserContext.GetInstance().UserPerson.Id).ToList())
            {
                habbits.Add(item);
            }
            return habbits;
        }
    }
}