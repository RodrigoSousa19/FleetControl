using FleetControl.Application.Commands.Projects.DisableProject;
using FleetControl.Core.Entities;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace FleetControl.Tests.Application.Projects
{
    public class DisableProjectHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly ProjectGenerator _entityGenerator = new ProjectGenerator();

        [Fact]
        public async Task ProjectExists_Disable_Success()
        {
            var project = _entityGenerator.Generate();

            project.Enable();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.ProjectRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)project));
            unitOfWork.ProjectRepository.Update(Arg.Any<Project>()).Returns(Task.CompletedTask);

            var handler = new DisableProjectHandler(unitOfWork);

            var command = _generatorsWork.ProjectCommandsGenerator.Commands[CommandType.Disable] as DisableProjectCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await unitOfWork.ProjectRepository.Received(1).Update(Arg.Any<Project>());
        }

        [Fact]
        public async Task ProjectNotExists_Disable_Fail()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.ProjectRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)null));

            var handler = new DisableProjectHandler(unitOfWork);

            var command = _generatorsWork.ProjectCommandsGenerator.Commands[CommandType.Disable] as DisableProjectCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ProjectExistsAndIsDisabled_Disable_Fail()
        {
            var project = _entityGenerator.Generate();

            project.Disable();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.ProjectRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)project));
            unitOfWork.ProjectRepository.Update(Arg.Any<Project>()).Returns(Task.CompletedTask);

            var handler = new DisableProjectHandler(unitOfWork);

            var command = _generatorsWork.ProjectCommandsGenerator.Commands[CommandType.Disable] as DisableProjectCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }
    }
}
