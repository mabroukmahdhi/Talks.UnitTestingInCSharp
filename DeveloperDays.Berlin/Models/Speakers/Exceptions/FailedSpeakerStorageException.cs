// ---------------------------------------------------------------------
// Copyright (c) 2024 eBiz Consulting GmbH
// Made w/ love by Mabrouk Mahdhi for all .NET developer days attendees
// ---------------------------------------------------------------------

using System;
using Xeptions;

namespace DeveloperDays.Berlin.Models.Speakers.Exceptions
{
    public class FailedSpeakerStorageException : Xeption
    {
        public FailedSpeakerStorageException(Exception innerException)
            : base(message: "Failed Speaker storage error occurred, contact support.", innerException)
        { }
    }
}