using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Drivers.InsertDriver
{
    public class InsertDriverCommandHandler : IRequestHandler<InsertDriverCommand, ResultViewModel<Driver>>
    {
        private readonly IGenericRepository<Driver> _repository;

        public InsertDriverCommandHandler(IGenericRepository<Driver> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<Driver>> Handle(InsertDriverCommand request, CancellationToken cancellationToken)
        {
            var driver = await _repository.Create(request.ToEntity());

            return ResultViewModel<Driver>.Success(driver);
        }
    }
}
