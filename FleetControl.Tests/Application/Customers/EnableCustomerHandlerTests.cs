using FleetControl.Application.Commands.Customers.EnableCustomer;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace FleetControl.Tests.Application.Customers
{
    public class EnableCustomerHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly CustomerGenerator _entityGenerator = new CustomerGenerator();

        [Fact]
        public async Task CustomerExists_Enable_Success()
        {
            var customer = _entityGenerator.Generate();
            customer.Disable();

            var repository = Substitute.For<IGenericRepository<Customer>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();

            unitOfWork.CustomerRepository.Returns(repository);
            repository.GetById(Arg.Any<int>()).Returns(customer);
            repository.Update(Arg.Any<Customer>()).Returns(Task.CompletedTask);

            var handler = new EnableCustomerHandler(unitOfWork);
            var command = _generatorsWork.CustomerCommandsGenerator.Commands[CommandType.Enable] as EnableCustomerCommand;

            var result = await handler.Handle(command, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            await repository.Received(1).Update(Arg.Any<Customer>());
        }

        [Fact]
        public async Task CustomerNotExists_Enable_Fail()
        {
            var repository = Substitute.For<IGenericRepository<Customer>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();

            unitOfWork.CustomerRepository.Returns(repository);
            repository.GetById(Arg.Any<int>()).Returns((Customer?)null);

            var handler = new EnableCustomerHandler(unitOfWork);
            var command = _generatorsWork.CustomerCommandsGenerator.Commands[CommandType.Enable] as EnableCustomerCommand;

            var result = await handler.Handle(command, CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task CustomerExistsAndIsEnabled_Enable_Fail()
        {
            var customer = _entityGenerator.Generate();
            customer.Enable();

            var repository = Substitute.For<IGenericRepository<Customer>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();

            unitOfWork.CustomerRepository.Returns(repository);
            repository.GetById(Arg.Any<int>()).Returns(customer);
            repository.Update(Arg.Any<Customer>()).Returns(Task.CompletedTask);

            var handler = new EnableCustomerHandler(unitOfWork);
            var command = _generatorsWork.CustomerCommandsGenerator.Commands[CommandType.Enable] as EnableCustomerCommand;

            var result = await handler.Handle(command, CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
        }

    }
}
