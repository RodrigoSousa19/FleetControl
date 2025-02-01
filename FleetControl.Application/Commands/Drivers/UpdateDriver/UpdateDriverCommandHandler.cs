using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Drivers.UpdateDriver
{
    public class UpdateDriverCommandHandler : IRequestHandler<UpdateDriverCommand, ResultViewModel>
    {
        private readonly IGenericRepository<Driver> _repository;
        public UpdateDriverCommandHandler(IGenericRepository<Driver> repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
        {
            var driver = await _repository.GetById(request.IdDriver);

            if (driver is null)
                return ResultViewModel.Error("Não foi possível encontrar o motorista informado.");

            driver.Update(request.DocumentNumber, request.DocumentType);

            await _repository.Update(driver);

            return ResultViewModel.Success();
        }
    }
}
