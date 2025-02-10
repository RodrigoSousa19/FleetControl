using FleetControl.Application.Models;
using FleetControl.Application.Validations;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Users.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, ResultViewModel>
    {

        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            new Validator().IsNotNullOrEmpty(request.Name, ErrorsList.InvalidUserName).IsEmailValid(request.Email, ErrorsList.InvalidEmail).Validate();

            var user = await _unitOfWork.UserRepository.GetById(request.IdUser);

            if (user is null)
                return ResultViewModel.Error("Não foi possível encontrar o usuário especificado");

            user.Update(request.Name, request.Email);

            await _unitOfWork.UserRepository.Update(user);

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
