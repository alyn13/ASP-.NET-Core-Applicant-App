using BaseCode.Data.Models;
using BaseCode.Domain.Contracts;
using System;
using System.Collections.Generic;
using Constants = BaseCode.Data.Constants;
using System.Text;
using BaseCode.Domain.Services;
using static BaseCode.Data.Constants;
using Applicant = BaseCode.Data.Models.Applicant;

namespace BaseCode.Domain.Handlers
{
    public class ApplicantHandler
    {
        private readonly IApplicantService _applicantService;

        public ApplicantHandler(IApplicantService applicantService)
        {
            _applicantService = applicantService;
        }

        public IEnumerable<ValidationResult> CanAdd(Applicant applicant)
        {
            var validationErrors = new List<ValidationResult>();

            if (applicant != null)
            {
                if (_applicantService.IsApplicantExists(applicant.FirstName, applicant.LastName))
                {
                    validationErrors.Add(new ValidationResult(Constants.Applicant.ApplicantNameExists));
                }
                if (_applicantService.IsApplicantEmailExists(applicant.Email))
                {
                    validationErrors.Add(new ValidationResult(Constants.Applicant.ApplicantEmailExists));
                }
            }
            else
            {
                validationErrors.Add(new ValidationResult(Constants.Applicant.ApplicantNameExists));
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanUpdate(Applicant applicant)
        {
            var validationErrors = new List<ValidationResult>();

            if (applicant != null)
            {
                var dbApplicant = _applicantService.Find(applicant.ApplicantId);

                if (dbApplicant != null)
                {
                    if (!(dbApplicant.FirstName.Equals(applicant.FirstName) && dbApplicant.LastName.Equals(applicant.LastName)) && _applicantService.IsApplicantExists(applicant.FirstName, applicant.LastName))
                    {
                        validationErrors.Add(new ValidationResult(Constants.Applicant.ApplicantNameExists));
                    }
                    if (!dbApplicant.Email.Equals(applicant.Email) && _applicantService.IsApplicantExists(applicant.FirstName, applicant.LastName))
                    {
                        validationErrors.Add(new ValidationResult(Constants.Applicant.ApplicantNameExists));
                    }
                }
                else
                {
                    validationErrors.Add(new ValidationResult(Constants.Applicant.ApplicantNotExist));
                }
            }
            else
            {
                validationErrors.Add(new ValidationResult(Constants.Applicant.ApplicantEntryInvalid));
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanDelete(int id)
        {
            var validationErrors = new List<ValidationResult>();

            var applicant = _applicantService.Find(id);
            if (applicant == null)
            {
                validationErrors.Add(new ValidationResult(Constants.Applicant.ApplicantNotExist));
            }

            return validationErrors;
        }
    }
}
