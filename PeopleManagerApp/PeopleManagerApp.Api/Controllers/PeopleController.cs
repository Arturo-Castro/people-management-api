﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleManagerApp.Application.Interfaces;
using PeopleManagerApp.Domain.Dtos;

namespace PeopleManagerApp.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(PeopleController));
        public PeopleController(IPersonService personService)
        {
            this._personService = personService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(IEnumerable<PersonDto>))]
        public async Task<IActionResult> GetAllPeople()
        {
            try
            {
                var response = await this._personService.GetAllPeople();
                if (!response.Any())
                {
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                this._logger.Error(e);
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonDto))]
        public async Task<IActionResult> GetPersonById(long id)
        {
            try
            {
                var response = await this._personService.GetPersonById(id);
                if (response == null)
                {
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                this._logger.Error(e);
                return NotFound();
            }
        }

        [HttpGet("shuffle")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonDto))]
        public async Task<IActionResult> GetRandomPerson()
        {
            try
            {
                var response = await this._personService.GetRandomPerson();
                if (response == null)
                {
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                this._logger.Error(e);
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> SoftDeletePerson(long id)
        {
            try
            {
                var response = await this._personService.SoftDeletePerson(id);
                if (response == false)
                {
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                this._logger.Error(e);
                return NotFound();
            }
        }
    }
}