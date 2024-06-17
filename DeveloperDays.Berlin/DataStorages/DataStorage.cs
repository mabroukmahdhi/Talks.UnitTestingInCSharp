// ---------------------------------------------------------------------
// Copyright (c) 2024 eBiz Consulting GmbH
// Made w/ love by Mabrouk Mahdhi for all .NET developer days attendees
// ---------------------------------------------------------------------

using System.Collections.Generic;

namespace DeveloperDays.Berlin.DataStorages
{
    public class DataStorage
    {
        private readonly Dictionary<string, (double price, int stock)> inventory;
        private readonly List<(string itemId, int quantity)> cart;

        public DataStorage()
        {
            inventory = new Dictionary<string, (double price, int stock)>
            {
                { "item1", (10.0, 5) },
                { "item2", (20.0, 3) },
                { "item3", (15.0, 0) } // out of stock item
            };
            cart = [];
        }


        public Dictionary<string, (double price, int stock)> GetInventory() => inventory;

        public List<(string itemId, int quantity)> GetCart() => cart;
    }
}
