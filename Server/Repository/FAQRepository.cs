using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;
using JimS.FAQs.Models;

namespace JimS.FAQs.Repository
{
    public class FAQRepository : IFAQRepository, IService
    {
        private readonly FAQContext _db;

        public FAQRepository(FAQContext context)
        {
            _db = context;
        }

        public IEnumerable<FAQ> GetFAQs(int ModuleId)
        {
            return _db.FAQ.Where(item => item.ModuleId == ModuleId);
        }

        public FAQ GetFAQ(int FAQId)
        {
            return _db.FAQ.Find(FAQId);
        }

        public FAQ AddFAQ(FAQ FAQ)
        {
            _db.FAQ.Add(FAQ);
            _db.SaveChanges();
            return FAQ;
        }

        public FAQ UpdateFAQ(FAQ FAQ)
        {
            _db.Entry(FAQ).State = EntityState.Modified;
            _db.SaveChanges();
            return FAQ;
        }

        public void DeleteFAQ(int FAQId)
        {
            FAQ FAQ = _db.FAQ.Find(FAQId);
            _db.FAQ.Remove(FAQ);
            _db.SaveChanges();
        }
    }
}
