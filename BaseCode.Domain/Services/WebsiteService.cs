using AutoMapper;
using BaseCode.Data.Contracts;
using BaseCode.Data.ViewModels.Common;
using BaseCode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseCode.Data.Models;
using BaseCode.Data.Repositories;
using BaseCode.Domain.Contracts;

namespace BaseCode.Domain.Services
{
    public class WebsiteService : IWebsiteService
    {
        private readonly IWebsiteRepository _websiteRepository;
        private readonly IMapper _mapper;

        public WebsiteService(IWebsiteRepository websiteRepository, IMapper mapper)
        {
            _websiteRepository = websiteRepository;
            _mapper = mapper;
        }

        public WebsiteViewModel Find(int id)
        {
            WebsiteViewModel websiteViewModel = null;
            var website = _websiteRepository.Find(id);

            if (website != null)
            {
                websiteViewModel = _mapper.Map<WebsiteViewModel>(website);

            }

            return websiteViewModel;
        }

        public IQueryable<Website> RetrieveAll()
        {
            return _websiteRepository.RetrieveAll();
        }

        /*public ListViewModel FindWebsites(WebsiteSearchViewModel searchModel)
        {
            return _websiteRepository.FindWebsites(searchModel);
        }*/

        public void Create(Website website)
        {
            _websiteRepository.Create(website);
        }

        public void Update(Website website)
        {
            _websiteRepository.Update(website);
        }

        public void Delete(Website website)
        {
            _websiteRepository.Delete(website);
        }

        public void DeleteById(int id)
        {
            _websiteRepository.DeleteById(id);
        }

        public bool IsWebsiteExists(string websiteurl)
        {
            return _websiteRepository.IsWebsiteExists(websiteurl);
        }
    }
}
