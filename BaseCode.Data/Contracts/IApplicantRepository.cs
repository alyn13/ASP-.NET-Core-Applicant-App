using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Data.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BaseCode.Data.Contracts
{
    public interface IApplicantRepository
    {
        Applicant Find(int app_id);
        IQueryable<Applicant> RetrieveAll();
        ListViewModel FindApplicants(ApplicantSearchViewModel searchModel);
        void Create(Applicant applicant);
        void Update(Applicant applicant);
        void Delete(Applicant applicant);
        void DeleteById(int id);
        bool IsApplicantExists(string firstname, string lastname);
        string GetSortKey(string sortBy);
    }
}
