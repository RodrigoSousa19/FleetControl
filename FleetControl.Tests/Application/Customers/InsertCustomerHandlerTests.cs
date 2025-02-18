using FleetControl.Application.Commands.Customers.InsertCustomer;
using FleetControl.Core.Entities;
using FleetControl.Core.Exceptions;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace FleetControl.Tests.Application.Customers
{
    public class InsertCustomerHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();

        [Fact]
        public async Task InputDataAreOk_Insert_Success()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            var command = _generatorsWork.CustomerCommandsGenerator.Commands[CommandType.Insert] as InsertCustomerCommand;

            var handler = new InsertCustomerHandler(unitOfWork);

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await unitOfWork.CustomerRepository.Received(1).Create(Arg.Any<Customer>());
        }

        [Fact]
        public async Task InputDataAreNotOk_Insert_ThrowsBusinessException()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            var command = new InsertCustomerCommand
            {
                Address = "",
                Cnpj = "1233423524",
                Contact = "1231412",
                Email = "test@mail",
                Name = ""
            };

            var handler = new InsertCustomerHandler(unitOfWork);

            await FluentActions.Invoking(() => handler.Handle(command, new CancellationToken()))
                .Should().ThrowAsync<BusinessException>();

            await unitOfWork.CustomerRepository.DidNotReceive().Create(Arg.Any<Customer>());
        }
    }
}
