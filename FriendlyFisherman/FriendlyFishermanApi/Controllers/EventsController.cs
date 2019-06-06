using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Administration.Domain.Entities;
using Administration.Domain.Entities.Events;
using Administration.Domain.EntityViewModels.Events;
using Administration.Services.Abstraction.Events;
using Administration.Services.Request;
using FriendlyFisherman.SharedKernel.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EventParticipantViewModel = Administration.Domain.EntityViewModels.Events.EventParticipantViewModel;

namespace FriendlyFishermanApi.Controllers
{
    [Authorize("Bearer")]
    public class EventsController : BaseApiController
    {
        private readonly IEventsService _service;
        private readonly ILogger _logger;

        public EventsController(IEventsService eventsService, ILogger<EventsController> logger)
        {
            _service = eventsService;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllEvents()
        {
            var response = await _service.GetAllAsync(new ServiceRequestBase<Event>());

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }

            _logger.LogError(response.Exception, response.Exception.Message);
            return StatusCode(500, new ErrorResponse(response.Exception.Message));
        }

        [HttpGet]
        [Route("GetLatestEvents")]
        public async Task<IActionResult> GetLatestEvents()
        {
            var response = await _service.GetLatestEventsAsync(new ServiceRequestBase<EventViewModel>());

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
            var response = await _service.GetByIdAsync(new ServiceRequestBase<Event>()
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
        [Route("GetParticipantsForEvent/{id}")]
        public async Task<IActionResult> GetParticipantsForEvent(string id)
        {
            var response = await _service.GetParticipantsByEventIdAsync(new ServiceRequestBase<EventParticipantViewModel>()
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
        [Route("GetParticipantForEvent/{eventId}/{participantId}")]
        public async Task<IActionResult> GetParticipantForEvent(string eventId, string participantId)
        {
            var response = await _service.GetParticipantByEventIdAndUserIdAsync(new ServiceRequestBase<EventParticipantViewModel>()
            {
                Item = new EventParticipantViewModel()
                {
                    EventId = eventId,
                    UserId = participantId,
                }
            });

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }

            _logger.LogError(response.Exception, response.Exception.Message);
            return StatusCode(500, new ErrorResponse(response.Exception.Message));
        }

        [HttpGet]
        [Route("GetCommentsForEvent/{id}")]
        public async Task<IActionResult> GetCommentsForEvent(string id)
        {
            var response = await _service.GetCommentsByEventIdAsync(new ServiceRequestBase<EventCommentViewModel>()
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
        public async Task<IActionResult> Save([FromBody] EventViewModel model)
        {
            if (model.ImageName != null && model.ImageData != null)
            {
                var request = new SaveImageCoverRequest(model.ImageName, model.ImageData);
                var saveImageCoverResponse = await _service.SaveImageCoverAsync(request);

                if (ReferenceEquals(saveImageCoverResponse.Exception, null))
                {
                    model.ImageCover = saveImageCoverResponse.ImageCoverFilePath;
                }
            }

            var response = await _service.SaveAsync(new ServiceRequestBase<Event>()
            {
                Item = new Event()
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                    EndDate = model.EndDate,
                    EventStatus = model.EventStatus,
                    ImageCover = model.ImageCover,
                    StartDate = model.StartDate,
                    Lat = model.Lat,
                    Lng = model.Lng
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
        [Route("AddParticipant")]
        public async Task<IActionResult> AddParticipant([FromBody] EventParticipantViewModel model)
        {
            var response = await _service.AddParticipantAsync(new ServiceRequestBase<EventParticipantViewModel>()
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
        [Route("SaveComment")]
        public async Task<IActionResult> AddComment([FromBody] EventCommentViewModel model)
        {
            var response = await _service.AddCommentAsync(new ServiceRequestBase<EventCommentViewModel>()
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
            var response = await _service.DeleteAsync(new ServiceRequestBase<Event>()
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
        [Route("DeleteParticipant/{id}")]
        public async Task<IActionResult> DeleteReply([FromBody] EventParticipantViewModel model)
        {
            var response = await _service.DeleteParticipantAsync(new ServiceRequestBase<EventParticipantViewModel>()
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
        [Route("DeleteComment/{id}")]
        public async Task<IActionResult> DeleteComment(string Id)
        {
            var response = await _service.DeleteCommentAsync(new ServiceRequestBase<EventCommentViewModel>()
            {
                ID = Id
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