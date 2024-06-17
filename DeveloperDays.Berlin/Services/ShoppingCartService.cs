// ---------------------------------------------------------------------
// Copyright (c) 2024 eBiz Consulting GmbH
// Made w/ love by Mabrouk Mahdhi for all .NET developer days attendees
// ---------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using DeveloperDays.Berlin.Data;
using DeveloperDays.Berlin.DataStorages;

namespace DeveloperDays.Berlin.Services
{
    public class ShoppingCartService(IDataStorage dataStorage)
    {
        private readonly IDataStorage dataStorage = dataStorage;

        public bool AddItemToCart(string itemId, int quantity)
        {
            var inventory = dataStorage.GetInventory();

            var maybeItem = inventory.FirstOrDefault(item => item.ItemId == itemId);

            if (maybeItem is null || maybeItem.Stock < quantity)
            {
                return false;
            }

            var cart = dataStorage.GetCart();
            var existingItem = cart.FirstOrDefault(x => x.ItemId == itemId);

            if (existingItem != default)
            {
                cart.Remove(existingItem);
                cart.Add(new CartItem
                {
                    ItemId = itemId,
                    Quantity = existingItem.Quantity + quantity
                });
            }
            else
            {
                cart.Add(new CartItem
                {
                    ItemId = itemId,
                    Quantity = quantity
                });
            }

            maybeItem.Stock -= quantity;

            return true;
        }

        public double CalculateTotalPrice()
        {
            var inventory = this.dataStorage.GetInventory();
            var cart = this.dataStorage.GetCart();

            return cart.Sum(x => x.Quantity * inventory.First(item => item.ItemId == x.ItemId).Price);
        }

        public double ApplyDiscount(double discountPercentage)
        {
            var total = CalculateTotalPrice();
            return total - (total * (discountPercentage / 100));
        }

        public Dictionary<string, int> GetCartContents()
        {
            var cart = this.dataStorage.GetCart();

            return cart.ToDictionary(x => x.ItemId, x => x.Quantity);
        }
    }
}
