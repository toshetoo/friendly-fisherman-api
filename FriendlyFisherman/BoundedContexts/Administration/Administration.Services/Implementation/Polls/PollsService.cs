using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Administration.Domain.Entities.Polls;
using Administration.Domain.EntityViewModels.Polls;
using Administration.Domain.Repositories.Polls;
using Administration.Services.Abstraction.Polls;
using FriendlyFisherman.SharedKernel.Services.Impl;
using FriendlyFisherman.SharedKernel.Services.Models;

namespace Administration.Services.Implementation.Polls
{
    public class PollsService: BaseCrudService<Poll, IPollsRepository>, IPollsService
    {
        private readonly IUserPollAnswersRepository _pollAnswersRepository;
        public PollsService(IPollsRepository repo, IUserPollAnswersRepository pollAnswersRepository) : base(repo)
        {
            _pollAnswersRepository = pollAnswersRepository;
        }

        public async Task<ServiceResponseBase<UserPollAnswerViewModel>> AddUserVoteAsync(ServiceRequestBase<UserPollAnswerViewModel> request)
        {
            return await Task.Run(() => AddUserVote(request));
        }

        private ServiceResponseBase<UserPollAnswerViewModel> AddUserVote(ServiceRequestBase<UserPollAnswerViewModel> request)
        {
            var response = new ServiceResponseBase<UserPollAnswerViewModel>();


            try
            {
                var pollResp = new UserPollAnswer
                {
                    Id = request.Item.Id,
                    UserId = request.Item.UserId,
                    AnswerIndex = request.Item.AnswerIndex,
                    PollId = request.Item.PollId
                };
                _pollAnswersRepository.Create(pollResp);
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }
    }
}
