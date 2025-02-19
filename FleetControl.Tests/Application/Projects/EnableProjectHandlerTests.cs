using FleetControl.Application.Commands.Projects.EnableProject;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace FleetControl.Tests.Application.Projects
{
    public class EnableProjectHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly ProjectGenerator _entityGenerator = new ProjectGenerator();

        [Fact]
        public async Task ProjectExists_Enable_Success()
        {
            var project = _entityGenerator.Generate();

            project.Disable();

            var repository = Substitute.For<IGenericRepository<Project>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.ProjectRepository.Returns(repository);

            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)project));
            repository.Update(Arg.Any<Project>()).Returns(Task.CompletedTask);

            var handler = new EnableProjectHandler(unitOfWork);

            var command = _generatorsWork.ProjectCommandsGenerator.Commands[CommandType.Enable] as EnableProjectCommand;


            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await repository.Received(1).Update(Arg.Any<Project>());
        }

        [Fact]
        public async Task ProjectNotExists_Enable_Fail()
        {
            var repository = Substitute.For<IGenericRepository<Project>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.ProjectRepository.Returns(repository);

            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)null));

            var handler = new EnableProjectHandler(unitOfWork);

            var command = _generatorsWork.ProjectCommandsGenerator.Commands[CommandType.Enable] as EnableProjectCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ProjectExistsAndIsEnabled_Enable_Fail()
        {
            var project = _entityGenerator.Generate();
            project.Enable();

            var repository = Substitute.For<IGenericRepository<Project>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.ProjectRepository.Returns(repository);

            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)project));
            repository.Update(Arg.Any<Project>()).Returns(Task.CompletedTask);

            var handler = new EnableProjectHandler(unitOfWork);

            var command = _generatorsWork.ProjectCommandsGenerator.Commands[CommandType.Enable] as EnableProjectCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }
    }
}
