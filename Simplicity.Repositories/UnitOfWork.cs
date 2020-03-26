using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Simplicity.Entities;
using Simplicity.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Simplicity.Repositories
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        public SimplicityContext Context { get; private set; }
        private IDbContextTransaction transaction = null;

        public UnitOfWork(SimplicityContext context)
        {
            this.Context = context;
            this.transaction = Context.Database.BeginTransaction();
        }

        public void Commit()
        {
            if (transaction != null)
            {
                this.transaction.Commit();
                this.transaction = null;
            }
        }

        public void RollBack()
        {
            if (this.transaction != null)
            {
                this.transaction.Rollback();
                this.transaction = null;
            }
        }

        public void Dispose()
        {
            Commit();
            Context.Dispose();
        }
    }
}
