using FleetControl.Application.Models;
using FleetControl.Application.Validations;
using FleetControl.Core.Entities;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Users.InsertUser
{
    public class InsertUserHandler : IRequestHandler<InsertUserCommand, ResultViewModel<User>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public InsertUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<User>> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            new Validator()
                .IsNotNullOrEmpty(request.Name, ErrorsList.InvalidUserName)
                .IsEmailValid(request.Email, ErrorsList.InvalidEmail)
                .Validate();

            var user = await _unitOfWork.UserRepository.Create(request.ToEntity());

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel<User>.Success(user);
        }
    }
}
