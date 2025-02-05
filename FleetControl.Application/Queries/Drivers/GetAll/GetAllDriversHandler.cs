using FleetControl.Application.Models;
using FleetControl.Application.Models.Drivers;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Queries.Drivers.GetAll
{
    public class GetAllDriversHandler : IRequestHandler<GetAllDriversQuery, ResultViewModel<IList<DriverViewModel>>>
    {

        private readonly IGenericRepository<Driver> _repository;

        public GetAllDriversHandler(IGenericRepository<Driver> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<IList<DriverViewModel>>> Handle(GetAllDriversQuery request, CancellationToken cancellationToken)
        {
            var driver = await _repository.GetAll();

            var model = driver.Select(DriverViewModel.FromEntity).ToList();

            return ResultViewModel<IList<DriverViewModel>>.Success(model);
        }
    }
}
