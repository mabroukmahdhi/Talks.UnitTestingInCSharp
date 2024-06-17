// ---------------------------------------------------------------------
// Copyright (c) 2024 eBiz Consulting GmbH
// Made w/ love by Mabrouk Mahdhi for all .NET developer days attendees
// ---------------------------------------------------------------------

using Xeptions;

namespace DeveloperDays.Berlin.Models.Speakers.Exceptions
{
    public class NullSpeakerException : Xeption
    {
        public NullSpeakerException()
            : base(message: "Speaker is null.")
        { }
    }
}