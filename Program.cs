using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FTPFinal
{
    class Program
    {


        static void Main(string[] args)
        {
            // FTP DOSYA YOLLAMA
          

          
            string username = "pideB0641";
            string password = "ide30Ffg";
            FileInfo PureFileName = new FileInfo("imsreport.xml");
            String fileUrl = @"C:\Users\mirkankacan\Desktop\" + PureFileName;
            String uploadUrl = String.Format("{0}/{1}/{2}", "ftp://ftp.ideaktif.com.tr", "deneme", PureFileName.Name);
            FtpWebRequest request = null;
            FtpWebResponse response = null ;
            StreamReader sourceStream = null ;
            byte[] fileContents = null;
            Stream requestStream = null;
             


            //Find
            request = (FtpWebRequest)WebRequest.Create(uploadUrl);
            request.Credentials = new NetworkCredential(username, password);
            request.Method = WebRequestMethods.Ftp.GetFileSize;

            Console.WriteLine("İşlem başlatılıyor bekleyiniz...\n");
            try
            {
                response = (FtpWebResponse)request.GetResponse();
                //Delete
                request = (FtpWebRequest)WebRequest.Create(uploadUrl);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.Credentials = new NetworkCredential(username, password);
                response = (FtpWebResponse)request.GetResponse();
                Console.WriteLine("Dosya başarıyla silindi" + " - " + response.StatusDescription);
                if(response.StatusCode == FtpStatusCode.FileActionOK)
                {
                    Console.WriteLine("Dosya aktarılıyor programı kapatmayınız...\n");
                //Upload
                request = (FtpWebRequest)WebRequest.Create(uploadUrl);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                request.Credentials = new NetworkCredential(username, password);
                request.Proxy = null;
                request.KeepAlive = true;
                request.UseBinary = true;
                request.Method = WebRequestMethods.Ftp.UploadFile;
                
                 sourceStream = new StreamReader(fileUrl);
                fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                sourceStream.Close();
                request.ContentLength = fileContents.Length;
                 requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();
                response = (FtpWebResponse)request.GetResponse();
               
                    Console.WriteLine("Dosya başarıyla aktarıldı. (İşlemi sonlandırmak için Enter'a basınız)" + " - " + response.StatusDescription);
                    if (Console.ReadKey().Key == ConsoleKey.Enter)
                    {
                        Environment.Exit(0);

                    }
                }
                else
                {
                    Console.WriteLine("Hata. (İşlemi sonlandırmak için Enter'a basınız)" + " - " + response.StatusDescription );
                    response.Close();
                    if (Console.ReadKey().Key == ConsoleKey.Enter)
                    {
                    
                        Environment.Exit(0);
                    }
               
                }
            }
            catch (WebException ex)
            {
                 response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    //Does not exist

                    //Upload
                    request = (FtpWebRequest)WebRequest.Create(uploadUrl);
                    request.Method = WebRequestMethods.Ftp.UploadFile;

                    request.Credentials = new NetworkCredential(username, password);
                    request.Proxy = null;
                    request.KeepAlive = true;
                    request.UseBinary = true;
                    request.Method = WebRequestMethods.Ftp.UploadFile;

                    sourceStream = new StreamReader(fileUrl);
                    fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                    sourceStream.Close();
                    request.ContentLength = fileContents.Length;
                    requestStream = request.GetRequestStream();
                    requestStream.Write(fileContents, 0, fileContents.Length);
                    requestStream.Close();
                    response = (FtpWebResponse)request.GetResponse();

                    Console.WriteLine("Dosya başarıyla aktarıldı. (İşlemi sonlandırmak için Enter'a basınız)" + " - " + response.StatusDescription);
                    if (Console.ReadKey().Key == ConsoleKey.Enter)
                    {
                        Environment.Exit(0);

                    }
                }

           
            }
           
        }

      
    }
}


