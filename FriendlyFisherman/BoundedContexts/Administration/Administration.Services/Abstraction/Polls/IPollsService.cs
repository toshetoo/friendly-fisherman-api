using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Administration.Domain.Entities.Polls;
using Administration.Domain.EntityViewModels.Polls;
using FriendlyFisherman.SharedKernel.Services.Abstraction;
using FriendlyFisherman.SharedKernel.Services.Models;

namespace Administration.Services.Abstraction.Polls
{
    public interface IPollsService: IBaseCrudService<Poll>
    {
        Task<ServiceResponseBase<UserPollAnswerViewModel>> AddUserVoteAsync(ServiceRequestBase<UserPollAnswerViewModel> request);
    }
}
