using FluentFTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Code.Ftp
{
    public class FtpHelper
    {
        FtpClient _client;
        public FtpHelper()
        {
            _client = new FtpClient("192.168.99.159");
            _client.Credentials = new System.Net.NetworkCredential("user01", "G2aARV%On3H*ka2w");
            _client.Connect();
        }
        /// <summary>
        /// 上传ftp
        /// </summary>
        /// <param name="originalAddress"></param>
        /// <param name="destinationAddress"></param>
        /// <returns></returns>
        public bool UpLoadFile(string originalAddress, string destinationAddress)
        {
            if (_client.UploadFile(originalAddress, destinationAddress, FtpRemoteExists.Overwrite, true) == FtpStatus.Success)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 以流的方式 ftp
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="destinationAddress"></param>
        /// <returns></returns>
        public bool UpLoad(Stream fileStream, string destinationAddress)
        {
            if (_client.Upload(fileStream, destinationAddress, FtpRemoteExists.Overwrite, true) == FtpStatus.Success)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 下载ftp
        /// </summary>
        /// <param name="originalAddress"></param>
        /// <param name="destinationAddress"></param>
        /// <returns></returns>
        public bool DownloadFile(string originalAddress, string destinationAddress)
        {
            if (_client.DownloadFile(originalAddress, destinationAddress) == FtpStatus.Success)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 以流的方式下载
        /// </summary>
        /// <param name="outStream"></param>
        /// <param name="destinationAddress"></param>
        /// <returns></returns>

        public bool DownloadStream(Stream outStream, string destinationAddress)
        {
            if (_client.Download(outStream, destinationAddress) == true)
            {
                return true;
            }
            return false;
        }
    }
}
