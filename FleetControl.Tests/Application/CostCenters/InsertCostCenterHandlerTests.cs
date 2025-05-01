using FleetControl.Application.Commands.CostCenters.InsertCostCenter;
using FleetControl.Core.Entities;
using FleetControl.Core.Exceptions;
using FleetControl.Core.Interfaces.Generic;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FluentAssertions;
using NSubstitute;

namespace FleetControl.Tests.Application.CostCenterTests
{
    public class InsertCostCenterHandlerTests
    {
        private readonly GeneratorsWork _generatorsWork = new GeneratorsWork();

        [Fact]
        public async Task InputDataAreOk_Insert_Success()
        {
            var repository = Substitute.For<IGenericRepository<CostCenter>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.CostCenterRepository.Returns(repository);

            var command = _generatorsWork.CostCenterCommandsGenerator.Commands[Helpers.CommandType.Insert] as InsertCostCenterCommand;

            var handler = new InsertCostCenterHandler(unitOfWork);

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await repository.Received(1).Create(Arg.Any<CostCenter>());
        }

        [Fact]
        public async Task InputDataAreNotOk_Insert_ThrowsBusinessException()
        {
            var repository = Substitute.For<IGenericRepository<CostCenter>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();

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
