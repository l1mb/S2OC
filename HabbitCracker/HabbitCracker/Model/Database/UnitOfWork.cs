using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabbitCracker.Model.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HabbitCracker.Model.Database
{
    internal class UnitOfWork
    {
        private HabbitContext db = new HabbitContext();
        private HabbitRepository habbitRepository;
        private UserRepository userRepository;

        public HabbitRepository Books
        {
            get
            {
                if (habbitRepository == null)
                    habbitRepository = new HabbitRepository(db);
                return habbitRepository;
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