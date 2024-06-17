// ---------------------------------------------------------------------
// Copyright (c) 2024 eBiz Consulting GmbH
// Made w/ love by Mabrouk Mahdhi for all .NET developer days attendees
// ---------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace DeveloperDays.Berlin.Services
{
    public class ShoppingCartService
    {
        private readonly Dictionary<string, (double price, int stock)> inventory;
        private readonly List<(string itemId, int quantity)> cart;

        public ShoppingCartService()
        {
            inventory = new Dictionary<string, (double price, int stock)>
            {
                { "item1", (10.0, 5) },
                { "item2", (20.0, 3) },
                { "item3", (15.0, 0) } // out of stock item
            };
            cart = [];
        }

        public bool AddItemToCart(string itemId, int quantity)
        {
            if (!inventory.TryGetValue(itemId, out (double price, int stock) value) 
                || value.stock < quantity)
            {
                return false;
            }

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
            return cart.Sum(x => x.quantity * inventory[x.itemId].price);
        }

        public double ApplyDiscount(double discountPercentage)
        {
            var total = CalculateTotalPrice();
            return total - (total * (discountPercentage / 100));
        }

        public Dictionary<string, int> GetCartContents()
        {
            return cart.ToDictionary(x => x.itemId, x => x.quantity);
        }
    }
}
