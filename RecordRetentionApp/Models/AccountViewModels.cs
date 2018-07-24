using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecordRetentionApp.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Employee Number")]
        public string empl_no { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string userFullName { get; set; }

        public bool isValid = false;

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
