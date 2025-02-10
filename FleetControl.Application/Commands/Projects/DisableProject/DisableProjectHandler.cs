using FleetControl.Application.Models;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Projects.DisableProject
{
    public class DisableProjectHandler : IRequestHandler<DisableProjectCommand, ResultViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DisableProjectHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(DisableProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.ProjectRepository.GetById(request.Id);

            if (project is null)
                return ResultViewModel.Error("Não foi possível encontrar o projeto solicitado.");

            if (!project.Enabled)
                return ResultViewModel.Error("O projeto já se encontra inativo.");

            project.Disable();

            await _unitOfWork.ProjectRepository.Update(project);

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
