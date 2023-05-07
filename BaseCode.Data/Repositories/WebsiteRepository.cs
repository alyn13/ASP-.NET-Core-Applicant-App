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
    public class WebsiteRepository : BaseRepository, IWebsiteRepository
    {
        public WebsiteRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Website Find(int id)
        {
            return GetDbSet<Website>().Find(id);
        }

        public IQueryable<Website> RetrieveAll()
        {
            return GetDbSet<Website>();
        }

        /* public ListViewModel FindWebsites(WebsiteSearchViewModel searchModel)
         {
             var sortKey = GetSortKey(searchModel.SortBy);
             var sortDir = ((!string.IsNullOrEmpty(searchModel.SortOrder) && searchModel.SortOrder.Equals("dsc"))) ?
                 Constants.SortDirection.Descending : Constants.SortDirection.Ascending;

             var applicants = RetrieveAll()
                 .Where(x => (string.IsNullOrEmpty(searchModel.WebsiteId) || x.WebsiteId.ToString().Contains(searchModel.WebsiteId)) &&
                             (string.IsNullOrEmpty(searchModel.WebsiteUrl) || x.WebsiteUrl.Contains(searchModel.WebsiteUrl)) 
                             

                 .OrderByPropertyName(sortKey, sortDir);

             if (searchModel.Page == 0)
                 searchModel.Page = 1;
             var totalCount = applicants.Count();
             var totalPages = (int)Math.Ceiling((double)totalCount / searchModel.PageSize);

             var results = websites.Skip(searchModel.PageSize * (searchModel.Page - 1))
                 .Take(searchModel.PageSize)
                 .AsEnumerable()
                 .Select(website => new {
                     id = website.WebsiteId,
                     websiteurl = website.WebsiteUrl,
                 })
                 .ToList();

             var pagination = new
             {
                 pages = totalPages,
                 size = totalCount
             };

             return new ListViewModel { Pagination = pagination, Data = results };
         }
        */
        public void Create(Website website)
        {
            GetDbSet<Website>().Add(website);
            UnitOfWork.SaveChanges();
        }

        public void Update(Website website)
        {
            var websiteUpdate = Find(website.WebsiteId);
            websiteUpdate.WebsiteUrl = website.WebsiteUrl; 
           
            UnitOfWork.SaveChanges();
        }

        public void Delete(Website website)
        {
            GetDbSet<Website>().Remove(website);
            UnitOfWork.SaveChanges();
        }

        // Hard Deletion
        public void DeleteById(int id)
        {
            var website = Find(id);
            GetDbSet<Website>().Remove(website);
            UnitOfWork.SaveChanges();
        }

        public bool IsWebsiteExists(string websiteurl)
        {
            return GetDbSet<Website>().Any(x => x.WebsiteUrl.Equals(websiteurl, StringComparison.OrdinalIgnoreCase) );
        }

        public string GetSortKey(string sortBy)
        {
            string sortKey;

            switch (sortBy)
            {
                case (Constants.Website.WebsiteHeaderId):
                    sortKey = "WebsiteID";
                    break;

                case (Constants.Website.WebsiteHeaderUrl):
                    sortKey = "WebsiteUrl";
                    break;    
                
                default:
                    sortKey = "WebsiteID";
                    break;
            }

            return sortKey;
        }
    }
}
