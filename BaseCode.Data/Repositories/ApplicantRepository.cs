using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Data.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseCode.Data.Repositories
{
    public class ApplicantRepository : BaseRepository, IApplicantRepository
    {
        public ApplicantRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Applicant Find(int id)
        {
            var applicant = GetDbSet<Applicant>()
                .Include(a => a.Website) // Include the Website collection
                .Include(a => a.Skill)
                .Include(a => a.College)
                .Include(a => a.HighSchool)
                .Include(a => a.WorkExperience)
                .FirstOrDefault(a => a.ApplicantId == id);

            return applicant;
            //return GetDbSet<Applicant>().Find(id);
        }

        public IQueryable<Applicant> RetrieveAll()
        {
            return GetDbSet<Applicant>();
        }
        
        public ListViewModel FindApplicants(ApplicantSearchViewModel searchModel)
        {
            var sortKey = GetSortKey(searchModel.SortBy);
            var sortDir = ((!string.IsNullOrEmpty(searchModel.SortOrder) && searchModel.SortOrder.Equals("dsc"))) ?
                Constants.SortDirection.Descending : Constants.SortDirection.Ascending;

            var applicants = RetrieveAll()
                /*.Where(x => (string.IsNullOrEmpty(searchModel.ApplicantId) || x.ApplicantId.ToString().Contains(searchModel.ApplicantId)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicantFName) || x.FirstName.Contains(searchModel.ApplicantFName)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicantLName) || x.LastName.Contains(searchModel.ApplicantLName)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicantSubmissionDate.ToString()) || x.SubmissionDate.ToString().Contains(searchModel.ApplicantSubmissionDate.ToString())) &&
                            (string.IsNullOrEmpty(searchModel.ApplicantPosition) || x.JobApplied.ToString().Contains(searchModel.ApplicantPosition)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicantStatus) || x.Status.Contains(searchModel.ApplicantStatus)) )*/
                            
                .OrderByPropertyName(sortKey, sortDir);

            if (searchModel.Page == 0)
                searchModel.Page = 1;
            var totalCount = applicants.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / searchModel.PageSize);

            var results = applicants.Skip(searchModel.PageSize * (searchModel.Page - 1))
                .Take(searchModel.PageSize)
                .AsEnumerable()
                .Select(applicant => new {
                    id = applicant.ApplicantId,
                    firstname = applicant.FirstName,
                    lastname = applicant.LastName,
                    submitdate = applicant.SubmissionDate,
                    jobrole = applicant.JobApplied,
                    status = applicant.Status  
                })
                .ToList();

            var pagination = new
            {
                pages = totalPages,
                size = totalCount
            };

            return new ListViewModel { Pagination = pagination, Data = results };
        }

        public void Create(Applicant applicant)
        {
            GetDbSet<Applicant>().Add(applicant);
            UnitOfWork.SaveChanges();
        }

        public void Update(Applicant applicant)
        {
            var applicantUpdate = Find(applicant.ApplicantId);
            applicantUpdate.FirstName = applicant.FirstName;
            applicantUpdate.LastName = applicant.LastName;
            applicantUpdate.Street = applicant.Street;
            applicantUpdate.Barangay = applicant.Barangay;
            applicantUpdate.City = applicant.City;
            applicantUpdate.Province = applicant.Province;
            applicantUpdate.ZipCode = applicant.ZipCode;
            applicantUpdate.Country = applicant.Country;

            applicantUpdate.Email = applicant.Email;
            applicantUpdate.Phone = applicant.Phone;
            applicantUpdate.CVFileName = applicant.CVFileName;
            
            // Update Website entities
            var websites = Context.Set<Website>();
            foreach (var website in applicant.Website)
            {
                if (!websites.Local.Any(w => w.WebsiteId == website.WebsiteId))
                {
                    // Attach the website entity to the Context
                    Context.Website.Attach(website);
                    // Set the state of the website entity to Modified
                    Context.Entry(website).State = EntityState.Added;
                    applicantUpdate.Website.Add(website);
                }
            }
            // Update Skill entities
            var skills = Context.Set<Skill>();
            foreach (var skill in applicant.Skill)
            {
                if (!skills.Local.Any(s => s.SkillId == skill.SkillId))
                {
                    // Attach the skill entity to the Context
                    Context.Skill.Attach(skill);
                    // Set the state of the skill entity to Modified
                    Context.Entry(skill).State = EntityState.Modified;
                    applicantUpdate.Skill.Add(skill);
                }
            }
            // Update CollegeEducation entities
            var collegeEducations = Context.Set<CollegeEducation>();
            foreach (var collegeEducation in applicant.College)
            {
                if (!collegeEducations.Local.Any(c => c.CollegeEducId == collegeEducation.CollegeEducId))
                {
                    // Attach the collegeEducation entity to the Context
                    Context.College.Attach(collegeEducation);
                    // Set the state of the collegeEducation entity to Modified
                    Context.Entry(collegeEducation).State = EntityState.Modified;
                    applicantUpdate.College.Add(collegeEducation);
                }
            }
            // Update HighSchoolEducation entity
            var highSchoolEducations = Context.Set<HighSchoolEducation>();
            if (applicant.HighSchool != null && !highSchoolEducations.Local.Any(h => h.HighSchoolEducId == applicant.HighSchool.HighSchoolEducId))
            {
                // Attach the highSchool entity to the Context
                Context.HighSchool.Attach(applicant.HighSchool);
                // Set the state of the highSchool entity to Modified
                Context.Entry(applicant.HighSchool).State = EntityState.Modified;
                applicantUpdate.HighSchool = applicant.HighSchool;
            }

            applicantUpdate.IsFirstJob = applicant.IsFirstJob;


            // Update Experience entities
            var experiences = Context.Set<Experience>();
            foreach (var experience in applicant.WorkExperience)
            {
                if (!experiences.Local.Any(e => e.ExperienceId == experience.ExperienceId))
                {
                    // Attach the experience entity to the Context
                    Context.Experience.Attach(experience);
                    // Set the state of the experience entity to Modified
                    Context.Entry(experience).State = EntityState.Modified;
                    applicantUpdate.WorkExperience.Add(experience);
                }
            }
            applicantUpdate.SubmissionDate = applicant.SubmissionDate;
            applicantUpdate.Status = applicant.Status;
            applicantUpdate.Remarks = applicant.Remarks;
            applicantUpdate.JobApplied = applicant.JobApplied;

            UnitOfWork.SaveChanges();
        }

        public void Delete(Applicant applicant)
        {
            GetDbSet<Applicant>().Remove(applicant);
            UnitOfWork.SaveChanges();
        }

        // Hard Deletion
        public void DeleteById(int id)
        {
            var applicant = Find(id);
            GetDbSet<Applicant>().Remove(applicant);
            UnitOfWork.SaveChanges();
        }

        public bool IsApplicantExists(string firstname, string lastname)
        {
            return GetDbSet<Applicant>().Any(x => x.FirstName.Equals(firstname, StringComparison.OrdinalIgnoreCase) && x.LastName.Equals(lastname, StringComparison.OrdinalIgnoreCase));
        }

        public bool IsApplicantEmailExists(string email)
        {
            return GetDbSet<Applicant>().Any(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public string GetSortKey(string sortBy)
        {
            string sortKey;
            
            switch (sortBy)
            {
                case (Constants.Applicant.ApplicantHeaderId):
                    sortKey = "ApplicantID";
                    break;

                case (Constants.Applicant.ApplicantHeaderFirstName):
                    sortKey = "FirstName";
                    break;

                case (Constants.Applicant.ApplicantHeaderLastName):
                    sortKey = "LastName";
                    break;

                /*case (Constants.Applicant.ApplicantHeaderEmail):
                    sortKey = "Email";
                    break;    */            

                case (Constants.Applicant.ApplicantHeaderSubmissionDate):
                    sortKey = "SubmissionDate";
                    break;

                /* case (Constants.Applicant.ApplicantHeaderCity):
                     sortKey = "City";
                     break;

                 case (Constants.Applicant.ApplicantHeaderProvince):
                     sortKey = "Province";
                     break;

                 case (Constants.Applicant.ApplicantHeaderCountry):
                     sortKey = "Country";
                     break;*/
                case (Constants.Applicant.ApplicantHeaderJobApplied):
                    sortKey = "JobApplied";
                    break;

                case (Constants.Applicant.ApplicantHeaderStatus):
                    sortKey = "ApplicantStatus";
                    break;

                default:
                    sortKey = "ApplicantID";
                    break;
            }

            return sortKey;
        }
    }
}
