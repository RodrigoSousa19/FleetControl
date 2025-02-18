using FleetControl.Application.Commands.Drivers.InsertDriver;
using FleetControl.Core.Entities;
using FleetControl.Core.Enums.User;
using FleetControl.Core.Exceptions;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace FleetControl.Tests.Application.Drivers
{
    public class InsertDriverHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly UserGenerator _userGenerator = new UserGenerator();
        [Fact]
        public async Task InputDataAreOk_Insert_Success()
        {
            var user = _userGenerator.Generate();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.UserRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((User?)user));

            var command = _generatorsWork.DriverCommandsGenerator.Commands[CommandType.Insert] as InsertDriverCommand;

            var handler = new InsertDriverHandler(unitOfWork);

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await unitOfWork.DriverRepository.Received(1).Create(Arg.Any<Driver>());
        }

        [Fact]
        public async Task InputDataAreNotOk_Insert_ThrowsBusinessException()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            var command = new InsertDriverCommand
            {
                DocumentNumber = "12345",
                DocumentType = DocumentType.DriversLicense,
                IdUser = 0
            };

            var handler = new InsertDriverHandler(unitOfWork);

            await FluentActions.Invoking(() => handler.Handle(command, new CancellationToken()))
                .Should().ThrowAsync<BusinessException>();

            await unitOfWork.DriverRepository.DidNotReceive().Create(Arg.Any<Driver>());
        }
    }
}
