using ApplicationCore.Interfaces;
using Moq;
using MvvmInfrastructure;
using System;
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
            IDialogService? nullDialogService = null;
            var mockLocalizationService = new Mock<ILocalizationService>();
            var mockStoreMgmtSvc = new Mock<IStoreManagementService>();
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => new WorkspaceViewModel(nullDialogService!, mockLocalizationService.Object, mockStoreMgmtSvc.Object, mockLegacyWorkspace.Object));

            // Assert
            Assert.NotNull(ex);
        }

        [Fact]
        public void ConstructorThrowsIfLocalizationServiceIsNull()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            ILocalizationService? nullLocalizationService = null;
            var mockStoreMgmtSvc = new Mock<IStoreManagementService>();
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => new WorkspaceViewModel(mockDialogService.Object, nullLocalizationService!, mockStoreMgmtSvc.Object, mockLegacyWorkspace.Object));

            // Assert
            Assert.NotNull(ex);
        }

        [Fact]
        public void ConstructorThrowsIfStoreManagementServiceIsNull()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockLocalizationService = new Mock<ILocalizationService>();
            IStoreManagementService? nullStoreMgmtSvc = null;
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => new WorkspaceViewModel(mockDialogService.Object, mockLocalizationService.Object, nullStoreMgmtSvc!, mockLegacyWorkspace.Object));

            // Assert
            Assert.NotNull(ex);
        }

        [Fact]
        public void ConstructorThrowsIfLegacyWorkspaceContextIsNull()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockLocalizationService = new Mock<ILocalizationService>();
            var mockStoreMgmtSvc = new Mock<IStoreManagementService>();
            ILegacyWorkspaceContext? nullLegacyWorkspace = null;

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => new WorkspaceViewModel(mockDialogService.Object, mockLocalizationService.Object, mockStoreMgmtSvc.Object, nullLegacyWorkspace!));

            // Assert
            Assert.NotNull(ex);
        }

        [Fact]
        public void BeforeInitStoreSelectorIsNotNull()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockLocalizationService = new Mock<ILocalizationService>();
            var mockStoreMgmtSvc = new Mock<IStoreManagementService>();
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockLocalizationService.Object, mockStoreMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            var storeSelector = workspace.StoreSelector;

            // Assert
            Assert.NotNull(storeSelector);
        }

        [Fact]
        public void BeforeInitItIsAllowedToChangeStoreAndUnsavedEntryIsNotChecked()
        {
            // Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockLocalizationService = new Mock<ILocalizationService>();
            var mockStoreMgmtSvc = new Mock<IStoreManagementService>();
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            var legacyWorkspaceCheckedForUnsavedEntry = false;
            mockLegacyWorkspace
                .Setup(legacy => legacy.IsThereUnsavedEntry())
                .Callback(() => legacyWorkspaceCheckedForUnsavedEntry = true);
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockLocalizationService.Object, mockStoreMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            var result = workspace.IsItAllowedToChangeCurrentStore();

            // Assert
            Assert.True(result);
            Assert.False(legacyWorkspaceCheckedForUnsavedEntry);
        }

        [Fact]
        public async void AfterInitItIsForbiddenToChangeStoreIfThereAreUnsavedUserEntryAndUserCancelsReset()
        {
            // Arrange
            var numberOfAsksForUser = 0;
            var mockDialogService = new Mock<IDialogService>();
            const DialogResultEnum userReply = DialogResultEnum.Cancel;
            mockDialogService
                .Setup(dialog => dialog.PresentDialog(It.IsAny<string>(), It.IsAny<DialogOptionsEnum>()))
                .Callback(() => numberOfAsksForUser++)
                .Returns(userReply);
            var mockLocalizationService = new Mock<ILocalizationService>();
            var mockStoreMgmtSvc = new Mock<IStoreManagementService>();
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            const bool thereIsUnsavedUserEntryValue = true;
            var legacyWorkspaceCheckedForUnsavedEntry = false;
            mockLegacyWorkspace
                .Setup(legacy => legacy.IsThereUnsavedEntry())
                .Callback(() => legacyWorkspaceCheckedForUnsavedEntry = true)
                .Returns(thereIsUnsavedUserEntryValue);
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockLocalizationService.Object, mockStoreMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            await workspace.Initialize().ConfigureAwait(true);
            var result = workspace.IsItAllowedToChangeCurrentStore();

            // Assert
            Assert.False(result);
            Assert.True(legacyWorkspaceCheckedForUnsavedEntry);
            Assert.Equal(1, numberOfAsksForUser);
        }

        [Fact]
        public async void AfterInitItIsForbiddenToChangeStoreIfThereAreUnsavedUserEntryAndUserSaidNoToReset()
        {
            // Arrange
            var numberOfAsksForUser = 0;
            var mockDialogService = new Mock<IDialogService>();
            const DialogResultEnum userReply = DialogResultEnum.No;
            mockDialogService
                .Setup(dialog => dialog.PresentDialog(It.IsAny<string>(), It.IsAny<DialogOptionsEnum>()))
                .Callback(() => numberOfAsksForUser++)
                .Returns(userReply);
            var mockLocalizationService = new Mock<ILocalizationService>();
            var mockStoreMgmtSvc = new Mock<IStoreManagementService>();
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            const bool thereIsUnsavedUserEntryValue = true;
            var legacyWorkspaceCheckedForUnsavedEntry = false;
            mockLegacyWorkspace
                .Setup(legacy => legacy.IsThereUnsavedEntry())
                .Callback(() => legacyWorkspaceCheckedForUnsavedEntry = true)
                .Returns(thereIsUnsavedUserEntryValue);
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockLocalizationService.Object, mockStoreMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            await workspace.Initialize().ConfigureAwait(true);
            var result = workspace.IsItAllowedToChangeCurrentStore();

            // Assert
            Assert.False(result);
            Assert.True(legacyWorkspaceCheckedForUnsavedEntry);
            Assert.Equal(1, numberOfAsksForUser);
        }

        [Fact]
        public async void AfterInitItIsAllowedToChangeStoreIfThereAreUnsavedUserEntryAndUserSaidYesToReset()
        {
            // Arrange
            var numberOfAsksForUser = 0;
            var mockDialogService = new Mock<IDialogService>();
            const DialogResultEnum userReply = DialogResultEnum.Yes;
            mockDialogService
                .Setup(dialog => dialog.PresentDialog(It.IsAny<string>(), It.IsAny<DialogOptionsEnum>()))
                .Callback(() => numberOfAsksForUser++)
                .Returns(userReply);
            var mockLocalizationService = new Mock<ILocalizationService>();
            var mockStoreMgmtSvc = new Mock<IStoreManagementService>();
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            const bool thereIsUnsavedUserEntryValue = true;
            var legacyWorkspaceCheckedForUnsavedEntry = false;
            mockLegacyWorkspace
                .Setup(legacy => legacy.IsThereUnsavedEntry())
                .Callback(() => legacyWorkspaceCheckedForUnsavedEntry = true)
                .Returns(thereIsUnsavedUserEntryValue);
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockLocalizationService.Object, mockStoreMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            await workspace.Initialize().ConfigureAwait(true);
            var result = workspace.IsItAllowedToChangeCurrentStore();

            // Assert
            Assert.True(result);
            Assert.True(legacyWorkspaceCheckedForUnsavedEntry);
            Assert.Equal(1, numberOfAsksForUser);
        }

        [Fact]
        public async void AfterInitItIsAllowedToChangeStoreIfThereIsNoUnsavedUserEntry()
        {
            // Arrange
            var numberOfAsksForUser = 0;
            var mockDialogService = new Mock<IDialogService>();
            mockDialogService
                .Setup(dialog => dialog.PresentDialog(It.IsAny<string>(), It.IsAny<DialogOptionsEnum>()))
                .Callback(() => numberOfAsksForUser++);
            var mockLocalizationService = new Mock<ILocalizationService>();
            var mockStoreMgmtSvc = new Mock<IStoreManagementService>();
            var mockLegacyWorkspace = new Mock<ILegacyWorkspaceContext>();
            const bool thereIsNoUnsavedUserEntryValue = false;
            var legacyWorkspaceCheckedForUnsavedEntry = false;
            mockLegacyWorkspace
                .Setup(legacy => legacy.IsThereUnsavedEntry())
                .Callback(() => legacyWorkspaceCheckedForUnsavedEntry = true)
                .Returns(thereIsNoUnsavedUserEntryValue);
            var workspace = new WorkspaceViewModel(mockDialogService.Object, mockLocalizationService.Object, mockStoreMgmtSvc.Object, mockLegacyWorkspace.Object);

            // Act
            await workspace.Initialize().ConfigureAwait(true);
            var result = workspace.IsItAllowedToChangeCurrentStore();

            // Assert
            Assert.True(result);
            Assert.True(legacyWorkspaceCheckedForUnsavedEntry);
            Assert.Equal(0, numberOfAsksForUser);
        }
    }
}
