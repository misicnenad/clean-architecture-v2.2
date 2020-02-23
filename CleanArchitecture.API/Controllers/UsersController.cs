using CleanArchitecture.Domain.Commands;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.Models;
using MediatR;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            try
            {
                var userCreated = await _mediator.Send(new CreateUser
                {
                    CommandId = Guid.NewGuid(),
                    IdNumber = user.IdNumber,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                });

                if (userCreated)
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (IdNumberValidationException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}