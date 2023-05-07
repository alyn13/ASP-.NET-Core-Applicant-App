using BaseCode.Domain.Contracts;
using System;
using System.Collections.Generic;
using Constants = BaseCode.Data.Constants;
using System.Text;
using static BaseCode.Data.Constants;
using BaseCode.Data.Models;

namespace BaseCode.Domain.Handlers
{
    public class HighSchoolHandler
    {
        private readonly IHighSchoolService _highschoolService;

        public HighSchoolHandler(IHighSchoolService highschoolService)
        {
            _highschoolService = highschoolService;
        }

        public IEnumerable<ValidationResult> CanAdd(HighSchoolEducation highschool)
        {
            var validationErrors = new List<ValidationResult>();

            if (highschool != null)
            {
                if (_highschoolService.IsHighSchoolExists(highschool.HighSchoolName))
                {
                    validationErrors.Add(new ValidationResult(HighSchool.HighSchoolNameExists));
                }
            }
            else
            {
                validationErrors.Add(new ValidationResult(HighSchool.HighSchoolEntryInvalid));
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanUpdate(HighSchoolEducation highschool)
        {
            var validationErrors = new List<ValidationResult>();

            if (highschool != null)
            {
                var dbHighSchool = _highschoolService.Find(highschool.HighSchoolEducId);

                if (dbHighSchool != null)
                {
                    if (!dbHighSchool.HighSchoolName.Equals(highschool.HighSchoolName) && _highschoolService.IsHighSchoolExists(highschool.HighSchoolName))
                    {
                        validationErrors.Add(new ValidationResult(HighSchool.HighSchoolNameExists));
                    }
                }
                else
                {
                    validationErrors.Add(new ValidationResult(HighSchool.HighSchoolNotExist));
                }
            }
            else
            {
                validationErrors.Add(new ValidationResult(HighSchool.HighSchoolEntryInvalid));
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanDelete(int id)
        {
            var validationErrors = new List<ValidationResult>();

            var highschool = _highschoolService.Find(id);
            if (highschool == null)
            {
                validationErrors.Add(new ValidationResult(HighSchool.HighSchoolNotExist));
            }

            return validationErrors;
        }
    }
}
