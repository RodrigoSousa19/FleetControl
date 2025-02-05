using FleetControl.Application.Commands.Users.DeleteUser;
using FleetControl.Application.Commands.Users.DisableUser;
using FleetControl.Application.Commands.Users.EnableUser;
using FleetControl.Application.Commands.Users.InsertUser;
using FleetControl.Application.Commands.Users.UpdateUser;
using FleetControl.Application.Queries.Users.GetAll;
using FleetControl.Application.Queries.Users.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FleetControl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllUsersQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetUserByIdQuery(id);

            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InsertUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteUserCommand(id));

            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }

        [HttpPut("{id}/disable")]
        public async Task<IActionResult> Disable(int id)
        {
            var result = await _mediator.Send(new DisableUserCommand(id));

            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }

        [HttpPut("{id}/enable")]
        public async Task<IActionResult> Enable(int id)
        {
            var result = await _mediator.Send(new EnableUserCommand(id));

            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }
    }
}
