using FleetControl.Application.Models;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Drivers.DriverProject
{
    public class DeleteDriverProjectHandler : IRequestHandler<DeleteDriverProjectCommand, ResultViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDriverProjectHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(DeleteDriverProjectCommand request, CancellationToken cancellationToken)
        {
            var driverProject = await _unitOfWork.DriverProjectsRepository.GetById(request.Id);

            if (driverProject is null)
                return ResultViewModel.Error("Não foi possível encontrar a associação de motorista e projeto informada.");

            driverProject.SetAsDeleted();

            await _unitOfWork.DriverProjectsRepository.Update(driverProject);

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
