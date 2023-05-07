using AutoMapper;
using BaseCode.Data.Contracts;
using BaseCode.Data.ViewModels.Common;
using BaseCode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseCode.Data.Models;
using BaseCode.Domain.Contracts;

namespace BaseCode.Domain.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly IApplicantRepository _applicantRepository;
        private readonly IMapper _mapper;

        public ApplicantService(IApplicantRepository applicantRepository, IMapper mapper)
        {
            _applicantRepository = applicantRepository;
            _mapper = mapper;
        }

        public ApplicantViewModel Find(int id)
        {
            ApplicantViewModel applicantViewModel = null;
            var applicant = _applicantRepository.Find(id);

            if (applicant != null)
            {
                applicantViewModel = _mapper.Map<ApplicantViewModel>(applicant);

            }

            return applicantViewModel;
        }

        public IQueryable<Applicant> RetrieveAll()
        {
            return _applicantRepository.RetrieveAll();
        }

        public ListViewModel FindApplicants(ApplicantSearchViewModel searchModel)
        {
            return _applicantRepository.FindApplicants(searchModel);
        }

        public void Create(Applicant applicant)
        {
            _applicantRepository.Create(applicant);
        }

        public void Update(Applicant applicant)
        {
            _applicantRepository.Update(applicant);
        }

        public void Delete(Applicant applicant)
        {
            _applicantRepository.Delete(applicant);
        }

        public void DeleteById(int id)
        {
            _applicantRepository.DeleteById(id);
        }

        public bool IsApplicantExists(string firstname, string lastname)
        {
            return _applicantRepository.IsApplicantExists(firstname, lastname);
        }
    }
}
