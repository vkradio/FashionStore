using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Moq;
using MvvmInfrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using Utilities;
using ViewModels;
using vm = ViewModels;
using Xunit;

namespace ViewModelsTests
{
    public class StoreSelectorViewModelUT
    {
        (Mock<IStoreManagementService>, WorkspaceViewModel) CreateBaseMocks(
            Mock<ILegacyWorkspaceContext>? mockOverrideLegacyWorkspaceContext = null,
            Mock<IDialogService>? mockOverrideDialogService = null
        )
        {
            var mockDialogService = mockOverrideDialogService ?? new Mock<IDialogService>();
            var mockLocalizationService = new Mock<ILocalizationService>();
            var mockStoreMgmtService = new Mock<IStoreManagementService>();
            var mockLegacyWorkspaceCtx = mockOverrideLegacyWorkspaceContext ?? new Mock<ILegacyWorkspaceContext>();
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockLocalizationService.Object, mockStoreMgmtService.Object, mockLegacyWorkspaceCtx.Object);

            return (mockStoreMgmtService, workspace);
        }

        [Fact]
        public void ConstructorThrowsIfWorkspaceViewModelIsNull()
        {
            // Arrange
            WorkspaceViewModel? nullWorkspace = null;
            var mockStoreMgmtService = new Mock<IStoreManagementService>();

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => new vm.StoreSelectorViewModel(nullWorkspace!, mockStoreMgmtService.Object));

