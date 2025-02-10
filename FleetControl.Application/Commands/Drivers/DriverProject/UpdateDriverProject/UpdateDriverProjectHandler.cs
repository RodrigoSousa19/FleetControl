using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Drivers.DriverProject
{
    public class UpdateDriverProjectHandler : IRequestHandler<UpdateDriverProjectCommand, ResultViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateDriverProjectHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(UpdateDriverProjectCommand request, CancellationToken cancellationToken)
        {
            var driverProject = await _unitOfWork.DriverProjectsRepository.GetById(request.Id);

            if (driverProject is null)
                return ResultViewModel.Error("Não foi possível encontrar a associação de motorista e projeto informada.");

            var user = await _unitOfWork.DriverRepository.GetById(request.IdDriver);
            if (user is null)
                return ResultViewModel<DriverProjects>.Error("Não foi possível encontrar o motorista especificado.");

            var project = await _unitOfWork.ProjectRepository.GetById(request.IdProject);
            if (project is null)
                return ResultViewModel<DriverProjects>.Error("Não foi possível encontrar o projeto especificado.");

            driverProject.Update(request.IdDriver, request.IdProject);

            await _unitOfWork.DriverProjectsRepository.Update(driverProject);

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
