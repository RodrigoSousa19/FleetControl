using FleetControl.Application.Models;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Reservations.DeleteReservation
{
    public class DeleteReservationHandler : IRequestHandler<DeleteReservationCommand, ResultViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteReservationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _unitOfWork.ReservationRepository.GetById(request.Id);

            if (reservation is null)
                return ResultViewModel.Error("Não foi possível encontrar a reserva especificada.");

            reservation.SetAsDeleted();

            await _unitOfWork.ReservationRepository.Update(reservation);

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
