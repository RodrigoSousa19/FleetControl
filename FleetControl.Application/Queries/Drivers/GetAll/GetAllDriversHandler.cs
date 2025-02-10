using FleetControl.Application.Models;
using FleetControl.Application.Models.Drivers;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Queries.Drivers.GetAll
{
    public class GetAllDriversHandler : IRequestHandler<GetAllDriversQuery, ResultViewModel<IList<DriverViewModel>>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public GetAllDriversHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<IList<DriverViewModel>>> Handle(GetAllDriversQuery request, CancellationToken cancellationToken)
        {
            var driver = await _unitOfWork.DriverRepository.GetAll(includeNavigation: true);

            var model = driver.Select(DriverViewModel.FromEntity).ToList();

            return ResultViewModel<IList<DriverViewModel>>.Success(model);
        }
    }
}
