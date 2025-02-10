using FleetControl.Application.Models;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Customers.DeleteCustomer
{
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, ResultViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCustomerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.GetById(request.Id);

            if (customer is null)
                return ResultViewModel.Error("Não foi possível encontrar o cliente informado.");

            customer.SetAsDeleted();

            await _unitOfWork.CustomerRepository.Update(customer);

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
