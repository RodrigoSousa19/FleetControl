using FleetControl.Application.Models;
using FleetControl.Application.Models.Customers;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Queries.Customers.GetById
{
    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, ResultViewModel<CustomerViewModel>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public GetCustomerByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<CustomerViewModel>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.GetById(request.Id);

            if (customer is null)
                return ResultViewModel<CustomerViewModel>.Error("Não foi possível localizar o cliente informado.");

            var model = CustomerViewModel.FromEntity(customer);

            return ResultViewModel<CustomerViewModel>.Success(model);
        }
    }
}
