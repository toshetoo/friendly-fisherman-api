using System;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel.Messages;
using FriendlyFisherman.SharedKernel.Repositories.Abstraction;
using FriendlyFisherman.SharedKernel.Services.Abstraction;
using FriendlyFisherman.SharedKernel.Services.Models;

namespace FriendlyFisherman.SharedKernel.Services.Impl
{
    public class BaseCrudService<TE, T> : IBaseCrudService<TE> where T : IBaseRepository<TE> where TE : BaseEntity
    {
        protected readonly T _repo;

        public BaseCrudService(T repo)
        {
            _repo = repo;
        }

        public async Task<ServiceResponseBase<TE>> DeleteAsync(ServiceRequestBase<TE> request)
        {
            return await Task.Run(() => Delete(request));
        }

        protected virtual ServiceResponseBase<TE> Delete(ServiceRequestBase<TE> request)
        {
            var response = new ServiceResponseBase<TE>();
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

        public async Task<ServiceResponseBase<TE>> GetAllAsync(ServiceRequestBase<TE> request)
        {
            return await Task.Run(() => GetAll(request));
        }

        protected virtual ServiceResponseBase<TE> GetAll(ServiceRequestBase<TE> request)
        {
            var response = new ServiceResponseBase<TE>();
            try
            {
                response.Items = _repo.GetAll();
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }

        public async Task<ServiceResponseBase<TE>> GetByIdAsync(ServiceRequestBase<TE> request)
        {
            return await Task.Run(() => GetById(request));
        }

        protected virtual ServiceResponseBase<TE> GetById(ServiceRequestBase<TE> request)
        {
            var response = new ServiceResponseBase<TE>();
            try
            {
                if (string.IsNullOrEmpty(request.ID))
                    throw new Exception(ErrorMessages.InvalidId);

                response.Item = _repo.Get(item => item.Id == request.ID);
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }

        public async Task<ServiceResponseBase<TE>> SaveAsync(ServiceRequestBase<TE> request)
        {
            return await Task.Run(() => Save(request));
        }

        protected virtual ServiceResponseBase<TE> Save(ServiceRequestBase<TE> request)
        {
            var response = new ServiceResponseBase<TE>();
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
