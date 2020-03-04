using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.Web.Api
{
    [ModelMetadataType(typeof(AuthorInputMetadata))]
    public partial class AuthorInput
    {
    }
    public class AuthorInputMetadata
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
    }
}
