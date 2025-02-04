using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Drivers.DriverProject
{
    public class InsertDriverProjectHandler : IRequestHandler<InsertDriverProjectCommand, ResultViewModel<DriverProjects>>
    {
        private readonly IGenericRepository<DriverProjects> _repository;
        public InsertDriverProjectHandler(IGenericRepository<DriverProjects> repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel<DriverProjects>> Handle(InsertDriverProjectCommand request, CancellationToken cancellationToken)
        {
            var driverProject = await _repository.Create(request.ToEntity());

            return ResultViewModel<DriverProjects>.Success(driverProject);
        }
    }
}
