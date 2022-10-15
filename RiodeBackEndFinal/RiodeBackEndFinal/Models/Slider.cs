using Microsoft.Extensions.Diagnostics.HealthChecks;
using RiodeBackEndFinal.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiodeBackEndFinal.Models
{
    public class Slider:BaseEntity
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public bool IsLeftSide { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

    }
}
