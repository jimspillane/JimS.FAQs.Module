using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using JimS.FAQs.Models;

namespace JimS.FAQs.Repository
{
    public class FAQContext : DBContextBase, IService
    {
        public virtual DbSet<FAQ> FAQ { get; set; }

        public FAQContext(ITenantResolver tenantResolver, IHttpContextAccessor accessor) : base(tenantResolver, accessor)
        {
            // ContextBase handles multi-tenant database connections
        }
    }
}
