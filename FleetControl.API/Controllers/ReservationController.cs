using FleetControl.Application.Commands.Reservations.CancelReservation;
using FleetControl.Application.Commands.Reservations.ConfirmReservation;
using FleetControl.Application.Commands.Reservations.DeleteReservation;
using FleetControl.Application.Commands.Reservations.FinishReservation;
using FleetControl.Application.Commands.Reservations.InsertReservation;
using FleetControl.Application.Commands.Reservations.ReservationsComments;
using FleetControl.Application.Commands.Reservations.UpdateReservation;
using FleetControl.Application.Queries.Reservations.GetAll;
using FleetControl.Application.Queries.Reservations.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FleetControl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReservationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllReservationsQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetReservationByIdQuery(id);

            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InsertReservationCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateReservationCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteReservationCommand(id));

            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            var result = await _mediator.Send(new CancelReservationCommand(id));

            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }

        [HttpPut("{id}/confirm")]
        public async Task<IActionResult> Enable(int id)
        {
            var result = await _mediator.Send(new ConfirmReservationCommand(id));

            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }

        [HttpPut("{id}/finish")]
        public async Task<IActionResult> Finish(int id)
        {
            var result = await _mediator.Send(new FinishReservationCommand(id));

            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }

        [HttpPost("comments/create")]
        public async Task<IActionResult> CreateComment([FromBody] InsertReservationCommentCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("comments/{id}/delete")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var result = await _mediator.Send(new DeleteReservationCommentCommand(id));

            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }
    }
}

