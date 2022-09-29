using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EmailApp
{
    public partial class Letters
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public DateTime SendDateTime { get; set; }
        public string Theme { get; set; }
        public string Body { get; set; }

        public virtual Users RecipientNavigation { get; set; }
        public virtual Users SenderNavigation { get; set; }
    }
}
