using FleetControl.Application.Models;
using FleetControl.Application.Models.Customers;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Queries.Customers.GetAll
{
    public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, ResultViewModel<IList<CustomerViewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCustomersHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<IList<CustomerViewModel>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _unitOfWork.CustomerRepository.GetAll();

            var model = customers.Select(CustomerViewModel.FromEntity).ToList();

            return ResultViewModel<IList<CustomerViewModel>>.Success(model);
        }
    }
}
