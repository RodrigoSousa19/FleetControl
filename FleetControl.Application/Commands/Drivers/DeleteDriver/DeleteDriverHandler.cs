using FleetControl.Application.Models;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Drivers.DeleteDriver
{
    public class DeleteDriverHandler : IRequestHandler<DeleteDriverCommand, ResultViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteDriverHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResultViewModel> Handle(DeleteDriverCommand request, CancellationToken cancellationToken)
        {
            var driver = await _unitOfWork.DriverRepository.GetById(request.Id);

            if (driver is null)
                return ResultViewModel.Error("Não foi possível encontrar o motorista informado.");

            driver.SetAsDeleted();

            await _unitOfWork.DriverRepository.Update(driver);

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
