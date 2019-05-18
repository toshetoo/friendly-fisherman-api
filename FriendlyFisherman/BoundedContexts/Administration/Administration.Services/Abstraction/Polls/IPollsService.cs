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
    public interface IPollsService: IBaseCrudService<PollViewModel, Poll>
    {
        Task<ServiceResponseBase<UserPollAnswerViewModel>> AddUserVoteAsync(ServiceRequestBase<UserPollAnswerViewModel> request);

        Task<ServiceResponseBase<UserPollAnswerViewModel>> GetVotedAnswerForPollAsync(
            ServiceRequestBase<UserPollAnswerViewModel> request);

        Task<ServiceResponseBase<PollViewModel>> MakePollOfTheWeekAsync(ServiceRequestBase<PollViewModel> request);

        Task<ServiceResponseBase<PollViewModel>> GetPollOfTheWeekAsync(ServiceRequestBase<PollViewModel> request);
    }
}
