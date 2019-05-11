﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Administration.Domain.Entities;
using Administration.Domain.EntityViewModels.Events;
using Administration.Services.Abstraction.Events;
using FriendlyFisherman.SharedKernel.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FriendlyFishermanApi.Controllers
{
    [Authorize("Bearer")]
    public class EventsController : BaseApiController
    {
        private readonly IEventsService _service;
        private readonly ILogger _logger;

        public EventsController(IEventsService eventsService, ILogger logger)
        {
            _service = eventsService;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllUsers()
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

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> SaveMessage([FromBody] EventViewModel model)
        {
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
                    StartDate = model.StartDate
                }
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
    }
}