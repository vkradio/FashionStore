using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Moq;
using MvvmInfrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Utilities;
using ViewModels;
using Xunit;

namespace ViewModelsTests
{
    public class WorkspaceViewModelUT
    {
        [Fact]
        public void ConstructorThrowsIfDialogServiceIsNull()
        {
            // Arrange
            IDialogService nullDialogService = null;
            var mockWarehouseMgmtSvc = new Mock<IWarehouseManagementService>();
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => new WorkspaceViewModel(nullDialogService, mockWarehouseMgmtSvc.Object, mockLegacyWorkspace.Object));

            // Assert
            Assert.NotNull(ex);
        }

        [Fact]
        public void ConstructorThrowsIfWarehouseManagementServiceIsNull()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            IWarehouseManagementService nullWhMgmtSvc = null;
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => new WorkspaceViewModel(mockDialogService.Object, nullWhMgmtSvc, mockLegacyWorkspace.Object));

            // Assert
            Assert.NotNull(ex);
        }

        [Fact]
        public void ConstructorThrowsIfLegacyWorkspaceContextIsNull()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockWarehouseMgmtSvc = new Mock<IWarehouseManagementService>();
            ILegacyWorkspaceContext nullLegacyWorkspace = null;

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => new WorkspaceViewModel(mockDialogService.Object, mockWarehouseMgmtSvc.Object, nullLegacyWorkspace));

            // Assert
            Assert.NotNull(ex);
        }

        [Fact]
        public void BeforeInitUnsavedEntryIsTrueIfTrue()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockWarehouseMgmtSvc = new Mock<IWarehouseManagementService>();
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            const bool thereIsUnsavedEntryValue = true;
            mockLegacyWorkspace
                .Setup(w => w.IsThereUnsavedEntry())
                .Returns(thereIsUnsavedEntryValue);
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockWarehouseMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            var result = workspace.IsThereUnsavedEntry();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void BeforeInitUnsavedEntryIsFalseIfFalse()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockWarehouseMgmtSvc = new Mock<IWarehouseManagementService>();
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            const bool thereIsNoUnsavedEntryValue = false;
            mockLegacyWorkspace
                .Setup(w => w.IsThereUnsavedEntry())
                .Returns(thereIsNoUnsavedEntryValue);
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockWarehouseMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            var result = workspace.IsThereUnsavedEntry();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void AfterInitUnsavedEntryIsTrueIfTrue()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockWarehouseMgmtSvc = new Mock<IWarehouseManagementService>();
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            const bool thereIsUnsavedEntryValue = true;
            mockLegacyWorkspace
                .Setup(w => w.IsThereUnsavedEntry())
                .Returns(thereIsUnsavedEntryValue);
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockWarehouseMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            await workspace.Initialize();
            var result = workspace.IsThereUnsavedEntry();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void AfterInitUnsavedEntryIsFalseIfFalse()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockWarehouseMgmtSvc = new Mock<IWarehouseManagementService>();
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            const bool thereIsNoUnsavedEntryValue = false;
            mockLegacyWorkspace
                .Setup(w => w.IsThereUnsavedEntry())
                .Returns(thereIsNoUnsavedEntryValue);
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockWarehouseMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            await workspace.Initialize();
            var result = workspace.IsThereUnsavedEntry();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void BeforeInitWarehouseSelectorIsNotNull()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockWarehouseMgmtSvc = new Mock<IWarehouseManagementService>();
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockWarehouseMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            var whSelector = workspace.WarehouseSelector;

            // Assert
            Assert.NotNull(whSelector);
        }

        [Fact]
        public void BeforeInitCurrentWarehouseIsNull()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockWarehouseMgmtSvc = new Mock<IWarehouseManagementService>();
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockWarehouseMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            var currentWarehouse = workspace.CurrentWarehouse;

            // Assert
            Assert.Null(currentWarehouse);
        }

        [Fact]
        public async void AfterInitCurrentWarehouseIsNullIfThereAreNoWarehouses()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockWarehouseMgmtSvc = new Mock<IWarehouseManagementService>();
            var emptyWarehouseCollection = Array.Empty<Warehouse>();
            mockWarehouseMgmtSvc
                .Setup(wService => wService.GetFunctioningOrderedWarehousesAsync())
                .ReturnsAsync(emptyWarehouseCollection);
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockWarehouseMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            await workspace.Initialize();
            var currentWarehouse = workspace.CurrentWarehouse;

            // Assert
            Assert.Null(currentWarehouse);
        }

        [Fact]
        public async void AfterInitCurrentWarehouseIsSetFromSingleWarehouse()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockWarehouseMgmtSvc = new Mock<IWarehouseManagementService>();
            var singleWarehouse = new Warehouse { WarehouseId = 1 };
            var singleWarehouseCollection = new Warehouse[] { singleWarehouse };
            mockWarehouseMgmtSvc
                .Setup(wService => wService.GetFunctioningOrderedWarehousesAsync())
                .ReturnsAsync(singleWarehouseCollection);
            mockWarehouseMgmtSvc
                .Setup(wService => wService.GetWarehouse(It.IsAny<int>()))
                .ReturnsAsync(singleWarehouse);
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockWarehouseMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            await workspace.Initialize();
            var currentWarehouse = workspace.CurrentWarehouse;

            // Assert
            Assert.Equal(singleWarehouse, currentWarehouse);
        }

        [Fact]
        public async void AfterInitCurrentWarehouseIsSetToFirstFrom5Warehouses()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockWarehouseMgmtSvc = new Mock<IWarehouseManagementService>();
            const string namePrefix = "Warehouse ";
            var fiveWarehouseCollection = Enumerable
                .Range(1, 5)
                .Select(i => new Warehouse { WarehouseId = i, Name = namePrefix + i.ToString(CultureInfo.InvariantCulture) })
                .ToList();
            var firstWarehouse = fiveWarehouseCollection[0];
            mockWarehouseMgmtSvc
                .Setup(wService => wService.GetFunctioningOrderedWarehousesAsync())
                .ReturnsAsync(fiveWarehouseCollection);
            mockWarehouseMgmtSvc
                .Setup(wService => wService.GetWarehouse(It.IsAny<int>()))
                .ReturnsAsync(firstWarehouse);
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockWarehouseMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            await workspace.Initialize();
            var currentWarehouse = workspace.CurrentWarehouse;

            // Assert
            Assert.Equal(firstWarehouse, currentWarehouse);
        }
    }
}
