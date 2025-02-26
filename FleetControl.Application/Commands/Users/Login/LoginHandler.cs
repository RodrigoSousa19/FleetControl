using FleetControl.Application.Models;
using FleetControl.Application.Validations;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Infrastructure.Security;
using MediatR;

namespace FleetControl.Application.Commands.Users.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, ResultViewModel<string>>
    {
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;

        public LoginHandler(IAuthService authService, IUnitOfWork unitOfWork)
        {
            _authService = authService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            new Validator().IsEmailValid(request.Email, ErrorsList.InvalidEmail).Validate();

            var hashPassword = _authService.ComputeHash(request.Password);

            var user = await _unitOfWork.UserRepository.GetUserDetailsLogin(request.Email, hashPassword);

            if (user is null)
                return ResultViewModel<string>.Error("Email e/ou senha inválidos, tente novamente.");

            var token = _authService.GenerateToken(user.Email, user.Role, user.Name);

            return ResultViewModel<string>.Success(token);
        }
    }
}
