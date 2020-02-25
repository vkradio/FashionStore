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

        [Fact]
        public void BeforeInitItIsAllowedToChangeWarehouseAndUnsavedEntryIsNotChecked()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockWarehouseMgmtSvc = new Mock<IWarehouseManagementService>();
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            var legacyWorkspaceCheckedForUnsavedEntry = false;
            mockLegacyWorkspace
                .Setup(legacy => legacy.IsThereUnsavedEntry())
                .Callback(() => legacyWorkspaceCheckedForUnsavedEntry = true);
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockWarehouseMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            var result = workspace.IsItAllowedToChangeCurrentWarehouse();

            // Assert
            Assert.True(result);
            Assert.False(legacyWorkspaceCheckedForUnsavedEntry);
        }

        [Fact]
        public void AfterInitItIsForbiddenToChangeWarehouseIfThereAreUnsavedUserEntryAndUserCancelsReset()
        {
            // Arrange
            var numberOfAsksForUser = 0;
            var mockDialogService = new Mock<IDialogService>();
            const DialogResultEnum userReply = DialogResultEnum.Cancel;
            mockDialogService
                .Setup(dialog => dialog.PresentDialog(It.IsAny<string>(), It.IsAny<DialogOptionsEnum>()))
                .Callback(() => numberOfAsksForUser++)
                .Returns(userReply);
            var mockWarehouseMgmtSvc = new Mock<IWarehouseManagementService>();
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            const bool thereIsUnsavedUserEntryValue = true;
            var legacyWorkspaceCheckedForUnsavedEntry = false;
            mockLegacyWorkspace
                .Setup(legacy => legacy.IsThereUnsavedEntry())
                .Callback(() => legacyWorkspaceCheckedForUnsavedEntry = true)
                .Returns(thereIsUnsavedUserEntryValue);
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockWarehouseMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            var result = workspace.IsItAllowedToChangeCurrentWarehouse();

            // Assert
            Assert.False(result);
            Assert.True(legacyWorkspaceCheckedForUnsavedEntry);
            Assert.Equal(1, numberOfAsksForUser);
        }

        [Fact]
        public void AfterInitItIsForbiddenToChangeWarehouseIfThereAreUnsavedUserEntryAndUserSaidNoToReset()
        {
            // Arrange
            var numberOfAsksForUser = 0;
            var mockDialogService = new Mock<IDialogService>();
            const DialogResultEnum userReply = DialogResultEnum.No;
            mockDialogService
                .Setup(dialog => dialog.PresentDialog(It.IsAny<string>(), It.IsAny<DialogOptionsEnum>()))
                .Callback(() => numberOfAsksForUser++)
                .Returns(userReply);
            var mockWarehouseMgmtSvc = new Mock<IWarehouseManagementService>();
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            const bool thereIsUnsavedUserEntryValue = true;
            var legacyWorkspaceCheckedForUnsavedEntry = false;
            mockLegacyWorkspace
                .Setup(legacy => legacy.IsThereUnsavedEntry())
                .Callback(() => legacyWorkspaceCheckedForUnsavedEntry = true)
                .Returns(thereIsUnsavedUserEntryValue);
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockWarehouseMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            var result = workspace.IsItAllowedToChangeCurrentWarehouse();

            // Assert
            Assert.False(result);
            Assert.True(legacyWorkspaceCheckedForUnsavedEntry);
            Assert.Equal(1, numberOfAsksForUser);
        }

        [Fact]
        public void AfterInitItIsAllowedToChangeWarehouseIfThereAreUnsavedUserEntryAndUserSaidYesToReset()
        {
            // Arrange
            var numberOfAsksForUser = 0;
            var mockDialogService = new Mock<IDialogService>();
            const DialogResultEnum userReply = DialogResultEnum.Yes;
            mockDialogService
                .Setup(dialog => dialog.PresentDialog(It.IsAny<string>(), It.IsAny<DialogOptionsEnum>()))
                .Callback(() => numberOfAsksForUser++)
                .Returns(userReply);
            var mockWarehouseMgmtSvc = new Mock<IWarehouseManagementService>();
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            const bool thereIsUnsavedUserEntryValue = true;
            var legacyWorkspaceCheckedForUnsavedEntry = false;
            mockLegacyWorkspace
                .Setup(legacy => legacy.IsThereUnsavedEntry())
                .Callback(() => legacyWorkspaceCheckedForUnsavedEntry = true)
                .Returns(thereIsUnsavedUserEntryValue);
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockWarehouseMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            var result = workspace.IsItAllowedToChangeCurrentWarehouse();

            // Assert
            Assert.True(result);
            Assert.True(legacyWorkspaceCheckedForUnsavedEntry);
            Assert.Equal(1, numberOfAsksForUser);
        }

        [Fact]
        public void AfterInitItIsAllowedToChangeWarehouseIfThereIsNoUnsavedUserEntry()
        {
            // Arrange
            var numberOfAsksForUser = 0;
            var mockDialogService = new Mock<IDialogService>();
            mockDialogService
                .Setup(dialog => dialog.PresentDialog(It.IsAny<string>(), It.IsAny<DialogOptionsEnum>()))
                .Callback(() => numberOfAsksForUser++);
            var mockWarehouseMgmtSvc = new Mock<IWarehouseManagementService>();
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            const bool thereIsNoUnsavedUserEntryValue = false;
            var legacyWorkspaceCheckedForUnsavedEntry = false;
            mockLegacyWorkspace
                .Setup(legacy => legacy.IsThereUnsavedEntry())
                .Callback(() => legacyWorkspaceCheckedForUnsavedEntry = true)
                .Returns(thereIsNoUnsavedUserEntryValue);
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockWarehouseMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            var result = workspace.IsItAllowedToChangeCurrentWarehouse();

            // Assert
            Assert.True(result);
            Assert.True(legacyWorkspaceCheckedForUnsavedEntry);
            Assert.Equal(0, numberOfAsksForUser);
        }
    }
}
