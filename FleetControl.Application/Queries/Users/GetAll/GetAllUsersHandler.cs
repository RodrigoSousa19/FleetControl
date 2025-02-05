using FleetControl.Application.Models;
using FleetControl.Application.Models.Users;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Queries.Users.GetAll
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, ResultViewModel<IList<UserViewModel>>>
    {

        private readonly IGenericRepository<User> _repository;

        public GetAllUsersHandler(IGenericRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<IList<UserViewModel>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetAll();

            var model = users.Select(UserViewModel.FromEntity).ToList();

            return ResultViewModel<IList<UserViewModel>>.Success(model);
        }
    }
}
