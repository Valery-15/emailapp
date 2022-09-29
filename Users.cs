using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EmailApp
{
    public partial class Users
    {
        public Users()
        {
            LettersRecipientNavigation = new HashSet<Letters>();
            LettersSenderNavigation = new HashSet<Letters>();
        }

        public string Username { get; set; }

        public virtual ICollection<Letters> LettersRecipientNavigation { get; set; }
        public virtual ICollection<Letters> LettersSenderNavigation { get; set; }
    }
}
