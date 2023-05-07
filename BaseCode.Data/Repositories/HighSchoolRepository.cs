using BaseCode.Data.Contracts;
using BaseCode.Data.ViewModels.Common;
using BaseCode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseCode.Data.Models;

namespace BaseCode.Data.Repositories
{
    public class HighSchoolRepository : BaseRepository, IHighSchoolRepository
    {
        public HighSchoolRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public HighSchoolEducation Find(int id)
        {
            return GetDbSet<HighSchoolEducation>().Find(id);
        }

        public IQueryable<HighSchoolEducation> RetrieveAll()
        {
            return GetDbSet<HighSchoolEducation>();
        }

      /*  public ListViewModel FindApplicants(ApplicantSearchViewModel searchModel)
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
                            (string.IsNullOrEmpty(searchModel.ApplicantCVFileLocation) || x.CVFileLocation.Contains(searchModel.ApplicantCVFileLocation)) &&
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
        }*/

        public void Create(HighSchoolEducation highschool)
        {
            GetDbSet<HighSchoolEducation>().Add(highschool);
            UnitOfWork.SaveChanges();
        }

        public void Update(HighSchoolEducation highschool)
        {
            var highschoolUpdate = Find(highschool.HighSchoolEducId);
            highschoolUpdate.HighSchoolName = highschool.HighSchoolName;
            highschoolUpdate.YearStarted = highschool.YearStarted;
            highschoolUpdate.YearEnded = highschool.YearEnded;
           
            UnitOfWork.SaveChanges();
        }

        public void Delete(HighSchoolEducation highschool)
        {
            GetDbSet<HighSchoolEducation>().Remove(highschool);
            UnitOfWork.SaveChanges();
        }

        // Hard Deletion
        public void DeleteById(int id)
        {
            var applicant = Find(id);
            GetDbSet<HighSchoolEducation>().Remove(applicant);
            UnitOfWork.SaveChanges();
        }

        public bool IsHighSchoolExists(string highschool)
        {
            return GetDbSet<HighSchoolEducation>().Any(x => x.HighSchoolName.Equals(highschool, StringComparison.OrdinalIgnoreCase) );
        }

        public string GetSortKey(string sortBy)
        {
            string sortKey;

            switch (sortBy)
            {
                case (Constants.HighSchool.HighSchoolHeaderId):
                    sortKey = "HighSchoolID";
                    break;

                case (Constants.HighSchool.HighSchoolHeaderName):
                    sortKey = "HighSchoolName";
                    break;

                case (Constants.HighSchool.HighSchoolHeaderYearStarted):
                    sortKey = "HighSchoolYearStarted";
                    break;

                case (Constants.HighSchool.HighSchoolHeaderYearEnded):
                    sortKey = "HighSchoolYearEnded";
                    break;
               
                default:
                    sortKey = "HighSchoolID";
                    break;
            }

            return sortKey;
        }
    }
}
