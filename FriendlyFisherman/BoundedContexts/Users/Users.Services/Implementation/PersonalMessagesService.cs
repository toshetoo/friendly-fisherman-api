using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel.Messages;
using Microsoft.AspNetCore.Identity;
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
        private readonly IUserRepository _usersUserRepository;

        public PersonalMessagesService(IPersonalMessagesRepository repo, IUserRepository usersUserRepository)
        {
            _repo = repo;
            _usersUserRepository = usersUserRepository;
        }

        public async Task<GetNewMessagesCountResponse> GetNewMessagesCountAsync(GetNewMessagesCountRequest request)
        {
            return await Task.Run(() => GetNewMessagesCount(request));
        }

        /// <summary>
        /// Retrieves all messages for a specific sender and maps them to the ViewModel
        /// </summary>
        /// <param name="request">The attributes used to filter the messages</param>
        /// <returns>A Response object containing the list of messaged and/or an exception if one occured</returns>
        private GetNewMessagesCountResponse GetNewMessagesCount(GetNewMessagesCountRequest request)
        {
            var response = new GetNewMessagesCountResponse();
            try
            {
                if (String.IsNullOrWhiteSpace(request.ReceiverId))
                    throw new Exception(ErrorMessages.InvalidId);

                response.NumberOfNewMessages = _repo.GetNewMessagesCount(request.ReceiverId);
            }
            catch (Exception e)
            {
                response.Exception = e;
            }


            return response;
        }

        public async Task<GetAllMessagesResponse> GetAllMessagesBySenderIdAsync(GetAllMessagesRequest request)
        {
            return await Task.Run(() => GetAllMessagesBySenderId(request));
        }

        /// <summary>
        /// Retrieves all messages for a specific sender and maps them to the ViewModel
        /// </summary>
        /// <param name="request">The attributes used to filter the messages</param>
        /// <returns>A Response object containing the list of messaged and/or an exception if one occured</returns>
        private GetAllMessagesResponse GetAllMessagesBySenderId(GetAllMessagesRequest request)
        {
            var response = new GetAllMessagesResponse();
            try
            {
                if (String.IsNullOrWhiteSpace(request.SenderId))
                    throw new Exception(ErrorMessages.InvalidId);

                var result = _repo.GetAllMessagesBySenderId(request.SenderId);
                var messages = result.Select(m => new PersonalMessageViewModel(m)).ToList();

                foreach (var message in messages)
                {
                    var receiver = _usersUserRepository.GetById(message.ReceiverId);
                    message.ReceiverName = $"{receiver.FirstName} {receiver.LastName}";

                    var sender = _usersUserRepository.GetById(message.SenderId);
                    message.SenderName = $"{sender.FirstName} {sender.LastName}";
                }

                response.Items = messages;
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

        /// <summary>
        /// Retrieves all messages for a specific receiver and maps them to the ViewModel
        /// </summary>
        /// <param name="request">The attributes used to filter the messages</param>
        /// <returns>A Response object containing the list of messaged and/or an exception if one occured</returns>
        private GetAllMessagesResponse GetAllMessagesByReceiverId(GetAllMessagesRequest request)
        {
            var response = new GetAllMessagesResponse();
            try
            {
                if (String.IsNullOrWhiteSpace(request.ReceiverId))
                    throw new Exception(ErrorMessages.InvalidId);

                var result = _repo.GetAllMessagesByReceiverId(request.ReceiverId);
                var messages = result.Select(m => new PersonalMessageViewModel(m)).ToList();

                foreach (var message in messages)
                {
                    var receiver = _usersUserRepository.GetById(message.ReceiverId);
                    message.ReceiverName = $"{receiver.FirstName} {receiver.LastName}";

                    var sender = _usersUserRepository.GetById(message.SenderId);
                    message.SenderName = $"{sender.FirstName} {sender.LastName}";
                }

                response.Items = messages;
            }
            catch (Exception e)
            {
                response.Exception = e;
            }


            return response;
        }

        public async Task<GetAllMessagesResponse> GetMessageThreadAsync(GetMessagesRequest request)
        {
            return await Task.Run(() => GetMessageThread(request));
        }

        private GetAllMessagesResponse GetMessageThread(GetMessagesRequest request)
        {
            var response = new GetAllMessagesResponse();

            try
            {
                if (String.IsNullOrWhiteSpace(request.SenderId))
                    throw new Exception(ErrorMessages.InvalidId);

                if (String.IsNullOrWhiteSpace(request.ReceiverId))
                    throw new Exception(ErrorMessages.InvalidId);

                response.Items = _repo.GetAllMessagesBySenderIdAndReceiverId(request.SenderId, request.ReceiverId)
                    .OrderByDescending(m => m.SentOn)
                    .Select(m => new PersonalMessageViewModel(m))
                    .ToList();

                foreach (var message in response.Items)
                {
                    var receiver = _usersUserRepository.GetById(message.ReceiverId);
                    message.ReceiverName = receiver.FirstName + receiver.LastName;

                    var sender = _usersUserRepository.GetById(message.SenderId);
                    message.SenderName = sender.FirstName + sender.LastName;
                }
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

        /// <summary>
        /// Retrieves a single message and maps it to the ViewModel
        /// </summary>
        /// <param name="request">The attributes used to filter the messages</param>
        /// <returns>A Response object containing the list of messaged and/or an exception if one occured</returns>
        private GetMessageResponse GetMessageById(GetMessagesRequest request)
        {
            var response = new GetMessageResponse();
            try
            {
                if (String.IsNullOrWhiteSpace(request.MessageId))
                    throw new Exception(ErrorMessages.InvalidId);

                var result = _repo.GetMessageById(request.MessageId);
                response.Item = new PersonalMessageViewModel(result);
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

        /// <summary>
        /// Modifies an existing message or creates a new one
        /// </summary>
        /// <param name="request">
        /// The message information that should be handled. If an ID exist in
        /// this object, the message will be modified otherwise a new message will be created
        /// </param>
        /// <returns>An exception if the message data is invalid</returns>
        private EditMessageResponse SaveMessage(EditMessageRequest request)
        {
            var response = new EditMessageResponse();

            try
            {
                if (!string.IsNullOrEmpty(request.Message.Id))
                {
                    var m = _repo.GetMessageById(request.Message.Id);
                    if (ReferenceEquals(m, null))
                        throw new Exception($"There is no message with Id: {request.Message.Id}");
                }
                

                var message = new PersonalMessage
                {
                    Id = request.Message.Id,
                    ReceiverId = request.Message.ReceiverId,
                    SenderId = request.Message.SenderId,
                    Seen = request.Message.Seen,
                    SentOn = DateTime.UtcNow,
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

        /// <summary>
        /// Marks a message as read or unread depending on the passed flag
        /// </summary>
        /// <param name="request">The ID of the message and a flag weather it should be marked as read or unread</param>
        /// <returns>An exception if the message data is invalid</returns>
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

        /// <summary>
        /// Deletes a message from the DB
        /// </summary>
        /// <param name="request">A request object containing the ID of the message that should be deleted</param>
        /// <returns>An exception if the message could not be removed</returns>
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
