using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Reservations.InsertReservation
{
    public class InsertReservationHandler : IRequestHandler<InsertReservationCommand, ResultViewModel<Reservation>>
    {
        private readonly IGenericRepository<Reservation> _repository;

        public InsertReservationHandler(IGenericRepository<Reservation> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<Reservation>> Handle(InsertReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _repository.Create(request.ToEntity());

            return ResultViewModel<Reservation>.Success(reservation);
        }
    }
}
