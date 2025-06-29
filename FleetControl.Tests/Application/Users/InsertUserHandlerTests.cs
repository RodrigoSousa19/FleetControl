﻿using FleetControl.Application.Commands.Users.InsertUser;
using FleetControl.Core.Entities;
using FleetControl.Core.Exceptions;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Infrastructure.Security;
using FleetControl.Tests.Helpers;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using NSubstitute;

namespace FleetControl.Tests.Application.Users
{
    public class InsertUserHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();

        [Fact]
        public async Task InputDataAreOk_Insert_Success()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();
            var cache = Substitute.For<IMemoryCache>();
            var authService = Substitute.For<IAuthService>();

            var command = _generatorsWork.UserCommandsGenerator.Commands[CommandType.Insert] as InsertUserCommand;

            var handler = new InsertUserHandler(unitOfWork, authService, cache);

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await unitOfWork.UserRepository.Received(1).Create(Arg.Any<User>());
        }

        [Fact]
        public async Task InputDataAreNotOk_Insert_ThrowsBusinessException()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();
            var cache = Substitute.For<IMemoryCache>();
            var authService = Substitute.For<IAuthService>();

            var command = new InsertUserCommand
            {
                Email = "ab@abc@.com",
                Name = "",
                Password = "",
                BirthDate = DateTime.MinValue
            };

            var handler = new InsertUserHandler(unitOfWork, authService, cache);

            await FluentActions.Invoking(() => handler.Handle(command, new CancellationToken()))
                .Should().ThrowAsync<BusinessException>();

            await unitOfWork.UserRepository.DidNotReceive().Create(Arg.Any<User>());
        }
    }
}
