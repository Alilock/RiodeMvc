using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace RiodeBackEndFinal.ViewModels
{
    public class ForgotPassVM
    {
        [Required]
        public string UserName { get; set; }
    }
}
