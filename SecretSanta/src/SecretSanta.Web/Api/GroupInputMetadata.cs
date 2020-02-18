using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Web.Api
{
    [ModelMetadataType(typeof(GroupInputMetadata))]
    public partial class GroupInput
    {
    }
    public class GroupInputMetadata
    {
        [Display(Name = "Title")]
        public string? Title { get; set; }
    }
}
