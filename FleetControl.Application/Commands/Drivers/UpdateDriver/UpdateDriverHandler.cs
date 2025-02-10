using FleetControl.Application.Commands.Drivers.InsertDriver;
using FleetControl.Application.Models;
using FleetControl.Application.Validations;
using FleetControl.Application.Validations.CustomValidators;
using FleetControl.Core.Entities;
using FleetControl.Core.Enums.User;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Drivers.UpdateDriver
{
    public class UpdateDriverHandler : IRequestHandler<UpdateDriverCommand, ResultViewModel>
    {
        private readonly IGenericRepository<Driver> _repository;
        public UpdateDriverHandler(IGenericRepository<Driver> repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
        {
            var driver = await _repository.GetById(request.IdDriver);

            if (driver is null)
                return ResultViewModel.Error("Não foi possível encontrar o motorista informado.");

            ValidateRequest(request);

            driver.Update(request.DocumentNumber, request.DocumentType);

            await _repository.Update(driver);

            return ResultViewModel.Success();
        }

        private void ValidateRequest(UpdateDriverCommand request)
        {
            switch (request.DocumentType)
            {
                case DocumentType.CPF:
                    new Validator()
                        .ProveCustomValidation(new CpfValidator(request.DocumentNumber), ErrorsList.InvalidCpf)
                        .Validate();
                    break;
                case DocumentType.DriversLicense:
                    new Validator()
                        .ProveCustomValidation(new CnhValidator(request.DocumentNumber), ErrorsList.InvalidCnh)
                        .Validate();
                    break;
            }
        }
    }
}
