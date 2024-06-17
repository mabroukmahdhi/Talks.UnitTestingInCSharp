// ---------------------------------------------------------------------
// Copyright (c) 2024 eBiz Consulting GmbH
// Made w/ love by Mabrouk Mahdhi for all .NET developer days attendees
// --------------------------------------------------------------------- 

using DeveloperDays.Berlin.Data;
using DeveloperDays.Berlin.Services;

namespace DeveloperDays.Berlin.Tests.Unit.TheBad
{
    public class ShoppingCartServiceTests
    {
        private readonly ShoppingCartService service = new(new DataStorages.DataStorage());

        [Fact]
        public void ShouldAddItemToCart()
        {
            var item = new Item { ItemId = "item1", Price = 10 };
            var isAdded = service.AddItemToCart("item1", 2);

            Assert.True(isAdded);
        }

        [Fact]
        public void ShouldCalculateCartTotalPrice()
        {
            var total = service.CalculateTotalPrice();

            Assert.Equal(0, total);
        }

        [Fact]
        public void ShouldAddMultipleItems()
        {
            var item1Added = service.AddItemToCart("item1", 1);
            var item2Added = service.AddItemToCart("item2", 1);

            Assert.True(item1Added);
            Assert.True(item2Added);
        }
    }
}
