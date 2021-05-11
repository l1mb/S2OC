using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabitCracker.Model.Entities;

namespace HabitCracker.ViewModel
{
    internal class DataBaseViewModel : BaseViewModel
    {
        public Model.Entities.Auth AuthObject { get; set; } = new();
        public Model.Entities.Challenge ChallengeObject { get; set; } = new();
        public Model.Entities.Event EventObject { get; set; } = new();
        public Model.Entities.Habit HabitObject { get; set; } = new();
        public Model.Entities.Person PersonObject { get; set; } = new();
        public Model.Entities.Wallet WalletObject { get; set; } = new();
    }
}