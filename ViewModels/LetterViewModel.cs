using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EmailApp.ViewModels
{
    public class LetterViewModel
    {
        [Required]
        [Display(Name = "Recipient")]
        [StringLength(25, ErrorMessage = "Field {0} must have at least {2} and maximum {1} characters.", MinimumLength = 1)]
        public string Recipient { get; set; }

        [Required]
        [Display(Name = "Theme")]
        [StringLength(100, ErrorMessage = "Field {0} must have at least {2} and maximum {1} characters.", MinimumLength = 1)]
        public string Theme { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; }
    }
}
