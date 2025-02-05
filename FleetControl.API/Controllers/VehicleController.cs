using FleetControl.Application.Commands.Projects.DeleteProject;
using FleetControl.Application.Commands.Projects.DisableProject;
using FleetControl.Application.Commands.Projects.EnableProject;
using FleetControl.Application.Commands.Projects.InsertProject;
using FleetControl.Application.Commands.Projects.UpdateProject;
using FleetControl.Application.Commands.Vehicles.DeleteVehicle;
using FleetControl.Application.Commands.Vehicles.DisableVehicle;
using FleetControl.Application.Commands.Vehicles.EnableVehicle;
using FleetControl.Application.Commands.Vehicles.InsertVehicle;
using FleetControl.Application.Commands.Vehicles.UpdateVehicle;
using FleetControl.Application.Queries.Projects.GetAll;
using FleetControl.Application.Queries.Projects.GetById;
using FleetControl.Application.Queries.Vehicles.GetAll;
using FleetControl.Application.Queries.Vehicles.GetById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FleetControl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehicleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllVehiclesQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetVehicleByIdQuery(id);

            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InsertVehicleCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateVehicleCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteVehicleCommand(id));

            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }

        [HttpPut("{id}/disable")]
        public async Task<IActionResult> Disable(int id)
        {
            var result = await _mediator.Send(new DisableVehicleCommand(id));

            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }

        [HttpPut("{id}/enable")]
        public async Task<IActionResult> Enable(int id)
        {
            var result = await _mediator.Send(new EnableVehicleCommand(id));

            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }
    }
}
