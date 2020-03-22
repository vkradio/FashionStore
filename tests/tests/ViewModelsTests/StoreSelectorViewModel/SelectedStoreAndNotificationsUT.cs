using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Moq;
using MvvmInfrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using vm = ViewModels;
using Xunit;

namespace ViewModelsTests.StoreSelectorViewModel
{
    public class SelectedStoreAndNotificationsUT
    {
        (Mock<IStoreManagementService>, vm.WorkspaceViewModel) CreateBaseMocks(
            Mock<vm.ILegacyWorkspaceContext> mockOverrideLegacyWorkspaceContext = null,
            Mock<IDialogService> mockOverrideDialogService = null
        )
        {
            var mockDialogService = mockOverrideDialogService ?? new Mock<IDialogService>();
            var mockLocalizationService = new Mock<ILocalizationService>();
            var mockStoreMgmtService = new Mock<IStoreManagementService>();
            var mockLegacyWorkspaceCtx = mockOverrideLegacyWorkspaceContext ?? new Mock<vm.ILegacyWorkspaceContext>();
            var workspace = new vm.WorkspaceViewModel(mockDialogService.Object, mockLocalizationService.Object, mockStoreMgmtService.Object, mockLegacyWorkspaceCtx.Object);

            return (mockStoreMgmtService, workspace);
        }

        [Theory]
        [InlineData(false, false, DialogResultEnum.Cancel)]
        [InlineData(true, false, DialogResultEnum.Cancel)]
        [InlineData(true, true, DialogResultEnum.Cancel)]
        [InlineData(true, true, DialogResultEnum.Yes)]
        public async void DifferentSelectionConditions(bool selectDifferentStore, bool needToSaveWorkspace, DialogResultEnum userReply)
        {
            // Arrange
            var mockLegacyWorkspaceCtx = new Mock<vm.ILegacyWorkspaceContext>();
            var legacyCtxAskedCount = 0;
            mockLegacyWorkspaceCtx
                .Setup(leg => leg.IsThereUnsavedEntry())
                .Callback(() => legacyCtxAskedCount++)
                .Returns(needToSaveWorkspace);

            var mockDialogService = new Mock<IDialogService>();
            var askDialogPresentedCount = 0;
            mockDialogService
                .Setup(dial => dial.PresentDialog(It.IsAny<string>(), It.IsAny<DialogOptionsEnum>()))
                .Callback(() => askDialogPresentedCount++)
                .Returns(userReply);

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
            var nextStore = workspace.StoreSelector.SelectorButtons[nextStoreIndex];
            Assert.NotEqual(oldStore, nextStore);

            // Act
            if (selectDifferentStore)
                workspace.StoreSelector.SelectedStore = nextStore;
            else
                workspace.StoreSelector.SelectedStore = oldStore;

            // Assert
            if (selectDifferentStore)
            {
                // If we need to select different Store, legacy context will always be asked once, whether it is allowed to do that.
                Assert.Equal(1, legacyCtxAskedCount);

                if (needToSaveWorkspace)
                {
                    // If we need to save user entry, user will always be asked once, whether he wants to discard unsaved changes.
                    Assert.Equal(1, askDialogPresentedCount);

                    if (userReply == DialogResultEnum.Yes)
                    {
                        // If user allow to discard changes, we'll register the second change notification on selected Store and...
                        Assert.Equal(2, storeChangedNotifyCount);
                        // ... that selected Store will be the new (next) one.
                        Assert.Equal(nextStore, workspace.StoreSelector.SelectedStore);
                    }
                    else
                    {
                        // If user do not allow to discard changes, we'll register the second change notification on selected Store anyway...
                        Assert.Equal(2, storeChangedNotifyCount);
                        // ... but this will be re-selection of the old Store, so our WPF View will roll back it's selection.
                        Assert.Equal(oldStore, workspace.StoreSelector.SelectedStore);
                    }
                }
                else
                {
                    // If we don't need to save unsaved user data, no need to ask him...
                    Assert.Equal(0, askDialogPresentedCount);
                    // ... so we register the second Store change notification...
                    Assert.Equal(2, storeChangedNotifyCount);
                    // ... and select the second Store.
                    Assert.Equal(nextStore, workspace.StoreSelector.SelectedStore);
                }
            }
            else
            {
                // Here we do not select anything and do not check anything. The only reason why Store change notification count is 1 is
                // because it was registered right after the selector initialization.
                Assert.Equal(0, legacyCtxAskedCount);
                Assert.Equal(0, askDialogPresentedCount);
                Assert.Equal(1, storeChangedNotifyCount);
                Assert.Equal(oldStore, workspace.StoreSelector.SelectedStore);
            }
        }
    }
}
