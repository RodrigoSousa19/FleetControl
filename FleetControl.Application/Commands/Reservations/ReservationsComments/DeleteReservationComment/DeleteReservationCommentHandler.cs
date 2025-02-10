using FleetControl.Application.Models;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Reservations.ReservationsComments
{
    public class DeleteReservationCommentHandler : IRequestHandler<DeleteReservationCommentCommand, ResultViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteReservationCommentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(DeleteReservationCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _unitOfWork.ReservationCommentRepository.GetById(request.Id);

            if (comment is null)
                return ResultViewModel.Error("Não foi possível encontrar o comentário especificado");

            comment.SetAsDeleted();

            await _unitOfWork.ReservationCommentRepository.Update(comment);

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
