using FleetControl.Application.Commands.CostCenters.UpdateCostCenter;
using FleetControl.Core.Entities;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace FleetControl.Tests.Application.CostCenters
{
    public class UpdateCostCenterHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();

        [Fact]
        public async Task InputDataAreOk_Update_Success()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            var command = _generatorsWork.CostCenterCommandsGenerator.Commands[Helpers.CommandType.Update] as UpdateCostCenterCommand;

            var handler = new UpdateCostCenterHandler(unitOfWork);

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await unitOfWork.CostCenterRepository.Received(1).Update(Arg.Any<CostCenter>());
        } 
    }
}
