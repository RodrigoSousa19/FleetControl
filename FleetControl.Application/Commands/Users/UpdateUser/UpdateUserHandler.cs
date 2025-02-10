using FleetControl.Application.Models;
using FleetControl.Application.Validations;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Users.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, ResultViewModel>
    {

        private readonly IGenericRepository<User> _repository;

        public UpdateUserHandler(IGenericRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            new Validator().IsNotNullOrEmpty(request.Name, ErrorsList.InvalidUserName).IsEmailValid(request.Email, ErrorsList.InvalidEmail).Validate();

            var user = await _repository.GetById(request.IdUser);

            if (user is null)
                return ResultViewModel.Error("Não foi possível encontrar o usuário especificado");

            user.Update(request.Name, request.Email);

            await _repository.Update(user);

            return ResultViewModel.Success();
        }
    }
}
