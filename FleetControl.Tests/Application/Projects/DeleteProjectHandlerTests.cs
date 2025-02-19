using FleetControl.Application.Commands.Projects.DeleteProject;
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
    public class DeleteProjectHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly ProjectGenerator _entityGenerator = new ProjectGenerator();

        [Fact]
        public async Task ProjectExists_Delete_Success()
        {
            var project = _entityGenerator.Generate();

            var repository = Substitute.For<IGenericRepository<Project>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.ProjectRepository.Returns(repository);

            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)project));
            repository.Update(Arg.Any<Project>()).Returns(Task.CompletedTask);

            var handler = new DeleteProjectHandler(unitOfWork);

            var command = _generatorsWork.ProjectCommandsGenerator.Commands[CommandType.Delete] as DeleteProjectCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await repository.Received(1).Update(Arg.Any<Project>());
        }

        [Fact]
        public async Task ProjectNotExists_Delete_Failt()
        {
            var repository = Substitute.For<IGenericRepository<Project>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.ProjectRepository.Returns(repository);

            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)null));

            var handler = new DeleteProjectHandler(unitOfWork);

            var command = _generatorsWork.ProjectCommandsGenerator.Commands[CommandType.Delete] as DeleteProjectCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }
    }
}

