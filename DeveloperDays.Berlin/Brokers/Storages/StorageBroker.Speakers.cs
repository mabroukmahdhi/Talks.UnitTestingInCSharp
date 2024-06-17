// ---------------------------------------------------------------------
// Copyright (c) 2024 eBiz Consulting GmbH
// Made w/ love by Mabrouk Mahdhi for all .NET developer days attendees
// ---------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using DeveloperDays.Berlin.Models.Speakers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DeveloperDays.Berlin.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Speaker> Speakers { get; set; }

        public async ValueTask<Speaker> InsertSpeakerAsync(Speaker Speaker)
        {
            EntityEntry<Speaker> SpeakerEntityEntry =
                await Speakers.AddAsync(Speaker);

            await SaveChangesAsync();

            return SpeakerEntityEntry.Entity;
        }

        public IQueryable<Speaker> SelectAllSpeakers() => this.Speakers;

        public async ValueTask<Speaker> SelectSpeakerByIdAsync(Guid SpeakerId) =>
            await Speakers.FindAsync(SpeakerId);

        public async ValueTask<Speaker> UpdateSpeakerAsync(Speaker Speaker)
        {
            EntityEntry<Speaker> SpeakerEntityEntry =
                Speakers.Update(Speaker);

            await SaveChangesAsync();

            return SpeakerEntityEntry.Entity;
        }

        public async ValueTask<Speaker> DeleteSpeakerAsync(Speaker Speaker)
        {
            EntityEntry<Speaker> SpeakerEntityEntry =
                Speakers.Remove(Speaker);

            await SaveChangesAsync();

            return SpeakerEntityEntry.Entity;
        }
    }
}
