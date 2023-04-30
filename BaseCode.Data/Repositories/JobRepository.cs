using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BaseCode.Data.Repositories
{
    public class JobRepository : BaseRepository, IJobRepository
    {
        public JobRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Job Find(int id)
        {
            return GetDbSet<Job>().Find(id);
        }

        public IQueryable<Job> RetrieveAll()
        {
            return GetDbSet<Job>();
        }

        public ListViewModel FindJobs(JobSearchViewModel searchModel)
        {
            var sortKey = GetSortKey(searchModel.JobSortBy);
            var sortDir = ((!string.IsNullOrEmpty(searchModel.JobSortOrder) && searchModel.JobSortOrder.Equals("dsc"))) ?
                Constants.SortDirection.Descending : Constants.SortDirection.Ascending;

            // ========= Method Syntax =========
            var jobs = RetrieveAll()
                .Where(x => (string.IsNullOrEmpty(searchModel.jobId) || x.JobId.ToString().Contains(searchModel.jobId)) &&
                            (string.IsNullOrEmpty(searchModel.jobName) || x.JobName.Contains(searchModel.jobName)) &&
                            (string.IsNullOrEmpty(searchModel.jobShortDescription) || x.JobShortDescription.Contains(searchModel.jobShortDescription)) &&
                            (string.IsNullOrEmpty(searchModel.jobDescription) || x.JobDescription.Contains(searchModel.jobDescription)) &&
                            (string.IsNullOrEmpty(searchModel.jobResponsibilities) || x.JobResponsibilities.Contains(searchModel.jobResponsibilities)) &&
                            (string.IsNullOrEmpty(searchModel.jobQualifications) || x.JobQualifications.Contains(searchModel.jobQualifications)))
                .OrderByPropertyName(sortKey, sortDir);

            // ========= Query Syntax =========
            //var students = RetrieveAll().AsNoTracking();
            //var filteredStudents =
            //    from stud in students
            //    where (string.IsNullOrEmpty(searchModel.StudId) || stud.StudentId.ToString().Contains(searchModel.StudId)) &&
            //          (string.IsNullOrEmpty(searchModel.StudName) || stud.Name.Contains(searchModel.StudName)) &&
            //          (string.IsNullOrEmpty(searchModel.StudEmail) || stud.Email.Contains(searchModel.StudEmail)) &&
            //          (string.IsNullOrEmpty(searchModel.StudClass) || stud.Class.Contains(searchModel.StudClass)) &&
            //          (string.IsNullOrEmpty(searchModel.StudEnrollYear) || stud.EnrollYear.Contains(searchModel.StudEnrollYear)) &&
            //          (string.IsNullOrEmpty(searchModel.StudCity) || stud.City.Contains(searchModel.StudCity)) &&
            //          (string.IsNullOrEmpty(searchModel.StudCountry) || stud.Country.Contains(searchModel.StudCountry))
            //    orderby stud.StudentId ascending
            //    select stud;

            if (searchModel.JobPage == 0)
                searchModel.JobPage = 1;
            var totalCount = jobs.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / searchModel.JobPageSize);

            var results = jobs.Skip(searchModel.JobPageSize * (searchModel.JobPage - 1))
                .Take(searchModel.JobPageSize)
                .AsEnumerable()
                .Select(job => new
                {
                    job_id = job.JobId,
                    job_name = job.JobName,
                    job_short_description = job.JobShortDescription,
                    job_description = job.JobDescription,
                    job_responsibilities = job.JobResponsibilities,
                    job_qualifications = job.JobQualifications
                })
                .ToList();

            var pagination = new
            {
                pages = totalPages,
                size = totalCount
            };

            return new ListViewModel { Pagination = pagination, Data = results };
        }

        public void Create(Job job)
        {
            GetDbSet<Job>().Add(job);
            UnitOfWork.SaveChanges();
        }

        public void Update(Job job)
        {
            var jobUpdate = Find(job.JobId);
            jobUpdate.JobName = job.JobName;
            jobUpdate.JobShortDescription = job.JobShortDescription;
            jobUpdate.JobDescription = job.JobDescription;
            jobUpdate.JobResponsibilities = job.JobResponsibilities;
            jobUpdate.JobQualifications = job.JobQualifications;
            jobUpdate.CreatedBy = job.CreatedBy;
            jobUpdate.CreatedDate = job.CreatedDate;
            jobUpdate.ModifiedBy = job.ModifiedBy;
            jobUpdate.ModifiedDate = job.ModifiedDate;
            //this.SetEntityState(student, System.Data.Entity.EntityState.Modified);
            UnitOfWork.SaveChanges();            
        }        

        public void Delete(Job job)
        {
            GetDbSet<Job>().Remove(job);
            UnitOfWork.SaveChanges();
        }

        // Hard Deletion
        public void DeleteById(int id)
        {
            var job = Find(id);
            GetDbSet<Job>().Remove(job);
            UnitOfWork.SaveChanges();
        }

        public bool IsJobExists(string name)
        {            
            return GetDbSet<Job>().Any(x => x.JobName.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public string GetSortKey(string sortBy)
        {
            string sortKey;

            switch (sortBy)
            {
                case (Constants.Job.JobHeaderId):
                    sortKey = "JobID";
                    break;

                case (Constants.Job.JobHeaderName):
                    sortKey = "JobName";
                    break;

                case (Constants.Job.JobHeaderShortDescription):
                    sortKey = "JobShortDescription";
                    break;

                case (Constants.Job.JobHeaderDescription):
                    sortKey = "JobDescription";
                    break;

                case (Constants.Job.JobHeaderResponsibilities):
                    sortKey = "JobResponsibilities";
                    break;

                case (Constants.Job.JobHeaderQualifications):
                    sortKey = "JobQualitifications";
                    break;

                default:
                    sortKey = "JobID";
                    break;
            }

            return sortKey;
        }
    }
}
