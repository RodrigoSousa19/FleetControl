using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Drivers.DriverProject
{
    public class InsertDriverProjectHandler : IRequestHandler<InsertDriverProjectCommand, ResultViewModel<DriverProjects>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public InsertDriverProjectHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResultViewModel<DriverProjects>> Handle(InsertDriverProjectCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.DriverRepository.GetById(request.IdDriver);
            if (user is null)
                return ResultViewModel<DriverProjects>.Error("Não foi possível encontrar o motorista especificado.");

            var project = await _unitOfWork.ProjectRepository.GetById(request.IdProject);
            if (project is null)
                return ResultViewModel<DriverProjects>.Error("Não foi possível encontrar o projeto especificado.");

            var driverProject = await _unitOfWork.DriverProjectsRepository.Create(request.ToEntity());

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel<DriverProjects>.Success(driverProject);
        }
    }
}
