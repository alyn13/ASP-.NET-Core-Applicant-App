﻿using BaseCode.Data.Contracts;
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
                            (string.IsNullOrEmpty(searchModel.ApplicantCVFileLocation) || x.CVFileLocation.Contains(searchModel.ApplicantCVFileLocation)) && 
                           // (string.IsNullOrEmpty(searchModel.ApplicantSkill) || x.Skill.ToString().Contains(searchModel.ApplicantSkill)) &&
                            //(string.IsNullOrEmpty(searchModel.ApplicantCollege) || x.College. .Contains(searchModel.ApplicantCollege)) &&
                            //(string.IsNullOrEmpty(searchModel.ApplicantHighSchool) || x.HighSchool.HighSchoolName.Contains(searchModel.ApplicantHighSchool)) &&
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
            applicantUpdate.CVFileLocation = applicant.CVFileLocation;
            applicantUpdate.Website = applicant.Website ;
            applicantUpdate.Skill = applicant.Skill;
            applicantUpdate.College = applicant.College;
            applicantUpdate.HighSchool = applicant.HighSchool;
            applicantUpdate.WorkExperience = applicant.WorkExperience;
            applicantUpdate.SubmissionDate = applicant.SubmissionDate;
            applicantUpdate.Status = applicant.Status;
            applicantUpdate.Remarks = applicant.Remarks;
            applicantUpdate.JobId = applicant.JobId;

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
                    sortKey = "FirstName";
                    break;

                case (Constants.Applicant.ApplicantHeaderLastName):
                    sortKey = "LastName";
                    break;

                case (Constants.Applicant.ApplicantHeaderEmail):
                    sortKey = "Email";
                    break;                

                case (Constants.Applicant.ApplicantHeaderSubmissionDate):
                    sortKey = "SubmissionDate";
                    break;

                case (Constants.Applicant.ApplicantHeaderCity):
                    sortKey = "City";
                    break;

                case (Constants.Applicant.ApplicantHeaderProvince):
                    sortKey = "Province";
                    break;

                case (Constants.Applicant.ApplicantHeaderCountry):
                    sortKey = "Country";
                    break;

                default:
                    sortKey = "ApplicantID";
                    break;
            }

            return sortKey;
        }
    }
}
