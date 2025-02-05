using FleetControl.Application.Models;
using FleetControl.Application.Models.Vehicles;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Queries.Vehicles.GetAll
{
    public class GetAllVehiclesHandler : IRequestHandler<GetAllVehiclesQuery, ResultViewModel<IList<VehicleViewModel>>>
    {

        private readonly IGenericRepository<Vehicle> _repository;

        public GetAllVehiclesHandler(IGenericRepository<Vehicle> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<IList<VehicleViewModel>>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _repository.GetAll();

            var model = vehicles.Select(VehicleViewModel.FromEntity).ToList();

            return ResultViewModel<IList<VehicleViewModel>>.Success(model);
        }
    }
}
