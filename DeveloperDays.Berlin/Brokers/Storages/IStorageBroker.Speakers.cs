// ---------------------------------------------------------------------
// Copyright (c) 2024 eBiz Consulting GmbH
// Made w/ love by Mabrouk Mahdhi for all .NET developer days attendees
// ---------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using DeveloperDays.Berlin.Models.Speakers;

namespace DeveloperDays.Berlin.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Speaker> InsertSpeakerAsync(Speaker Speaker);
        IQueryable<Speaker> SelectAllSpeakers();
        ValueTask<Speaker> SelectSpeakerByIdAsync(Guid SpeakerId);
        ValueTask<Speaker> UpdateSpeakerAsync(Speaker Speaker);
        ValueTask<Speaker> DeleteSpeakerAsync(Speaker Speaker);
    }
}
