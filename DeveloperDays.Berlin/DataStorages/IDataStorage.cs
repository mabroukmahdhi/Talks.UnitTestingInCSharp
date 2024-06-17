// ---------------------------------------------------------------------
// Copyright (c) 2024 eBiz Consulting GmbH
// Made w/ love by Mabrouk Mahdhi for all .NET developer days attendees
// ---------------------------------------------------------------------

using System.Collections.Generic;
using DeveloperDays.Berlin.Data;

namespace DeveloperDays.Berlin.DataStorages
{
    public interface IDataStorage
    {
        List<Item> GetInventory();
        List<CartItem> GetCart();
    }
}