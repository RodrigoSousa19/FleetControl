using FleetControl.Application.Models;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Users.DisableUser
{
    public class DisableUserHandler : IRequestHandler<DisableUserCommand, ResultViewModel>
    {

        private readonly IUnitOfWork _unitOfWork;

        public DisableUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(DisableUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetById(request.Id);

            if (user is null)
                return ResultViewModel.Error("Não foi possível encontrar o usuário especificado");

            if (!user.Enabled)
                return ResultViewModel.Error("O usuário já está inativo.");

            user.Disable();

            await _unitOfWork.UserRepository.Update(user);

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
