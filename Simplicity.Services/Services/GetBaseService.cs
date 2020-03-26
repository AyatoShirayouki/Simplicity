using Simplicity.Entities;
using Simplicity.Repositories;
using Simplicity.Repositories.RepositoryInterfaces;
using Simplicity.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Simplicity.Services.Services
{
    public abstract class GetBaseService<T> : IGetBaseService<T> where T : BaseEntitity, new()
    {
        private readonly IBaseRepository<T> _baseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetBaseService(IBaseRepository<T> baseRepository, IUnitOfWork unitOfWork)
        {
            _baseRepository = baseRepository;
            _unitOfWork = unitOfWork;
        }

        public GetBaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public T GetById(int id)
        {
            return _baseRepository.GetById(id);
        }

    }
}
