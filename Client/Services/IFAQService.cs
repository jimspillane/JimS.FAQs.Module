using System.Collections.Generic;
using System.Threading.Tasks;
using JimS.FAQs.Models;

namespace JimS.FAQs.Services
{
    public interface IFAQService 
    {
        Task<List<FAQ>> GetFAQsAsync(int ModuleId);

        Task<FAQ> GetFAQAsync(int FAQId, int ModuleId);

        Task<FAQ> AddFAQAsync(FAQ FAQ);

        Task<FAQ> UpdateFAQAsync(FAQ FAQ);

        Task DeleteFAQAsync(int FAQId);
    }
}
