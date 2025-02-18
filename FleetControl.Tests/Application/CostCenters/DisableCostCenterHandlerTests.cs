using FleetControl.Application.Commands.CostCenters.DisableCostCenter;
using FleetControl.Core.Entities;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace FleetControl.Tests.Application.CostCenters
{
    public class DisableCostCenterHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly CostCenterGenerator _entityGenerator = new CostCenterGenerator();

        [Fact]
        public async Task CostCenterExists_Disable_Success()
        {
            var costCenter = _entityGenerator.Generate();

            costCenter.Enable();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.CostCenterRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((CostCenter?)costCenter));
            unitOfWork.CostCenterRepository.Update(Arg.Any<CostCenter>()).Returns(Task.CompletedTask);

            var handler = new DisableCostCenterHandler(unitOfWork);

            var command = _generatorsWork.CostCenterCommandsGenerator.Commands[CommandType.Disable] as DisableCostCenterCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await unitOfWork.CostCenterRepository.Received(1).Update(Arg.Any<CostCenter>());
        }

        [Fact]
        public async Task CostCenterNotExists_Disable_Fail()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.CostCenterRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((CostCenter?)null));

            var handler = new DisableCostCenterHandler(unitOfWork);

            var command = _generatorsWork.CostCenterCommandsGenerator.Commands[CommandType.Disable] as DisableCostCenterCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task CostCenterExistsAndIsDisabled_Disable_Fail()
        {
            var costCenter = _entityGenerator.Generate();

            costCenter.Disable();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.CostCenterRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((CostCenter?)costCenter));
            unitOfWork.CostCenterRepository.Update(Arg.Any<CostCenter>()).Returns(Task.CompletedTask);

            var handler = new DisableCostCenterHandler(unitOfWork);

            var command = _generatorsWork.CostCenterCommandsGenerator.Commands[CommandType.Disable] as DisableCostCenterCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }
    }
}
