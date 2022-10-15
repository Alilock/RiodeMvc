using RiodeBackEndFinal.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace RiodeBackEndFinal.Models
{
    public class Setting :BaseEntity
    {
        [Required]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
