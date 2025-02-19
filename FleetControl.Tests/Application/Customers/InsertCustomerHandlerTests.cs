using FleetControl.Application.Commands.Customers.InsertCustomer;
using FleetControl.Core.Entities;
using FleetControl.Core.Exceptions;
using FleetControl.Core.Interfaces.Generic;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FluentAssertions;
using NSubstitute;

namespace FleetControl.Tests.Application.Customers
{
    public class InsertCustomerHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();

        [Fact]
        public async Task InputDataAreOk_Insert_Success()
        {
            var repository = Substitute.For<IGenericRepository<Customer>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.CustomerRepository.Returns(repository);

            var command = _generatorsWork.CustomerCommandsGenerator.Commands[CommandType.Insert] as InsertCustomerCommand;
            var handler = new InsertCustomerHandler(unitOfWork);

            var result = await handler.Handle(command, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            await repository.Received(1).Create(Arg.Any<Customer>());
        }

        [Fact]
        public async Task InputDataAreNotOk_Insert_ThrowsBusinessException()
        {
            var repository = Substitute.For<IGenericRepository<Customer>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.CustomerRepository.Returns(repository);

            var command = new InsertCustomerCommand
            {
                Address = "",
                Cnpj = "1233423524",
                Contact = "1231412",
                Email = "test@mail",
                Name = ""
            };

            var handler = new InsertCustomerHandler(unitOfWork);

            await FluentActions.Invoking(() => handler.Handle(command, CancellationToken.None))
                .Should().ThrowAsync<BusinessException>();

            await repository.DidNotReceive().Create(Arg.Any<Customer>());
        }
    }
}
