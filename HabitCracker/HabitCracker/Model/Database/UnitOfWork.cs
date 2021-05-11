using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabitCracker.Model.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HabitCracker.Model.Database
{
    internal class UnitOfWork
    {
        private HabitContext db = new HabitContext();
        private HabitRepository HabitRepository;
        private UserRepository userRepository;

        public HabitRepository Books
        {
            get
            {
                if (HabitRepository == null)
                    HabitRepository = new HabitRepository(db);
                return HabitRepository;
            }
        }

        public UserRepository Orders
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}