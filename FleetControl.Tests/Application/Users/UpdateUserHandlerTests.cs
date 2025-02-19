using FleetControl.Application.Commands.Users.UpdateUser;
using FleetControl.Core.Entities;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace FleetControl.Tests.Application.Users
{
    public class UpdateUserHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly UserGenerator _entityGenerator = new UserGenerator();

        [Fact]
        public async Task UserExists_Update_Success()
        {
            var user = _entityGenerator.Generate();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.UserRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((User?)user));
            unitOfWork.UserRepository.Update(Arg.Any<User>()).Returns(Task.CompletedTask);

            var handler = new UpdateUserHandler(unitOfWork);

            var command = _generatorsWork.UserCommandsGenerator.Commands[CommandType.Update] as UpdateUserCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await unitOfWork.UserRepository.Received(1).Update(Arg.Any<User>());
        }

        [Fact]
        public async Task UserNotExists_Update_Fail()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.UserRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((User?)null));

            var handler = new UpdateUserHandler(unitOfWork);

            var command = _generatorsWork.UserCommandsGenerator.Commands[CommandType.Update] as UpdateUserCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }
    }
}
