using FleetControl.Application.Models;
using FleetControl.Application.Models.Users;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Queries.Users.GetById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, ResultViewModel<UserViewModel>>
    {

        private readonly IGenericRepository<User> _repository;

        public GetUserByIdHandler(IGenericRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<UserViewModel>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetById(request.Id);

            if (user is null)
                return ResultViewModel<UserViewModel>.Error("Não foi possível localizar o usuário informado.");

            var model = UserViewModel.FromEntity(user);

            return ResultViewModel<UserViewModel>.Success(model);
        }
    }
}
