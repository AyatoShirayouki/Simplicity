using Simplicity.Entities;
using Simplicity.Repositories;
using Simplicity.Repositories.Repositories;
using Simplicity.Repositories.RepositoryInterfaces;
using Simplicity.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Simplicity.Services.Services
{
    public abstract class BaseService<T> : IBaseService<T> where T : BaseEntitity, new()
    {
        private readonly IBaseRepository<T> _baseRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public BaseService(IBaseRepository<T> baseRepository, IUnitOfWork unitOfWork)
        {
            _baseRepository = baseRepository;
            _unitOfWork = unitOfWork;
        }

        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public List<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            return (List<T>)_baseRepository.GetAll(filter);
        }

        public T GetById(int id)
        {
            return _baseRepository.GetById(id);
        }

        public bool Save(T item)
        {
            try
            {
                if (item.ID > 0)
                {
                    OnBeforeEdit(item);
                }
                else
                {
                    OnBeforeAdd(item);
                }

                _baseRepository.Save(item);
                //_unitOfWork.Commit();
                return true;
            }
            catch (Exception e)
            {
                //_unitOfWork.RollBack();
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                _baseRepository.Delete(id);
                //_unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                //_unitOfWork.RollBack();
                return false;
            }
        }

        public virtual void OnBeforeEdit(T item)
        {

        }

        public virtual void OnBeforeAdd(T item)
        {

        }

    }
}
