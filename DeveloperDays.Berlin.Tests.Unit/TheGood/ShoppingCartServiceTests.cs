// ---------------------------------------------------------------------
// Copyright (c) 2024 eBiz Consulting GmbH
// Made w/ love by Mabrouk Mahdhi for all .NET developer days attendees
// ---------------------------------------------------------------------

using System.Collections.Generic;
using DeveloperDays.Berlin.Data;
using DeveloperDays.Berlin.DataStorages;
using DeveloperDays.Berlin.Services;
using Moq;
using Tynamix.ObjectFiller;

namespace DeveloperDays.Berlin.Tests.Unit.TheGood
{
    public partial class ShoppingCartServiceTests
    {
        private readonly Mock<IDataStorage> dataStorageMock;
        private readonly ShoppingCartService service;

        public ShoppingCartServiceTests()
        {
            this.dataStorageMock = new Mock<IDataStorage>();
            this.service = new ShoppingCartService(
                dataStorage: this.dataStorageMock.Object);
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

        private static List<Item> GetRandomInventory(
            List<CartItem> cartItems,
            double totalPrice)
        {
            var inventory = new List<Item>();

            var cartItemsCount = cartItems.Count;

            var pricePerItem = totalPrice / cartItemsCount;

            for (var i = 0; i < cartItems.Count; i++)
            {
                inventory.Add(new Item
                {
                    ItemId = cartItems[i].ItemId,
                    Price = pricePerItem / cartItems[i].Quantity,
                });
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

        private static List<CartItem> GetRandomCart()
        {
            var randomCount = GetRandomNumber(1, 10);

            var cart = new List<CartItem>();

            for (var i = 0; i < randomCount; i++)
            {
                cart.Add(CreateRandomCartItem());
            }
            return cart;
        }

        private static CartItem CreateRandomCartItem()
        {
            var filler = new Filler<CartItem>();

            var randomQuantity = GetRandomNumber(1, 10);

            filler.Setup()
                .OnProperty(item => item.Quantity)
                .Use(randomQuantity);

            return filler.Create();
        }

        private static int GetRandomNumber(int min, int max) =>
            new IntRange(min, max).GetValue();

        private static double GetRandomPrice(double min, double max) =>
           new DoubleRange(min, max).GetValue();
    }
}
