// ---------------------------------------------------------------------
// Copyright (c) 2024 eBiz Consulting GmbH
// Made w/ love by Mabrouk Mahdhi for all .NET developer days attendees
// ---------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using DeveloperDays.Berlin.Models.Speakers;

namespace DeveloperDays.Berlin.Services.Foundations.Speakers
{
    public interface ISpeakerService
    {
        ValueTask<Speaker> AddSpeakerAsync(Speaker Speaker);
        IQueryable<Speaker> RetrieveAllSpeakers();
        ValueTask<Speaker> RetrieveSpeakerByIdAsync(Guid SpeakerId);
        ValueTask<Speaker> ModifySpeakerAsync(Speaker Speaker);
        ValueTask<Speaker> RemoveSpeakerByIdAsync(Guid SpeakerId);
    }
}