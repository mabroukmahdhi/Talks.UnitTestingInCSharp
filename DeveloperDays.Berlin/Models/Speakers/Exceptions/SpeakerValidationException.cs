// ---------------------------------------------------------------------
// Copyright (c) 2024 eBiz Consulting GmbH
// Made w/ love by Mabrouk Mahdhi for all .NET developer days attendees
// ---------------------------------------------------------------------

using Xeptions;

namespace DeveloperDays.Berlin.Models.Speakers.Exceptions
{
    public class SpeakerValidationException : Xeption
    {
        public SpeakerValidationException(Xeption innerException)
            : base(message: "Speaker validation errors occurred, please try again.",
                  innerException)
        { }
    }
}