using FleetControl.Application.Commands.Vehicles;
using FleetControl.Application.Queries.Vehicles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FleetControl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpPost("/maintenance/create")]
        public async Task<IActionResult> SendToMaintenance([FromBody] InsertMaintenanceCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("/maintenance/{id}/start")]
        public async Task<IActionResult> StartVehicleMaintenance(int id)
        {
            var result = await _mediator.Send(new StartMaintenanceCommand(id));

            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }

        [HttpPut("/maintenance/{id}/finish")]
        public async Task<IActionResult> FinishVehicleMaintenance(int id)
        {
            var result = await _mediator.Send(new FinishMaintenanceCommand(id));

            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }

        [HttpPut("/maintenance/{id}/cancel")]
        public async Task<IActionResult> CancelVehicleMaintenance(int id)
        {
            var result = await _mediator.Send(new CancelMaintenanceCommand(id));

            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }

        [HttpPut("/maintenance/{id}/update")]
        public async Task<IActionResult> UpdateVehicleMaintenance(int id, UpdateMaintenanceCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }

        [HttpDelete("/maintenance/{id}/delete")]
        public async Task<IActionResult> DeleteVehicleMaintenance(int id)
        {
            var result = await _mediator.Send(new CancelMaintenanceCommand(id));

            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }
    }
}
