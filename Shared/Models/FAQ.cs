using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace JimS.FAQs.Models
{
    [Table("JimSFAQ")]
    public class FAQ : IAuditable
    {
        public int FAQId { get; set; }
        public int ModuleId { get; set; }

        [MaxLength(4000, ErrorMessage = "Question cannot be greater than 4000 characters")]
        public string Question { get; set; }

        [MaxLength(4000, ErrorMessage = "Answer cannot be greater than 4000 characters")]
        public string Answer { get; set; }

        [Required]
        [Range(1, 99, ErrorMessage = "Order must be between 1 and 99")]
        public int Order { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
