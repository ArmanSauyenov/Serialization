using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Xml.Serialization;

namespace Integration.Erp.Lib
{
    public class ServiceFtp
    {
        public ServiceFtp(NetworkCredential credential, string path)
        {
            this.credential = credential;
            this.path = path;
        }

        private NetworkCredential credential;
        private string path;

        public void DownLoadFile()
        {

            FtpWebRequest Request = (FtpWebRequest)WebRequest.Create(path);
            Request.Method = WebRequestMethods.Ftp.ListDirectory;
            Request.Credentials = credential;
            Request.Proxy = null;

            var responce = (FtpWebResponse)Request.GetResponse();
            Stream stream = responce.GetResponseStream();

            List<string> listFiles = new List<string>();

            if (stream != null)
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line = reader.ReadLine();
                    while (!string.IsNullOrEmpty(line))
                    {
                        //Console.WriteLine(line);
                        listFiles.Add(line);
                        line = reader.ReadLine();
                    }
                }
                using (var ftpClient = new WebClient(){ Credentials = credential})
                {
                    foreach (string item in listFiles)
                    {
                        ftpClient.DownloadFile(path + "/" + item, "new_" + item);
                        GoodsReceipt g = Df("new_" + item);
                    }
                }
                    
            }
        }

        public GoodsReceipt Df(string pathToFile)
        {
            GoodsReceipt gr = null;
            XmlSerializer formatter = new XmlSerializer(typeof(GoodsReceipt));

            using (FileStream fs= new FileStream(pathToFile, FileMode.Open))
            {
               gr = (GoodsReceipt)formatter.Deserialize(fs);
            }
            return gr;
        }

    }
}
