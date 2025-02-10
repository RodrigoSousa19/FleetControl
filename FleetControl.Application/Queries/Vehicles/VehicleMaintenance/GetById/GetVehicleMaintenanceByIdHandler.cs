using FleetControl.Application.Models;
using FleetControl.Application.Models.Vehicles;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Queries.Vehicles
{
    public class GetVehicleMaintenanceByIdHandler : IRequestHandler<GetVehicleMaintenanceByIdQuery, ResultViewModel<VehicleMaintenanceViewModel>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public GetVehicleMaintenanceByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<VehicleMaintenanceViewModel>> Handle(GetVehicleMaintenanceByIdQuery request, CancellationToken cancellationToken)
        {
            var maintenance = await _unitOfWork.VehicleMaintenanceRepository.GetById(request.Id, includeNavigation: true);

            if (maintenance is null)
                return ResultViewModel<VehicleMaintenanceViewModel>.Error("Não foi possível localizar a manutenção informada.");

            var model = VehicleMaintenanceViewModel.FromEntity(maintenance);

            return ResultViewModel<VehicleMaintenanceViewModel>.Success(model);
        }
    }
}
