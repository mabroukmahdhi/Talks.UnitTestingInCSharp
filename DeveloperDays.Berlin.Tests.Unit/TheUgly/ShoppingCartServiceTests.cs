// ---------------------------------------------------------------------
// Copyright (c) 2024 eBiz Consulting GmbH
// Made w/ love by Mabrouk Mahdhi for all .NET developer days attendees
// ---------------------------------------------------------------------

using DeveloperDays.Berlin.Services;

namespace DeveloperDays.Berlin.Tests.Unit.TheUgly
{
    public class ShoppingCartServiceTests
    {
        [Fact]
        public void Test1()
        {
            var service = new ShoppingCartService();
            service.AddItemToCart("item1", 2);
            var total = service.CalculateTotalPrice();
            Assert.True(total == 20); // Hardcoded magic number
        }

        [Fact]
        public void Test2()
        {
            var service = new ShoppingCartService();
            service.AddItemToCart("item1", 1);
            service.AddItemToCart("item2", 1);
            service.AddItemToCart("item3", 0); // What is wrong with this line?
            var total = service.CalculateTotalPrice();
            Assert.True(total == 30); // Hardcoded magic number
        }
    }
}
