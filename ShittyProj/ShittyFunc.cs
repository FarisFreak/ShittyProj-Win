using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Principal;
using System.Net;
using System.Web.Script.Serialization;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Security;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Management;

namespace ShittyProj
{
    public static class InjectData
    {
        public static string JSonData = "http://localhost/w/x.php"; // Get user data
        public static string JSonGame = "http://localhost/w/cheatlist.php"; // Get game list
        public static string IPData = "http://ipv4bot.whatismyipaddress.com"; // Please dont change this
    }
    public static class PCInfo
    {
        public static string IP()
        {
            string res = new WebClient().DownloadString(InjectData.IPData);
            return res;
        }

        public static string MAC()
        {
            var macAddr = (
                from nic in NetworkInterface.GetAllNetworkInterfaces()
                where nic.OperationalStatus == OperationalStatus.Up
                select nic.GetPhysicalAddress().ToString()).FirstOrDefault();
            var regex = "(.{2})(.{2})(.{2})(.{2})(.{2})(.{2})";
            var replace = "$1:$2:$3:$4:$5:$6";
            var newformat = Regex.Replace(macAddr, regex, replace);
            return newformat;
        }
        public static string Name()
        {
            string res = Environment.MachineName;
            return res;
        }
        public static string HDSN()
        {
            string res = ShittyFunc.GetHDSN();
            return res;
        }
    }
    public static class Fnc
    {
        public static JavaScriptSerializer ser = new JavaScriptSerializer();
        public static string result { get; set; }
        public static bool IsUserAdministrator()
        {
            bool isAdmin;
            try
            {
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException ex)
            {
                isAdmin = false;
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                isAdmin = false;
                MessageBox.Show(ex.Message);
            }
            return isAdmin;
        }
        public static string PHP(string url, string method, string data)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Method = method;
                var postData = data;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
                return (responseFromServer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ("ERROR");
        }

    }
    public static class ShittyFunc
    {
        public static string username { get; set; }
        public static string password { get; set; }
        public static string res { get; set; }
        public static string res2 { get; set; }
        public static JavaScriptSerializer ser = new JavaScriptSerializer();
        public static string GetHDSN()
        {
            ManagementClass mangnmt = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection mcol = mangnmt.GetInstances();
            string result = "";

            foreach (ManagementObject strt in mcol)
            {
                if (Convert.ToString(strt["MediaType"]).ToUpper().Contains("FIXED"))
                {
                    result += Convert.ToString(strt["SerialNumber"]);
                }
            }
            result = result.Replace(Convert.ToString(Convert.ToChar(0x20)), string.Empty);
            return result;
        }
    }
    public class data
    {
        public int available;
        public string nama;
        public string username;
        public string saldo;
        public string point;
        public int expiry;
        public int aktif;
        public string level;

        public class Rootobject
        {
            public Datum[] data { get; set; }
        }

        public class Datum
        {
            public string game { get; set; }
            public string url { get; set; }
            public string path { get; set; }
        }

    }
}
