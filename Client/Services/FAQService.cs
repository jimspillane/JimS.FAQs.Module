using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Oqtane.Modules;
using Oqtane.Services;
using Oqtane.Shared;
using JimS.FAQs.Models;

namespace JimS.FAQs.Services
{
    public class FAQService : ServiceBase, IFAQService, IService
    {
        private readonly NavigationManager _navigationManager;
        private readonly SiteState _siteState;

        public FAQService(HttpClient http, SiteState siteState, NavigationManager navigationManager) : base(http)
        {
            _siteState = siteState;
            _navigationManager = navigationManager;
        }

        private string Apiurl
        {
            get { return CreateApiUrl(_siteState.Alias, _navigationManager.Uri, "FAQ"); }
        }

        public async Task<List<FAQ>> GetFAQsAsync(int ModuleId)
        {
            var FAQs = await GetJsonAsync<List<FAQ>>($"{Apiurl}?moduleid={ModuleId}&entityid={ModuleId}");
            return FAQs
                .OrderBy(item => item.Order)
                .ThenBy(item => item.FAQId)
                .ToList();
        }

        public async Task<FAQ> GetFAQAsync(int FAQId, int ModuleId)
        {
            return await GetJsonAsync<FAQ>($"{Apiurl}/{FAQId}?entityid={ModuleId}");
        }

        public async Task<FAQ> AddFAQAsync(FAQ FAQ)
        {

            var post = await PostJsonAsync<FAQ>($"{Apiurl}?entityid={FAQ.ModuleId}", FAQ);

            if (post==null)
            {
                throw new Exception("Error adding FAQ.");
            }
            return post;
        }

        public async Task<FAQ> UpdateFAQAsync(FAQ FAQ)
        {
            var put = await PutJsonAsync<FAQ>($"{Apiurl}/{FAQ.FAQId}?entityid={FAQ.ModuleId}", FAQ);

            if (put == null)
            {
                throw new Exception("Error saving FAQ.");
            }
            return put;
        }

        public async Task DeleteFAQAsync(int FAQId)
        {
            await DeleteAsync(Apiurl + "/" + FAQId.ToString());
        }
    }
}
