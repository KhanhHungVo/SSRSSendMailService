using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using SSRSSendMailService.Implements;

namespace SSRSSendMailService
{
    public partial class Service : ServiceBase
    {
        MailProcess mailServer;
        int SCAN_INTERNAL = 10;
        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
#if (DEBUG)
            System.Diagnostics.Debugger.Launch();
#endif
            InitConfiguration();
        }

        protected override void OnStop()
        {
            Logger.Logger.LogToFile(LogFileType.TRACE, "STOP.");
        }

        public void InitConfiguration()
        {
            try
            {
                string tmpInterval = ConfigurationManager.AppSettings["ScanInterval"];
                if (tmpInterval != null)
                {
                    int.TryParse(tmpInterval, out SCAN_INTERNAL);
                }

                mailServer = new MailProcess();
                string emailSender = ConfigurationManager.AppSettings["EmailSender"];
                string mailServerIp = ConfigurationManager.AppSettings["MailServerIp"];
                string emailUserName = ConfigurationManager.AppSettings["EmailUserName"];
                string emailPassword = ConfigurationManager.AppSettings["EmailPassword"];
                string mailServicePort = ConfigurationManager.AppSettings["MailServicePort"];
                string enableSSL = ConfigurationManager.AppSettings["EnableSSL"];

                if (emailSender != null)
                {
                    mailServer.EmailSend = emailSender;
                }

                if (mailServerIp != null)
                {
                    mailServer.AddressMailServer = mailServerIp;
                }

                if (emailUserName != null)
                {
                    mailServer.EmailUsername = emailUserName;
                }

                if (emailPassword != null)
                {
                    mailServer.EmailPassword = emailPassword;
                }

                if (mailServicePort != null)
                {
                    mailServer.Port = Convert.ToInt32(mailServicePort);
                }

                if (enableSSL != null)
                {
                    if (enableSSL.ToUpper() == "true".ToUpper())
                    {
                        mailServer.EnableSsl = true;
                    }
                    else
                    {
                        mailServer.EnableSsl = false;
                    }
                }

                Logger.Logger.LogToFile(LogFileType.TRACE, "START SUCCESS...");

                Task.Factory.StartNew(() => SendNotificationToCustomer());

            }
            catch (Exception ex)
            {
                Logger.Logger.LogExceptionToFile(ex);
            }
        }

        protected void SendNotificationToCustomer()
        {
            while (true)
            {
                try
                {
                    //Logger.Logger.LogToFile(LogFileType.TRACE, "Still sending....");

                    SSRSEmailService model = new SSRSEmailService();

                    List<SSRSEmail> listEmail = model.GetAllInactive();
                    foreach (var item in listEmail)
                    {
                        try
                        {
                            EmailAccount eAccount = new EmailAccount(mailServer);
                            eAccount.Send(item.tieu_de, item.noi_dung, item.nguoi_nhan, null);
                            Logger.Logger.LogToFile(LogFileType.TRACE, string.Format("to :{0} , content ; {1}, subject {2}", item.nguoi_nhan, item.noi_dung, item.noi_dung));
                            model.UpdateAfterSend(item);
                        }
                        catch (Exception ex)
                        {
                            Logger.Logger.LogExceptionToFile(ex);
                            continue;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Logger.LogToFile(LogFileType.EXCEPTION, ex.Message);
                }

                int tmpDelayTrans = Convert.ToInt32(1000 * 60 * SCAN_INTERNAL);
            }
        }
    }
    
}
