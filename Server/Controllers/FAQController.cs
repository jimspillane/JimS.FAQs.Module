using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Net;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using JimS.FAQs.Models;
using JimS.FAQs.Repository;

namespace JimS.FAQs.Controllers
{
    [Route("{site}/api/[controller]")]
    public class FAQController : Controller
    {
        private readonly IFAQRepository _FAQs;
        private readonly ILogManager _logger;

        public FAQController(IFAQRepository FAQs, ILogManager logger)
        {
            _FAQs = FAQs;
            _logger = logger;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = "ViewModule")]
        public IEnumerable<FAQ> Get(string moduleid)
        {
            return _FAQs.GetFAQs(int.Parse(moduleid));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Policy = "ViewModule")]
        public FAQ Get(int id)
        {
            return _FAQs.GetFAQ(id);
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Roles = Constants.AdminRole)]
        public FAQ Post([FromBody] FAQ FAQ)
        {
            if (!ModelState.IsValid)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return FAQ;
            }

            FAQ = _FAQs.AddFAQ(FAQ);
            _logger.Log(LogLevel.Information, this, LogFunction.Create, $"FAQ Added {FAQ}");
            return FAQ;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Roles = Constants.AdminRole)]
        public FAQ Put(int id, [FromBody] FAQ FAQ)
        {
            if (!ModelState.IsValid)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return FAQ;
            }

            FAQ = _FAQs.UpdateFAQ(FAQ);
            _logger.Log(LogLevel.Information, this, LogFunction.Update, $"FAQ Updated {FAQ}");
            return FAQ;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = Constants.AdminRole)]
        public void Delete(int id)
        {
            _FAQs.DeleteFAQ(id);
            _logger.Log(LogLevel.Information, this, LogFunction.Delete, $"FAQ Deleted {id}");
        }
    }
}
