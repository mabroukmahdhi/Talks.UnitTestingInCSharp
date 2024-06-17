// ---------------------------------------------------------------------
// Copyright (c) 2024 eBiz Consulting GmbH
// Made w/ love by Mabrouk Mahdhi for all .NET developer days attendees
// ---------------------------------------------------------------------

using Xeptions;

namespace DeveloperDays.Berlin.Models.Speakers.Exceptions
{
    public class InvalidSpeakerException : Xeption
    {
        public InvalidSpeakerException()
            : base(message: "Invalid Speaker. Please correct the errors and try again.")
        { }
    }
}