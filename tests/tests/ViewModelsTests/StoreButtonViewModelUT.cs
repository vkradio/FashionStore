using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using Utilities;
using ViewModels;
using Xunit;

namespace ViewModelsTests
{
    public class StoreButtonViewModelUT
    {
        [Fact]
        public void WillThrowExceptionIfConstructorPassNullStore()
        {
            // Arrange
            Store? nullStore = null;

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => new StoreButtonViewModel(nullStore!));

            // Assert
            Assert.NotNull(ex);
        }

        [Fact]
        public void WillContainProperName()
        {
            // Arrange
            const string storeName = "Big Bright Store";
            var store = new Store { Name = storeName };

            // Act
            var storeButton = new StoreButtonViewModel(store);

            // Assert
            Assert.Equal(storeName, storeButton.StoreName);
        }

        [Fact]
        public void ChangeNameAndNotifyTheSubscriber()
        {
            // Arrange
            var storeName1 = "Big Bright Store";
            var storeName2 = "Small Store";
            var store = new Store { Name = storeName1 };
            var storeButton = new StoreButtonViewModel(store);
            var propertyChangeRegistry = new Dictionary<string, int>();
            storeButton.PropertyChanged += (s, a) =>
            {
                Assert.Equal(storeButton, s);
                propertyChangeRegistry.Inc(a.PropertyName);
            };
            Assert.Equal(storeName1, storeButton.StoreName);

            // Act
            storeButton.StoreName = storeName2;

            // Assert
            Assert.Equal(storeName2, storeButton.StoreName);
            Assert.Collection(propertyChangeRegistry,
                item =>
                {
                    Assert.Equal(nameof(StoreButtonViewModel.StoreName), item.Key);
                    Assert.Equal(1, item.Value);
                }
            );
        }

        [Fact]
        public void WillContainProperId()
        {
            // Arrange
            const int id = 5;
            var store = new Store { Id = id };

            // Act
            var storeButton = new StoreButtonViewModel(store);

            // Assert
            Assert.Equal(id, storeButton.StoreId);
        }

        [Fact]
        public void ChangeIdAndNotifyTheSubscriber()
        {
            // Arrange
            const int initialId = 1;
            const int newId = 2;
            var store = new Store { Id = initialId };
            var storeButton = new StoreButtonViewModel(store);
            var propertyChangeRegistry = new Dictionary<string, int>();
            storeButton.PropertyChanged += (s, a) =>
            {
                Assert.Equal(storeButton, s);
                propertyChangeRegistry.Inc(a.PropertyName);
            };
            Assert.Equal(initialId, storeButton.StoreId);

            // Act
            storeButton.StoreId = newId;

            // Assert
            Assert.Equal(newId, storeButton.StoreId);
            Assert.Collection(propertyChangeRegistry,
                item =>
                {
                    Assert.Equal(nameof(StoreButtonViewModel.StoreId), item.Key);
                    Assert.Equal(1, item.Value);
                }
            );
        }
    }
}
