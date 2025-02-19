using FleetControl.Application.Commands.Projects.UpdateProject;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using FleetControl.Infrastructure.Persistence.Repositories;
using FleetControl.Tests.Helpers;
using FleetControl.Tests.Helpers.Generators;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

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

            var repository = Substitute.For<IGenericRepository<Project>>();
            var customerRepository = Substitute.For<IGenericRepository<Customer>>();
            var costCenterRepository = Substitute.For<IGenericRepository<CostCenter>>();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.ProjectRepository.Returns(repository);
            unitOfWork.CustomerRepository.Returns(customerRepository);
            unitOfWork.CostCenterRepository.Returns(costCenterRepository);

            customerRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Customer?)customer));
            costCenterRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((CostCenter?)costCenter));
            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)project));

            unitOfWork.ProjectRepository.Update(Arg.Any<Project>()).Returns(Task.CompletedTask);

            var handler = new UpdateProjectHandler(unitOfWork);

            var command = _generatorsWork.ProjectCommandsGenerator.Commands[CommandType.Update] as UpdateProjectCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            await repository.Received(1).Update(Arg.Any<Project>());
        }

        [Fact]
        public async Task InputDataAreNotOk_Update_Fail()
        {
            var repository = Substitute.For<IGenericRepository<Project>>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.ProjectRepository.Returns(repository);

            repository.GetById(Arg.Any<int>()).ReturnsNull();

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

            var repository = Substitute.For<IGenericRepository<Project>>();
            var customerRepository = Substitute.For<IGenericRepository<Customer>>();
            var costCenterRepository = Substitute.For<IGenericRepository<CostCenter>>();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.ProjectRepository.Returns(repository);
            unitOfWork.CustomerRepository.Returns(customerRepository);
            unitOfWork.CostCenterRepository.Returns(costCenterRepository);

            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)project));
            costCenterRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((CostCenter?)costCenter));
            customerRepository.GetById(Arg.Any<int>()).ReturnsNull();

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

            var repository = Substitute.For<IGenericRepository<Project>>();
            var customerRepository = Substitute.For<IGenericRepository<Customer>>();
            var costCenterRepository = Substitute.For<IGenericRepository<CostCenter>>();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.ProjectRepository.Returns(repository);
            unitOfWork.CustomerRepository.Returns(customerRepository);
            unitOfWork.CostCenterRepository.Returns(costCenterRepository);

            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)project));
            costCenterRepository.GetById(Arg.Any<int>()).ReturnsNull();
            customerRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Customer?)customer));

            var handler = new UpdateProjectHandler(unitOfWork);
            var command = _generatorsWork.ProjectCommandsGenerator.Commands[CommandType.Update] as UpdateProjectCommand;

            var result = await handler.Handle(command, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
        }
    }
}
