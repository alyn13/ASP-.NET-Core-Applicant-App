using BaseCode.Data.Models;
using BaseCode.Domain.Contracts;
using System.Collections.Generic;
using Constants = BaseCode.Data.Constants;


namespace BaseCode.Domain.Handlers
{
    public class JobHandler
    {
        private readonly IJobService _jobService;

        public JobHandler(IJobService jobService)
        {
            _jobService = jobService;
        }

        public IEnumerable<ValidationResult> CanAdd(Job job)
        {
            var validationErrors = new List<ValidationResult>();

            if (job != null)
            {
                if (_jobService.IsJobExists(job.JobName))
                {
                    validationErrors.Add(new ValidationResult(Constants.Job.JobNameExists));
                }
            }
            else
            {
                validationErrors.Add(new ValidationResult(Constants.Job.JobEntryInvalid));
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanUpdate(Job job)
        {
            var validationErrors = new List<ValidationResult>();

            if (job != null)
            {
                var dbJob= _jobService.Find(job.JobId);

                if (dbJob != null)
                {
                    if (!dbJob.JobName.Equals(job.JobName) && _jobService.IsJobExists(job.JobName))
                    {
                        validationErrors.Add(new ValidationResult(Constants.Job.JobNameExists));
                    }
                }
                else
                {
                    validationErrors.Add(new ValidationResult(Constants.Job.JobNotExist));
                }
            }
            else
            {
                validationErrors.Add(new ValidationResult(Constants.Job.JobEntryInvalid));
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanDelete(int id)
        {
            var validationErrors = new List<ValidationResult>();

            var job = _jobService.Find(id);
            if (job == null)
            {
                validationErrors.Add(new ValidationResult(Constants.Job.JobNotExist));
            }

            return validationErrors;
        }
    }
}
