// ---------------------------------------------------------------------
// Copyright (c) 2024 eBiz Consulting GmbH
// Made w/ love by Mabrouk Mahdhi for all .NET developer days attendees
// ---------------------------------------------------------------------

using System;
using Xeptions;

namespace DeveloperDays.Berlin.Models.Speakers.Exceptions
{
    public class LockedSpeakerException : Xeption
    {
        public LockedSpeakerException(Exception innerException)
            : base(message: "Locked Speaker record exception, please try again later", innerException)
        {
        }
    }
}