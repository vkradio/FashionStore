﻿using ApplicationCore.Entities;
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
            var mockStoreMgmtSvc = new Mock<IStoreManagementService>();
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => new WorkspaceViewModel(nullDialogService, mockStoreMgmtSvc.Object, mockLegacyWorkspace.Object));

            // Assert
            Assert.NotNull(ex);
        }

        [Fact]
        public void ConstructorThrowsIfStoreManagementServiceIsNull()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            IStoreManagementService nullStoreMgmtSvc = null;
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => new WorkspaceViewModel(mockDialogService.Object, nullStoreMgmtSvc, mockLegacyWorkspace.Object));

            // Assert
            Assert.NotNull(ex);
        }

        [Fact]
        public void ConstructorThrowsIfLegacyWorkspaceContextIsNull()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockStoreMgmtSvc = new Mock<IStoreManagementService>();
            ILegacyWorkspaceContext nullLegacyWorkspace = null;

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => new WorkspaceViewModel(mockDialogService.Object, mockStoreMgmtSvc.Object, nullLegacyWorkspace));

            // Assert
            Assert.NotNull(ex);
        }

        [Fact]
        public void BeforeInitStoreSelectorIsNotNull()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockStoreMgmtSvc = new Mock<IStoreManagementService>();
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockStoreMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            var storeSelector = workspace.StoreSelector;

            // Assert
            Assert.NotNull(storeSelector);
        }

        [Fact]
        public void BeforeInitCurrentStoreIsNull()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockStoreMgmtSvc = new Mock<IStoreManagementService>();
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockStoreMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            var currentStore = workspace.CurrentStore;

            // Assert
            Assert.Null(currentStore);
        }

        [Fact]
        public async void AfterInitCurrentStoreIsNullIfThereAreNoStores()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockStoreMgmtSvc = new Mock<IStoreManagementService>();
            var emptyStoreCollection = Array.Empty<Store>();
            mockStoreMgmtSvc
                .Setup(wService => wService.GetFunctioningOrderedStoresAsync())
                .ReturnsAsync(emptyStoreCollection);
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockStoreMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            await workspace.Initialize();
            var currentStore = workspace.CurrentStore;

            // Assert
            Assert.Null(currentStore);
        }

        [Fact]
        public async void AfterInitCurrentStoreIsSetFromSingleStore()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockStoreMgmtSvc = new Mock<IStoreManagementService>();
            var singleStore = new Store { Id = 1 };
            var singleStoreCollection = new Store[] { singleStore };
            mockStoreMgmtSvc
                .Setup(wService => wService.GetFunctioningOrderedStoresAsync())
                .ReturnsAsync(singleStoreCollection);
            mockStoreMgmtSvc
                .Setup(wService => wService.GetStore(It.IsAny<int>()))
                .ReturnsAsync(singleStore);
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockStoreMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            await workspace.Initialize();
            var currentStore = workspace.CurrentStore;

            // Assert
            Assert.Equal(singleStore, currentStore);
        }

        [Fact]
        public async void AfterInitCurrentStoreIsSetToFirstFrom5Stores()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockStoreMgmtSvc = new Mock<IStoreManagementService>();
            const string namePrefix = "Store ";
            var fiveStoreCollection = Enumerable
                .Range(1, 5)
                .Select(i => new Store { Id = i, Name = namePrefix + i.ToString(CultureInfo.InvariantCulture) })
                .ToList();
            var firstStore = fiveStoreCollection[0];
            mockStoreMgmtSvc
                .Setup(wService => wService.GetFunctioningOrderedStoresAsync())
                .ReturnsAsync(fiveStoreCollection);
            mockStoreMgmtSvc
                .Setup(wService => wService.GetStore(It.IsAny<int>()))
                .ReturnsAsync(firstStore);
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockStoreMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            await workspace.Initialize();
            var currentStore = workspace.CurrentStore;

            // Assert
            Assert.Equal(firstStore, currentStore);
        }

        [Fact]
        public void BeforeInitItIsAllowedToChangeStoreAndUnsavedEntryIsNotChecked()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockStoreMgmtSvc = new Mock<IStoreManagementService>();
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            var legacyWorkspaceCheckedForUnsavedEntry = false;
            mockLegacyWorkspace
                .Setup(legacy => legacy.IsThereUnsavedEntry())
                .Callback(() => legacyWorkspaceCheckedForUnsavedEntry = true);
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockStoreMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            var result = workspace.IsItAllowedToChangeCurrentStore();

            // Assert
            Assert.True(result);
            Assert.False(legacyWorkspaceCheckedForUnsavedEntry);
        }

        [Fact]
        public void AfterInitItIsForbiddenToChangeStoreIfThereAreUnsavedUserEntryAndUserCancelsReset()
        {
            // Arrange
            var numberOfAsksForUser = 0;
            var mockDialogService = new Mock<IDialogService>();
            const DialogResultEnum userReply = DialogResultEnum.Cancel;
            mockDialogService
                .Setup(dialog => dialog.PresentDialog(It.IsAny<string>(), It.IsAny<DialogOptionsEnum>()))
                .Callback(() => numberOfAsksForUser++)
                .Returns(userReply);
            var mockStoreMgmtSvc = new Mock<IStoreManagementService>();
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            const bool thereIsUnsavedUserEntryValue = true;
            var legacyWorkspaceCheckedForUnsavedEntry = false;
            mockLegacyWorkspace
                .Setup(legacy => legacy.IsThereUnsavedEntry())
                .Callback(() => legacyWorkspaceCheckedForUnsavedEntry = true)
                .Returns(thereIsUnsavedUserEntryValue);
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockStoreMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            var result = workspace.IsItAllowedToChangeCurrentStore();

            // Assert
            Assert.False(result);
            Assert.True(legacyWorkspaceCheckedForUnsavedEntry);
            Assert.Equal(1, numberOfAsksForUser);
        }

        [Fact]
        public void AfterInitItIsForbiddenToChangeStoreIfThereAreUnsavedUserEntryAndUserSaidNoToReset()
        {
            // Arrange
            var numberOfAsksForUser = 0;
            var mockDialogService = new Mock<IDialogService>();
            const DialogResultEnum userReply = DialogResultEnum.No;
            mockDialogService
                .Setup(dialog => dialog.PresentDialog(It.IsAny<string>(), It.IsAny<DialogOptionsEnum>()))
                .Callback(() => numberOfAsksForUser++)
                .Returns(userReply);
            var mockStoreMgmtSvc = new Mock<IStoreManagementService>();
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            const bool thereIsUnsavedUserEntryValue = true;
            var legacyWorkspaceCheckedForUnsavedEntry = false;
            mockLegacyWorkspace
                .Setup(legacy => legacy.IsThereUnsavedEntry())
                .Callback(() => legacyWorkspaceCheckedForUnsavedEntry = true)
                .Returns(thereIsUnsavedUserEntryValue);
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockStoreMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            var result = workspace.IsItAllowedToChangeCurrentStore();

            // Assert
            Assert.False(result);
            Assert.True(legacyWorkspaceCheckedForUnsavedEntry);
            Assert.Equal(1, numberOfAsksForUser);
        }

        [Fact]
        public void AfterInitItIsAllowedToChangeStoreIfThereAreUnsavedUserEntryAndUserSaidYesToReset()
        {
            // Arrange
            var numberOfAsksForUser = 0;
            var mockDialogService = new Mock<IDialogService>();
            const DialogResultEnum userReply = DialogResultEnum.Yes;
            mockDialogService
                .Setup(dialog => dialog.PresentDialog(It.IsAny<string>(), It.IsAny<DialogOptionsEnum>()))
                .Callback(() => numberOfAsksForUser++)
                .Returns(userReply);
            var mockStoreMgmtSvc = new Mock<IStoreManagementService>();
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            const bool thereIsUnsavedUserEntryValue = true;
            var legacyWorkspaceCheckedForUnsavedEntry = false;
            mockLegacyWorkspace
                .Setup(legacy => legacy.IsThereUnsavedEntry())
                .Callback(() => legacyWorkspaceCheckedForUnsavedEntry = true)
                .Returns(thereIsUnsavedUserEntryValue);
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockStoreMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            var result = workspace.IsItAllowedToChangeCurrentStore();

            // Assert
            Assert.True(result);
            Assert.True(legacyWorkspaceCheckedForUnsavedEntry);
            Assert.Equal(1, numberOfAsksForUser);
        }

        [Fact]
        public void AfterInitItIsAllowedToChangeStoreIfThereIsNoUnsavedUserEntry()
        {
            // Arrange
            var numberOfAsksForUser = 0;
            var mockDialogService = new Mock<IDialogService>();
            mockDialogService
                .Setup(dialog => dialog.PresentDialog(It.IsAny<string>(), It.IsAny<DialogOptionsEnum>()))
                .Callback(() => numberOfAsksForUser++);
            var mockStoreMgmtSvc = new Mock<IStoreManagementService>();
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            const bool thereIsNoUnsavedUserEntryValue = false;
            var legacyWorkspaceCheckedForUnsavedEntry = false;
            mockLegacyWorkspace
                .Setup(legacy => legacy.IsThereUnsavedEntry())
                .Callback(() => legacyWorkspaceCheckedForUnsavedEntry = true)
                .Returns(thereIsNoUnsavedUserEntryValue);
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockStoreMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            var result = workspace.IsItAllowedToChangeCurrentStore();

            // Assert
            Assert.True(result);
            Assert.True(legacyWorkspaceCheckedForUnsavedEntry);
            Assert.Equal(0, numberOfAsksForUser);
        }
    }
}
