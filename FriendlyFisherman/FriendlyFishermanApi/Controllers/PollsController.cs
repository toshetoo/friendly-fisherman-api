using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Administration.Domain.Entities.Polls;
using Administration.Domain.EntityViewModels.Polls;
using Administration.Services.Abstraction.Polls;
using FriendlyFisherman.SharedKernel.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FriendlyFishermanApi.Controllers
{
    public class PollsController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly IPollsService _service;

        public PollsController(ILogger logger, IPollsService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllPolls()
        {
            var response = await _service.GetAllAsync(new ServiceRequestBase<Poll>());

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
            var response = await _service.GetByIdAsync(new ServiceRequestBase<Poll>()
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
        [Route("Save")]
        public async Task<IActionResult> SavePoll([FromBody] PollViewModel model)
        {
            var response = await _service.SaveAsync(new ServiceRequestBase<Poll>()
            {
                Item = new Poll()
                {
                    Id = model.Id,
                    CreatedOn = model.CreatedOn,
                    Answers = model.Answers,
                    CreatedBy = model.CreatedBy,
                    EndOn = model.EndOn,
                    Question = model.Question
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
        [Route("AddPollVote")]
        public async Task<IActionResult> AddPollVote([FromBody] UserPollAnswerViewModel model)
        {
            var response = await _service.AddUserVoteAsync(new ServiceRequestBase<UserPollAnswerViewModel>()
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

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _service.DeleteAsync(new ServiceRequestBase<Poll>()
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
    }
}