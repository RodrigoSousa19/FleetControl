using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Drivers.DeleteDriver
{
    public class DeleteDriverCommandHandler : IRequestHandler<DeleteDriverCommand, ResultViewModel>
    {
        private readonly IGenericRepository<Driver> _repository;
        public DeleteDriverCommandHandler(IGenericRepository<Driver> repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel> Handle(DeleteDriverCommand request, CancellationToken cancellationToken)
        {
            var driver = await _repository.GetById(request.Id);

            if (driver is null)
                return ResultViewModel.Error("Não foi possível encontrar o motorista informado.");

            driver.SetAsDeleted();

            await _repository.Update(driver);

            return ResultViewModel.Success();
        }
    }
}
