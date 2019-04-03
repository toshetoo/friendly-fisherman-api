using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel.Messages;
using Users.Domain.Entities;
using Users.Domain.EntityViewModels.PersonalMessage;
using Users.Domain.Repositories;
using Users.Services.Abstraction;
using Users.Services.Request.PersonalMessage;
using Users.Services.Response.PersonalMessage;

namespace Users.Services.Implementation
{
    public class PersonalMessagesService: IPersonalMessagesService
    {
        private readonly IPersonalMessagesRepository _repo;

        public PersonalMessagesService(IPersonalMessagesRepository repo)
        {
            _repo = repo;
        }

        public async Task<GetAllMessagesResponse> GetAllMessagesBySenderIdAsync(GetAllMessagesRequest request)
        {
            return await Task.Run(() => GetAllMessagesBySenderId(request));
        }

        private GetAllMessagesResponse GetAllMessagesBySenderId(GetAllMessagesRequest request)
        {
            var response = new GetAllMessagesResponse();
            try
            {
                if (String.IsNullOrWhiteSpace(request.SenderId))
                    throw new Exception(ErrorMessages.InvalidId);

                var result = _repo.GetAllMessagesBySenderId(request.SenderId);
                response.Messages = result.Select(m => new PersonalMessageViewModel(m));
            }
            catch (Exception e)
            {
                response.Exception = e;
            }
            

            return response;
        }

        public async Task<GetAllMessagesResponse> GetAllMessagesByReceiverIdAsync(GetAllMessagesRequest request)
        {
            return await Task.Run(() => GetAllMessagesByReceiverId(request));
        }

        private GetAllMessagesResponse GetAllMessagesByReceiverId(GetAllMessagesRequest request)
        {
            var response = new GetAllMessagesResponse();
            try
            {
                if (String.IsNullOrWhiteSpace(request.ReceiverId))
                    throw new Exception(ErrorMessages.InvalidId);

                var result = _repo.GetAllMessagesByReceiverId(request.ReceiverId);
                response.Messages = result.Select(m => new PersonalMessageViewModel(m));
            }
            catch (Exception e)
            {
                response.Exception = e;
            }


            return response;
        }

        public async Task<GetMessageResponse> GetMessageByIdAsync(GetMessagesRequest request)
        {
            return await Task.Run(() => GetMessageById(request));
        }

        private GetMessageResponse GetMessageById(GetMessagesRequest request)
        {
            var response = new GetMessageResponse();
            try
            {
                if (String.IsNullOrWhiteSpace(request.MessageId))
                    throw new Exception(ErrorMessages.InvalidId);

                var result = _repo.GetMessageById(request.MessageId);
                response.Message = new PersonalMessageViewModel(result);
            }
            catch (Exception e)
            {
                response.Exception = e;
            }


            return response;
        }

        public async Task<EditMessageResponse> SaveMessageAsync(EditMessageRequest request)
        {
            return await Task.Run(() => SaveMessage(request));
        }

        private EditMessageResponse SaveMessage(EditMessageRequest request)
        {
            var response = new EditMessageResponse();

            try
            {
                var message = new PersonalMessage
                {
                    Id = request.Message.Id,
                    ReceiverId = request.Message.ReceiverId,
                    SenderId = request.Message.SenderId,
                    Seen = request.Message.Seen,
                    SentOn = request.Message.SentOn,
                    Content = request.Message.Content,
                    Title = request.Message.Title
                };

                _repo.SaveMessage(message);

            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }

        public async Task<EditMessageResponse> MarkMessageAsReadAsync(EditMessageRequest request)
        {
            return await Task.Run(() => MarkMessageAsRead(request));
        }

        private EditMessageResponse MarkMessageAsRead(EditMessageRequest request)
        {
            var response = new EditMessageResponse();

            try
            {
                if (String.IsNullOrWhiteSpace(request.Message.Id))
                    throw new Exception(ErrorMessages.InvalidId);

                var message = _repo.GetMessageById(request.Message.Id);
                message.Seen = request.Message.Seen;

                _repo.SaveMessage(message);

            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }

        public async Task<GetMessageResponse> DeleteMessageAsync(GetMessagesRequest request)
        {
            return await Task.Run(() => DeleteMessage(request));
        }

        private GetMessageResponse DeleteMessage(GetMessagesRequest request)
        {
            var response = new GetMessageResponse();

            try
            {
                if (String.IsNullOrWhiteSpace(request.MessageId))
                    throw new Exception(ErrorMessages.InvalidId);

                _repo.DeleteMessage(request.MessageId);
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }
    }
}
