using FleetControl.Application.Models;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Drivers.DisableDriver
{
    public class DisableDriverHandler : IRequestHandler<DisableDriverCommand, ResultViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DisableDriverHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResultViewModel> Handle(DisableDriverCommand request, CancellationToken cancellationToken)
        {
            var driver = await _unitOfWork.DriverRepository.GetById(request.Id);

            if (driver is null)
                return ResultViewModel.Error("Não foi possível encontrar o motorista informado.");

            if (!driver.Enabled)
                return ResultViewModel.Error("O motorista informado já se encontra inativo.");

            driver.Disable();

            await _unitOfWork.DriverRepository.Update(driver);

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
