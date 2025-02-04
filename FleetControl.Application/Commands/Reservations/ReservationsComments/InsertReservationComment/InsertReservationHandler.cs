using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Reservations.ReservationsComments.InsertReservationComment
{
    public class InsertReservationHandler : IRequestHandler<InsertReservationCommand, ResultViewModel>
    {

        private readonly IGenericRepository<ReservationComment> _repository;

        public InsertReservationHandler(IGenericRepository<ReservationComment> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(InsertReservationCommand request, CancellationToken cancellationToken)
        {
            var comment = await _repository.Create(request.ToEntity());

            return ResultViewModel<ReservationComment>.Success(comment);
        }
    }
}
