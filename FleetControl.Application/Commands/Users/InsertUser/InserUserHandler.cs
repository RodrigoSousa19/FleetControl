using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Users.InsertUser
{
    public class InserUserHandler : IRequestHandler<InsertUserCommand, ResultViewModel<User>>
    {

        private readonly IGenericRepository<User> _repository;

        public InserUserHandler(IGenericRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<User>> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.Create(request.ToEntity());

            return ResultViewModel<User>.Success(user);
        }
    }
}
