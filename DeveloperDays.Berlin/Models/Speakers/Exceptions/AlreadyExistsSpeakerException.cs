// ---------------------------------------------------------------------
// Copyright (c) 2024 eBiz Consulting GmbH
// Made w/ love by Mabrouk Mahdhi for all .NET developer days attendees
// ---------------------------------------------------------------------

using System;
using Xeptions;

namespace DeveloperDays.Berlin.Models.Speakers.Exceptions
{
    public class AlreadyExistsSpeakerException : Xeption
    {
        public AlreadyExistsSpeakerException(Exception innerException)
            : base(message: "Speaker with the same Id already exists.", innerException)
        { }
    }
}