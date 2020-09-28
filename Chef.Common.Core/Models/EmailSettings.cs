using Chef.Common.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chef.Common.Core
{
   public class EmailSettings : Model
    {
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Password { get; set; }
    }
}
