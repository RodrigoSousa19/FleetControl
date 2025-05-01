using FleetControl.Application.Commands.CostCenters.DeleteCostCenter;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace FleetControl.Tests.Application.CostCenters
{
    public class DeleteCostCenterHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();
        private readonly CostCenterGenerator _entityGenerator = new CostCenterGenerator();

        [Fact]
        public async Task CostCenterExists_Delete_Success()
        {
            var costCenter = _entityGenerator.Generate();

            var repository = Substitute.For<IGenericRepository<CostCenter>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.CostCenterRepository.Returns(repository);

            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((CostCenter?)costCenter));
            repository.Update(Arg.Any<CostCenter>()).Returns(Task.CompletedTask);

            var handler = new DeleteCostCenterHandler(unitOfWork);

            var command = _generatorsWork.CostCenterCommandsGenerator.Commands[CommandType.Delete] as DeleteCostCenterCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await repository.Received(1).Update(Arg.Any<CostCenter>());
        }

        [Fact]
        public async Task CostCenterNotExists_Delete_Failt()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.CostCenterRepository.GetById(Arg.Any<int>()).ReturnsNull();

            var handler = new DeleteCostCenterHandler(unitOfWork);

            var command = _generatorsWork.CostCenterCommandsGenerator.Commands[CommandType.Delete] as DeleteCostCenterCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }
    }
}
