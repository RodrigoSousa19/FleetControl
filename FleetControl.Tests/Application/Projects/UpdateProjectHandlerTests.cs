using FleetControl.Application.Commands.Projects.UpdateProject;
using FleetControl.Core.Entities;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace FleetControl.Tests.Application.Projects
{
    public class UpdateProjectHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly ProjectGenerator _entityGenerator = new ProjectGenerator();
        private readonly CustomerGenerator _customerGenerator = new CustomerGenerator();
        private readonly CostCenterGenerator _costCenterGenerator = new CostCenterGenerator();

        [Fact]
        public async Task ProjectExists_Update_Success()
        {
            var project = _entityGenerator.Generate();
            var customer = _customerGenerator.Generate();
            var costCenter = _costCenterGenerator.Generate();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.CustomerRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Customer?)customer));
            unitOfWork.CostCenterRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((CostCenter?)costCenter));
            unitOfWork.ProjectRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)project));

            unitOfWork.ProjectRepository.Update(Arg.Any<Project>()).Returns(Task.CompletedTask);

            var handler = new UpdateProjectHandler(unitOfWork);

            var command = _generatorsWork.ProjectCommandsGenerator.Commands[CommandType.Update] as UpdateProjectCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await unitOfWork.ProjectRepository.Received(1).Update(Arg.Any<Project>());
        }

        [Fact]
        public async Task InputDataAreNotOk_Update_Fail()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.ProjectRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)null));

            var handler = new UpdateProjectHandler(unitOfWork);

            var command = _generatorsWork.ProjectCommandsGenerator.Commands[CommandType.Update] as UpdateProjectCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ProjectExistsButCustomerNotExists_Update_Fail()
        {
            var project = _entityGenerator.Generate();
            var costCenter = _costCenterGenerator.Generate();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.ProjectRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)project));
            unitOfWork.CostCenterRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((CostCenter?)costCenter));
            unitOfWork.CustomerRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Customer?)null));

            var handler = new UpdateProjectHandler(unitOfWork);
            var command = _generatorsWork.ProjectCommandsGenerator.Commands[CommandType.Update] as UpdateProjectCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ProjectExistsButCostCenterNotExists_Update_Fail()
        {
            var project = _entityGenerator.Generate();
            var customer = _customerGenerator.Generate();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.ProjectRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)project));
            unitOfWork.CostCenterRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((CostCenter?)null));
            unitOfWork.CustomerRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Customer?)customer));

            var handler = new UpdateProjectHandler(unitOfWork);
            var command = _generatorsWork.ProjectCommandsGenerator.Commands[CommandType.Update] as UpdateProjectCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }
    }
}
