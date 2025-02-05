using FleetControl.Application.Models;
using FleetControl.Application.Models.Drivers;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Queries.Drivers.GetById
{
    public class GetDriverByIdHandler : IRequestHandler<GetDriverByIdQuery, ResultViewModel<DriverViewModel>>
    {

        private readonly IGenericRepository<Driver> _repository;

        public GetDriverByIdHandler(IGenericRepository<Driver> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<DriverViewModel>> Handle(GetDriverByIdQuery request, CancellationToken cancellationToken)
        {
            var driver = await _repository.GetById(request.Id, includeNavigation: true);

            if (driver is null)
                return ResultViewModel<DriverViewModel>.Error("Não foi possível localizar o motorista informado.");

            var model = DriverViewModel.FromEntity(driver);

            return ResultViewModel<DriverViewModel>.Success(model);
        }
    }
}
