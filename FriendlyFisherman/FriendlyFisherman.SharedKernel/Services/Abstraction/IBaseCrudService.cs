using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FriendlyFisherman.SharedKernel.Services.Abstraction
{
    public interface IBaseCrudService<T>
    {
        Task<ServiceResponseBase<T>> GetByIdAsync(ServiceRequestBase<T> request);
        Task<ServiceResponseBase<T>> GetAllAsync(ServiceRequestBase<T> request);
        Task<ServiceResponseBase<T>> SaveAsync(ServiceRequestBase<T> request);
        Task<ServiceResponseBase<T>> DeleteAsync(ServiceRequestBase<T> request);
    }
}
