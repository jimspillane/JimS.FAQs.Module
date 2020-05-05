using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Repository;
using JimS.FAQs.Models;
using JimS.FAQs.Repository;

namespace JimS.FAQs.Manager
{
    public class FAQManager : IInstallable, IPortable
    {
        private readonly IFAQRepository _FAQs;
        private readonly ISqlRepository _sql;

        public FAQManager(IFAQRepository FAQs, ISqlRepository sql)
        {
            _FAQs = FAQs;
            _sql = sql;
        }

        public bool Install(Tenant tenant, string version)
        {
            return _sql.ExecuteScript(tenant, GetType().Assembly, $"JimS.FAQ.{version}.sql");
        }

        public bool Uninstall(Tenant tenant)
        {
            return _sql.ExecuteScript(tenant, GetType().Assembly, "JimS.FAQ.Uninstall.sql");
        }

        public string ExportModule(Module module)
        {
            var content = "";
            var FAQs = _FAQs.GetFAQs(module.ModuleId).ToList();
           
            if (FAQs != null)
            {
                content = JsonSerializer.Serialize(FAQs);
            }

            return content;
        }

        public void ImportModule(Module module, string content, string version)
        {
            List<FAQ> FAQs = null;
            if (!string.IsNullOrEmpty(content))
            {
                FAQs = JsonSerializer.Deserialize<List<FAQ>>(content);
            }

            if (FAQs == null) return;

            foreach (var FAQ in FAQs)
            {
                var _FAQ = new FAQ
                {
                    ModuleId = module.ModuleId,
                    Question = FAQ.Question,
                    Answer = FAQ.Answer,
                    Order = FAQ.Order
                };
                _FAQs.AddFAQ(_FAQ);
            }
        }
    }
}