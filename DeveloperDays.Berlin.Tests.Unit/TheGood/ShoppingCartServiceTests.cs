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
