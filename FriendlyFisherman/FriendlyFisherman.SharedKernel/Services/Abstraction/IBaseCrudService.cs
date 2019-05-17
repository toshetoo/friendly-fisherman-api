using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel.Services.Models;

namespace FriendlyFisherman.SharedKernel.Services.Abstraction
{
    public interface IBaseCrudService<TR, T>
    {
        Task<ServiceResponseBase<TR>> GetByIdAsync(ServiceRequestBase<T> request);
        Task<ServiceResponseBase<TR>> GetAllAsync(ServiceRequestBase<T> request);
        Task<ServiceResponseBase<TR>> SaveAsync(ServiceRequestBase<T> request);
        Task<ServiceResponseBase<TR>> DeleteAsync(ServiceRequestBase<T> request);
    }
}
