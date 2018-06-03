using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSRSSendMailService.Interfaces
{
    interface ISSRSEmailService
    {
        List<SSRSEmail> GetAllInactive();
        SSRSEmail UpdateAfterSend(SSRSEmail item);
    }
}
