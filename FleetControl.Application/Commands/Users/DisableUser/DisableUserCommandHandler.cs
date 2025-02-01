using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Users.DisableUser
{
    public class DisableUserCommandHandler : IRequestHandler<DisableUserCommand, ResultViewModel>
    {

        private readonly IGenericRepository<User> _repository;

        public DisableUserCommandHandler(IGenericRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(DisableUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetById(request.Id);

            if (user is null)
                return ResultViewModel.Error("Não foi possível encontrar o usuário especificado");

            if (!user.Enabled)
                return ResultViewModel.Error("O usuário já está inativo.");

            user.Disable();

            await _repository.Update(user);

            return ResultViewModel.Success();
        }
    }
}
