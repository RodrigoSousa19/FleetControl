using FleetControl.Application.Models;
using FleetControl.Application.Models.Drivers;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Queries.Drivers.GetById
{
    public class GetDriverByIdHandler : IRequestHandler<GetDriverByIdQuery, ResultViewModel<DriverViewModel>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public GetDriverByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<DriverViewModel>> Handle(GetDriverByIdQuery request, CancellationToken cancellationToken)
        {
            var driver = await _unitOfWork.DriverRepository.GetById(request.Id, includeNavigation: true);

            if (driver is null)
                return ResultViewModel<DriverViewModel>.Error("Não foi possível localizar o motorista informado.");

            var model = DriverViewModel.FromEntity(driver);

            return ResultViewModel<DriverViewModel>.Success(model);
        }
    }
}
