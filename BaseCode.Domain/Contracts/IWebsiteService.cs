using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseCode.Domain.Contracts
{
    public interface IWebsiteService
    {
        WebsiteViewModel Find(int web_id);
        IQueryable<Website> RetrieveAll();
        //ListViewModel FindWebsites(WebsiteSearchViewModel searchModel);
        void Create(Website website);
        void Update(Website website);
        void Delete(Website website);
        void DeleteById(int id);
        bool IsWebsiteExists(string websiteurl);
    }
}
