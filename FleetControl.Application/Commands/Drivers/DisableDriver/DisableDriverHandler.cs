using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Drivers.DisableDriver
{
    public class DisableDriverHandler : IRequestHandler<DisableDriverCommand, ResultViewModel>
    {
        private readonly IGenericRepository<Driver> _repository;
        public DisableDriverHandler(IGenericRepository<Driver> repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel> Handle(DisableDriverCommand request, CancellationToken cancellationToken)
        {
            var driver = await _repository.GetById(request.Id);

            if (driver is null)
                return ResultViewModel.Error("Não foi possível encontrar o motorista informado.");

            if (!driver.Enabled)
                return ResultViewModel.Error("O motorista informado já se encontra inativo.");

            driver.Disable();

            await _repository.Update(driver);

            return ResultViewModel.Success();
        }
    }
}
