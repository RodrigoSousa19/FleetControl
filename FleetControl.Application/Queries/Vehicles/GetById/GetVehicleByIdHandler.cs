using FleetControl.Application.Models;
using FleetControl.Application.Models.Vehicles;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Queries.Vehicles.GetById
{
    public class GetVehicleByIdHandler : IRequestHandler<GetVehicleByIdQuery, ResultViewModel<VehicleViewModel>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public GetVehicleByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<VehicleViewModel>> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            var vehicle = await _unitOfWork.VehicleRepository.GetById(request.Id);

            if (vehicle is null)
                return ResultViewModel<VehicleViewModel>.Error("Não foi possível localizar o veículo informado.");

            var model = VehicleViewModel.FromEntity(vehicle);

            return ResultViewModel<VehicleViewModel>.Success(model);
        }
    }
}
