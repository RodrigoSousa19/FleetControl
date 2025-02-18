using FleetControl.Application.Commands.Drivers.DriverProject;
using FleetControl.Core.Entities;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace FleetControl.Tests.Application.Drivers.DriverProject
{
    public class DeleteDriverProjectHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly DriverProjectsGenerator _entityGenerator = new DriverProjectsGenerator();

        [Fact]
        public async Task DriverProjectExists_Delete_Success()
        {
            var driverProject = _entityGenerator.Generate();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.DriverProjectsRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((DriverProjects?)driverProject));
            unitOfWork.DriverProjectsRepository.Update(Arg.Any<DriverProjects>()).Returns(Task.CompletedTask);

            var handler = new DeleteDriverProjectHandler(unitOfWork);

            var command = _generatorsWork.DriverProjectsCommandsGenerator.Commands[CommandType.Delete] as DeleteDriverProjectCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await unitOfWork.DriverProjectsRepository.Received(1).Update(Arg.Any<DriverProjects>());
        }

        [Fact]
        public async Task DriverProjectNotExists_Delete_Failt()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.DriverProjectsRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((DriverProjects?)null));

            var handler = new DeleteDriverProjectHandler(unitOfWork);

            var command = _generatorsWork.DriverProjectsCommandsGenerator.Commands[CommandType.Delete] as DeleteDriverProjectCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }
    }
}
