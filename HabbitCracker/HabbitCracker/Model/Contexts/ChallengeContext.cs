﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabbitCracker.Model.Entities;
using HabbitCracker.View.Menu;
using Microsoft.EntityFrameworkCore;

namespace HabbitCracker.Model.Contexts
{
    internal class ChallengeContext : DbContext
    {
        public static readonly Lazy<ChallengeContext> Instance = new(() => new ChallengeContext());

        public static ChallengeContext GetInstance()
        {
            return Instance.Value;
        }

        public readonly CourseworkDbContext _dbContext;

        public ChallengeContext()
        {
            _dbContext = CourseworkDbContext.GetInstance();
        }

        public ObservableCollection<Challenge> getChallenges()
        {
            var challenges = new ObservableCollection<Challenge>();
            foreach (var challenge in _dbContext.Challenges)
            {
                challenges.Add(challenge);
            }
            return challenges;
        }
    }
}