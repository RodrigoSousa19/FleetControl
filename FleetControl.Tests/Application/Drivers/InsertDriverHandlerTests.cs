using FleetControl.Application.Commands.Drivers.InsertDriver;
using FleetControl.Core.Entities;
using FleetControl.Core.Enums.User;
using FleetControl.Core.Exceptions;
using FleetControl.Core.Interfaces.Generic;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
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

            var repository = Substitute.For<IGenericRepository<Driver>>();
            var userRepository = Substitute.For<IGenericRepository<User>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();

            unitOfWork.DriverRepository.Returns(repository);
            unitOfWork.UserRepository.Returns(userRepository);

            userRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((User?)user));

            var command = _generatorsWork.DriverCommandsGenerator.Commands[CommandType.Insert] as InsertDriverCommand;

            var handler = new InsertDriverHandler(unitOfWork);

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await repository.Received(1).Create(Arg.Any<Driver>());
        }

        [Fact]
        public async Task InputDataAreNotOk_Insert_ThrowsBusinessException()
        {
            var repository = Substitute.For<IGenericRepository<Driver>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.DriverRepository.Returns(repository);

            var command = new InsertDriverCommand
            {
                DocumentNumber = "12345",
                DocumentType = DocumentType.DriversLicense,
                IdUser = 0
            };

            var handler = new InsertDriverHandler(unitOfWork);

            await FluentActions.Invoking(() => handler.Handle(command, new CancellationToken()))
                .Should().ThrowAsync<BusinessException>();

            await repository.DidNotReceive().Create(Arg.Any<Driver>());
        }
    }
}
