using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel.Helpers;
using FriendlyFisherman.SharedKernel.Messages;
using FriendlyFisherman.SharedKernel.Repositories.Abstraction;
using FriendlyFisherman.SharedKernel.Services.Abstraction;
using FriendlyFisherman.SharedKernel.Services.Models;

namespace FriendlyFisherman.SharedKernel.Services.Impl
{
    public class BaseCrudService<TR, TE, T> : IBaseCrudService<TR, TE> where T : IBaseRepository<TE> where TE : BaseEntity where TR: class, new()
    {
        protected readonly T _repo;

        public BaseCrudService(T repo)
        {
            _repo = repo;
        }

        public async Task<ServiceResponseBase<TR>> DeleteAsync(ServiceRequestBase<TE> request)
        {
            return await Task.Run(() => Delete(request));
        }

        protected virtual ServiceResponseBase<TR> Delete(ServiceRequestBase<TE> request)
        {
            var response = new ServiceResponseBase<TR>();
            try
            {
                if(string.IsNullOrEmpty(request.ID))
                    throw new Exception(ErrorMessages.InvalidId);

                _repo.Delete(item => item.Id == request.ID);
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        } 

        public async Task<ServiceResponseBase<TR>> GetAllAsync(ServiceRequestBase<TE> request)
        {
            return await Task.Run(() => GetAll(request));
        }

        protected virtual ServiceResponseBase<TR> GetAll(ServiceRequestBase<TE> request)
        {
            var response = new ServiceResponseBase<TR>();
            try
            {
                response.Items = Mapper<TR, TE>.MapList(_repo.GetAll().ToList());
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }

        public async Task<ServiceResponseBase<TR>> GetByIdAsync(ServiceRequestBase<TE> request)
        {
            return await Task.Run(() => GetById(request));
        }

        protected virtual ServiceResponseBase<TR> GetById(ServiceRequestBase<TE> request)
        {
            var response = new ServiceResponseBase<TR>();
            try
            {
                if (string.IsNullOrEmpty(request.ID))
                    throw new Exception(ErrorMessages.InvalidId);

                var a = _repo.Get(item => item.Id == request.ID);
                response.Item = Mapper<TR, TE>.Map(a);
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }

        public async Task<ServiceResponseBase<TR>> SaveAsync(ServiceRequestBase<TE> request)
        {
            return await Task.Run(() => Save(request));
        }

        protected virtual ServiceResponseBase<TR> Save(ServiceRequestBase<TE> request)
        {
            var response = new ServiceResponseBase<TR>();
            try
            {
                if (string.IsNullOrEmpty(request.Item.Id))
                {
                    _repo.Create(request.Item);
                }
                else
                {
                    _repo.Update(request.Item);
                }
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }
    }
}
