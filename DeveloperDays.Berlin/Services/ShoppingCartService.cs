// ---------------------------------------------------------------------
// Copyright (c) 2024 eBiz Consulting GmbH
// Made w/ love by Mabrouk Mahdhi for all .NET developer days attendees
// ---------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using DeveloperDays.Berlin.DataStorages;

namespace DeveloperDays.Berlin.Services
{
    public class ShoppingCartService
    {
        private readonly DataStorage dataStorage;

        public ShoppingCartService() =>
            this.dataStorage = new DataStorage();

        public bool AddItemToCart(string itemId, int quantity)
        {
            var inventory = dataStorage.GetInventory();

            if (!inventory.TryGetValue(itemId, out (double price, int stock) value)
                || value.stock < quantity)
            {
                return false;
            }

            var cart = dataStorage.GetCart();
            var existingItem = cart.FirstOrDefault(x => x.itemId == itemId);
            if (existingItem != default)
            {
                cart.Remove(existingItem);
                cart.Add((itemId, existingItem.quantity + quantity));
            }
            else
            {
                cart.Add((itemId, quantity));
            }

            inventory[itemId] = (value.price, value.stock - quantity);
            return true;
        }

        public double CalculateTotalPrice()
        {
            var inventory = this.dataStorage.GetInventory();
            var cart = this.dataStorage.GetCart();

            return cart.Sum(x => x.quantity * inventory[x.itemId].price);
        }

        public double ApplyDiscount(double discountPercentage)
        {
            var total = CalculateTotalPrice();
            return total - (total * (discountPercentage / 100));
        }

        public Dictionary<string, int> GetCartContents()
        {
            var cart = this.dataStorage.GetCart();

            return cart.ToDictionary(x => x.itemId, x => x.quantity);
        }
    }
}
