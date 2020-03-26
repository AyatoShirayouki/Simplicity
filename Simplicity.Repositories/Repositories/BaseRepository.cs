using Microsoft.EntityFrameworkCore;
using Simplicity.Entities;
using Simplicity.Repositories.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Simplicity.Repositories.Repositories
{

    public class BaseRepository<T> : IDisposable, IBaseRepository<T> where T : BaseEntitity
    {
        protected readonly SimplicityContext context;
        protected readonly DbSet<T> dbSet;
        protected IUnitOfWork unitOfWork;

        public BaseRepository(SimplicityContext context)
        {
            this.context = context;
            this.dbSet = this.context.Set<T>();
        }

        public BaseRepository(IUnitOfWork unitOfWork, SimplicityContext context)
        {
            this.unitOfWork = unitOfWork;
            this.context = context;
            this.dbSet = this.context.Set<T>();
        }

        public BaseRepository()
        {

        }

        public virtual List<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> result = dbSet;
            if (filter != null)
                return result.Where(filter).ToList();
            else
                return result.ToList();
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual void Save(T item)
        {
            if (item.ID == 0)
                Insert(item);
            else
                Update(item);
            this.context.SaveChanges();
        }

        private void Insert(T item)
        {
            this.dbSet.Add(item);
        }

        private void Update(T item)
        {
            this.context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            this.dbSet.Remove(GetById(id));
            this.context.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~BaseRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}