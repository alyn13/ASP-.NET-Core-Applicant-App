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
                .Where(x => (string.IsNullOrEmpty(searchModel.ApplicantId) || x.ApplicantId.ToString().Contains(searchModel.ApplicantId)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicantFName) || x.FirstName.Contains(searchModel.ApplicantFName)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicantLName) || x.LastName.Contains(searchModel.ApplicantLName)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicantStreet) || x.Street.Contains(searchModel.ApplicantStreet)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicantBarangay) || x.Barangay.Contains(searchModel.ApplicantBarangay)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicantCity) || x.City.Contains(searchModel.ApplicantCity)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicantProvince) || x.Province.Contains(searchModel.ApplicantProvince)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicantZipCode) || x.ZipCode.ToString().Contains(searchModel.ApplicantZipCode)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicantCountry) || x.Country.Contains(searchModel.ApplicantCountry)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicantEmail) || x.Email.Contains(searchModel.ApplicantEmail)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicantPhone) || x.Phone.Contains(searchModel.ApplicantPhone)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicantCVFileName) || x.CVFileName.Contains(searchModel.ApplicantCVFileName)) && //Watchout /might change to url
                           //(string.IsNullOrEmpty(searchModel.ApplicantCVFileLocation) || x.CVFileLocation.Contains(searchModel.ApplicantCVFileLocation)) && 
                            (string.IsNullOrEmpty(searchModel.ApplicantSkill) || x.Skill.ToString().Contains(searchModel.ApplicantSkill)) &&
                            //(string.IsNullOrEmpty(searchModel.ApplicantCollege) || x.College. .Contains(searchModel.ApplicantCollege)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicantHighSchool) || x.HighSchool.HighSchoolName.Contains(searchModel.ApplicantHighSchool)) &&
                           // (string.IsNullOrEmpty(searchModel.ApplicantExperience) || x.WorkExperience.ToString().Contains(searchModel.ApplicantExperience)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicantSubmissionDate.ToString()) || x.SubmissionDate.ToString().Contains(searchModel.ApplicantSubmissionDate.ToString())) && 
                            (string.IsNullOrEmpty(searchModel.ApplicantStatus) || x.Status.Contains(searchModel.ApplicantStatus)) )
                           // (string.IsNullOrEmpty(searchModel.ApplicantPosition) || x.JobId.ToString().Contains(searchModel.ApplicantPosition)))
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
                    street = applicant.Street,
                    barrangay = applicant.Barangay,
                    city = applicant.City,
                    province = applicant.Province,
                    zipcode = applicant.ZipCode,
                    country = applicant.Country,
                    email = applicant.Email,
                    phone = applicant.Phone,
                    cvName = applicant.CVFileName,
                    skill = applicant.Skill,
                    highSchool = applicant.HighSchool,
                    submitdate = applicant.SubmissionDate,
                    status = applicant.Status,
                    //status = applicant.Status,
                    //jobrole = applicant.JobId
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
            var websites = Context.Set<Website>();
            foreach (var website in applicant.Website)
            {
                if (!websites.Local.Any(w => w.WebsiteId == website.WebsiteId))
                {
                    applicantUpdate.Website.Add(website);
                }
            }
            //applicantUpdate.Website = applicant.Website;

            var skills = Context.Set<Skill>();
            foreach (var skill in applicant.Skill)
            {
                if (!skills.Local.Any(s => s.SkillId == skill.SkillId))
                {
                    applicantUpdate.Skill.Add(skill);
                }
            }
            //applicantUpdate.Skill = applicant.Skill;
            var collegeEducations = Context.Set<CollegeEducation>();
            foreach (var collegeEducation in applicant.College)
            {
                if (!collegeEducations.Local.Any(c => c.CollegeEducId == collegeEducation.CollegeEducId))
                {
                    applicantUpdate.College.Add(collegeEducation);
                }
            }
            //applicantUpdate.College = applicant.College;
            var highSchoolEducations = Context.Set<HighSchoolEducation>();
            if (applicant.HighSchool != null && !highSchoolEducations.Local.Any(h => h.HighSchoolEducId == applicant.HighSchool.HighSchoolEducId))
            {
                applicantUpdate.HighSchool = applicant.HighSchool;
            }
            //applicantUpdate.HighSchool = applicant.HighSchool;
            var experiences = Context.Set<Experience>();
            foreach (var experience in applicant.WorkExperience)
            {
                if (!experiences.Local.Any(e => e.ExperienceId == experience.ExperienceId))
                {
                    applicantUpdate.WorkExperience.Add(experience);
                }
            }
            //applicantUpdate.WorkExperience = applicant.WorkExperience;
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
        
        public string GetSortKey(string sortBy)
        {
            string sortKey;
            
            switch (sortBy)
            {
                case (Constants.Applicant.ApplicantHeaderId):
                    sortKey = "ApplicantID";
                    break;

                case (Constants.Applicant.ApplicantHeaderFirstName):
                    sortKey = "Name";
                    break;

                case (Constants.Applicant.ApplicantHeaderEmail):
                    sortKey = "Email";
                    break;
/*
                case (Constants.Student.StudentHeaderClass):
                    sortKey = "Class";
                    break;

                case (Constants.Student.StudentHeaderEnrollYear):
                    sortKey = "EnrollYear";
                    break;

                case (Constants.Student.StudentHeaderCity):
                    sortKey = "City";
                    break;

                case (Constants.Student.StudentHeaderCountry):
                    sortKey = "Country";
                    break;
*/
                default:
                    sortKey = "ApplicantID";
                    break;
            }

            return sortKey;
        }
    }
}
