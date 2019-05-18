using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Administration.Domain.Entities.Polls;
using Administration.Domain.EntityViewModels.Polls;
using Administration.Domain.Repositories.Polls;
using Administration.Services.Abstraction.Polls;
using FriendlyFisherman.SharedKernel.Helpers;
using FriendlyFisherman.SharedKernel.Messages;
using FriendlyFisherman.SharedKernel.Services.Impl;
using FriendlyFisherman.SharedKernel.Services.Models;

namespace Administration.Services.Implementation.Polls
{
    public class PollsService: BaseCrudService<PollViewModel, Poll, IPollsRepository>, IPollsService
    {
        private readonly IUserPollAnswersRepository _pollAnswersRepository;
        public PollsService(IPollsRepository repo, IUserPollAnswersRepository pollAnswersRepository) : base(repo)
        {
            _pollAnswersRepository = pollAnswersRepository;
        }

        protected override ServiceResponseBase<PollViewModel> GetById(ServiceRequestBase<Poll> request)
        {
            var response = new ServiceResponseBase<PollViewModel>();
            try
            {
                if (string.IsNullOrEmpty(request.ID))
                    throw new Exception(ErrorMessages.InvalidId);

                response.Item = new PollViewModel(_repo.Get(item => item.Id == request.ID, item => item.Answers));


                var answers = _pollAnswersRepository
                    .GetWhere(a => a.PollId == response.Item.Id);
                    
                    
                var grouped = answers.GroupBy(a => a.AnswerId)
                    .ToDictionary(e => e.Key, e => e.Count());

                for (int i = 0; i < response.Item.Answers.Count; i++)
                {
                    var answer = response.Item.Answers[i];

                    answer.Percentage = (Convert.ToDouble(grouped[answer.Id]) / answers.Count()) * 100;
                }
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
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
                // if we have old vote, remove it
                var oldVote = _pollAnswersRepository.Get(ans =>
                    ans.PollId == request.Item.PollId && ans.UserId == request.Item.UserId);

                if (oldVote != null)
                {
                    _pollAnswersRepository.Delete(oldVote);
                }

                var pollResp = new UserPollAnswer
                {
                    Id = request.Item.Id,
                    UserId = request.Item.UserId,
                    AnswerId = request.Item.AnswerId,
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

        public async Task<ServiceResponseBase<UserPollAnswerViewModel>> GetVotedAnswerForPollAsync(ServiceRequestBase<UserPollAnswerViewModel> request)
        {
            return await Task.Run(() => GetVotedAnswerForPoll(request));
        }

        private ServiceResponseBase<UserPollAnswerViewModel> GetVotedAnswerForPoll(ServiceRequestBase<UserPollAnswerViewModel> request)
        {
            var response = new ServiceResponseBase<UserPollAnswerViewModel>();

            try
            {
                var answer = _pollAnswersRepository.Get(ans =>
                    ans.PollId == request.Item.PollId && ans.UserId == request.Item.UserId);

                if (answer != null)
                    response.Item = Mapper<UserPollAnswerViewModel,UserPollAnswer>.Map(answer);
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }

        public async Task<ServiceResponseBase<PollViewModel>> MakePollOfTheWeekAsync(ServiceRequestBase<PollViewModel> request)
        {
            return await Task.Run(() => MakePollOfTheWeek(request));
        }

        private ServiceResponseBase<PollViewModel> MakePollOfTheWeek(ServiceRequestBase<PollViewModel> request)
        {
            var response = new ServiceResponseBase<PollViewModel>();

            try
            {
                var prevPollOTW = _repo.Get(poll => poll.IsPollOfTheWeek);
                if (prevPollOTW != null)
                {
                    prevPollOTW.IsPollOfTheWeek = false;
                    _repo.Update(prevPollOTW);
                }

                var newPOTW = _repo.Get(poll => poll.Id == request.ID);
                newPOTW.IsPollOfTheWeek = true;

                _repo.Update(newPOTW);
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }

        public async Task<ServiceResponseBase<PollViewModel>> GetPollOfTheWeekAsync(ServiceRequestBase<PollViewModel> request)
        {
            return await Task.Run(() => GetPollOfTheWeek(request));
        }

        private ServiceResponseBase<PollViewModel> GetPollOfTheWeek(ServiceRequestBase<PollViewModel> request)
        {
            var response = new ServiceResponseBase<PollViewModel>();

            try
            {
                var poll = _repo.Get(p => p.IsPollOfTheWeek);

                if (poll == null)
                {
                    response.Item = null;
                    return response;
                }

                response.Item = GetById(new ServiceRequestBase<Poll>() {ID = poll.Id}).Item;
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }
    }
}
