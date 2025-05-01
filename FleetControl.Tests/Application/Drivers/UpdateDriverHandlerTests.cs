using FleetControl.Application.Commands.Drivers.UpdateDriver;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Infrastructure.Persistence.Repositories.Interfaces;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace FleetControl.Tests.Application.Drivers
{
    public class UpdateDriverHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly DriverGenerator _entityGenerator = new DriverGenerator();
        private readonly UserGenerator _userGenerator = new UserGenerator();

        [Fact]
        public async Task DriverExists_Update_Success()
        {
            var driver = _entityGenerator.Generate();
            var user = _userGenerator.Generate();

            var repository = Substitute.For<IGenericRepository<Driver>>();
            var userRepository = Substitute.For<IUserRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();

            unitOfWork.DriverRepository.Returns(repository);
            unitOfWork.UserRepository.Returns(userRepository);

            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Driver?)driver));
            userRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((User?)user));
            repository.Update(Arg.Any<Driver>()).Returns(Task.CompletedTask);

            var handler = new UpdateDriverHandler(unitOfWork);

            var command = _generatorsWork.DriverCommandsGenerator.Commands[CommandType.Update] as UpdateDriverCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await repository.Received(1).Update(Arg.Any<Driver>());
        }

        [Fact]
        public async Task DriverNotExists_Update_Fail()
        {
            var repository = Substitute.For<IGenericRepository<Driver>>();
            var userRepository = Substitute.For<IUserRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();

            unitOfWork.DriverRepository.Returns(repository);
            unitOfWork.UserRepository.Returns(userRepository);

            repository.GetById(Arg.Any<int>()).ReturnsNull();
            unitOfWork.UserRepository.GetById(Arg.Any<int>()).ReturnsNull();

            var handler = new UpdateDriverHandler(unitOfWork);

            var command = _generatorsWork.DriverCommandsGenerator.Commands[CommandType.Update] as UpdateDriverCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }
    }
}
