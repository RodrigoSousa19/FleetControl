using FleetControl.Application.Models;
using FleetControl.Application.Validations;
using FleetControl.Core.Entities;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Projects.InsertProject
{
    public class InsertProjectHandler : IRequestHandler<InsertProjectCommand, ResultViewModel<Project>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InsertProjectHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<Project>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
        {
            new Validator()
                .IsNotNullOrEmpty(request.Description, ErrorsList.EmptyDescription)
                .Validate();

            var customer = await _unitOfWork.CustomerRepository.GetById(request.IdCustomer);
            if (customer is null)
                return ResultViewModel<Project>.Error("Não foi possível encontrar o cliente espeficiado.");

            var costCenter = await _unitOfWork.CostCenterRepository.GetById(request.IdCostCenter);
            if (costCenter is null)
                return ResultViewModel<Project>.Error("Não foi possível encontrar o centro de custo espeficiado.");

            var project = await _unitOfWork.ProjectRepository.Create(request.ToEntity());

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel<Project>.Success(project);
        }
    }
}
