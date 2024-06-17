// ---------------------------------------------------------------------
// Copyright (c) 2024 eBiz Consulting GmbH
// Made w/ love by Mabrouk Mahdhi for all .NET developer days attendees
// ---------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using DeveloperDays.Berlin.Brokers.DateTimes;
using DeveloperDays.Berlin.Brokers.Loggings;
using DeveloperDays.Berlin.Brokers.Storages;
using DeveloperDays.Berlin.Models.Speakers;

namespace DeveloperDays.Berlin.Services.Foundations.Speakers
{
    public partial class SpeakerService : ISpeakerService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public SpeakerService(
            IStorageBroker storageBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Speaker> AddSpeakerAsync(Speaker Speaker) =>
            TryCatch(async () =>
            {
                ValidateSpeakerOnAdd(Speaker);

                return await this.storageBroker.InsertSpeakerAsync(Speaker);
            });

        public IQueryable<Speaker> RetrieveAllSpeakers() =>
            TryCatch(() => this.storageBroker.SelectAllSpeakers());

        public ValueTask<Speaker> RetrieveSpeakerByIdAsync(Guid SpeakerId) =>
            TryCatch(async () =>
            {
                ValidateSpeakerId(SpeakerId);

                Speaker maybeSpeaker = await this.storageBroker
                    .SelectSpeakerByIdAsync(SpeakerId);

                ValidateStorageSpeaker(maybeSpeaker, SpeakerId);

                return maybeSpeaker;
            });

        public ValueTask<Speaker> ModifySpeakerAsync(Speaker Speaker) =>
            TryCatch(async () =>
            {
                ValidateSpeakerOnModify(Speaker);

                Speaker maybeSpeaker =
                    await this.storageBroker.SelectSpeakerByIdAsync(Speaker.Id);

                ValidateStorageSpeaker(maybeSpeaker, Speaker.Id);
                ValidateAgainstStorageSpeakerOnModify(inputSpeaker: Speaker, storageSpeaker: maybeSpeaker);

                return await this.storageBroker.UpdateSpeakerAsync(Speaker);
            });

        public ValueTask<Speaker> RemoveSpeakerByIdAsync(Guid SpeakerId) =>
            TryCatch(async () =>
            {
                ValidateSpeakerId(SpeakerId);

                Speaker maybeSpeaker = await this.storageBroker
                    .SelectSpeakerByIdAsync(SpeakerId);

                ValidateStorageSpeaker(maybeSpeaker, SpeakerId);

                return await this.storageBroker.DeleteSpeakerAsync(maybeSpeaker);
            });
    }
}