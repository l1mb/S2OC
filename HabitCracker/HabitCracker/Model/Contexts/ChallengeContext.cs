using HabitCracker.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;

namespace HabitCracker.Model.Contexts
{
    internal class ChallengeContext
    {
        private static readonly Lazy<ChallengeContext> Instance = new(() => new ChallengeContext());

        public static ChallengeContext GetInstance()
        {
            return Instance.Value;
        }

        public readonly CoolerContext _dbContext;

        public ChallengeContext()
        {
            _dbContext = CoolerContext.GetInstance();
        }

        public ObservableCollection<Challenge> GetChallenges()
        {
            var challenges = new ObservableCollection<Challenge>();
            foreach (var challenge in _dbContext.Challenges.Include(p => p.Challengers).Include(p => p.Events))
            {
                challenges.Add(challenge);
            }
            return challenges;
        }
    }
}