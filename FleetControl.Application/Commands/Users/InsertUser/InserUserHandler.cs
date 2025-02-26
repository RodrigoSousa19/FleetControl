using FleetControl.Application.Models;
using FleetControl.Application.Validations;
using FleetControl.Core.Entities;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Infrastructure.Security;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace FleetControl.Application.Commands.Users.InsertUser
{
    public class InsertUserHandler : IRequestHandler<InsertUserCommand, ResultViewModel>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public InsertUserHandler(IUnitOfWork unitOfWork, IAuthService authService, IMemoryCache memoryCache)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        public async Task<ResultViewModel> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            new Validator()
                .IsNotNullOrEmpty(request.Name, ErrorsList.InvalidUserName)
                .IsEmailValid(request.Email, ErrorsList.InvalidEmail)
                .Validate();

            var hashPassword = _authService.ComputeHash(request.Password);

            var user = new User(request.Name, request.Email, hashPassword, request.Role, request.BirthDate);

            var result = await _unitOfWork.UserRepository.Create(user);

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
