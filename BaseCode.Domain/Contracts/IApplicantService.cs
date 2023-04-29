using BaseCode.Data.ViewModels.Common;
using BaseCode.Data.ViewModels;
using BaseCode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseCode.Domain.Contracts
{
    public interface IApplicantService
    {
        ApplicantViewModel Find(int id);
        IQueryable<Applicant> RetrieveAll();
        ListViewModel FindApplicants(ApplicantSearchViewModel searchModel);
        void Create(Applicant applicant);
        void Update(Applicant applicant);
        void Delete(Applicant applicant);
        void DeleteById(int id);
        bool IsApplicantExists(string firstname, string lastname); //might change parameter to firstname and lastname
    }
}
