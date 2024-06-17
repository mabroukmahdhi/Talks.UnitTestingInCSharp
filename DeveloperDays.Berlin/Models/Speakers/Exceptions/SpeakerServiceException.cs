// ---------------------------------------------------------------------
// Copyright (c) 2024 eBiz Consulting GmbH
// Made w/ love by Mabrouk Mahdhi for all .NET developer days attendees
// ---------------------------------------------------------------------

using System;
using Xeptions;

namespace DeveloperDays.Berlin.Models.Speakers.Exceptions
{
    public class SpeakerServiceException : Xeption
    {
        public SpeakerServiceException(Exception innerException)
            : base(message: "Speaker service error occurred, contact support.", innerException)
        { }
    }
}