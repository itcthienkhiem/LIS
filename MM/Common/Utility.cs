/* Copyright (c) 2016, Cocosoft Inc.
 All rights reserved.
 http://www.Cocosofttech.com

 This file is part of the LIS open source project.

 The LIS  open source project is free software: you can
 redistribute it and/or modify it under the terms of the GNU General Public
 License as published by the Free Software Foundation, either version 3 of the
 License, or (at your option) any later version.

 The ClearCanvas LIS open source project is distributed in the hope that it
 will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
 Public License for more details.

 You should have received a copy of the GNU General Public License along with
 the LIS open source project.  If not, see
 <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Data;
using System.Reflection;
using System.Drawing;
using System.Diagnostics;
using Microsoft.SqlServer.Management.Smo;
using System.ServiceProcess;
using System.Threading;
using System.Runtime.InteropServices;
using SpreadsheetGear;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace MM.Common
{
    public class Utility
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr SetFocus(HandleRef hWnd);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);
        private const int SW_MAXIMIZE = 3;
        private const uint SW_RESTORE = 0x09;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsIconic(IntPtr hWnd);

        public const string Png = "PNG Portable Network Graphics (*.png)|" + "*.png";
        public const string Jpg = "JPEG File Interchange Format (*.jpg *.jpeg *jfif)|" + "*.jpg;*.jpeg;*.jfif";
        public const string Bmp = "BMP Windows Bitmap (*.bmp)|" + "*.bmp";
        public const string Tif = "TIF Tagged Imaged File Format (*.tif *.tiff)|" + "*.tif;*.tiff";
        public const string Gif = "GIF Graphics Interchange Format (*.gif)|" + "*.gif";
        public const string AllImages = "Image file|" + "*.png; *.jpg; *.jpeg; *.jfif; *.bmp;*.tif; *.tiff; *.gif";
        public const string AllFiles = "All files (*.*)" + "|*.*";
        public static List<string> imagesTypes;

        static Utility()
        {
            imagesTypes = new List<string>();
            imagesTypes.Add(Png);
            imagesTypes.Add(Jpg);
            imagesTypes.Add(Bmp);
            imagesTypes.Add(Tif);
            imagesTypes.Add(Gif);
        }

        #region WriteToTraceLog
        public static string fpTraceLog = "";
        public static string GenTraceLogFilename()
        {
            const long maxsize = 1024 * 1024;//1MB
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LOG")))
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LOG"));

            if (fpTraceLog == "")
                fpTraceLog = String.Format("{0}\\LOG\\TraceLog_{1}_{2}.txt", AppDomain.CurrentDomain.BaseDirectory, System.Diagnostics.Process.GetCurrentProcess().Id, DateTime.Now.ToString("yy_MM_dd_HH_mm_ss"));
            else if (File.Exists(fpTraceLog))
            {
                //check size
                FileInfo ofi = new FileInfo(fpTraceLog);
                if (ofi.Length > maxsize)
                {
                    //create new file name
                    fpTraceLog = String.Format("{0}\\LOG\\TraceLog_{1}_{2}.txt", AppDomain.CurrentDomain.BaseDirectory, System.Diagnostics.Process.GetCurrentProcess().Id, DateTime.Now.ToString("yy_MM_dd_HH_mm_ss"));
                }
                ofi = null;
            }

            if (!File.Exists(fpTraceLog))
            {
                FileStream fs = null;
                StreamWriter sw = null;
                Assembly executingAssembly = null;
                try
                {
                    fs = new FileStream(fpTraceLog, FileMode.Create, FileAccess.Write);
                    sw = new StreamWriter(fs);

                    executingAssembly = Assembly.GetExecutingAssembly();

                    //Add Version
                    string sversion = string.Format("==========================================================\r\n TraceLog Version on Date : {0}\r\n==========================================================\r\n", File.GetLastWriteTime(AppDomain.CurrentDomain.BaseDirectory).ToString("yyyy-MM-dd"));
                    sw.WriteLine(sversion);
                    sversion = null;
                }
                catch
                {
                }
                finally
                {
                    if (sw != null)
                        sw.Close();
                    if (fs != null)
                        fs.Close();

                    executingAssembly = null;
                }
            }

            return fpTraceLog;
        }

        public static void WriteToTraceLog(object my_obj, string Desc)
        {
            TextWriterTraceListener oListener = null;
            FileStream fs = null;
            try
            {
                fs = new FileStream(GenTraceLogFilename(), FileMode.Append, FileAccess.Write);
                oListener = new TextWriterTraceListener(fs);
                System.Diagnostics.Trace.Listeners.Clear();
                System.Diagnostics.Trace.Listeners.Add(oListener);

                Exception EX = null;
                if (my_obj != null)
                    EX = (Exception)my_obj;


                oListener.WriteLine(DateTime.Now.ToString() + "----------------------------------------------------");

                if (EX != null)
                {
                    oListener.WriteLine("(Message) : " + EX.Message);
                    if (EX.TargetSite != null)
                        oListener.WriteLine("(TargetSite) : " + EX.TargetSite.Name);
                    oListener.WriteLine("(StackTrace) : " + EX.StackTrace);
                    oListener.WriteLine("(ToString) : " + EX.ToString());
                }

                oListener.WriteLine("(Desc) : " + Desc);
                //System.Diagnostics.Trace.WriteLine("(Schema) : " + GlobalData.m_SchemaCtrl.m_strDocumentFileName);
                oListener.WriteLine(DateTime.Now.ToString() + "----------------------------------------------------");

                System.Diagnostics.Trace.Flush();
            }
            catch //( Exception ex )
            {
                //string a ="";
            }
            finally
            {
                System.Diagnostics.Trace.Listeners.Remove(oListener);
                if (oListener != null)
                    oListener.Close();
                if (fs != null)
                    fs.Close();
            }
        }

        public static void WriteToTraceLog(string Desc)
        {
            WriteToTraceLog(null, Desc);
        }

        #endregion

        public static bool IsImageExtension(string ext)
        {
            return imagesTypes.Contains(ext);
        }

        public static bool IsValidEmail(string email)
        {
            RegexUtilities regex = new RegexUtilities();
            return regex.IsValidEmail(email);
            //string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            //+ @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            //+ @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";//@"^[-a-zA-Z0-9][-.a-zA-Z0-9]*@[-.a-zA-Z0-9]+(\.[-.a-zA-Z0-9]+)*\.
            ////(com|edu|info|gov|int|mil|net|org|biz|name|museum|coop|aero|pro|tv|[a-zA-Z]{2})$";

            //Regex check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            //bool valid = false;

            //if (string.IsNullOrEmpty(email))
            //{
            //    valid = false;
            //}
            //else
            //{
            //    valid = check.IsMatch(email);
            //}

            //return valid;
        }

        public static bool IsValidPassword(string password)
        {
            string pattern = "(?!^[0-9][a-zA-Z]*$).{4,12}$";
            Regex check = new Regex(pattern);
            bool valid = false;

            if (string.IsNullOrEmpty(password))
            {
                valid = false;
            }
            else
            {
                valid = check.IsMatch(password);
            }

            return valid;
        }

        public static bool IsValidUsername(string username)
        {
            string pattern = "(?!^[0-9][a-zA-Z]*$).{2,12}$";
            Regex check = new Regex(pattern);
            bool valid = false;

            if (string.IsNullOrEmpty(username))
            {
                valid = false;
            }
            else
            {
                valid = check.IsMatch(username);
            }

            return valid;
        }

        public static void GetSurNameFirstNameFromFullName(string fullName, ref string surName, ref string firstName)
        {
            fullName = fullName.Trim();
            int index = fullName.LastIndexOf(" ");

            if (index >= 0)
            {
                surName = fullName.Substring(0, index);
                firstName = fullName.Substring(index + 1, fullName.Length - index - 1);
            }
            else
            {
                surName = string.Empty;
                firstName = fullName;
            }
        }

        public static int GetAge(string dobStr)
        {
            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "dd/MM/yyyy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "d/MM/yyyy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "d/M/yyyy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "dd/M/yyyy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "dd/MM/yy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "d/MM/yy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "d/M/yy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "dd/M/yy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "dd-MM-yyyy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "d-MM-yyyy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "d-M-yyyy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "dd-M-yyyy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "dd-MM-yy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "d-MM-yy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "d-M-yy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "dd-M-yy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                int year = int.Parse(dobStr);
                return DateTime.Now.Year - year;
            }
            catch
            {
            }

            return 0;
        }

        public static bool IsValidDOB(string dobStr)
        {
            try
            {
                DateTime.ParseExact(dobStr, "dd/MM/yyyy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "d/MM/yyyy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "d/M/yyyy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "dd/M/yyyy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "dd/MM/yy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "d/MM/yy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "d/M/yy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "dd/M/yy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "dd-MM-yyyy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "d-MM-yyyy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "d-M-yyyy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "dd-M-yyyy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "dd-MM-yy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "d-MM-yy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "d-M-yy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "dd-M-yy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                int year = int.Parse(dobStr);
                if (year > 0 && year < DateTime.Now.Year) return true;
            }
            catch
            {
            }
            
            return false;
        }

        public static bool IsValidDateTime(string dateTimeStr, ref string format)
        {
            List<string> dateFormats = new List<string>();
            dateFormats.Add("dd/MM/yyyy");
            dateFormats.Add("dd/MM/yy");
            dateFormats.Add("dd/M/yyyy");
            dateFormats.Add("dd/M/yy");
            dateFormats.Add("d/MM/yyyy");
            dateFormats.Add("d/MM/yy");
            dateFormats.Add("d/M/yy");
            dateFormats.Add("d/M/yyyy");

            List<string> timeFormats = new List<string>();
            timeFormats.Add("HH:mm:ss");
            timeFormats.Add("HH:mm:s");
            timeFormats.Add("HH:m:s");
            timeFormats.Add("HH:m:ss");
            timeFormats.Add("H:m:ss");
            timeFormats.Add("H:mm:ss");
            timeFormats.Add("H:mm:s");
            timeFormats.Add("H:m:s");

            foreach (var dateFormat in dateFormats)
            {
                foreach (var timeFormat in timeFormats)
                {
                    try
                    {
                        DateTime.ParseExact(dateTimeStr, string.Format("{0} {1}", dateFormat, timeFormat), null);
                        format = string.Format("{0} {1}", dateFormat, timeFormat);
                        return true;
                    }
                    catch
                    {
                    }
                }
            }

            return false;
        }

        public static List<string> GetSQLServerInstances()
        {
            List<string> instances = new List<string>();
            DataTable dtSQLServer = SmoApplication.EnumAvailableSqlServers(false);

            foreach (DataRow row in dtSQLServer.Rows)
            {
                string serverName = row[0].ToString().Trim();
                if (serverName == string.Empty) continue;
                instances.Add(serverName);
            }

            return instances;
        }

        public static string ConvertVNI2Unicode(string strInput)
        {
            string c = "";

            bool db = false;

            int[] maAcii = new int[134] { 7845, 7847, 7849, 7851, 7853, 226, 7843, 227, 7841, 7855, 7857, 7859, 7861, 7863, 259, 250, 249, 7911, 361, 7909, 7913, 7915, 7917, 7919, 7921, 432, 7871, 7873, 7875, 7877, 7879, 234, 233, 232, 7867, 7869, 7865, 7889, 7891, 7893, 7895, 7897, 7887, 245, 7885, 7899, 7901, 7903, 7905, 7907, 417, 237, 236, 7881, 297, 7883, 253, 7923, 7927, 7929, 7925, 273, 7844, 7846, 7848, 7850, 7852, 194, 7842, 195, 7840, 7854, 7856, 7858, 7860, 7862, 258, 218, 217, 7910, 360, 7908, 7912, 7914, 7916, 7918, 7920, 431, 7870, 7872, 7874, 7876, 7878, 202, 201, 200, 7866, 7868, 7864, 7888, 7890, 7892, 7894, 7896, 7886, 213, 7884, 7898, 7900, 7902, 7904, 7906, 416, 205, 204, 7880, 296, 7882, 221, 7922, 7926, 7928, 7924, 272, 225, 224, 244, 243, 242, 193, 192, 212, 211, 210 };
            string[] Vni = new string[134]{"aá", "aà", "aå", "aã", "aä", "aâ", "aû", "aõ", "aï", "aé", "aè",
                            "aú", "aü", "aë", "aê", "uù", "uø", "uû", "uõ", "uï", "öù", "öø", "öû", "öõ",
                            "öï", "ö", "eá", "eà", "eå", "eã", "eä", "eâ", "eù", "eø", "eû", "eõ", "eï",
                            "oá", "oà", "oå", "oã", "oä", "oû", "oõ", "oï", "ôù", "ôø",
                            "ôû", "ôõ", "ôï", "ô", "í", "ì", "æ", "ó", "ò", "yù", "yø", "yû", "yõ", "î",
                            "ñ", "AÁ", "AÀ", "AÅ", "AÃ", "AÄ", "AÂ", "AÛ", "AÕ",
                            "AÏ", "AÉ", "AÈ", "AÚ", "AÜ", "AË", "AÊ", "UÙ", "UØ", "UÛ", "UÕ",
                            "UÏ", "ÖÙ", "ÖØ", "ÖÛ", "ÖÕ", "ÖÏ", "Ö", "EÁ", "EÀ", "EÅ",
                            "EÃ", "EÄ", "EÂ", "EÙ", "EØ", "EÛ", "EÕ", "EÏ", "OÁ", "OÀ", "OÅ",
                            "OÃ", "OÄ", "OÛ", "OÕ", "OÏ", "ÔÙ", "ÔØ", "ÔÛ",
                            "ÔÕ", "ÔÏ", "Ô", "Í", "Ì", "Æ", "Ó", "Ò", "YÙ", "YØ", "YÛ", "YÕ",
                            "Î", "Ñ", "aù", "aø", "oâ", "où", "oø", "AÙ", "AØ", "OÂ", "OÙ", "OØ"};

            string result = strInput;
            for (int i = 0; i < 134; i++)
            {
                result = result.Replace(Vni[i], Convert.ToChar(maAcii[i]).ToString());
            }

            return result;
        }

        public static string GetCode(string prefix, int value, int lenght)
        {
            string strValue = value.ToString();
            if (strValue.Length > lenght)
                return string.Format("{0}{1}", prefix, value);

            string s = string.Empty;
            int index = lenght - 1;
            for (int i = strValue.Length - 1; i >= 0; i--)
            {
                s = strValue[i].ToString() + s;
                index--;                      
            }

            for (int i = 0; i <= index; i++)
                s = "0" + s;

            return string.Format("{0}{1}", prefix, s);
        }

        private static string docso(int i, int x, string n)
        {

            string s = "";
            switch (x)
            {
                case 0: if (i % 3 == 0 && (n.Substring(n.Length - i + 1, 2) != "00"))
                        s = "không ";
                    else s = "";
                    break;
                case 1:
                    if (i % 3 == 2)
                        s = "";
                    else
                    {
                        if ((n.Length >= i + 1 && (n[n.Length - i - 1] == '1' || n[n.Length - i - 1] == '0')) || n.Length == i || i % 3 == 0)
                        {
                            s = "một ";
                        }
                        else
                        {
                            s = "mốt ";
                        }
                    }
                    break;
                case 2:
                    s = "hai ";
                    break;
                case 3:
                    s = "ba ";
                    break;
                case 4:
                    s = "bốn ";
                    break;
                case 5:
                    if (n.Length != i && i % 3 == 1 && n.Substring(n.Length - i - 1, 1) != "0")
                        s = "lăm ";
                    else
                        s = "năm ";
                    break;
                case 6:
                    s = "sáu ";
                    break;
                case 7:
                    s = "bảy ";
                    break;
                case 8:
                    s = "tám ";
                    break;
                case 9:
                    s = "chín ";
                    break;
            }
            return s;
        }

        private static string hang(int i, int x, string n)
        {
            string s = "";
            int t = i % 3;
            switch (t)
            {
                case 0: if (n.Substring(n.Length - i, 3) != "000")
                        s = "trăm ";
                    else s = "";
                    break;
                case 1:
                    if (i % 9 == 1)
                    {
                        if (i - 1 == 0)
                            s = "";
                        else
                            s = "tỷ ";
                    }
                    else if (i % 6 == 1)
                        if (n.Length > 9 && n.Substring(n.Length - i - 2, 3) == "000")
                            s = "";
                        else
                            s = "triệu ";
                    else
                        if (n.Length > 6 && n.Substring(n.Length - i - 2, 3) == "000")
                            s = "";
                        else
                            s = "ngàn ";
                    break;
                case 2:
                    if (x == 0 && n.Substring(n.Length - i + 1, 1) != "0")
                        s = "linh ";
                    else
                        if (n.Substring(n.Length - i, 2) == "00")
                            s = "";
                        else
                        {
                            if (i % 3 == 2 && n[n.Length - i] == '1')
                                s = "mười ";
                            else
                                s = "mươi ";
                        }
                    break;
            }
            return s;
        }

        public static string ReadNumberAsString(long so)
        {
            int i;
            string s="";
            string n = so.ToString();
            int[] A = new int[n.Length + 1];
            for (i = n.Length; i > 0; i--)
            {
                A[i] = Int32.Parse(n.Substring(n.Length - i, 1));
                s += docso(i, A[i], n) + hang(i, A[i], n);
            }
            s = s + " đồng";
            //capital first letter
            if (s.Length > 0)
            {
                s = char.ToUpper(s[0]) + s.Substring(1);
            }

            s += ".";

            return s;
        }

        public static string ParseCoQuanEnumToName(CoQuan coQuan)
        {
            switch (coQuan)
            {
                case CoQuan.Mat:
                    return "Eyes (Mắt)";
                case CoQuan.TaiMuiHong:
                    return "Ear, Nose, Throat (Tai, mũi, họng)";
                case CoQuan.RangHamMat:
                    return "Odontology (Răng, hàm, mặt)";
                case CoQuan.HoHap:
                    return "Respiratory system (Hô hấp)";
                case CoQuan.TimMach:
                    return "Cardiovascular system (Tim mạch)";
                case CoQuan.TieuHoa:
                    return "Gastro - intestinal system (Tiêu hóa)";
                case CoQuan.TietNieuSinhDuc:
                    return "Genitourinary system (Tiết niệu, sinh dục)";
                case CoQuan.CoXuongKhop:
                    return "Musculoskeletal system (Cơ, xương, khớp)";
                case CoQuan.DaLieu:
                    return "Dermatology (Da liễu)";
                case CoQuan.ThanKinh:
                    return "Neurological system (Thần kinh)";
                case CoQuan.NoiTiet:
                    return "Endocrine system (Nội tiết)";
                case CoQuan.Khac:
                    return "Orthers (Các cơ quan khác)";
                case CoQuan.KhamPhuKhoa:
                    return "Gynecology (Khám phụ khoa)";
            }

            return string.Empty;
        }

        public static string ParseStaffTypeEnumToName(StaffType type)
        {
            switch (type)
            {
                case StaffType.BacSi:
                    return "Bác sĩ";
                case StaffType.BacSiSieuAm:
                    return "Bác sĩ siêu âm";
                case StaffType.BacSiNgoaiTongQuat:
                    return "Bác sĩ ngoại tổng quát";
                case StaffType.BacSiNoiTongQuat:
                    return "Bác sĩ nội tổng quát";
                case StaffType.DieuDuong:
                    return "Điều dưỡng";
                case StaffType.LeTan:
                    return "Lễ tân";
                case StaffType.BenhNhan:
                    return "Bệnh nhân";
                case StaffType.Admin:
                    return "Admin";
                case StaffType.XetNghiem:
                    return "Xét nghiệm";
                case StaffType.ThuKyYKhoa:
                    return "Thư ký y khoa";
                case StaffType.Sale:
                    return "Sale";
                case StaffType.KeToan:
                    return "Kế toán";
                case StaffType.BacSiPhuKhoa:
                    return "Bác sĩ phụ khoa";
                case StaffType.None:
                    return string.Empty;
            }

            return string.Empty;
        }

        public static string ParseLoaiNoiSoiEnumToName(LoaiNoiSoi type)
        {
            switch (type)
            {
                case LoaiNoiSoi.Tai:
                    return "Tai";
                case LoaiNoiSoi.Mui:
                    return "Mũi";
                case LoaiNoiSoi.Hong_ThanhQuan:
                    return "Họng - Thanh quản";
                case LoaiNoiSoi.TaiMuiHong:
                    return "Tai - Mũi - Họng";
                case LoaiNoiSoi.TongQuat:
                    return "Tổng quát";
                case LoaiNoiSoi.DaDay:
                    return "Dạ dày";
                case LoaiNoiSoi.TrucTrang:
                    return "Trực tràng";
            }

            return string.Empty;
        }

        public static int FixedPrice(int price)
        {
            int fixedPrice = 0;

            int div = price / 1000;
            int mod = price % 1000;

            if (mod == 0 || mod == 500) return price;

            if (mod < 500) fixedPrice = div * 1000 + 500;
            if (mod > 500) fixedPrice = (div + 1) * 1000;

            return fixedPrice;
        }

        public static string ParseHinhThucThanhToanToStr(PaymentType type)
        {
            if (type == PaymentType.TienMat) return "TM";
            else if (type == PaymentType.ChuyenKhoan) return "CK";
            else return "TM/CK";
        }

        public static void ResetMMSerivice()
        {
            try
            {
                string serviceName = "MMServices";
                int delaystop = 10;//second
                ServiceController sc = new ServiceController(serviceName);
                sc.Stop();//Stop target services
                Thread.Sleep(delaystop * 1000);
                sc.Start();
            }
            catch
            {
                
            }
        }

        public static void CreateFolder(string dir)
        {
            try
            {
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
            }
            catch
            {

            }
        }

        public static string GeneratePassword(int length)
        {
            string[] a = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};

            Random rnd = new Random();
            string password = string.Empty;
            for (int i = 0; i < length; i++)
            {
                int index = rnd.Next(36);
                password += a[index];
            }

            return password;
        }

        public static string GetMaCongTy(string maBenhNhan)
        {
            string maCongTy = string.Empty;

            for (int i = 0; i < maBenhNhan.Length; i++)
            {
                char c = maBenhNhan[i];
                if (c == '0' || c == '1' || c == '2' || c == '3' || c == '4' ||
                    c == '5' || c == '6' || c == '7' || c == '8' || c == '9')
                    break;

                maCongTy += c.ToString();
            }

            return maCongTy;
        }

        public static PageSetup GetPageSetup(string template)
        {
            IWorkbook workBook = null;
            string fileName = string.Empty;
            PageSetup p = null;

            try
            {
                switch (template)
                {
                    case "Theo dõi thực hiện":
                        fileName = string.Format("{0}\\Templates\\CheckListTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Chi tiết phiếu thu dịch vụ":
                        fileName = string.Format("{0}\\Templates\\ChiTietPhieuThuTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Chi tiết phiếu thu thuốc":
                        fileName = string.Format("{0}\\Templates\\ChiTietPhieuThuThuocTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Danh sách bệnh nhân đến khám":
                        fileName = string.Format("{0}\\Templates\\DanhSachBenhNhanDenKhamTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Danh sách bệnh nhân":
                        fileName = string.Format("{0}\\Templates\\DanhSachBenhNhanTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Dịch vụ hợp đồng":
                        fileName = string.Format("{0}\\Templates\\DichVuHopDongTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Dịch vụ tự túc":
                        fileName = string.Format("{0}\\Templates\\DichVuTuTucTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Doanh thu theo ngày":
                        fileName = string.Format("{0}\\Templates\\DoanhThuTheoNgayTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Giá vốn dịch vụ":
                        fileName = string.Format("{0}\\Templates\\GiaVonDichVuTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Hóa đơn giá trị gia tăng":
                        fileName = string.Format("{0}\\Templates\\HDGTGTTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Kết quả nội soi thanh quản":
                        fileName = string.Format("{0}\\Templates\\KetQuaNoiSoiHongThanhQuanTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Kết quả nội soi mũi":
                        fileName = string.Format("{0}\\Templates\\KetQuaNoiSoiMuiTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Kết quả nội soi tai mũi họng":
                        fileName = string.Format("{0}\\Templates\\KetQuaNoiSoiTaiMuiHongTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Kết quả nội soi tai":
                        fileName = string.Format("{0}\\Templates\\KetQuaNoiSoiTaiTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Kết quả nội soi tổng quát":
                        fileName = string.Format("{0}\\Templates\\KetQuaNoiSoiTongQuatTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Kết quả nội soi cổ tử cung":
                        fileName = string.Format("{0}\\Templates\\KetQuaSoiCTCTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Khám sức khỏe tổng quát":
                        fileName = string.Format("{0}\\Templates\\KhamSucKhoeTongQuatTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Nhật kí liên hệ công ty":
                        fileName = string.Format("{0}\\Templates\\NhatKyLienHeCongTyTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Phiếu thu thuốc":
                        fileName = string.Format("{0}\\Templates\\PhieuThuThuocTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Phiếu thu dịch vụ":
                        fileName = string.Format("{0}\\Templates\\ReceiptTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Triệu chứng":
                        fileName = string.Format("{0}\\Templates\\SymptomTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Thuốc tồn kho theo khoảng thời gian":
                        fileName = string.Format("{0}\\Templates\\ThuocTonKhoTheoKhoangThoiGianTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Toa thuốc":
                        fileName = string.Format("{0}\\Templates\\ToaThuocTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Toa thuốc chung":
                        fileName = string.Format("{0}\\Templates\\ToaThuocChungTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Toa thuốc sản khoa":
                        fileName = string.Format("{0}\\Templates\\ToaThuocSanKhoaTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Ý kiến khách hàng":
                        fileName = string.Format("{0}\\Templates\\YKienKhachHangTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Kết quả xét nghiệm CellDyn3200":
                        fileName = string.Format("{0}\\Templates\\KetQuaXetNghiemCellDyn3200Template.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Kết quả xét nghiệm sinh hóa":
                        fileName = string.Format("{0}\\Templates\\KetQuaXetNghiemSinhHoaTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Danh sách dịch vụ xuất phiếu thu":
                        fileName = string.Format("{0}\\Templates\\DanhSachDichVuXuatPhieuThuTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Danh sách dịch vụ":
                        if (Global.AllowShowServiePrice)
                            fileName = string.Format("{0}\\Templates\\DanhSachDichVuCoGiaTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        else
                            fileName = string.Format("{0}\\Templates\\DanhSachDichVuKhongGiaTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Danh sách nhân viên":
                        fileName = string.Format("{0}\\Templates\\DanhSachNhanVienTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Danh sách thuốc":
                        fileName = string.Format("{0}\\Templates\\DanhSachThuocTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Công tác ngoài giờ":
                        fileName = string.Format("{0}\\Templates\\CongTacNgoaiGioTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Lịch khám":
                        fileName = string.Format("{0}\\Templates\\LichKhamTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Bệnh nhân ngoài gói khám":
                        fileName = string.Format("{0}\\Templates\\BenhNhanNgoaiGoiKhamTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Phiếu thu cấp cứu":
                        fileName = string.Format("{0}\\Templates\\PhieuThuCapCuuTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Chi tiết phiếu thu cấp cứu":
                        fileName = string.Format("{0}\\Templates\\ChiTietPhieuThuCapCuuTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Báo cáo công nợ hợp đồng tổng hợp":
                        fileName = string.Format("{0}\\Templates\\BaoCaoCongNoHopDongTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Báo cáo công nợ hợp đồng chi tiết":
                        fileName = string.Format("{0}\\Templates\\BaoCaoCongNoHopDongChiTietTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Chỉ định":
                        fileName = string.Format("{0}\\Templates\\ChiDinhTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Dịch vụ xét nghiệm":
                        fileName = string.Format("{0}\\Templates\\DichVuXetNghiemTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Thống kế thuốc xuất hóa đơn":
                        fileName = string.Format("{0}\\Templates\\ThongKeThuocXuatHoaDonTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Doanh thu theo nhóm dịch vụ":
                        fileName = string.Format("{0}\\Templates\\DoanhThuTheoNhomDichVuTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Giá thuốc":
                        fileName = string.Format("{0}\\Templates\\GiaThuocTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Lô thuốc":
                        fileName = string.Format("{0}\\Templates\\LoThuocTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;
                }

                if (fileName != string.Empty && File.Exists(fileName))
                {
                    workBook = SpreadsheetGear.Factory.GetWorkbook(fileName);
                    IWorksheet workSheet = workBook.Worksheets[0];

                    p = new PageSetup();
                    p.LeftMargin = workSheet.PageSetup.LeftMargin / 72;
                    p.RightMargin = workSheet.PageSetup.RightMargin / 72;
                    p.TopMargin = workSheet.PageSetup.TopMargin / 72;
                    p.BottomMargin = workSheet.PageSetup.BottomMargin / 72;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return p;
        }

        public static string GetLoaiXetNghiem(string type)
        {
            switch (type)
            {
                case "Urine":
                    return "Nước tiểu";
                case "MienDich":
                    return "Miễn dịch";
                case "Khac":
                    return "Khác";
                case "Haematology":
                    return "Huyết học";
                case "Biochemistry":
                    return "Sinh hóa";
            }

            return string.Empty;
        }

        public static Image ParseImage(byte[] buffer, int width, int height)
        {
            Bitmap bmp = null;
            MemoryStream ms = null;

            try
            {
                ms = new MemoryStream(buffer);
                bmp = new Bitmap(ms);
                bmp = new Bitmap(bmp, new Size(width, height));

                return bmp;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (ms != null)
                {
                    ms.Close();
                    ms = null;
                }
            }
        }

        public static Image ParseImage(byte[] buffer)
        {
            Bitmap bmp = null;
            MemoryStream ms = null;

            try
            {
                ms = new MemoryStream(buffer);
                bmp = new Bitmap(ms);
                
                return bmp;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (ms != null)
                {
                    ms.Close();
                    ms = null;
                }
            }
        }

        public static void SaveImage(byte[] buffer, string fileName)
        {
            Bitmap bmp = null;
            MemoryStream ms = null;

            try
            {
                ms = new MemoryStream(buffer);
                bmp = new Bitmap(ms);
                bmp.Save(fileName, ImageFormat.Png);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (ms != null)
                {
                    ms.Close();
                    ms = null;
                }

                if (bmp != null)
                {
                    bmp.Dispose();
                    bmp = null;
                }
            }
        }

        public static byte[] GetBinaryFromImage(Image img)
        {
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.GetBuffer();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (ms != null)
                {
                    ms.Close();
                    ms = null;
                }
            }
        }

        public static void RunPlayCapProcess(bool isShowCapture)
        {
            string path = string.Format("{0}\\PlayCap.exe", AppDomain.CurrentDomain.BaseDirectory);
            Process.Start(path, isShowCapture.ToString());
        }

        public static void KillPlayCapProcess()
        {
            try
            {
                Process[] processList = Process.GetProcessesByName("PlayCap");
                if (processList != null && processList.Length > 0)
                {
                    foreach (Process p in processList)
                    {
                        p.Kill();
                    }
                }
            }
            catch
            {
                
            }
        }

        public static bool CheckPlayCapProcessExist()
        {
            try
            {
                Process[] processList = Process.GetProcessesByName("PlayCap");
                if (processList != null && processList.Length > 0)
                    return true;
            }
            catch
            {
                
            }

            return false;
        }

        public static string GetDayOfWeek(DateTime dt)
        {
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    return "Thứ 6";
                case DayOfWeek.Monday:
                    return "Thứ 2";
                case DayOfWeek.Saturday:
                    return "Thứ 7";
                case DayOfWeek.Sunday:
                    return "CN";
                case DayOfWeek.Thursday:
                    return "Thứ 5";
                case DayOfWeek.Tuesday:
                    return "Thứ 3";
                case DayOfWeek.Wednesday:
                    return "Thứ 4";
            }

            return string.Empty;
        }

        public static byte[] GetBytesFromFile(string fileName)
        {
            FileStream fs = null;
            try
            {
                fs = File.OpenRead(fileName);
                int count = (int)fs.Length;
                byte[] buff = new byte[count];
                fs.Read(buff, 0, count);
                return buff;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }
            }
        }

        public static void SaveFileFromBytes(string fileName, byte[] buff)
        {
            try
            {
                File.WriteAllBytes(fileName, buff);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void ExecuteFile(string fileName)
        {
            try
            {
                Process.Start(fileName);
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        public static bool CheckRunningProcess(string processName)
        {
            try
            {
                Process[] processList = Process.GetProcessesByName(processName);
                if (processList == null || processList.Length <= 0)
                    return false;
                else
                {
                    IntPtr hwndMain = processList[0].MainWindowHandle;
                    SetForegroundWindow(hwndMain);

                    if (IsIconic(hwndMain))
                        ShowWindow(hwndMain, SW_RESTORE);
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void XoaThongBaoTemp()
        {
            string path = string.Format("{0}\\Temp", AppDomain.CurrentDomain.BaseDirectory);
            if (!Directory.Exists(path)) return;
            string[] fileNames = Directory.GetFiles(path);
            foreach (string fn in fileNames)
            {
                if (fn.IndexOf("ThongBao") < 0) continue;
                try
                {
                    File.Delete(fn);
                }
                catch
                {
                }
            }
            
        }

        public static string ToStringFormat(double value)
        {
            string strValue = string.Empty;
            string s = value.ToString();
            int count = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                strValue = s[i] + strValue;
                count++;

                if (count == 3)
                {
                    if (i > 0) strValue = "." + strValue;
                    count = 0;
                }
            }

            return strValue;
        }

        public static string ConvertToUnSign(string text)
        {
            text = text.Replace("Ð", "D");
            for (int i = 32; i < 48; i++)
            {
                text = text.Replace(((char)i).ToString(), " ");
            }

            text = text.Replace(".", "-");
            text = text.Replace(" ", "-");
            text = text.Replace(",", "-");
            text = text.Replace(";", "-");
            text = text.Replace(":", "-");

            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static string ConvertToUnSign2(string text)
        {
            text = text.Replace("Ð", "D");
            for (int i = 32; i < 48; i++)
            {
                text = text.Replace(((char)i).ToString(), " ");
            }

            text = text.Replace(".", " ");
            //text = text.Replace(" ", " ");
            text = text.Replace(",", " ");
            text = text.Replace(";", " ");
            text = text.Replace(":", " ");

            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static string ConvertToUnSign3(string text)
        {
            text = text.Replace("Ð", "D");
            //for (int i = 32; i < 48; i++)
            //{
            //    text = text.Replace(((char)i).ToString(), " ");
            //}

            //text = text.Replace(".", " ");
            //text = text.Replace(" ", " ");
            //text = text.Replace(",", " ");
            //text = text.Replace(";", " ");
            //text = text.Replace(":", " ");

            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static bool RenameFileName(string sourceFileName, string destFileName)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(sourceFileName);
                fileInfo.MoveTo(destFileName);
            }
            catch (Exception e)
            {
                WriteToTraceLog(e.Message);
                return false;
                throw e;
            }

            return true;
        }

        public static Image LoadImageFromFile(string path)
        {
            var bytes = File.ReadAllBytes(path);
            var ms = new MemoryStream(bytes);
            var img = Image.FromStream(ms);
            return img;
        }

        public static byte[] LoadBytesFromFile(string path)
        {
            return File.ReadAllBytes(path); 
        }

        public static void CopyTemplates(string reportFileName)
        {
            try
            {
                if (File.Exists(reportFileName)) return;

                string templateFileName = reportFileName.Replace("\\Templates", "\\Templates_Install");
                File.Copy(templateFileName, reportFileName, true);
                Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                WriteToTraceLog(e.Message);
            }
        }

        public static string GetDNSHostName()
        {
            System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            return host.HostName;
        }

        public static Image Crop(Image image, int width, int height, int x, int y)
        {
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            //bmp.SetResolution(80, 60);

            Graphics gfx = Graphics.FromImage(bmp);
            gfx.SmoothingMode = SmoothingMode.AntiAlias;
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;
            gfx.DrawImage(image, new Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel);
            // Dispose to free up resources
            //image.Dispose();
            //bmp.Dispose();
            gfx.Dispose();

            return bmp;
        }

        public static Image StretchSize(Image imgPhoto, int Width, int Height)
        {
            Bitmap bmPhoto = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            //grPhoto.Clear(Color.White);
            //grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(0, 0, Width, Height),
                new Rectangle(0, 0, imgPhoto.Width, imgPhoto.Height),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();

            return bmPhoto;
        }

        public static Image FixedSize(Image imgPhoto, int Width, int Height)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((Width -
                              (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((Height -
                              (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.White);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();

            return bmPhoto;
        }

        public static Image FixedSizeAndCrop(Image imgPhoto, int Width, int Height)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((Width -
                              (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((Height -
                              (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.White);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();

            return Crop(bmPhoto, destWidth, destHeight, destX, destY);
        }

        public static Image RotateImage(Image image, RotateFlipType type)
        {
            image.RotateFlip(type);
            return image;
        }

        public static Image FillData2ImageTemplate(Image imageTemplate, Image logo, Image image, Rectangle logoRect, Rectangle contentRect,
            Rectangle textRect, string text1, string text2, string text3, bool hasStretch)
        {
            if (logo != null)
                logo = FixedSizeAndCrop(logo, logoRect.Width, logoRect.Height);

            if (!hasStretch)
                image = FixedSizeAndCrop(image, contentRect.Width, contentRect.Height);
            else
                image = StretchSize(image, contentRect.Width, contentRect.Height);

            Graphics grPhoto = Graphics.FromImage(imageTemplate);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            int x, y, w, h;
            if (logo != null)
            {
                x = logoRect.X;
                y = logoRect.Y;
                w = logoRect.Width;
                h = logoRect.Height;

                if (logo.Width < logoRect.Width)
                {
                    int delta = logoRect.Width - logo.Width;
                    x += delta / 2;
                    w = logo.Width;
                }

                if (logo.Height < logoRect.Height)
                {
                    int delta = logoRect.Height - logo.Height;
                    y += delta / 2;
                    h = logo.Height;
                }

                grPhoto.DrawImage(logo,
                    new Rectangle(x, y, w, h),
                    new Rectangle(0, 0, logo.Width, logo.Height),
                    GraphicsUnit.Pixel);
            }

            if (!hasStretch)
            {
                x = contentRect.X;
                y = contentRect.Y;
                w = contentRect.Width;
                h = contentRect.Height;

                if (image.Width < contentRect.Width)
                {
                    int delta = contentRect.Width - image.Width;
                    x += delta / 2;
                    w = image.Width;
                }

                if (image.Height < contentRect.Height)
                {
                    int delta = contentRect.Height - image.Height;
                    y += delta / 2;
                    h = image.Height;
                }

                grPhoto.DrawImage(image,
                    new Rectangle(x, y, w, h),
                    new Rectangle(0, 0, image.Width, image.Height),
                    GraphicsUnit.Pixel);
            }
            else
            {
                grPhoto.DrawImage(image,
                    new Rectangle(contentRect.X, contentRect.Y, contentRect.Width, contentRect.Height),
                    new Rectangle(0, 0, image.Width, image.Height),
                    GraphicsUnit.Pixel);
            }

            //Fill text
            Font font = new Font("Arial", 14);
            Brush brush = new SolidBrush(Color.Black);
            if (text1.Trim() != string.Empty)
                grPhoto.DrawString(text1, font, brush, textRect.X, textRect.Y);

            if (text2.Trim() != string.Empty)
                grPhoto.DrawString(text2, font, brush, textRect.X, textRect.Y + 36);

            if (text3.Trim() != string.Empty)
                grPhoto.DrawString(text3, font, brush, textRect.X, textRect.Y + 72);

            font.Dispose();
            brush.Dispose();
            grPhoto.Dispose();

            return imageTemplate;
        }

        public static bool ValidateNetworkPath(string path)
        {
            string pattern = @"^\\{2}[\w-]+(\\{1}(([\w-][\w-\s]*[\w-]+[$$]?)|([\w-][$$]?$)))+";
            return Regex.IsMatch(path, pattern);
        }

        public static bool CheckTVHomeFormat(string ext, TVHomeImageFormat format)
        {
            if (format == TVHomeImageFormat.BMP && ext.Trim().ToUpper() == ".BMP")
                return true;

            if (format == TVHomeImageFormat.JPG && ext.Trim().ToUpper() == ".JPG")
                return true;

            return false;
        }
    }
}


