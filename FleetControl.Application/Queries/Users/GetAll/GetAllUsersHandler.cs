using FleetControl.Application.Models;
using FleetControl.Application.Models.Users;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Queries.Users.GetAll
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, ResultViewModel<IList<UserViewModel>>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public GetAllUsersHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<IList<UserViewModel>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.UserRepository.GetAll();

            var model = users.Select(UserViewModel.FromEntity).ToList();

            return ResultViewModel<IList<UserViewModel>>.Success(model);
        }
    }
}
