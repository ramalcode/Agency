using System.ComponentModel.DataAnnotations;

namespace AgencyTemplate.ViewModels
{
    public class RegisterVM
    {
        [Required,MaxLength(25)]
        public string FullName { get; set; }
        [Required,MaxLength(25)]
        public string UserName { get; set; }
        [Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
