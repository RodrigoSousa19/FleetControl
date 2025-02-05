using FleetControl.Application.Models;
using FleetControl.Application.Models.Vehicles;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Queries.Vehicles.GetById
{
    public class GetUserByIdHandler : IRequestHandler<GetVehicleByIdQuery, ResultViewModel<VehicleViewModel>>
    {

        private readonly IGenericRepository<Vehicle> _repository;

        public GetUserByIdHandler(IGenericRepository<Vehicle> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<VehicleViewModel>> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            var vehicle = await _repository.GetById(request.Id);

            if (vehicle is null)
                return ResultViewModel<VehicleViewModel>.Error("Não foi possível localizar o veículo informado.");

            var model = VehicleViewModel.FromEntity(vehicle);

            return ResultViewModel<VehicleViewModel>.Success(model);
        }
    }
}
