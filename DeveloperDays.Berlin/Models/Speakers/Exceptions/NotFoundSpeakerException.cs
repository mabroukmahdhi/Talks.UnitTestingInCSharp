// ---------------------------------------------------------------------
// Copyright (c) 2024 eBiz Consulting GmbH
// Made w/ love by Mabrouk Mahdhi for all .NET developer days attendees
// ---------------------------------------------------------------------

using System;
using Xeptions;

namespace DeveloperDays.Berlin.Models.Speakers.Exceptions
{
    public class NotFoundSpeakerException : Xeption
    {
        public NotFoundSpeakerException(Guid SpeakerId)
            : base(message: $"Couldn't find Speaker with SpeakerId: {SpeakerId}.")
        { }
    }
}