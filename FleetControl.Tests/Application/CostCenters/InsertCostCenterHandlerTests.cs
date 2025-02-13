using FleetControl.Application.Commands.CostCenters.InsertCostCenter;
using FleetControl.Core.Entities;
using FleetControl.Core.Exceptions;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace FleetControl.Tests.Application.CostCenterTests
{
    public class InsertCostCenterHandlerTests
    {
        private readonly CostCenterGenerator _generator = new CostCenterGenerator();
        private readonly CostCenterCommandsGenerator _commandGenerator = new CostCenterCommandsGenerator();

        [Fact]
        public async Task InputDataAreOk_Insert_Success()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            var command = _commandGenerator.Commands[Helpers.CommandType.Insert] as InsertCostCenterCommand;

            var handler = new InsertCostCenterHandler(unitOfWork);

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await unitOfWork.CostCenterRepository.Received(1).Create(Arg.Any<CostCenter>());
        }

        [Fact]
        public async Task InputDataAreNotOk_Insert_ThrowsBusinessException()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            var command = new InsertCostCenterCommand
            {
                Description = ""
            };

            var handler = new InsertCostCenterHandler(unitOfWork);

            await FluentActions.Invoking(() => handler.Handle(command, new CancellationToken()))
                .Should().ThrowAsync<BusinessException>();

            await unitOfWork.CostCenterRepository.DidNotReceive().Create(Arg.Any<CostCenter>());
        }
    }
}
