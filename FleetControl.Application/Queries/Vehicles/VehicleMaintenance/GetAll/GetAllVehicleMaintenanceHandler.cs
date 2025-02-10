using FleetControl.Application.Models;
using FleetControl.Application.Models.Vehicles;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Queries.Vehicles
{
    public class GetAllVehicleMaintenanceHandler : IRequestHandler<GetAllVehicleMaintenanceQuery, ResultViewModel<IList<VehicleMaintenanceViewModel>>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public GetAllVehicleMaintenanceHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<IList<VehicleMaintenanceViewModel>>> Handle(GetAllVehicleMaintenanceQuery request, CancellationToken cancellationToken)
        {
            var maintenances = await _unitOfWork.VehicleMaintenanceRepository.GetAll(includeNavigation: true);

            var model = maintenances.Select(VehicleMaintenanceViewModel.FromEntity).ToList();

            return ResultViewModel<IList<VehicleMaintenanceViewModel>>.Success(model);
        }
    }
}
