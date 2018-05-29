using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.Erp.Lib;
using System.Net;

namespace Integration.Erp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceFtp sftp = new ServiceFtp( new NetworkCredential("AvisSite", "dm#o4%v#Wn43arWv2"),"ftp://new.avislogistics.kz:2021/GOODS_RECEIPT/");
            //sftp.DownLoadFile();

            GoodsReceipt g = sftp.Df("new_GOODS_RECEIPT_20180313-200655-249.xml");
        }
    }
}
