using FleetControl.Application.Models;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Projects.EnableProject
{
    public class EnableProjectHandler : IRequestHandler<EnableProjectCommand, ResultViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EnableProjectHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(EnableProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.ProjectRepository.GetById(request.Id);

            if (project is null)
                return ResultViewModel.Error("Não foi possível encontrar o projeto solicitado.");

            if (project.Enabled)
                return ResultViewModel.Error("O projeto se encontra ativo.");

            project.Enable();

            await _unitOfWork.ProjectRepository.Update(project);

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
