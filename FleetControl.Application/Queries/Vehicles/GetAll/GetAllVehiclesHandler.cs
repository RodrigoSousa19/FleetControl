using FleetControl.Application.Models;
using FleetControl.Application.Models.Vehicles;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Queries.Vehicles.GetAll
{
    public class GetAllVehiclesHandler : IRequestHandler<GetAllVehiclesQuery, ResultViewModel<IList<VehicleViewModel>>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public GetAllVehiclesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<IList<VehicleViewModel>>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _unitOfWork.VehicleRepository.GetAll();

            var model = vehicles.Select(VehicleViewModel.FromEntity).ToList();

            return ResultViewModel<IList<VehicleViewModel>>.Success(model);
        }
    }
}
