
using FleetControl.Application.Commands.Customers.DeleteCustomer;
using FleetControl.Application.Commands.Customers.DisableCustomer;
using FleetControl.Application.Commands.Customers.EnableCustomer;
using FleetControl.Application.Commands.Customers.InsertCustomer;
using FleetControl.Application.Commands.Customers.UpdateCustomer;
using FleetControl.Application.Queries.Customers.GetAll;
using FleetControl.Application.Queries.Customers.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FleetControl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllCustomersQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetCustomerByIdQuery(id);

            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InsertCustomerCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCustomerCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand(id));

            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }

        [HttpPut("{id}/disable")]
        public async Task<IActionResult> Disable(int id)
        {
            var result = await _mediator.Send(new DisableCustomerCommand(id));

            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }

        [HttpPut("{id}/enable")]
        public async Task<IActionResult> Enable(int id)
        {
            var result = await _mediator.Send(new EnableCustomerCommand(id));

            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }
    }
}
