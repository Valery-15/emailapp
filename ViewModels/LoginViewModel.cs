using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EmailApp.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(25, MinimumLength = 1)]
        public string UserName { get; set; }
    }
}
