using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogEngine.Data
{
    public class FingerPrintEntityBase : EntityBase
    {
        [Required]
        public string? CreatedBy { get; internal set; }
        [Required]
        public DateTime? CreatedOn { get; set; }
        [Required]
        public string? ModifiedBy { get; set; }
        [Required]
        public DateTime? ModifiedOn { get; set; }
    }
}
