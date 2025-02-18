using FleetControl.Application.Commands.Customers.UpdateCustomer;
using FleetControl.Core.Entities;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace FleetControl.Tests.Application.Customers
{
    public class UpdateCustomerHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly CustomerGenerator _entityGenerator = new CustomerGenerator();

        [Fact]
        public async Task CustomerExists_Update_Success()
        {
            var customer = _entityGenerator.Generate();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.CustomerRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Customer?)customer));
            unitOfWork.CustomerRepository.Update(Arg.Any<Customer>()).Returns(Task.CompletedTask);

            var handler = new UpdateCustomerHandler(unitOfWork);

            var command = _generatorsWork.CustomerCommandsGenerator.Commands[CommandType.Update] as UpdateCustomerCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await unitOfWork.CustomerRepository.Received(1).Update(Arg.Any<Customer>());
        }

        [Fact]
        public async Task CustomerNotExists_Update_Fail()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.CustomerRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Customer?)null));

            var handler = new UpdateCustomerHandler(unitOfWork);

            var command = _generatorsWork.CustomerCommandsGenerator.Commands[CommandType.Update] as UpdateCustomerCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }
    }
}
