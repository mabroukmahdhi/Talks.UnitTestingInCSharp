// ---------------------------------------------------------------------
// Copyright (c) 2024 eBiz Consulting GmbH
// Made w/ love by Mabrouk Mahdhi for all .NET developer days attendees
// ---------------------------------------------------------------------

using System;
using Xeptions;

namespace DeveloperDays.Berlin.Models.Speakers.Exceptions
{
    public class FailedSpeakerServiceException : Xeption
    {
        public FailedSpeakerServiceException(Exception innerException)
            : base(message: "Failed Speaker service occurred, please contact support", innerException)
        { }
    }
}