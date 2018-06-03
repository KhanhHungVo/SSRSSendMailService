using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SSRSSendMailService
{
    [Serializable]
    public class MailProcess
    {
        public string AddressMailServer { get; set; }
        public string EmailSend { get; set; }
        public string EmailUsername { get; set; }
        public string EmailPassword { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> PathFileAttach { get; set; }
        public List<string> ContentID { get; set; }
        public List<string> CCTo { get; set; }
        public List<string> AddressTo { get; set; }

        public MailProcess()
        {
            AddressMailServer = "123.30.60.226";
            Port = 25;
            EnableSsl = false;
            AddressTo = new List<string>();
            CCTo = new List<string>();
            ContentID = new List<string>();
            PathFileAttach = new List<string>();
        }

        public void Send()
        {
            var mail = new MailMessage
            {
                From = new MailAddress(EmailSend),
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true
            };
            foreach (var address in AddressTo)
            {
                mail.To.Add(address);
            }
            foreach (var address in CCTo)
            {
                mail.CC.Add(address);
            }
            mail.Subject = Subject;
            mail.Body = Body;

            if (PathFileAttach.Count > 0)
            {
                foreach (var pathFile in PathFileAttach)
                {
                    mail.Attachments.Add(new Attachment(pathFile));
                }
            }
            var smtpClient = new SmtpClient(AddressMailServer)
            {
                Port = Port,
                Credentials = new System.Net.NetworkCredential(EmailUsername, EmailPassword),
                EnableSsl = EnableSsl
            };
            smtpClient.Send(mail);
        }
        public void SendGuide()
        {
            var mail = new MailMessage
            {
                From = new MailAddress(EmailSend),
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true
            };
            foreach (var address in AddressTo)
            {
                mail.To.Add(address);
            }
            mail.Subject = Subject;
            mail.Body = Body;

            var view = AlternateView.CreateAlternateViewFromString(Body, null, "text/html");

            if (ContentID.Count > 0)
            {
                foreach (var contentId in ContentID)
                {
                    LinkedResource theEmailImage = new LinkedResource(contentId + ".png");
                    theEmailImage.ContentId = contentId;
                    view.LinkedResources.Add(theEmailImage);
                    mail.AlternateViews.Add(view);
                }
            }
            if (PathFileAttach.Count > 0)
            {
                foreach (var pathFile in PathFileAttach)
                {
                    mail.Attachments.Add(new Attachment(pathFile));
                }
            }
            var smtpClient = new SmtpClient(AddressMailServer)
            {
                Port = Port,
                Credentials = new System.Net.NetworkCredential(EmailUsername, EmailPassword),
                EnableSsl = EnableSsl
            };
            smtpClient.Send(mail);

        }



        public void Send(string subject, string body, string addressTo, string ccTo, params string[] pathFileAttach)
        {
            AddressTo = new List<string>();
            AddressTo.Add(addressTo);

            CCTo = new List<string>();
            if (!string.IsNullOrEmpty(ccTo)) CCTo.Add(ccTo);

            PathFileAttach = new List<string>();
            PathFileAttach.AddRange(pathFileAttach);

            Subject = subject;
            Body = body;
            Send();
        }

    }
}
