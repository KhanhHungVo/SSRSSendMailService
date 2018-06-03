using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSRSSendMailService
{
    public enum SystemType
    {
        SystemWeb = 0,
        SystemWin = 1,
    }

    public enum LogFileType
    {
        TRACE = 0,
        MESSAGE = 1,
        EXCEPTION = 2,
    }
}
