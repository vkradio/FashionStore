using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using Utilities;
using ViewModels;
using Xunit;

namespace ViewModelsTests
{
    public class WarehouseButtonViewModelUT
    {
        [Fact]
        public void WillThrowExceptionIfConstructorPassNullWarehouse()
        {
            // Arrange
            Warehouse nullWarehouse = null;

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => new WarehouseButtonViewModel(nullWarehouse));

            // Assert
            Assert.NotNull(ex);
        }

        [Fact]
        public void WillContainProperName()
        {
            // Arrange
            const string whName = "Big Bright Warehouse";
            var warehouse = new Warehouse { Name = whName };

            // Act
            var whButton = new WarehouseButtonViewModel(warehouse);

            // Assert
            Assert.Equal(whName, whButton.WarehouseName);
        }

        [Fact]
        public void ChangeNameAndNotifyTheSubscriber()
        {
            // Arrange
            var whName1 = "Big Bright Warehouse";
            var whName2 = "Small Warehouse";
            var warehouse = new Warehouse { Name = whName1 };
            var whButton = new WarehouseButtonViewModel(warehouse);
            var propertyChangeRegistry = new Dictionary<string, int>();
            whButton.PropertyChanged += (s, a) =>
            {
                Assert.Equal(whButton, s);
                propertyChangeRegistry.Inc(a.PropertyName);
            };
            Assert.Equal(whName1, whButton.WarehouseName);

            // Act
            whButton.WarehouseName = whName2;

            // Assert
            Assert.Equal(whName2, whButton.WarehouseName);
            Assert.Collection(propertyChangeRegistry,
                item =>
                {
                    Assert.Equal(nameof(WarehouseButtonViewModel.WarehouseName), item.Key);
                    Assert.Equal(1, item.Value);
                }
            );
        }

        [Fact]
        public void WillContainProperId()
        {
            // Arrange
            const int id = 5;
            var warehouse = new Warehouse { WarehouseId = id };

            // Act
            var whButton = new WarehouseButtonViewModel(warehouse);

            // Assert
            Assert.Equal(id, whButton.WarehouseId);
        }

        [Fact]
        public void ChangeIdAndNotifyTheSubscriber()
        {
            // Arrange
            const int initialId = 1;
            const int newId = 2;
            var warehouse = new Warehouse { WarehouseId = initialId };
            var whButton = new WarehouseButtonViewModel(warehouse);
            var propertyChangeRegistry = new Dictionary<string, int>();
            whButton.PropertyChanged += (s, a) =>
            {
                Assert.Equal(whButton, s);
                propertyChangeRegistry.Inc(a.PropertyName);
            };
            Assert.Equal(initialId, whButton.WarehouseId);

            // Act
            whButton.WarehouseId = newId;

            // Assert
            Assert.Equal(newId, whButton.WarehouseId);
            Assert.Collection(propertyChangeRegistry,
                item =>
                {
                    Assert.Equal(nameof(WarehouseButtonViewModel.WarehouseId), item.Key);
                    Assert.Equal(1, item.Value);
                }
            );
        }
    }
}
