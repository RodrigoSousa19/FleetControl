using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Reservations.ReservationsComments.DeleteReservationComment
{
    public class DeleteReservationCommentHandler : IRequestHandler<DeleteReservationCommentCommand, ResultViewModel>
    {
        private readonly IGenericRepository<ReservationComment> _repository;

        public DeleteReservationCommentHandler(IGenericRepository<ReservationComment> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(DeleteReservationCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _repository.GetById(request.Id);

            if (comment is null)
                return ResultViewModel.Error("Não foi possível encontrar o comentário especificado");

            comment.SetAsDeleted();

            await _repository.Update(comment);

            return ResultViewModel.Success();
        }
    }
}
