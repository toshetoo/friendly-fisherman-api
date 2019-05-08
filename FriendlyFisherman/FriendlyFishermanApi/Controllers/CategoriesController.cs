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
using Publishing.Domain.EntityViewModels.Categories;
using Publishing.Services.Abstraction.Categories;
using Users.Services.Request.PersonalMessage;

namespace FriendlyFishermanApi.Controllers
{
    [Authorize("Bearer")]
    public class CategoriesController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly IThreadCategoriesService _service;

        public CategoriesController(IThreadCategoriesService service, ILogger logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _service.GetAllAsync(new ServiceRequestBase<ThreadCategory>());

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
            var response = await _service.GetByIdAsync(new ServiceRequestBase<ThreadCategory>()
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
        [Route("SaveCategory")]
        public async Task<IActionResult> SaveMessage([FromBody] ThreadCategoryViewModel model)
        {
            var response = await _service.SaveAsync(new ServiceRequestBase<ThreadCategory>()
            {
                Item = new ThreadCategory()
                {
                    Id = model.Id,
                    Name = model.Name
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
            var response = await _service.DeleteAsync(new ServiceRequestBase<ThreadCategory>()
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