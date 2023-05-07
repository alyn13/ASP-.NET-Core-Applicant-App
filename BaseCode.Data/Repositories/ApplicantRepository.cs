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
                            (string.IsNullOrEmpty(searchModel.ApplicantCVFileName) || x.CVFileName.Contains(searchModel.ApplicantCVFileName))&&
                            (string.IsNullOrEmpty(searchModel.ApplicantSkill) || x.Skill.ToString().Contains(searchModel.ApplicantSkill)) &&
                            //(string.IsNullOrEmpty(searchModel.ApplicantCollege) || x.College. .Contains(searchModel.ApplicantCollege)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicantHighSchool) || x.HighSchool.HighSchoolName.Contains(searchModel.ApplicantHighSchool)) &&
                           // (string.IsNullOrEmpty(searchModel.ApplicantExperience) || x.WorkExperience.ToString().Contains(searchModel.ApplicantExperience)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicantSubmissionDate.ToString()) || x.SubmissionDate.ToString().Contains(searchModel.ApplicantSubmissionDate.ToString())) && 
                            (string.IsNullOrEmpty(searchModel.ApplicantStatus) || x.Status.Contains(searchModel.ApplicantStatus)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicantPosition) || x.JobId.ToString().Contains(searchModel.ApplicantPosition)))
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
                    city = applicant.City,
                    country = applicant.Country,
                   
                    email = applicant.Email,
                    status = applicant.Status,
                    jobrole = applicant.JobId
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
            var studentUpdate = Find(applicant.ApplicantId);
            studentUpdate.FirstName = applicant.FirstName;
            studentUpdate.LastName = applicant.LastName;
           /* studentUpdate.City = applicant.City;
            studentUpdate.Class = applicant.Class;
            studentUpdate.Country = applicant.Country;
            studentUpdate.Email = applicant.Email;
            studentUpdate.EnrollYear = applicant.EnrollYear;
            studentUpdate.CreatedBy = applicant.CreatedBy;
            studentUpdate.CreatedDate = applicant.CreatedDate;
            studentUpdate.ModifiedBy = applicant.ModifiedBy;
            /studentUpdate.ModifiedDate = applicant.ModifiedDate;*/
            //this.SetEntityState(student, System.Data.Entity.EntityState.Modified);
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
