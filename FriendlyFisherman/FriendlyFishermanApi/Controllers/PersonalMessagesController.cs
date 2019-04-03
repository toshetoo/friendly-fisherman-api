﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Users.Domain.EntityViewModels.PersonalMessage;
using Users.Services.Abstraction;
using Users.Services.Request;
using Users.Services.Request.PersonalMessage;

namespace FriendlyFishermanApi.Controllers
{
    [Authorize("Bearer")]
    public class PersonalMessagesController : BaseApiController
    {
        private readonly IPersonalMessagesService _service;
        private readonly ILogger _logger;

        public PersonalMessagesController(IPersonalMessagesService service, ILogger logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(string messageId)
        {
            var response = await _service.GetMessageByIdAsync(new GetMessagesRequest
            {
                MessageId = messageId
            });

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }

            _logger.LogError(response.Exception, response.Exception.Message);
            return StatusCode(500, new ErrorResponse(response.Exception.Message));
        }

        [HttpGet]
        [Route("GetAllForReceiver")]
        public async Task<IActionResult> GetAllForReceiver(string receiverId)
        {
            var response = await _service.GetAllMessagesByReceiverIdAsync(new GetAllMessagesRequest
            {
                ReceiverId = receiverId
            });

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }

            _logger.LogError(response.Exception, response.Exception.Message);
            return StatusCode(500, new ErrorResponse(response.Exception.Message));
        }

        [HttpGet]
        [Route("GetAllForSender")]
        public async Task<IActionResult> GetAllForSender(string senderId)
        {
            var response = await _service.GetAllMessagesBySenderIdAsync(new GetAllMessagesRequest
            {
                SenderId = senderId
            });

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }

            _logger.LogError(response.Exception, response.Exception.Message);
            return StatusCode(500, new ErrorResponse(response.Exception.Message));
        }

        [HttpPost]
        [Route("SaveMessage")]
        public async Task<IActionResult> SaveMessage([FromBody] PersonalMessageViewModel model)
        {
            var response = await _service.SaveMessageAsync(new EditMessageRequest(model));

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }

            _logger.LogError(response.Exception, response.Exception.Message);
            return StatusCode(500, new ErrorResponse(response.Exception.Message));
        }

        [HttpPost]
        [Route("MarkAsRead")]
        public async Task<IActionResult> MarkAsRead([FromBody] PersonalMessageViewModel model)
        {
            var response = await _service.MarkMessageAsReadAsync(new EditMessageRequest(model));

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }

            _logger.LogError(response.Exception, response.Exception.Message);
            return StatusCode(500, new ErrorResponse(response.Exception.Message));
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(string messageId)
        {
            var response = await _service.DeleteMessageAsync(new GetMessagesRequest { MessageId = messageId });

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }

            _logger.LogError(response.Exception, response.Exception.Message);
            return StatusCode(500, new ErrorResponse(response.Exception.Message));
        }
    }
}