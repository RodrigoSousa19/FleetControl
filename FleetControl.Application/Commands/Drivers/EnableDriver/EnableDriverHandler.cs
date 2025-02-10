using FleetControl.Application.Models;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Drivers.EnableDriver
{
    public class EnableDriverHandler : IRequestHandler<EnableDriverCommand, ResultViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public EnableDriverHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResultViewModel> Handle(EnableDriverCommand request, CancellationToken cancellationToken)
        {
            var driver = await _unitOfWork.DriverRepository.GetById(request.Id);

            if (driver is null)
                return ResultViewModel.Error("Não foi possível encontrar o motorista informado.");

            if (driver.Enabled)
                return ResultViewModel.Error("O motorista informado já se encontra ativo.");

            driver.Enable();

            await _unitOfWork.DriverRepository.Update(driver);

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
