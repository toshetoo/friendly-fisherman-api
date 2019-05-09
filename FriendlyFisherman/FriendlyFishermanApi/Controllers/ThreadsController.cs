using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel;
using FriendlyFisherman.SharedKernel.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Publishing.Domain.Entities.Categories;
using Publishing.Domain.Entities.Threads;
using Publishing.Domain.EntityViewModels.Categories;
using Publishing.Domain.EntityViewModels.Threads;
using Publishing.Services.Abstraction.Categories;
using Publishing.Services.Abstraction.Threads;
using Publishing.Services.Request.Threads;

namespace FriendlyFishermanApi.Controllers
{
    [Authorize("Bearer")]
    public class ThreadsController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly IThreadsService _service;

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _service.GetAllAsync(new ServiceRequestBase<Thread>());

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }

            _logger.LogError(response.Exception, response.Exception.Message);
            return StatusCode(500, new ErrorResponse(response.Exception.Message));
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _service.GetByIdAsync(new ServiceRequestBase<Thread>()
            {
                ID = id
            });

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }

            _logger.LogError(response.Exception, response.Exception.Message);
            return StatusCode(500, new ErrorResponse(response.Exception.Message));
        }

        [HttpGet]
        [Route("GetSeenCount/{id}")]
        public async Task<IActionResult> GetSeenCount(string id)
        {
            var response = await _service.GetSeenCountAsync(new ServiceRequestBase<ThreadViewModel>()
            {
                ID = id
            });

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }

            _logger.LogError(response.Exception, response.Exception.Message);
            return StatusCode(500, new ErrorResponse(response.Exception.Message));
        }

        [HttpPost]
        [Route("SaveThread")]
        public async Task<IActionResult> SaveMessage([FromBody] ThreadViewModel model)
        {
            var response = await _service.SaveAsync(new ServiceRequestBase<Thread>()
            {
                Item = new Thread()
                {
                    Id = model.Id,
                    AuthorId = model.AuthorId,
                    CategoryId = model.CategoryId,
                    CreatedOn = model.CreatedOn,
                    Subtitle = model.Subtitle,
                    Title = model.Title
                }
            });

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }

            _logger.LogError(response.Exception, response.Exception.Message);
            return StatusCode(500, new ErrorResponse(response.Exception.Message));
        }

        [HttpPost]
        [Route("AddReply")]
        public async Task<IActionResult> AddReply([FromBody] ThreadReplyViewModel model)
        {
            var response = await _service.AddThreadReplyAsync(new ServiceRequestBase<ThreadReplyViewModel>()
            {
                Item = model
            });

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }

            _logger.LogError(response.Exception, response.Exception.Message);
            return StatusCode(500, new ErrorResponse(response.Exception.Message));
        }

        [HttpPost]
        [Route("EditReply")]
        public async Task<IActionResult> EditReply([FromBody] ThreadReplyViewModel model)
        {
            var response = await _service.EditThreadReplyAsync(new ServiceRequestBase<ThreadReplyViewModel>()
            {
                Item = model
            });

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }

            _logger.LogError(response.Exception, response.Exception.Message);
            return StatusCode(500, new ErrorResponse(response.Exception.Message));
        }

        [HttpPost]
        [Route("MarkAsSeen")]
        public async Task<IActionResult> MarkAsSeen([FromBody] ThreadReplyViewModel model)
        {
            var response = await _service.MarkAsSeenAsync(new MarkThreadAsSeenRequest()
            {
                ThreadId = model.ThreadId,
                ID = model.Id,
                IsSeen = true,
                SeenBy = model.UserId
            });

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }

            _logger.LogError(response.Exception, response.Exception.Message);
            return StatusCode(500, new ErrorResponse(response.Exception.Message));
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _service.DeleteAsync(new ServiceRequestBase<Thread>()
            {
                ID = id
            });

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }

            _logger.LogError(response.Exception, response.Exception.Message);
            return StatusCode(500, new ErrorResponse(response.Exception.Message));
        }

        [HttpDelete]
        [Route("DeleteReply/{id}")]
        public async Task<IActionResult> DeleteReply([FromBody] ThreadReplyViewModel model)
        {
            var response = await _service.DeleteThreadReplyAsync(new ServiceRequestBase<ThreadReplyViewModel>()
            {
               Item = model
            });

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }

            _logger.LogError(response.Exception, response.Exception.Message);
            return StatusCode(500, new ErrorResponse(response.Exception.Message));
        }
    }
}