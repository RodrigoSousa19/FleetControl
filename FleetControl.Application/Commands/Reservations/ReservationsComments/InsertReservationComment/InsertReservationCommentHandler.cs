using FleetControl.Application.Models;
using FleetControl.Application.Validations;
using FleetControl.Core.Entities;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Reservations.ReservationsComments
{
    public class InsertReservationCommentHandler : IRequestHandler<InsertReservationCommentCommand, ResultViewModel<ReservationComment>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public InsertReservationCommentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<ReservationComment>> Handle(InsertReservationCommentCommand request, CancellationToken cancellationToken)
        {
            new Validator().IsNotNullOrEmpty(request.Content, ErrorsList.EmptyComment).Validate();

            var reservation = await _unitOfWork.ReservationRepository.GetById(request.IdReservation);
            if (reservation is null)
                return ResultViewModel<ReservationComment>.Error("Não foi possível encontrar a reserva especificada.");

            var user = await _unitOfWork.UserRepository.GetById(request.IdUser);
            if (user is null)
                return ResultViewModel<ReservationComment>.Error("Não foi possível encontrar o usuário especificado.");

            var comment = await _unitOfWork.ReservationCommentRepository.Create(request.ToEntity());

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel<ReservationComment>.Success(comment);
        }
    }
}
