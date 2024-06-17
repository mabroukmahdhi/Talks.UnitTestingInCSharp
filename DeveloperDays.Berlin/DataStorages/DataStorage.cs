// ---------------------------------------------------------------------
// Copyright (c) 2024 eBiz Consulting GmbH
// Made w/ love by Mabrouk Mahdhi for all .NET developer days attendees
// ---------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using DeveloperDays.Berlin.Data;

namespace DeveloperDays.Berlin.DataStorages
{
    public class DataStorage: IDataStorage
    {
        private readonly List<Item> inventory;
        private readonly List<CartItem> cart;

        public DataStorage()
        {
            inventory =
            [
                new Item{ItemId= "item1", Price=10.0,Stock= 5 },
                new Item{ItemId= "item2", Price=20.0,Stock= 3 },
                new Item{ItemId= "item3", Price=15.0,Stock= 0 } // out of stock item
            ];
            cart = [];
        }


        public List<Item> GetInventory() => inventory;

        public List<CartItem> GetCart() => cart;
    }
}
