using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSRSSendMailService
{
    [Serializable]
    class EmailAccount
    {
        private MailProcess _mail;

        public MailProcess Mail
        {
            get { return _mail; }
        }

        public EmailAccount(MailProcess _mail1)
        {
            _mail = _mail1;
        }


        public void Send(string subject, string body, string addressTo, string ccTo, params string[] pathFileAttach)
        {
            Mail.Send(subject, body, addressTo, ccTo, pathFileAttach);
        }

    }
}
