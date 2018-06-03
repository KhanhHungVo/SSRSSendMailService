using SSRSSendMailService.Constants;
using SSRSSendMailService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSRSSendMailService.Implements
{
    class SSRSEmailService : ISSRSEmailService
    {
        public List<SSRSEmail> GetAllInactive()
        {
            using (var entity = new SSRSTransactionDevEntities())
            {
                return entity.SSRSEmails.Where(x => x.trang_thai == CONST.IS_NOT_SENT_EMAIL).ToList();
            }

        }

        public SSRSEmail UpdateAfterSend(SSRSEmail item)
        {
            using (var context = new SSRSTransactionDevEntities())
            {
                var model = context.SSRSEmails.FirstOrDefault(x => x.Id == item.Id);
                model.trang_thai = CONST.IS_SENT_EMAIL;
                model.thoi_gian_gui = DateTime.Now;
                context.SaveChanges();
                return item;
            }
        }
    }
}
