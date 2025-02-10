using FleetControl.Application.Models;
using FleetControl.Application.Validations;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Reservations.ReservationsComments
{
    public class InsertReservationCommentHandler : IRequestHandler<InsertReservationCommentCommand, ResultViewModel<ReservationComment>>
    {

        private readonly IGenericRepository<ReservationComment> _repository;

        public InsertReservationCommentHandler(IGenericRepository<ReservationComment> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<ReservationComment>> Handle(InsertReservationCommentCommand request, CancellationToken cancellationToken)
        {
            new Validator().IsNotNullOrEmpty(request.Content,ErrorsList.EmptyComment).Validate();

            var comment = await _repository.Create(request.ToEntity());

            return ResultViewModel<ReservationComment>.Success(comment);
        }
    }
}
