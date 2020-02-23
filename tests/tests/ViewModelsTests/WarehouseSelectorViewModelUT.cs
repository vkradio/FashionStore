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
    public class WarehouseSelectorViewModelUT
    {
        [Fact]
        public void ConstructorThrowsIfWorkspaceViewModelIsNull()
        {
            // Arrange
            WorkspaceViewModel nullWorkspace = null;
            var mockDialogService = new Mock<IDialogService>();
            var mockWhMgmtService = new Mock<IWarehouseManagementService>();

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => new WarehouseSelectorViewModel(nullWorkspace, mockDialogService.Object, mockWhMgmtService.Object));

            // Assert
            Assert.NotNull(ex);
        }

        [Fact]
        public void ConstructorThrowsIfDialogServiceIsNull()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockWhMgmtService = new Mock<IWarehouseManagementService>();
            var mockLegacyWorkspaceCtx = new Mock<ILegacyWorkspaceContext>();
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockWhMgmtService.Object, mockLegacyWorkspaceCtx.Object);
            IDialogService nullDialogService = null;

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => new WarehouseSelectorViewModel(workspace, nullDialogService, mockWhMgmtService.Object));

            // Assert
            Assert.NotNull(ex);
        }

        [Fact]
        public void ConstructorThrowsIfWarehouseManagementServiceIsNull()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockWhMgmtService = new Mock<IWarehouseManagementService>();
            var mockLegacyWorkspaceCtx = new Mock<ILegacyWorkspaceContext>();
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockWhMgmtService.Object, mockLegacyWorkspaceCtx.Object);
            IWarehouseManagementService nullWhMgmtService = null;

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => new WarehouseSelectorViewModel(workspace, mockDialogService.Object, nullWhMgmtService));

            // Assert
            Assert.NotNull(ex);
        }

        [Fact]
        public void BeforeInitSelectorButtonsAndSelectedWarehouseAreNull()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockWhMgmtService = new Mock<IWarehouseManagementService>();
            var mockLegacyWorkspaceCtx = new Mock<ILegacyWorkspaceContext>();
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockWhMgmtService.Object, mockLegacyWorkspaceCtx.Object);

            // Act
            var whSelectorViewModel = new WarehouseSelectorViewModel(workspace, mockDialogService.Object, mockWhMgmtService.Object);

            // Assert
            Assert.Null(whSelectorViewModel.SelectorButtons);
            Assert.Null(whSelectorViewModel.SelectedWarehouse);
        }

        [Fact]
        public async void AfterInitFromEmptyWhCollectionSelectorButtonsIsEmptyButNotNullAndSelectedWhIsNull()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockWhMgmtService = new Mock<IWarehouseManagementService>();
            var mockLegacyWorkspaceCtx = new Mock<ILegacyWorkspaceContext>();
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockWhMgmtService.Object, mockLegacyWorkspaceCtx.Object);
            var whSelectorViewModel = new WarehouseSelectorViewModel(workspace, mockDialogService.Object, mockWhMgmtService.Object);

            // Act
            await whSelectorViewModel.Initialize();

            // Assert
            Assert.NotNull(whSelectorViewModel.SelectorButtons);
            Assert.Empty(whSelectorViewModel.SelectorButtons);
            Assert.Null(whSelectorViewModel.SelectedWarehouse);
        }

        [Fact]
        public async void AfterInitFromWhCollectionOf1SelectorButtonsAndSelectedWhAreProperlySet()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockWhMgmtService = new Mock<IWarehouseManagementService>();
            var mockLegacyWorkspaceCtx = new Mock<ILegacyWorkspaceContext>();
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockWhMgmtService.Object, mockLegacyWorkspaceCtx.Object);
            var whSelectorViewModel = new WarehouseSelectorViewModel(workspace, mockDialogService.Object, mockWhMgmtService.Object);
            var warehouseName = "warehouse 1";
            var warehouse = new Warehouse { Name = warehouseName };
            mockWhMgmtService
                .Setup(service => service.GetFunctioningOrderedWarehousesAsync())
                .ReturnsAsync(new Warehouse[] { warehouse });

            // Act
            await whSelectorViewModel.Initialize();

            // Assert
            Assert.Collection(whSelectorViewModel.SelectorButtons,
                item => Assert.Equal(warehouseName, item.WarehouseName)
            );
            Assert.Equal(warehouseName, whSelectorViewModel.SelectedWarehouse.WarehouseName);
        }

        [Fact]
        public async void AfterInitFromWhCollectionOf5SelectorButtonsAndSelectedWhAreProperlySet()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockWhMgmtService = new Mock<IWarehouseManagementService>();
            var mockLegacyWorkspaceCtx = new Mock<ILegacyWorkspaceContext>();
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockWhMgmtService.Object, mockLegacyWorkspaceCtx.Object);
            var whSelectorViewModel = new WarehouseSelectorViewModel(workspace, mockDialogService.Object, mockWhMgmtService.Object);
            var warehouseName1 = "warehouse 1";
            var warehouseName2 = "warehouse 2";
            var warehouseName3 = "warehouse 3";
            var warehouseName4 = "warehouse 4";
            var warehouseName5 = "warehouse 5";
            var warehouse1 = new Warehouse { Name = warehouseName1 };
            var warehouse2 = new Warehouse { Name = warehouseName2 };
            var warehouse3 = new Warehouse { Name = warehouseName3 };
            var warehouse4 = new Warehouse { Name = warehouseName4 };
            var warehouse5 = new Warehouse { Name = warehouseName5 };
            mockWhMgmtService
                .Setup(service => service.GetFunctioningOrderedWarehousesAsync())
                .ReturnsAsync(new Warehouse[] { warehouse1, warehouse2, warehouse3, warehouse4, warehouse5 });

            // Act
            await whSelectorViewModel.Initialize();

            // Assert
            Assert.Collection(whSelectorViewModel.SelectorButtons,
                item => Assert.Equal(warehouseName1, item.WarehouseName),
                item => Assert.Equal(warehouseName2, item.WarehouseName),
                item => Assert.Equal(warehouseName3, item.WarehouseName),
                item => Assert.Equal(warehouseName4, item.WarehouseName),
                item => Assert.Equal(warehouseName5, item.WarehouseName)
            );
            Assert.Equal(warehouseName1, whSelectorViewModel.SelectedWarehouse.WarehouseName);
        }

        [Fact]
        public async void AfterInitFromWhCollectionOf5NotificationOfSelectorButtonsAndSelectedWhAreFiredOnce()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockWhMgmtService = new Mock<IWarehouseManagementService>();
            var mockLegacyWorkspaceCtx = new Mock<ILegacyWorkspaceContext>();
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockWhMgmtService.Object, mockLegacyWorkspaceCtx.Object);
            var whSelectorViewModel = new WarehouseSelectorViewModel(workspace, mockDialogService.Object, mockWhMgmtService.Object);
            var propertyChangeRegistry = new Dictionary<string, int>();
            whSelectorViewModel.PropertyChanged += (s, a) =>
            {
                Assert.Equal(whSelectorViewModel, s);
                propertyChangeRegistry.Inc(a.PropertyName);
            };
            mockWhMgmtService
                .Setup(service => service.GetFunctioningOrderedWarehousesAsync())
                .ReturnsAsync(Enumerable
                    .Range(1, 5)
                    .Select(i => new Warehouse { WarehouseId = i })
                );

            // Act
            await whSelectorViewModel.Initialize();

            // Assert
            Assert.Equal(5, whSelectorViewModel.SelectorButtons.Count());
            Assert.Collection(propertyChangeRegistry,
                item =>
                {
                    Assert.Equal(nameof(WarehouseSelectorViewModel.SelectorButtons), item.Key);
                    Assert.Equal(1, item.Value);
                },
                item =>
                {
                    Assert.Equal(nameof(WarehouseSelectorViewModel.SelectedWarehouse), item.Key);
                    Assert.Equal(1, item.Value);
                }
            );
        }

        [Fact]
        public async void NoChangeNotificationsOnSelectingAlreadySelectedWarehouse()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockWhMgmtService = new Mock<IWarehouseManagementService>();
            var mockLegacyWorkspaceCtx = new Mock<ILegacyWorkspaceContext>();
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockWhMgmtService.Object, mockLegacyWorkspaceCtx.Object);
            var whSelectorViewModel = new WarehouseSelectorViewModel(workspace, mockDialogService.Object, mockWhMgmtService.Object);
            var propertyChangeRegistry = new Dictionary<string, int>();
            whSelectorViewModel.PropertyChanged += (s, a) =>
            {
                Assert.Equal(whSelectorViewModel, s);
                propertyChangeRegistry.Inc(a.PropertyName);
            };
            var warehouses = Enumerable
                .Range(1, 5)
                .Select(i => new Warehouse { WarehouseId = i, Name = i.ToString(CultureInfo.InvariantCulture) })
                .ToList();
            mockWhMgmtService
                .Setup(service => service.GetFunctioningOrderedWarehousesAsync())
                .ReturnsAsync(warehouses);
            await whSelectorViewModel.Initialize();
            propertyChangeRegistry.Clear();
            Assert.Equal(5, whSelectorViewModel.SelectorButtons.Count());
            Assert.Equal(1.ToString(CultureInfo.InvariantCulture), whSelectorViewModel.SelectedWarehouse.WarehouseName);

            // Act
            whSelectorViewModel.SelectedWarehouse = whSelectorViewModel.SelectedWarehouse;

            // Assert
            Assert.Empty(propertyChangeRegistry);
        }

        [Fact]
        public async void WillChangeAndNotifySelectedWarehouseIfNoNeedToSaveWorkspace()
        {
            // Arrange
            var numberOfAsksForUser = 0;
            var mockDialogService = new Mock<IDialogService>();
            mockDialogService
                .Setup(dialog => dialog.PresentDialog(It.IsAny<string>(), It.IsAny<DialogOptionsEnum>()))
                .Callback(() => numberOfAsksForUser++);
            var mockWhMgmtService = new Mock<IWarehouseManagementService>();
            var mockLegacyWorkspaceCtx = new Mock<ILegacyWorkspaceContext>();
            const bool noUnsavedEntryValue = false;
            var numberOfChecksForUnsavedData = 0;
            mockLegacyWorkspaceCtx
                .Setup(wkspc => wkspc.IsThereUnsavedEntry())
                .Callback(() => numberOfChecksForUnsavedData++)
                .Returns(noUnsavedEntryValue);
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockWhMgmtService.Object, mockLegacyWorkspaceCtx.Object);
            var whSelectorViewModel = new WarehouseSelectorViewModel(workspace, mockDialogService.Object, mockWhMgmtService.Object);
            var propertyChangeRegistry = new Dictionary<string, int>();
            whSelectorViewModel.PropertyChanged += (s, a) =>
            {
                Assert.Equal(whSelectorViewModel, s);
                propertyChangeRegistry.Inc(a.PropertyName);
            };
            const string whPrefix = "Warehouse ";
            var warehouses = Enumerable
                .Range(1, 5)
                .Select(i => new Warehouse { WarehouseId = i, Name = whPrefix + i.ToString(CultureInfo.InvariantCulture) })
                .ToList();
            mockWhMgmtService
                .Setup(service => service.GetFunctioningOrderedWarehousesAsync())
                .ReturnsAsync(warehouses);
            await whSelectorViewModel.Initialize();
            propertyChangeRegistry.Clear();
            var firstWarehouseName = warehouses[0].Name;
            var secondWarehouseName = warehouses[1].Name;
            Assert.Equal(5, whSelectorViewModel.SelectorButtons.Count());
            Assert.Equal(firstWarehouseName, whSelectorViewModel.SelectedWarehouse.WarehouseName);

            // Act
            whSelectorViewModel.SelectedWarehouse = whSelectorViewModel.SelectorButtons[1];

            // Assert
            Assert.Equal(1, numberOfChecksForUnsavedData);
            Assert.Equal(0, numberOfAsksForUser);
            Assert.Collection(propertyChangeRegistry,
                item =>
                {
                    Assert.Equal(nameof(WarehouseSelectorViewModel.SelectedWarehouse), item.Key);
                    Assert.Equal(1, item.Value);
                }
            );
            Assert.Equal(secondWarehouseName, whSelectorViewModel.SelectedWarehouse.WarehouseName);
        }

        [Fact]
        public async void WillNotChangeButWillNotifySelectedWarehouseIfThereIsNeedToSaveWorkspaceAndUserDidntConfirmChange()
        {
            // Arrange
            var numberOfAsksForUser = 0;
            var mockDialogService = new Mock<IDialogService>();
            const DialogResultEnum userDidntConfirmChangeValue = DialogResultEnum.Cancel;
            mockDialogService
                .Setup(dialog => dialog.PresentDialog(It.IsAny<string>(), It.IsAny<DialogOptionsEnum>()))
                .Callback(() => numberOfAsksForUser++)
                .Returns(userDidntConfirmChangeValue);
            var mockWhMgmtService = new Mock<IWarehouseManagementService>();
            var mockLegacyWorkspaceCtx = new Mock<ILegacyWorkspaceContext>();
            const bool thereIsUnsavedEntryValue = true;
            var numberOfChecksForUnsavedData = 0;
            mockLegacyWorkspaceCtx
                .Setup(wkspc => wkspc.IsThereUnsavedEntry())
                .Callback(() => numberOfChecksForUnsavedData++)
                .Returns(thereIsUnsavedEntryValue);
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockWhMgmtService.Object, mockLegacyWorkspaceCtx.Object);
            var whSelectorViewModel = new WarehouseSelectorViewModel(workspace, mockDialogService.Object, mockWhMgmtService.Object);
            var propertyChangeRegistry = new Dictionary<string, int>();
            whSelectorViewModel.PropertyChanged += (s, a) =>
            {
                Assert.Equal(whSelectorViewModel, s);
                propertyChangeRegistry.Inc(a.PropertyName);
            };
            const string whPrefix = "Warehouse ";
            var warehouses = Enumerable
                .Range(1, 5)
                .Select(i => new Warehouse { WarehouseId = i, Name = whPrefix + i.ToString(CultureInfo.InvariantCulture) })
                .ToList();
            mockWhMgmtService
                .Setup(service => service.GetFunctioningOrderedWarehousesAsync())
                .ReturnsAsync(warehouses);
            await whSelectorViewModel.Initialize();
            propertyChangeRegistry.Clear();
            var firstWarehouseName = warehouses[0].Name;
            Assert.Equal(5, whSelectorViewModel.SelectorButtons.Count());
            Assert.Equal(firstWarehouseName, whSelectorViewModel.SelectedWarehouse.WarehouseName);

            // Act
            whSelectorViewModel.SelectedWarehouse = whSelectorViewModel.SelectorButtons[1];

            // Assert
            Assert.Equal(1, numberOfChecksForUnsavedData);
            Assert.Equal(1, numberOfAsksForUser);
            Assert.Collection(propertyChangeRegistry,
                item =>
                {
                    Assert.Equal(nameof(WarehouseSelectorViewModel.SelectedWarehouse), item.Key);
                    Assert.Equal(1, item.Value);
                }
            );
            Assert.Equal(whSelectorViewModel.SelectorButtons[0], whSelectorViewModel.SelectedWarehouse);
            Assert.Equal(firstWarehouseName, whSelectorViewModel.SelectedWarehouse.WarehouseName);
        }

        [Fact]
        public async void WillChangeAndNotifySelectedWarehouseIfThereIsNeedToSaveWorkspaceAndUserConfirmedChange()
        {
            // Arrange
            var numberOfAsksForUser = 0;
            var mockDialogService = new Mock<IDialogService>();
            const DialogResultEnum userConfirmChangeValue = DialogResultEnum.Yes;
            mockDialogService
                .Setup(dialog => dialog.PresentDialog(It.IsAny<string>(), It.IsAny<DialogOptionsEnum>()))
                .Callback(() => numberOfAsksForUser++)
                .Returns(userConfirmChangeValue);
            var mockWhMgmtService = new Mock<IWarehouseManagementService>();
            var mockLegacyWorkspaceCtx = new Mock<ILegacyWorkspaceContext>();
            const bool thereIsUnsavedEntryValue = true;
            var numberOfChecksForUnsavedData = 0;
            mockLegacyWorkspaceCtx
                .Setup(wkspc => wkspc.IsThereUnsavedEntry())
                .Callback(() => numberOfChecksForUnsavedData++)
                .Returns(thereIsUnsavedEntryValue);
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockWhMgmtService.Object, mockLegacyWorkspaceCtx.Object);
            var whSelectorViewModel = new WarehouseSelectorViewModel(workspace, mockDialogService.Object, mockWhMgmtService.Object);
            var propertyChangeRegistry = new Dictionary<string, int>();
            whSelectorViewModel.PropertyChanged += (s, a) =>
            {
                Assert.Equal(whSelectorViewModel, s);
                propertyChangeRegistry.Inc(a.PropertyName);
            };
            const string whPrefix = "Warehouse ";
            var warehouses = Enumerable
                .Range(1, 5)
                .Select(i => new Warehouse { WarehouseId = i, Name = whPrefix + i.ToString(CultureInfo.InvariantCulture) })
                .ToList();
            mockWhMgmtService
                .Setup(service => service.GetFunctioningOrderedWarehousesAsync())
                .ReturnsAsync(warehouses);
            await whSelectorViewModel.Initialize();
            propertyChangeRegistry.Clear();
            var firstWarehouseName = warehouses[0].Name;
            var secondWarehouseName = warehouses[1].Name;
            Assert.Equal(5, whSelectorViewModel.SelectorButtons.Count());
            Assert.Equal(firstWarehouseName, whSelectorViewModel.SelectedWarehouse.WarehouseName);

            // Act
            whSelectorViewModel.SelectedWarehouse = whSelectorViewModel.SelectorButtons[1];

            // Assert
            Assert.Equal(1, numberOfChecksForUnsavedData);
            Assert.Equal(1, numberOfAsksForUser);
            Assert.Collection(propertyChangeRegistry,
                item =>
                {
                    Assert.Equal(nameof(WarehouseSelectorViewModel.SelectedWarehouse), item.Key);
                    Assert.Equal(1, item.Value);
                }
            );
            Assert.Equal(whSelectorViewModel.SelectorButtons[1], whSelectorViewModel.SelectedWarehouse);
            Assert.Equal(secondWarehouseName, whSelectorViewModel.SelectedWarehouse.WarehouseName);
        }
    }
}
