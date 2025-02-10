using FleetControl.Application.Models;
using FleetControl.Application.Validations;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Projects.UpdateProject
{
    public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ResultViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProjectHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResultViewModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.ProjectRepository.GetById(request.IdProject);

            if (project is null)
                return ResultViewModel.Error("Não foi possível encontrar o projeto solicitado.");

            var customer = await _unitOfWork.CustomerRepository.GetById(request.IdCustomer);

            if (customer is null)
                return ResultViewModel.Error("Não foi possível encontrar o cliente especificado.");

            var costCenter = await _unitOfWork.CostCenterRepository.GetById(request.IdCostCenter);

            if (costCenter is null)
                return ResultViewModel.Error("Não foi possível encontrar o centro de custo especificado.");

            new Validator()
                .IsNotNullOrEmpty(request.Description, ErrorsList.EmptyDescription)
                .Validate();

            project.Update(request.Description, request.IdCostCenter, request.IdCustomer);

            await _unitOfWork.ProjectRepository.Update(project);

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
