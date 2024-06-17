// ---------------------------------------------------------------------
// Copyright (c) 2024 eBiz Consulting GmbH
// Made w/ love by Mabrouk Mahdhi for all .NET developer days attendees
// ---------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using DeveloperDays.Berlin.Models.Speakers;
using DeveloperDays.Berlin.Models.Speakers.Exceptions;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Xeptions;

namespace DeveloperDays.Berlin.Services.Foundations.Speakers
{
    public partial class SpeakerService
    {
        private delegate ValueTask<Speaker> ReturningSpeakerFunction();
        private delegate IQueryable<Speaker> ReturningSpeakersFunction();

        private async ValueTask<Speaker> TryCatch(ReturningSpeakerFunction returningSpeakerFunction)
        {
            try
            {
                return await returningSpeakerFunction();
            }
            catch (NullSpeakerException nullSpeakerException)
            {
                throw CreateAndLogValidationException(nullSpeakerException);
            }
            catch (InvalidSpeakerException invalidSpeakerException)
            {
                throw CreateAndLogValidationException(invalidSpeakerException);
            }
            catch (SqlException sqlException)
            {
                var failedSpeakerStorageException =
                    new FailedSpeakerStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedSpeakerStorageException);
            }
            catch (NotFoundSpeakerException notFoundSpeakerException)
            {
                throw CreateAndLogValidationException(notFoundSpeakerException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsSpeakerException =
                    new AlreadyExistsSpeakerException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistsSpeakerException);
            }
            catch (ForeignKeyConstraintConflictException foreignKeyConstraintConflictException)
            {
                var invalidSpeakerReferenceException =
                    new InvalidSpeakerReferenceException(foreignKeyConstraintConflictException);

                throw CreateAndLogDependencyValidationException(invalidSpeakerReferenceException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedSpeakerException = new LockedSpeakerException(dbUpdateConcurrencyException);

                throw CreateAndLogDependencyValidationException(lockedSpeakerException);
            }
            catch (DbUpdateException databaseUpdateException)
            {
                var failedSpeakerStorageException =
                    new FailedSpeakerStorageException(databaseUpdateException);

                throw CreateAndLogDependencyException(failedSpeakerStorageException);
            }
            catch (Exception exception)
            {
                var failedSpeakerServiceException =
                    new FailedSpeakerServiceException(exception);

                throw CreateAndLogServiceException(failedSpeakerServiceException);
            }
        }

        private IQueryable<Speaker> TryCatch(ReturningSpeakersFunction returningSpeakersFunction)
        {
            try
            {
                return returningSpeakersFunction();
            }
            catch (SqlException sqlException)
            {
                var failedSpeakerStorageException =
                    new FailedSpeakerStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedSpeakerStorageException);
            }
            catch (Exception exception)
            {
                var failedSpeakerServiceException =
                    new FailedSpeakerServiceException(exception);

                throw CreateAndLogServiceException(failedSpeakerServiceException);
            }
        }

        private SpeakerValidationException CreateAndLogValidationException(Xeption exception)
        {
            var SpeakerValidationException =
                new SpeakerValidationException(exception);

            this.loggingBroker.LogError(SpeakerValidationException);

            return SpeakerValidationException;
        }

        private SpeakerDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var SpeakerDependencyException = new SpeakerDependencyException(exception);
            this.loggingBroker.LogCritical(SpeakerDependencyException);

            return SpeakerDependencyException;
        }

        private SpeakerDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var SpeakerDependencyValidationException =
                new SpeakerDependencyValidationException(exception);

            this.loggingBroker.LogError(SpeakerDependencyValidationException);

            return SpeakerDependencyValidationException;
        }

        private SpeakerDependencyException CreateAndLogDependencyException(
            Xeption exception)
        {
            var SpeakerDependencyException = new SpeakerDependencyException(exception);
            this.loggingBroker.LogError(SpeakerDependencyException);

            return SpeakerDependencyException;
        }

        private SpeakerServiceException CreateAndLogServiceException(
            Xeption exception)
        {
            var SpeakerServiceException = new SpeakerServiceException(exception);
            this.loggingBroker.LogError(SpeakerServiceException);

            return SpeakerServiceException;
        }
    }
}