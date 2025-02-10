using FleetControl.Application.Models;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Customers.DisableCustomer
{
    public class DisableCustomerHandler : IRequestHandler<DisableCustomerCommand, ResultViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DisableCustomerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(DisableCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.GetById(request.Id);

            if (customer is null)
                return ResultViewModel.Error("Não foi possível encontrar o cliente informado.");

            if (!customer.Enabled)
                return ResultViewModel.Error("O cliente informado já se encontra inativo.");

            customer.Disable();

            await _unitOfWork.CustomerRepository.Update(customer);

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
