﻿using FleetControl.Application.Commands.Customers.UpdateCustomer;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

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

            var repository = Substitute.For<IGenericRepository<Customer>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.CustomerRepository.Returns(repository);

            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Customer?)customer));
            repository.Update(Arg.Any<Customer>()).Returns(Task.CompletedTask);

            var handler = new UpdateCustomerHandler(unitOfWork);

            var command = _generatorsWork.CustomerCommandsGenerator.Commands[CommandType.Update] as UpdateCustomerCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await repository.Received(1).Update(Arg.Any<Customer>());
        }

        [Fact]
        public async Task CustomerNotExists_Update_Fail()
        {
            var repository = Substitute.For<IGenericRepository<Customer>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.CustomerRepository.Returns(repository);

            repository.GetById(Arg.Any<int>()).ReturnsNull();

            var handler = new UpdateCustomerHandler(unitOfWork);

            var command = _generatorsWork.CustomerCommandsGenerator.Commands[CommandType.Update] as UpdateCustomerCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }
    }
}
