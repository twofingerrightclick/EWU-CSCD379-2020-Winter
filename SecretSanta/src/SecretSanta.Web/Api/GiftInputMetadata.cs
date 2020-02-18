using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Web.Api
{
    [ModelMetadataType(typeof(GiftInputMetadata))]
    public partial class GiftInput
    {
    }
    public class GiftInputMetadata
    {


        [Display(Name = "Title*")]
        public string? Title { get; set; }
        
        [Display(Name = "Description")]
        public string? Description { get; set; }
       
        [Display(Name = "Url to gift:")]
        public string? Url { get; set; }

        [Display(Name = "Associated User ID*")]
        public int? UserId { get; set; }


    }
}
