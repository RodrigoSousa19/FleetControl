using FleetControl.Core.Entities;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers.Generators;
using FleetControl.Tests.Helpers;
using MediatR;
using NSubstitute;
using FleetControl.Application.Commands.Drivers.DriverProject;
using FluentAssertions;

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

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.DriverProjectsRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((DriverProjects?)driverProject));
            unitOfWork.ProjectRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)project));
            unitOfWork.DriverRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Driver?)driver));

            unitOfWork.DriverProjectsRepository.Update(Arg.Any<DriverProjects>()).Returns(Task.CompletedTask);

            var handler = new UpdateDriverProjectHandler(unitOfWork);

            var command = _generatorsWork.DriverProjectsCommandsGenerator.Commands[CommandType.Update] as UpdateDriverProjectCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await unitOfWork.DriverProjectsRepository.Received(1).Update(Arg.Any<DriverProjects>());
        }

        [Fact]
        public async Task DriverProjectNotExists_Update_Fail()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.DriverProjectsRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((DriverProjects?)null));
            unitOfWork.ProjectRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)null));
            unitOfWork.DriverRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Driver?)null));

            var handler = new UpdateDriverProjectHandler(unitOfWork);

            var command = _generatorsWork.DriverProjectsCommandsGenerator.Commands[CommandType.Update] as UpdateDriverProjectCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }
    }
}
