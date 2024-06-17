// ---------------------------------------------------------------------
// Copyright (c) 2024 eBiz Consulting GmbH
// Made w/ love by Mabrouk Mahdhi for all .NET developer days attendees
// ---------------------------------------------------------------------

using System;
using DeveloperDays.Berlin.Models.Speakers;
using DeveloperDays.Berlin.Models.Speakers.Exceptions;

namespace DeveloperDays.Berlin.Services.Foundations.Speakers
{
    public partial class SpeakerService
    {
        private void ValidateSpeakerOnAdd(Speaker Speaker)
        {
            ValidateSpeakerIsNotNull(Speaker);

            Validate(
                (Rule: IsInvalid(Speaker.Id), Parameter: nameof(Speaker.Id)),

                // TODO: Add any other required validation rules

                (Rule: IsInvalid(Speaker.CreatedDate), Parameter: nameof(Speaker.CreatedDate)),
                (Rule: IsInvalid(Speaker.CreatedByUserId), Parameter: nameof(Speaker.CreatedByUserId)),
                (Rule: IsInvalid(Speaker.UpdatedDate), Parameter: nameof(Speaker.UpdatedDate)),
                (Rule: IsInvalid(Speaker.UpdatedByUserId), Parameter: nameof(Speaker.UpdatedByUserId)),

                (Rule: IsNotSame(
                    firstDate: Speaker.UpdatedDate,
                    secondDate: Speaker.CreatedDate,
                    secondDateName: nameof(Speaker.CreatedDate)),
                Parameter: nameof(Speaker.UpdatedDate)),

                (Rule: IsNotSame(
                    firstId: Speaker.UpdatedByUserId,
                    secondId: Speaker.CreatedByUserId,
                    secondIdName: nameof(Speaker.CreatedByUserId)),
                Parameter: nameof(Speaker.UpdatedByUserId)),

                (Rule: IsNotRecent(Speaker.CreatedDate), Parameter: nameof(Speaker.CreatedDate)));
        }

        private void ValidateSpeakerOnModify(Speaker Speaker)
        {
            ValidateSpeakerIsNotNull(Speaker);

            Validate(
                (Rule: IsInvalid(Speaker.Id), Parameter: nameof(Speaker.Id)),

                // TODO: Add any other required validation rules

                (Rule: IsInvalid(Speaker.CreatedDate), Parameter: nameof(Speaker.CreatedDate)),
                (Rule: IsInvalid(Speaker.CreatedByUserId), Parameter: nameof(Speaker.CreatedByUserId)),
                (Rule: IsInvalid(Speaker.UpdatedDate), Parameter: nameof(Speaker.UpdatedDate)),
                (Rule: IsInvalid(Speaker.UpdatedByUserId), Parameter: nameof(Speaker.UpdatedByUserId)),

                (Rule: IsSame(
                    firstDate: Speaker.UpdatedDate,
                    secondDate: Speaker.CreatedDate,
                    secondDateName: nameof(Speaker.CreatedDate)),
                Parameter: nameof(Speaker.UpdatedDate)),

                (Rule: IsNotRecent(Speaker.UpdatedDate), Parameter: nameof(Speaker.UpdatedDate)));
        }

        public void ValidateSpeakerId(Guid SpeakerId) =>
            Validate((Rule: IsInvalid(SpeakerId), Parameter: nameof(Speaker.Id)));

        private static void ValidateStorageSpeaker(Speaker maybeSpeaker, Guid SpeakerId)
        {
            if (maybeSpeaker is null)
            {
                throw new NotFoundSpeakerException(SpeakerId);
            }
        }

        private static void ValidateSpeakerIsNotNull(Speaker Speaker)
        {
            if (Speaker is null)
            {
                throw new NullSpeakerException();
            }
        }

        private static void ValidateAgainstStorageSpeakerOnModify(Speaker inputSpeaker, Speaker storageSpeaker)
        {
            Validate(
                (Rule: IsNotSame(
                    firstDate: inputSpeaker.CreatedDate,
                    secondDate: storageSpeaker.CreatedDate,
                    secondDateName: nameof(Speaker.CreatedDate)),
                Parameter: nameof(Speaker.CreatedDate)),

                (Rule: IsNotSame(
                    firstId: inputSpeaker.CreatedByUserId,
                    secondId: storageSpeaker.CreatedByUserId,
                    secondIdName: nameof(Speaker.CreatedByUserId)),
                Parameter: nameof(Speaker.CreatedByUserId)),

                (Rule: IsSame(
                    firstDate: inputSpeaker.UpdatedDate,
                    secondDate: storageSpeaker.UpdatedDate,
                    secondDateName: nameof(Speaker.UpdatedDate)),
                Parameter: nameof(Speaker.UpdatedDate)));
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static dynamic IsSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate == secondDate,
                Message = $"Date is the same as {secondDateName}"
            };

        private static dynamic IsNotSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate != secondDate,
                Message = $"Date is not the same as {secondDateName}"
            };

        private static dynamic IsNotSame(
            Guid firstId,
            Guid secondId,
            string secondIdName) => new
            {
                Condition = firstId != secondId,
                Message = $"Id is not the same as {secondIdName}"
            };

        private dynamic IsNotRecent(DateTimeOffset date) => new
        {
            Condition = IsDateNotRecent(date),
            Message = "Date is not recent"
        };

        private bool IsDateNotRecent(DateTimeOffset date)
        {
            DateTimeOffset currentDateTime =
                this.dateTimeBroker.GetCurrentDateTimeOffset();

            TimeSpan timeDifference = currentDateTime.Subtract(date);
            TimeSpan oneMinute = TimeSpan.FromMinutes(1);

            return timeDifference.Duration() > oneMinute;
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidSpeakerException = new InvalidSpeakerException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidSpeakerException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidSpeakerException.ThrowIfContainsErrors();
        }
    }
}