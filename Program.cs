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
            // FTP
        
          
            string username = "";
            string password = "";
            string PureFileName = new FileInfo("example.xml").Name;
            String fileUrl = @"C:\Users\example\Desktop\" + PureFileName;
            String uploadUrl = String.Format("{0}/{1}/{2}", "ftp://ftp.example.com", "", "");
            FtpWebRequest request=null;
            FtpWebResponse response=null;
            StreamReader sourceStream=null;
            byte[] fileContents = null;

            //Find
            request = (FtpWebRequest)WebRequest.Create(uploadUrl);
            request.Credentials = new NetworkCredential(username, password);
            request.Method = WebRequestMethods.Ftp.GetFileSize;

            Console.WriteLine("İşlem başlıyor bekleyiniz..."); 

            try
            {
                response = (FtpWebResponse)request.GetResponse();
                //Delete
                request = (FtpWebRequest)WebRequest.Create(uploadUrl);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.Credentials = new NetworkCredential(username, password);
                response = (FtpWebResponse)request.GetResponse();
                Console.WriteLine("Dosya başarıyla silindi. (Devam etmek için Enter'a basınız)", response.StatusDescription);
                if(Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    Console.WriteLine("Bekleyiniz...");
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
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();
                response = (FtpWebResponse)request.GetResponse();
                Console.WriteLine("Dosya başarıyla aktarıldı. (İşlemi sonlandırmak için Enter'a basınız)", response.StatusDescription);
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
                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(fileContents, 0, fileContents.Length);
                    requestStream.Close();
                    response = (FtpWebResponse)request.GetResponse();
                    Console.WriteLine("Dosya başarıyla aktarıldı. (İşlemi sonlandırmak için Enter'a basınız)", response.StatusDescription);
                    if (Console.ReadKey().Key == ConsoleKey.Enter)
                    {
                    
                        Environment.Exit(0);    
                    }


                }
            }




        }
    }
}

