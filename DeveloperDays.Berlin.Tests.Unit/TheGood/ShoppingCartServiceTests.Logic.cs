// ---------------------------------------------------------------------
// Copyright (c) 2024 eBiz Consulting GmbH
// Made w/ love by Mabrouk Mahdhi for all .NET developer days attendees
// ---------------------------------------------------------------------

using System.Collections.Generic;
using DeveloperDays.Berlin.Data;
using FluentAssertions;
using Moq;

namespace DeveloperDays.Berlin.Tests.Unit.TheGood
{
    public partial class ShoppingCartServiceTests
    {
        [Fact]
        public void ShouldAddItemToCart()
        {
            // given
            var randomInventory = GetRandomInventory();
            var retrievedInventory = randomInventory;
            var retrievedInventoryCount = retrievedInventory.Count;

            var randomItem = retrievedInventory[GetRandomNumber(0, retrievedInventoryCount - 1)];
            var inputItem = randomItem;
            var availableStock = inputItem.Stock;

            var randomQuantity = GetRandomNumber(1, availableStock);
            var inputQuantity = randomQuantity;

            var randomCart = new List<CartItem>(); // empty cart, can be also random!!
            var retrievedCart = randomCart;

            this.dataStorageMock.Setup(storage =>
                storage.GetInventory())
                .Returns(retrievedInventory);

            this.dataStorageMock.Setup(storage =>
                storage.GetCart())
                .Returns(retrievedCart);

            // when
            var isAdded = service.AddItemToCart(
                itemId: inputItem.ItemId,
                quantity: inputQuantity);

            // then
            isAdded.Should().BeTrue();

            this.dataStorageMock.Verify(storage =>
                storage.GetInventory(), Times.Once);

            this.dataStorageMock.Verify(storage =>
                storage.GetCart(), Times.Once);

            this.dataStorageMock.VerifyNoOtherCalls();
        }
    }
}
