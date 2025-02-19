using FleetControl.Application.Commands.Drivers.DriverProject;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using NSubstitute;

namespace FleetControl.Tests.Application.DriverProjectss.DriverProject
{
    public class InsertDriverProjectsProjectHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly ProjectGenerator _projectGenerator = new ProjectGenerator();
        private readonly DriverGenerator _driverGenerator = new DriverGenerator();
        [Fact]
        public async Task InputDataAreOk_Insert_Success()
        {
            var project = _projectGenerator.Generate();
            var driver = _driverGenerator.Generate();

            var driverRepository = Substitute.For<IGenericRepository<Driver>>();
            var projectRepository = Substitute.For<IGenericRepository<Project>>();
            var repository = Substitute.For<IGenericRepository<DriverProjects>>();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.DriverRepository.Returns(driverRepository);
            unitOfWork.ProjectRepository.Returns(projectRepository);
            unitOfWork.DriverProjectsRepository.Returns(repository);

            projectRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)project));
            driverRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Driver?)driver));

            var command = _generatorsWork.DriverProjectsCommandsGenerator.Commands[CommandType.Insert] as InsertDriverProjectCommand;

            var handler = new InsertDriverProjectHandler(unitOfWork);

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await repository.Received(1).Create(Arg.Any<DriverProjects>());
        }

        [Fact]
        public async Task InputDataAreNotOk_Insert_ThrowsBusinessException()
        {
            var repository = Substitute.For<IGenericRepository<DriverProjects>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.DriverProjectsRepository.Returns(repository);

            var command = new InsertDriverProjectCommand
            {
                IdDriver = 0,
                IdProject = 0
            };

            var handler = new InsertDriverProjectHandler(unitOfWork);

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();

            await repository.DidNotReceive().Create(Arg.Any<DriverProjects>());
        }
    }
}
