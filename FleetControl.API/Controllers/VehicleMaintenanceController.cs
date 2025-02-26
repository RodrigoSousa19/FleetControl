using FleetControl.Application.Commands.Vehicles;
using FleetControl.Application.Queries.Vehicles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetControl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VehicleMaintenanceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehicleMaintenanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllVehicleMaintenanceQuery());

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetVehicleMaintenanceByIdQuery(id));

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> SendToMaintenance([FromBody] InsertMaintenanceCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("{id}/start")]
        public async Task<IActionResult> StartVehicleMaintenance(int id)
        {
            var result = await _mediator.Send(new StartMaintenanceCommand(id));

            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }

        [HttpPut("{id}/finish")]
        public async Task<IActionResult> FinishVehicleMaintenance(int id)
        {
            var result = await _mediator.Send(new FinishMaintenanceCommand(id));

            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelVehicleMaintenance(int id)
        {
            var result = await _mediator.Send(new CancelMaintenanceCommand(id));

            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }

        [HttpPut("{id}/update")]
        public async Task<IActionResult> UpdateVehicleMaintenance(int id, UpdateMaintenanceCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }

        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> DeleteVehicleMaintenance(int id)
        {
            var result = await _mediator.Send(new CancelMaintenanceCommand(id));

            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }
    }
}
