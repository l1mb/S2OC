using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabbitCracker.Model.Entities;

namespace HabbitCracker.ViewModel
{
    internal class DataBaseViewModel : BaseViewModel
    {
        public Model.Entities.Auth AuthObject = new();
        public Model.Entities.Challenge ChallengeObject = new();
        public Model.Entities.Event EventObject = new();
        public Model.Entities.Habbit HabbitObject = new();
        public Model.Entities.Person PersonObject = new();
        public Model.Entities.Wallet WalletObject = new();
    }
}