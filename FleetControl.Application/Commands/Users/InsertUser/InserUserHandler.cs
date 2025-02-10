using FleetControl.Application.Models;
using FleetControl.Application.Validations;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Users.InsertUser
{
    public class InsertUserHandler : IRequestHandler<InsertUserCommand, ResultViewModel<User>>
    {

        private readonly IGenericRepository<User> _repository;

        public InsertUserHandler(IGenericRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<User>> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            new Validator()
                .IsNotNullOrEmpty(request.Name, ErrorsList.InvalidUserName)
                .IsEmailValid(request.Email, ErrorsList.InvalidEmail)
                .Validate();

            var user = await _repository.Create(request.ToEntity());

            return ResultViewModel<User>.Success(user);
        }
    }
}
