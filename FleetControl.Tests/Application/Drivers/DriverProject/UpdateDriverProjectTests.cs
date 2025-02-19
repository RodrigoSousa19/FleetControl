using FleetControl.Application.Commands.Drivers.DriverProject;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace FleetControl.Tests.Application.DriverProject
{
    public class UpdateDriverProjectsProjectTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly DriverProjectsGenerator _entityGenerator = new DriverProjectsGenerator();
        private readonly ProjectGenerator _userGenerator = new ProjectGenerator();
        private readonly DriverGenerator _driverGenerator = new DriverGenerator();

        [Fact]
        public async Task DriverProjectExists_Update_Success()
        {
            var driverProject = _entityGenerator.Generate();
            var project = _userGenerator.Generate();
            var driver = _driverGenerator.Generate();

            var driverRepository = Substitute.For<IGenericRepository<Driver>>();
            var projectRepository = Substitute.For<IGenericRepository<Project>>();
            var repository = Substitute.For<IGenericRepository<DriverProjects>>();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.DriverRepository.Returns(driverRepository);
            unitOfWork.ProjectRepository.Returns(projectRepository);
            unitOfWork.DriverProjectsRepository.Returns(repository);

            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((DriverProjects?)driverProject));
            projectRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)project));
            driverRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Driver?)driver));

            repository.Update(Arg.Any<DriverProjects>()).Returns(Task.CompletedTask);

            var handler = new UpdateDriverProjectHandler(unitOfWork);

            var command = _generatorsWork.DriverProjectsCommandsGenerator.Commands[CommandType.Update] as UpdateDriverProjectCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await repository.Received(1).Update(Arg.Any<DriverProjects>());
        }

        [Fact]
        public async Task DriverProjectNotExists_Update_Fail()
        {
            var driverRepository = Substitute.For<IGenericRepository<Driver>>();
            var projectRepository = Substitute.For<IGenericRepository<Project>>();
            var repository = Substitute.For<IGenericRepository<DriverProjects>>();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.DriverRepository.Returns(driverRepository);
            unitOfWork.ProjectRepository.Returns(projectRepository);

            repository.GetById(Arg.Any<int>()).ReturnsNull();
            projectRepository.GetById(Arg.Any<int>()).ReturnsNull();
            driverRepository.GetById(Arg.Any<int>()).ReturnsNull();

            var handler = new UpdateDriverProjectHandler(unitOfWork);

            var command = _generatorsWork.DriverProjectsCommandsGenerator.Commands[CommandType.Update] as UpdateDriverProjectCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }
    }
}
