using System.ComponentModel.DataAnnotations;

namespace RiodeBackEndFinal.ViewModels
{
    public class ResetPasswordVM
    {
        [Required,DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required, DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
