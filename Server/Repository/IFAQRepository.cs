using System.Collections.Generic;
using JimS.FAQs.Models;

namespace JimS.FAQs.Repository
{
    public interface IFAQRepository
    {
        IEnumerable<FAQ> GetFAQs(int ModuleId);
        FAQ GetFAQ(int FAQId);
        FAQ AddFAQ(FAQ FAQ);
        FAQ UpdateFAQ(FAQ FAQ);
        void DeleteFAQ(int FAQId);
    }
}
