// ---------------------------------------------------------------------
// Copyright (c) 2024 eBiz Consulting GmbH
// Made w/ love by Mabrouk Mahdhi for all .NET developer days attendees
// ---------------------------------------------------------------------

using System;
using Xeptions;

namespace DeveloperDays.Berlin.Models.Speakers.Exceptions
{
    public class InvalidSpeakerReferenceException : Xeption
    {
        public InvalidSpeakerReferenceException(Exception innerException)
            : base(message: "Invalid Speaker reference error occurred.", innerException) { }
    }
}