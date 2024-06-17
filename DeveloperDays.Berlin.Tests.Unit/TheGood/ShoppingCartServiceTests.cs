// ---------------------------------------------------------------------
// Copyright (c) 2024 eBiz Consulting GmbH
// Made w/ love by Mabrouk Mahdhi for all .NET developer days attendees
// ---------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Xml.Linq;
using DeveloperDays.Berlin.Data;
using DeveloperDays.Berlin.DataStorages;
using DeveloperDays.Berlin.Services;
using FluentAssertions;
using Moq;
using Tynamix.ObjectFiller;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DeveloperDays.Berlin.Tests.Unit.TheGood
{
    public class ShoppingCartServiceTests
    {
        private readonly Mock<IDataStorage> dataStorageMock;
        private readonly ShoppingCartService service;

        public ShoppingCartServiceTests()
        {
            this.dataStorageMock = new Mock<IDataStorage>();
            this.service = new ShoppingCartService(
                dataStorage: this.dataStorageMock.Object);
        }


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

        private static List<Item> GetRandomInventory()
        {
            var randomCount = GetRandomNumber(1, 10);

            var inventory = new List<Item>();

            for (var i = 0; i < randomCount; i++)
            {
                inventory.Add(CreateRandomItem());
            }
            return inventory;
        }

        private static Item CreateRandomItem()
        {
            var filler = new Filler<Item>();

            var randomQuantity = GetRandomNumber(1, 10);

            filler.Setup()
                .OnProperty(item => item.Stock)
                .Use(randomQuantity);

            return filler.Create();
        }

        private static int GetRandomNumber(int min, int max) =>
            new IntRange(min, max).GetValue();
    }
}
