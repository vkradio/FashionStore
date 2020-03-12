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
    public class StoreSelectorViewModelUT
    {
        (Mock<IStoreManagementService>, WorkspaceViewModel) CreateBaseMocks()
        {
            var mockDialogService = new Mock<IDialogService>();
            var mockStoreMgmtService = new Mock<IStoreManagementService>();
            var mockLegacyWorkspaceCtx = new Mock<ILegacyWorkspaceContext>();
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockStoreMgmtService.Object, mockLegacyWorkspaceCtx.Object);

            return (mockStoreMgmtService, workspace);
        }

        [Fact]
        public void ConstructorThrowsIfWorkspaceViewModelIsNull()
        {
            // Arrange
            WorkspaceViewModel nullWorkspace = null;
            var mockStoreMgmtService = new Mock<IStoreManagementService>();

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => new StoreSelectorViewModel(nullWorkspace, mockStoreMgmtService.Object));

            // Assert
            Assert.NotNull(ex);
        }

        [Fact]
        public void ConstructorThrowsIfStoreManagementServiceIsNull()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockStoreMgmtService = new Mock<IStoreManagementService>();
            var mockLegacyWorkspaceCtx = new Mock<ILegacyWorkspaceContext>();
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockStoreMgmtService.Object, mockLegacyWorkspaceCtx.Object);
            IStoreManagementService nullStoreMgmtService = null;

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => new StoreSelectorViewModel(workspace, nullStoreMgmtService));

            // Assert
            Assert.NotNull(ex);
        }

        [Fact]
        public void BeforeInitSelectorButtonsAndSelectedStoreAreNull()
        {
            // Arrange
            var (mockStoreMgmtService, workspace) = CreateBaseMocks();

            // Act
            var storeSelectorViewModel = new StoreSelectorViewModel(workspace, mockStoreMgmtService.Object);

            // Assert
            Assert.Null(storeSelectorViewModel.SelectorButtons);
            Assert.Null(storeSelectorViewModel.SelectedStore);
        }

        [Fact]
        public async void AfterInitFromEmptyStoreCollectionSelectorButtonsIsEmptyButNotNullAndSelectedStoreIsNull()
        {
            // Arrange
            var (mockStoreMgmtService, workspace) = CreateBaseMocks();
            var storeSelectorViewModel = new StoreSelectorViewModel(workspace, mockStoreMgmtService.Object);

            // Act
            await storeSelectorViewModel.Initialize();

            // Assert
            Assert.NotNull(storeSelectorViewModel.SelectorButtons);
            Assert.Empty(storeSelectorViewModel.SelectorButtons);
            Assert.Null(storeSelectorViewModel.SelectedStore);
        }

        [Fact]
        public async void AfterInitFromStoreCollectionOf1SelectorButtonsAndSelectedStoreAreProperlySet()
        {
            // Arrange
            var (mockStoreMgmtService, workspace) = CreateBaseMocks();
            var storeSelectorViewModel = new StoreSelectorViewModel(workspace, mockStoreMgmtService.Object);

            var storeName = "store 1";
            var store = new Store { Name = storeName };
            mockStoreMgmtService
                .Setup(service => service.GetFunctioningOrderedStoresAsync())
                .ReturnsAsync(new Store[] { store });

            // Act
            await storeSelectorViewModel.Initialize();

            // Assert
            Assert.Collection(storeSelectorViewModel.SelectorButtons,
                item => Assert.Equal(storeName, item.StoreName)
            );
            Assert.Equal(storeName, storeSelectorViewModel.SelectedStore.StoreName);
        }

        [Fact]
        public async void AfterInitFromStoreCollectionOf5SelectorButtonsAndSelectedStoreAreProperlySet()
        {
            // Arrange
            var (mockStoreMgmtService, workspace) = CreateBaseMocks();
            var storeSelectorViewModel = new StoreSelectorViewModel(workspace, mockStoreMgmtService.Object);

            var storeName1 = "store 1";
            var storeName2 = "store 2";
            var storeName3 = "store 3";
            var storeName4 = "store 4";
            var storeName5 = "store 5";
            var store1 = new Store { Name = storeName1 };
            var store2 = new Store { Name = storeName2 };
            var store3 = new Store { Name = storeName3 };
            var store4 = new Store { Name = storeName4 };
            var store5 = new Store { Name = storeName5 };
            mockStoreMgmtService
                .Setup(service => service.GetFunctioningOrderedStoresAsync())
                .ReturnsAsync(new Store[] { store1, store2, store3, store4, store5 });

            // Act
            await storeSelectorViewModel.Initialize();

            // Assert
            Assert.Collection(storeSelectorViewModel.SelectorButtons,
                item => Assert.Equal(storeName1, item.StoreName),
                item => Assert.Equal(storeName2, item.StoreName),
                item => Assert.Equal(storeName3, item.StoreName),
                item => Assert.Equal(storeName4, item.StoreName),
                item => Assert.Equal(storeName5, item.StoreName)
            );
            Assert.Equal(storeName1, storeSelectorViewModel.SelectedStore.StoreName);
        }

        [Fact]
        public async void AfterInitFromStoreCollectionOf5NotificationOfSelectorButtonsAndSelectedStoreAreFiredOnce()
        {
            // Arrange
            var (mockStoreMgmtService, workspace) = CreateBaseMocks();
            var storeSelectorViewModel = new StoreSelectorViewModel(workspace, mockStoreMgmtService.Object);
            var propertyChangeRegistry = new Dictionary<string, int>();
            storeSelectorViewModel.PropertyChanged += (s, a) =>
            {
                Assert.Equal(storeSelectorViewModel, s);
                propertyChangeRegistry.Inc(a.PropertyName);
            };
            mockStoreMgmtService
                .Setup(service => service.GetFunctioningOrderedStoresAsync())
                .ReturnsAsync(Enumerable
                    .Range(1, 5)
                    .Select(i => new Store { Id = i })
                );

            // Act
            await storeSelectorViewModel.Initialize();

            // Assert
            Assert.Equal(5, storeSelectorViewModel.SelectorButtons.Count());
            Assert.Collection(propertyChangeRegistry,
                item =>
                {
                    Assert.Equal(nameof(StoreSelectorViewModel.SelectorButtons), item.Key);
                    Assert.Equal(1, item.Value);
                },
                item =>
                {
                    Assert.Equal(nameof(StoreSelectorViewModel.SelectedStore), item.Key);
                    Assert.Equal(1, item.Value);
                }
            );
        }

        [Fact]
        public async void NoChangeNotificationsOnSelectingAlreadySelectedStore()
        {
            // Arrange
            var (mockStoreMgmtService, workspace) = CreateBaseMocks();
            var storeSelectorViewModel = new StoreSelectorViewModel(workspace, mockStoreMgmtService.Object);
            var propertyChangeRegistry = new Dictionary<string, int>();
            storeSelectorViewModel.PropertyChanged += (s, a) =>
            {
                Assert.Equal(storeSelectorViewModel, s);
                propertyChangeRegistry.Inc(a.PropertyName);
            };
            var stores = Enumerable
                .Range(1, 5)
                .Select(i => new Store { Id = i, Name = i.ToString(CultureInfo.InvariantCulture) })
                .ToList();
            mockStoreMgmtService
                .Setup(service => service.GetFunctioningOrderedStoresAsync())
                .ReturnsAsync(stores);
            await storeSelectorViewModel.Initialize();
            propertyChangeRegistry.Clear();
            Assert.Equal(5, storeSelectorViewModel.SelectorButtons.Count());
            Assert.Equal(1.ToString(CultureInfo.InvariantCulture), storeSelectorViewModel.SelectedStore.StoreName);

            // Act
            storeSelectorViewModel.SelectedStore = storeSelectorViewModel.SelectedStore;

            // Assert
            Assert.Empty(propertyChangeRegistry);
        }

        //[Fact]
        //public async void WillChangeAndNotifySelectedWarehouseIfNoNeedToSaveWorkspace()
        //{
        //    // Arrange
        //    var numberOfAsksForUser = 0;
        //    var mockDialogService = new Mock<IDialogService>();
        //    mockDialogService
        //        .Setup(dialog => dialog.PresentDialog(It.IsAny<string>(), It.IsAny<DialogOptionsEnum>()))
        //        .Callback(() => numberOfAsksForUser++);
        //    var mockWhMgmtService = new Mock<IWarehouseManagementService>();
        //    var mockLegacyWorkspaceCtx = new Mock<ILegacyWorkspaceContext>();
        //    const bool noUnsavedEntryValue = false;
        //    var numberOfChecksForUnsavedData = 0;
        //    mockLegacyWorkspaceCtx
        //        .Setup(wkspc => wkspc.IsThereUnsavedEntry())
        //        .Callback(() => numberOfChecksForUnsavedData++)
        //        .Returns(noUnsavedEntryValue);
        //    var workspace = new WorkspaceViewModel(mockDialogService.Object, mockWhMgmtService.Object, mockLegacyWorkspaceCtx.Object);
        //    var whSelectorViewModel = new WarehouseSelectorViewModel(workspace, mockWhMgmtService.Object);
        //    var propertyChangeRegistry = new Dictionary<string, int>();
        //    whSelectorViewModel.PropertyChanged += (s, a) =>
        //    {
        //        Assert.Equal(whSelectorViewModel, s);
        //        propertyChangeRegistry.Inc(a.PropertyName);
        //    };
        //    const string whPrefix = "Warehouse ";
        //    var warehouses = Enumerable
        //        .Range(1, 5)
        //        .Select(i => new Warehouse { WarehouseId = i, Name = whPrefix + i.ToString(CultureInfo.InvariantCulture) })
        //        .ToList();
        //    mockWhMgmtService
        //        .Setup(service => service.GetFunctioningOrderedWarehousesAsync())
        //        .ReturnsAsync(warehouses);
        //    await whSelectorViewModel.Initialize();
        //    propertyChangeRegistry.Clear();
        //    var firstWarehouseName = warehouses[0].Name;
        //    var secondWarehouseName = warehouses[1].Name;
        //    Assert.Equal(5, whSelectorViewModel.SelectorButtons.Count());
        //    Assert.Equal(firstWarehouseName, whSelectorViewModel.SelectedWarehouse.WarehouseName);

        //    // Act
        //    whSelectorViewModel.SelectedWarehouse = whSelectorViewModel.SelectorButtons[1];

        //    // Assert
        //    Assert.Equal(1, numberOfChecksForUnsavedData);
        //    Assert.Equal(0, numberOfAsksForUser);
        //    Assert.Collection(propertyChangeRegistry,
        //        item =>
        //        {
        //            Assert.Equal(nameof(WarehouseSelectorViewModel.SelectedWarehouse), item.Key);
        //            Assert.Equal(1, item.Value);
        //        }
        //    );
        //    Assert.Equal(secondWarehouseName, whSelectorViewModel.SelectedWarehouse.WarehouseName);
        //}

        //[Fact]
        //public async void WillNotChangeButWillNotifySelectedWarehouseIfThereIsNeedToSaveWorkspaceAndUserDidntConfirmChange()
        //{
        //    // Arrange
        //    var numberOfAsksForUser = 0;
        //    var mockDialogService = new Mock<IDialogService>();
        //    const DialogResultEnum userDidntConfirmChangeValue = DialogResultEnum.Cancel;
        //    mockDialogService
        //        .Setup(dialog => dialog.PresentDialog(It.IsAny<string>(), It.IsAny<DialogOptionsEnum>()))
        //        .Callback(() => numberOfAsksForUser++)
        //        .Returns(userDidntConfirmChangeValue);
        //    var mockWhMgmtService = new Mock<IWarehouseManagementService>();
        //    var mockLegacyWorkspaceCtx = new Mock<ILegacyWorkspaceContext>();
        //    const bool thereIsUnsavedEntryValue = true;
        //    var numberOfChecksForUnsavedData = 0;
        //    mockLegacyWorkspaceCtx
        //        .Setup(wkspc => wkspc.IsThereUnsavedEntry())
        //        .Callback(() => numberOfChecksForUnsavedData++)
        //        .Returns(thereIsUnsavedEntryValue);
        //    var workspace = new WorkspaceViewModel(mockDialogService.Object, mockWhMgmtService.Object, mockLegacyWorkspaceCtx.Object);
        //    var whSelectorViewModel = new WarehouseSelectorViewModel(workspace, mockDialogService.Object, mockWhMgmtService.Object);
        //    var propertyChangeRegistry = new Dictionary<string, int>();
        //    whSelectorViewModel.PropertyChanged += (s, a) =>
        //    {
        //        Assert.Equal(whSelectorViewModel, s);
        //        propertyChangeRegistry.Inc(a.PropertyName);
        //    };
        //    const string whPrefix = "Warehouse ";
        //    var warehouses = Enumerable
        //        .Range(1, 5)
        //        .Select(i => new Warehouse { WarehouseId = i, Name = whPrefix + i.ToString(CultureInfo.InvariantCulture) })
        //        .ToList();
        //    mockWhMgmtService
        //        .Setup(service => service.GetFunctioningOrderedWarehousesAsync())
        //        .ReturnsAsync(warehouses);
        //    await whSelectorViewModel.Initialize();
        //    propertyChangeRegistry.Clear();
        //    var firstWarehouseName = warehouses[0].Name;
        //    Assert.Equal(5, whSelectorViewModel.SelectorButtons.Count());
        //    Assert.Equal(firstWarehouseName, whSelectorViewModel.SelectedWarehouse.WarehouseName);

        //    // Act
        //    whSelectorViewModel.SelectedWarehouse = whSelectorViewModel.SelectorButtons[1];

        //    // Assert
        //    Assert.Equal(1, numberOfChecksForUnsavedData);
        //    Assert.Equal(1, numberOfAsksForUser);
        //    Assert.Collection(propertyChangeRegistry,
        //        item =>
        //        {
        //            Assert.Equal(nameof(WarehouseSelectorViewModel.SelectedWarehouse), item.Key);
        //            Assert.Equal(1, item.Value);
        //        }
        //    );
        //    Assert.Equal(whSelectorViewModel.SelectorButtons[0], whSelectorViewModel.SelectedWarehouse);
        //    Assert.Equal(firstWarehouseName, whSelectorViewModel.SelectedWarehouse.WarehouseName);
        //}

        //[Fact]
        //public async void WillChangeAndNotifySelectedWarehouseIfThereIsNeedToSaveWorkspaceAndUserConfirmedChange()
        //{
        //    // Arrange
        //    var numberOfAsksForUser = 0;
        //    var mockDialogService = new Mock<IDialogService>();
        //    const DialogResultEnum userConfirmChangeValue = DialogResultEnum.Yes;
        //    mockDialogService
        //        .Setup(dialog => dialog.PresentDialog(It.IsAny<string>(), It.IsAny<DialogOptionsEnum>()))
        //        .Callback(() => numberOfAsksForUser++)
        //        .Returns(userConfirmChangeValue);
        //    var mockWhMgmtService = new Mock<IWarehouseManagementService>();
        //    var mockLegacyWorkspaceCtx = new Mock<ILegacyWorkspaceContext>();
        //    const bool thereIsUnsavedEntryValue = true;
        //    var numberOfChecksForUnsavedData = 0;
        //    mockLegacyWorkspaceCtx
        //        .Setup(wkspc => wkspc.IsThereUnsavedEntry())
        //        .Callback(() => numberOfChecksForUnsavedData++)
        //        .Returns(thereIsUnsavedEntryValue);
        //    var workspace = new WorkspaceViewModel(mockDialogService.Object, mockWhMgmtService.Object, mockLegacyWorkspaceCtx.Object);
        //    var whSelectorViewModel = new WarehouseSelectorViewModel(workspace, mockDialogService.Object, mockWhMgmtService.Object);
        //    var propertyChangeRegistry = new Dictionary<string, int>();
        //    whSelectorViewModel.PropertyChanged += (s, a) =>
        //    {
        //        Assert.Equal(whSelectorViewModel, s);
        //        propertyChangeRegistry.Inc(a.PropertyName);
        //    };
        //    const string whPrefix = "Warehouse ";
        //    var warehouses = Enumerable
        //        .Range(1, 5)
        //        .Select(i => new Warehouse { WarehouseId = i, Name = whPrefix + i.ToString(CultureInfo.InvariantCulture) })
        //        .ToList();
        //    mockWhMgmtService
        //        .Setup(service => service.GetFunctioningOrderedWarehousesAsync())
        //        .ReturnsAsync(warehouses);
        //    await whSelectorViewModel.Initialize();
        //    propertyChangeRegistry.Clear();
        //    var firstWarehouseName = warehouses[0].Name;
        //    var secondWarehouseName = warehouses[1].Name;
        //    Assert.Equal(5, whSelectorViewModel.SelectorButtons.Count());
        //    Assert.Equal(firstWarehouseName, whSelectorViewModel.SelectedWarehouse.WarehouseName);

        //    // Act
        //    whSelectorViewModel.SelectedWarehouse = whSelectorViewModel.SelectorButtons[1];

        //    // Assert
        //    Assert.Equal(1, numberOfChecksForUnsavedData);
        //    Assert.Equal(1, numberOfAsksForUser);
        //    Assert.Collection(propertyChangeRegistry,
        //        item =>
        //        {
        //            Assert.Equal(nameof(WarehouseSelectorViewModel.SelectedWarehouse), item.Key);
        //            Assert.Equal(1, item.Value);
        //        }
        //    );
        //    Assert.Equal(whSelectorViewModel.SelectorButtons[1], whSelectorViewModel.SelectedWarehouse);
        //    Assert.Equal(secondWarehouseName, whSelectorViewModel.SelectedWarehouse.WarehouseName);
        //}
    }
}
