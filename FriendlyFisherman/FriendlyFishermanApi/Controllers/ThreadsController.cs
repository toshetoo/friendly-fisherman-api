using System;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel;
using FriendlyFisherman.SharedKernel.Reports;
using FriendlyFisherman.SharedKernel.Requests.Images;
using FriendlyFisherman.SharedKernel.Services.Models;
using FriendlyFisherman.SharedKernel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Publishing.Domain.Entities.Threads;
using Publishing.Domain.EntityViewModels.Categories;
using Publishing.Domain.EntityViewModels.Threads;
using Publishing.Services.Abstraction.Reports;
using Publishing.Services.Abstraction.Threads;
using Publishing.Services.Request.Threads;
using Users.Domain.EntityViewModels;
using Users.Domain.EntityViewModels.User;
using Users.Services.Request;

namespace FriendlyFishermanApi.Controllers
{
    [Authorize("Bearer")]
    public class ThreadsController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly IThreadsService _service;

        public ThreadsController(ILogger<ThreadsController> logger, IThreadsService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [Route("GetAll")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllThreads()
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
        [AllowAnonymous]
        public async Task<IActionResult> GetById(string id, string userId)
        {
            var response = await _service.GetByIdAsync(new GetByIdRequest()
            {
                ID = id,
                UserId = userId
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
        public async Task<IActionResult> SaveThread([FromBody] ThreadViewModel model)
        {
            var response = await _service.SaveAsync(new ServiceRequestBase<Thread>()
            {
                Item = new Thread()
                {
                    Id = model.Id,
                    AuthorId = model.AuthorId,
                    CategoryId = model.CategoryId,
                    CreatedOn = model.CreatedOn,
                    Content = model.Content,
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
        [Route("LikeReply")]
        public async Task<IActionResult> LikeReply([FromBody] LikeViewModel model)
        {
            var response = await _service.LikeReplyAsync(new ServiceRequestBase<LikeViewModel>()
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
        public async Task<IActionResult> MarkAsSeen([FromBody] SeenViewModel model)
        {
            var response = await _service.MarkAsSeenAsync(new MarkThreadAsSeenRequest()
            {
                ThreadId = model.ThreadReplyId,
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
        public async Task<IActionResult> DeleteReply(string id)
        {
            var response = await _service.DeleteThreadReplyAsync(new ServiceRequestBase<ThreadReplyViewModel>()
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
        [Route("UploadImage")]
        public async Task<IActionResult> UploadImage(ImageUploadViewModel model)
        {
            var request = new UploadImageRequest(model);
            var response = await _service.UploadImageAsync(request);

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }

            _logger.LogError(response.Exception, response.Exception.Message);
            return StatusCode(500, new ErrorResponse(response.Exception.Message));
        }
    }
}