            // Assert
            Assert.NotNull(ex);
        }

        [Fact]
        public void ConstructorThrowsIfStoreManagementServiceIsNull()
        {
            // Arrange
            var (mockStoreMgmtService, workspace) = CreateBaseMocks();
            IStoreManagementService? nullStoreMgmtService = null;

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => new vm.StoreSelectorViewModel(workspace, nullStoreMgmtService!));

            // Assert
            Assert.NotNull(ex);
        }

        [Fact]
        public void BeforeInitSelectorButtonsAndSelectedStoreAreNull()
        {
            // Arrange
            var (mockStoreMgmtService, workspace) = CreateBaseMocks();

            // Act
            var storeSelectorViewModel = new vm.StoreSelectorViewModel(workspace, mockStoreMgmtService.Object);

            // Assert
            Assert.Null(storeSelectorViewModel.SelectorButtons);
            Assert.Null(storeSelectorViewModel.SelectedStore);
        }

        [Fact]
        public async void AfterInitFromEmptyStoreCollectionSelectorButtonsIsEmptyButNotNullAndSelectedStoreIsNull()
        {
            // Arrange
            var (mockStoreMgmtService, workspace) = CreateBaseMocks();
            var storeSelectorViewModel = new vm.StoreSelectorViewModel(workspace, mockStoreMgmtService.Object);

            // Act
            await storeSelectorViewModel.Initialize().ConfigureAwait(true);

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
            var storeSelectorViewModel = new vm.StoreSelectorViewModel(workspace, mockStoreMgmtService.Object);

            var storeName = "store 1";
            var store = new Store { Name = storeName };
            mockStoreMgmtService
                .Setup(service => service.GetFunctioningOrderedStoresAsync())
                .ReturnsAsync(new Store[] { store });

            // Act
            await storeSelectorViewModel.Initialize().ConfigureAwait(true);

            // Assert
            Assert.Collection(storeSelectorViewModel.SelectorButtons,
                item => Assert.Equal(storeName, item.StoreName)
            );
            Assert.Equal(storeName, storeSelectorViewModel.SelectedStore!.StoreName);
        }

        [Fact]
        public async void AfterInitFromStoreCollectionOf5SelectorButtonsAndSelectedStoreAreProperlySet()
        {
            // Arrange
            var (mockStoreMgmtService, workspace) = CreateBaseMocks();
            var storeSelectorViewModel = new vm.StoreSelectorViewModel(workspace, mockStoreMgmtService.Object);

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
            await storeSelectorViewModel.Initialize().ConfigureAwait(true);

            // Assert
            Assert.Collection(storeSelectorViewModel.SelectorButtons,
                item => Assert.Equal(storeName1, item.StoreName),
                item => Assert.Equal(storeName2, item.StoreName),
                item => Assert.Equal(storeName3, item.StoreName),
                item => Assert.Equal(storeName4, item.StoreName),
                item => Assert.Equal(storeName5, item.StoreName)
            );
            Assert.Equal(storeName1, storeSelectorViewModel.SelectedStore!.StoreName);
        }

        [Fact]
        public async void AfterInitFromStoreCollectionOf5NotificationOfSelectorButtonsAndSelectedStoreAreFiredOnce()
        {
            // Arrange
            var (mockStoreMgmtService, workspace) = CreateBaseMocks();
            var storeSelectorViewModel = new vm.StoreSelectorViewModel(workspace, mockStoreMgmtService.Object);
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
            await storeSelectorViewModel.Initialize().ConfigureAwait(true);

            // Assert
            Assert.Equal(5, storeSelectorViewModel.SelectorButtons?.Count);
            Assert.Collection(propertyChangeRegistry,
                item =>
                {
                    Assert.Equal(nameof(vm.StoreSelectorViewModel.SelectorButtons), item.Key);
                    Assert.Equal(1, item.Value);
                },
                item =>
                {
                    Assert.Equal(nameof(vm.StoreSelectorViewModel.SelectedStore), item.Key);
                    Assert.Equal(1, item.Value);
                }
            );
        }

        [Fact]
        public async void NoChangeNotificationsOnSelectingAlreadySelectedStore()
        {
            // Arrange
            var (mockStoreMgmtService, workspace) = CreateBaseMocks();
            var storeSelectorViewModel = new vm.StoreSelectorViewModel(workspace, mockStoreMgmtService.Object);
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
            await storeSelectorViewModel.Initialize().ConfigureAwait(true);
            propertyChangeRegistry.Clear();
            Assert.Equal(5, storeSelectorViewModel.SelectorButtons?.Count);
            Assert.Equal(1.ToString(CultureInfo.InvariantCulture), storeSelectorViewModel.SelectedStore!.StoreName);

            // Act
            storeSelectorViewModel.SelectedStore = storeSelectorViewModel.SelectedStore;

            // Assert
            Assert.Empty(propertyChangeRegistry);
        }

        [Fact]
        public async void WillChangeAndNotifySelectedStoreIfNoNeedToSaveWorkspace()
        {
            // Arrange
            const bool noNeedToSaveWorkspaceValue = false;

            var mockLegacyWorkspaceCtx = new Mock<ILegacyWorkspaceContext>();
            var legacyCtxAskedCount = 0;
            mockLegacyWorkspaceCtx
                .Setup(leg => leg.IsThereUnsavedEntry())
                .Callback(() => legacyCtxAskedCount++)
                .Returns(noNeedToSaveWorkspaceValue);

            var mockDialogService = new Mock<IDialogService>();
            var askDialogPresentedCount = 0;
            mockDialogService
                .Setup(dial => dial.PresentDialog(It.IsAny<string>(), It.IsAny<DialogOptionsEnum>()))
                .Callback(() => askDialogPresentedCount++);

            var (mockStoreMgmtService, workspace) = CreateBaseMocks(mockOverrideLegacyWorkspaceContext: mockLegacyWorkspaceCtx, mockOverrideDialogService: mockDialogService);

            var stores = Enumerable
                .Range(1, 5)
                .Select(i => new Store { Id = i, Name = i.ToString(CultureInfo.InvariantCulture) })
                .ToList();
            mockStoreMgmtService
                .Setup(service => service.GetFunctioningOrderedStoresAsync())
                .ReturnsAsync(stores);

            await workspace.Initialize().ConfigureAwait(true);

            const int nextStoreIndex = 1;
            var nextStore = workspace.StoreSelector.SelectorButtons![nextStoreIndex];
            Assert.NotEqual(nextStore, workspace.StoreSelector.SelectedStore);

            // Act
            workspace.StoreSelector.SelectedStore = nextStore;

            // Assert
            Assert.Equal(nextStore, workspace.StoreSelector.SelectedStore);
            Assert.Equal(1, legacyCtxAskedCount);
            Assert.Equal(0, askDialogPresentedCount);
        }

        [Fact]
        public async void WillNotChangeButWillNotifySelectedWarehouseIfThereIsNeedToSaveWorkspaceAndUserDidntConfirmChange()
        {
            // Arrange
            const bool needToSaveWorkspaceValue = true;
            const DialogResultEnum userRepliedNoValue = DialogResultEnum.No;

            var mockLegacyWorkspaceCtx = new Mock<ILegacyWorkspaceContext>();
            var legacyCtxAskedCount = 0;
            mockLegacyWorkspaceCtx
                .Setup(leg => leg.IsThereUnsavedEntry())
                .Callback(() => legacyCtxAskedCount++)
                .Returns(needToSaveWorkspaceValue);

            var mockDialogService = new Mock<IDialogService>();
            var askDialogPresentedCount = 0;
            mockDialogService
                .Setup(dial => dial.PresentDialog(It.IsAny<string>(), It.IsAny<DialogOptionsEnum>()))
                .Callback(() => askDialogPresentedCount++)
                .Returns(userRepliedNoValue);

            var (mockStoreMgmtService, workspace) = CreateBaseMocks(mockOverrideLegacyWorkspaceContext: mockLegacyWorkspaceCtx, mockOverrideDialogService: mockDialogService);

            var storeChangedNotifyCount = 0;
            workspace.StoreSelector.PropertyChanged += (s, a) =>
            {
                if (a.PropertyName == nameof(vm.StoreSelectorViewModel.SelectedStore))
                    storeChangedNotifyCount++;
            };

            var stores = Enumerable
                .Range(1, 5)
                .Select(i => new Store { Id = i, Name = i.ToString(CultureInfo.InvariantCulture) })
                .ToList();
            mockStoreMgmtService
                .Setup(service => service.GetFunctioningOrderedStoresAsync())
                .ReturnsAsync(stores);

            await workspace.Initialize().ConfigureAwait(true);
            Assert.Equal(1, storeChangedNotifyCount);

            const int nextStoreIndex = 1;
            var oldStore = workspace.StoreSelector.SelectedStore;
            var nextStore = workspace.StoreSelector.SelectorButtons![nextStoreIndex];
            Assert.NotEqual(nextStore, workspace.StoreSelector.SelectedStore);

            // Act
            workspace.StoreSelector.SelectedStore = nextStore;

            // Assert
            Assert.Equal(oldStore, workspace.StoreSelector.SelectedStore);
            Assert.Equal(1, legacyCtxAskedCount);
            Assert.Equal(1, askDialogPresentedCount);
            Assert.Equal(2, storeChangedNotifyCount);
        }

        [Fact]
        public async void WillChangeAndNotifySelectedWarehouseIfThereIsNeedToSaveWorkspaceAndUserConfirmedChange()
        {
            // Arrange
            const bool needToSaveWorkspaceValue = true;
            const DialogResultEnum userRepliedYesValue = DialogResultEnum.Yes;

            var mockLegacyWorkspaceCtx = new Mock<ILegacyWorkspaceContext>();
            var legacyCtxAskedCount = 0;
            mockLegacyWorkspaceCtx
                .Setup(leg => leg.IsThereUnsavedEntry())
                .Callback(() => legacyCtxAskedCount++)
                .Returns(needToSaveWorkspaceValue);

            var mockDialogService = new Mock<IDialogService>();
            var askDialogPresentedCount = 0;
            mockDialogService
                .Setup(dial => dial.PresentDialog(It.IsAny<string>(), It.IsAny<DialogOptionsEnum>()))
                .Callback(() => askDialogPresentedCount++)
                .Returns(userRepliedYesValue);

            var (mockStoreMgmtService, workspace) = CreateBaseMocks(mockOverrideLegacyWorkspaceContext: mockLegacyWorkspaceCtx, mockOverrideDialogService: mockDialogService);

            var storeChangedNotifyCount = 0;
            workspace.StoreSelector.PropertyChanged += (s, a) =>
            {
                if (a.PropertyName == nameof(vm.StoreSelectorViewModel.SelectedStore))
                    storeChangedNotifyCount++;
            };

            var stores = Enumerable
                .Range(1, 5)
                .Select(i => new Store { Id = i, Name = i.ToString(CultureInfo.InvariantCulture) })
                .ToList();
            mockStoreMgmtService
                .Setup(service => service.GetFunctioningOrderedStoresAsync())
                .ReturnsAsync(stores);

            await workspace.Initialize().ConfigureAwait(true);
            Assert.Equal(1, storeChangedNotifyCount);

            const int nextStoreIndex = 1;
            var oldStore = workspace.StoreSelector.SelectedStore;
            var nextStore = workspace.StoreSelector.SelectorButtons![nextStoreIndex];
            Assert.NotEqual(nextStore, workspace.StoreSelector.SelectedStore);

            // Act
            workspace.StoreSelector.SelectedStore = nextStore;

            // Assert
            Assert.Equal(nextStore, workspace.StoreSelector.SelectedStore);
            Assert.Equal(1, legacyCtxAskedCount);
            Assert.Equal(1, askDialogPresentedCount);
            Assert.Equal(2, storeChangedNotifyCount);
        }
    }
}
