using FleetControl.Application.Commands.Projects.InsertProject;
using FleetControl.Core.Entities;
using FleetControl.Core.Exceptions;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace FleetControl.Tests.Application.Projects
{
    public class InsertProjectHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly CustomerGenerator _customerGenerator = new CustomerGenerator();
        private readonly CostCenterGenerator _costCenterGenerator = new CostCenterGenerator();

        [Fact]
        public async Task InputDataAreOk_Insert_Success()
        {
            var customer = _customerGenerator.Generate();
            var costCenter = _costCenterGenerator.Generate();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.CustomerRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Customer?)customer));
            unitOfWork.CostCenterRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((CostCenter?)costCenter));

            var command = _generatorsWork.ProjectCommandsGenerator.Commands[CommandType.Insert] as InsertProjectCommand;

            var handler = new InsertProjectHandler(unitOfWork);

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await unitOfWork.ProjectRepository.Received(1).Create(Arg.Any<Project>());
        }

        [Fact]
        public async Task InputDataAreNotOk_Insert_ThrowsBusinessException()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            var command = new InsertProjectCommand
            {
                Description = "",
                IdCostCenter = 0,
                IdCustomer = 0
            };

            var handler = new InsertProjectHandler(unitOfWork);

            await FluentActions.Invoking(() => handler.Handle(command, new CancellationToken()))
                .Should().ThrowAsync<BusinessException>();

            await unitOfWork.ProjectRepository.DidNotReceive().Create(Arg.Any<Project>());
        }

        [Fact]
        public async Task CustomerNotExists_Insert_Fail()
        {
            var costCenter = _costCenterGenerator.Generate();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.CustomerRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Customer?)null));
            unitOfWork.CostCenterRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((CostCenter?)costCenter));

            var command = _generatorsWork.ProjectCommandsGenerator.Commands[CommandType.Insert] as InsertProjectCommand;

            var handler = new InsertProjectHandler(unitOfWork);

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();

            await unitOfWork.ProjectRepository.DidNotReceive().Create(Arg.Any<Project>());
        }

        [Fact]
        public async Task CostCenterNotExists_Insert_Fail()
        {
            var customer = _customerGenerator.Generate();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.CustomerRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Customer?)customer));
            unitOfWork.CostCenterRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((CostCenter?)null));

            var command = _generatorsWork.ProjectCommandsGenerator.Commands[CommandType.Insert] as InsertProjectCommand;

            var handler = new InsertProjectHandler(unitOfWork);

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();

            await unitOfWork.ProjectRepository.DidNotReceive().Create(Arg.Any<Project>());
        }
    }
}
