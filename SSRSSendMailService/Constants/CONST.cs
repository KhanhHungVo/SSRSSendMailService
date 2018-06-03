using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSRSSendMailService.Constants
{
    public class CONST
    {
        public static string product_owner = "TOPICA Edtech Group";

        public static string OK = "OK";

        public static string INQUIRY_MESSAGE_TYPE = "INQUIRY_MESSAGE_TYPE";
        public static string PENDING_MSG_Q = "PendingMessageQ";
        public static string RESPONSE_MSG_Q = "ResponseMessageQ";
        public static string INIT_PENDING_QUEUE_SIZE = "100000";
        public static string INIT_RESPONSE_QUEUE_SIZE = "100000";

        public static string ERROR_RETVAL = "99";
        public static string SUCCESS_CODE_RETURN = "00";
        public static string EMPTY_RETVAL = "01";


        public static string IS_SENT_EMAIL = "Y";
        public static string IS_NOT_SENT_EMAIL = "N";


        public static string ACTIVE_STATE = "A";
        public static string INACTIVE_STATE = "I";
        public static string PENDING_STATE = "P";

        public static string MMDDYYY = "MM/dd/yyyy";

        public static int ADDEDNEW_RECORD = 1;
        public static int UPDATED_RECORD = 3;
        public static int DELETED_RECORD = 5;



        public static string CURRENT_KY = "1";

        public static string DEV_ENVIRONMENT = "DEV";
        public static string PRODUCT_ENVIRONMENT = "DEV";

        public static int LOI_LOGIC = 1;
        public static int LOI_NHAP_SAI = 2;
        public static int LOI_CONVERT = 3;
        public static int LOI_KHONG_TIM_THAY_SERIAL = 4;
        public static int KHONG_LOI = 0;
        public static string TEN_DANG_NHAP = "testdev@topica.edu.vn";

        public static string CONG = "C";
        public static string TRU = "T";
        public static string SO_AM = "So_Am";
        public static string NOT_SHOW_AI = "NOT_SHOW_ON_TOT";
        public static string H1_2018 = "Q1.2018";

        public static string BU_LOAI_MA_CU = "MA_CU     ";
        public static string BU_LOAI_MA_MOI = "MA_MOI    ";
    }
}
