using FleetControl.Application.Models;
using FleetControl.Application.Validations;
using FleetControl.Application.Validations.CustomValidators;
using FleetControl.Core.Entities;
using FleetControl.Core.Enums.User;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Drivers.InsertDriver
{
    public class InsertDriverHandler : IRequestHandler<InsertDriverCommand, ResultViewModel<Driver>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InsertDriverHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<Driver>> Handle(InsertDriverCommand request, CancellationToken cancellationToken)
        {
            ValidateRequest(request);

            var user = await _unitOfWork.UserRepository.GetById(request.IdUser);

            if (user is null)
                return ResultViewModel<Driver>.Error("Usuário informado não existente na base de dados.");

            var driver = await _unitOfWork.DriverRepository.Create(request.ToEntity());

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel<Driver>.Success(driver);
        }

        private void ValidateRequest(InsertDriverCommand request)
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
