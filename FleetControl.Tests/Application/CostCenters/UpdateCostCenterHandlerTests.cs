using FleetControl.Application.Commands.CostCenters.UpdateCostCenter;
using FleetControl.Core.Entities;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace FleetControl.Tests.Application.CostCenters
{
    public class UpdateCostCenterHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly CostCenterGenerator _entityGenerator = new CostCenterGenerator();

        [Fact]
        public async Task CostCenterExists_Update_Success()
        {
            var costCenter = _entityGenerator.Generate();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();

            unitOfWork.CostCenterRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((CostCenter?)costCenter));
            unitOfWork.CostCenterRepository.Update(Arg.Any<CostCenter>()).Returns(Task.CompletedTask);

            var handler = new UpdateCostCenterHandler(unitOfWork);

            var command = _generatorsWork.CostCenterCommandsGenerator.Commands[Helpers.CommandType.Update] as UpdateCostCenterCommand;
            
            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await unitOfWork.CostCenterRepository.Received(1).Update(Arg.Any<CostCenter>());
        } 
    }
}
