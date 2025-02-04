using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Users.EnableUser
{
    public class EnableUserHandler : IRequestHandler<EnableUserCommand, ResultViewModel>
    {

        private readonly IGenericRepository<User> _repository;

        public EnableUserHandler(IGenericRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(EnableUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetById(request.Id);

            if (user is null)
                return ResultViewModel.Error("Não foi possível encontrar o usuário especificado");

            if (user.Enabled)
                return ResultViewModel.Error("O usuário já está ativo.");

            user.Enable();

            await _repository.Update(user);

            return ResultViewModel.Success();
        }
    }
}
