using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMailer
{
    public class Options
    {
        public string Host;
        public int Port;
        public string User;
        public string Password;
        public string From;
        public List<string> To;
        public string Subject;
        public string Body;
        public List<string> Attachments;
    }
}
