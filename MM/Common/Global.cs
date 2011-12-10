﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MM.Common
{
    public class Global
    {
        public static string AppConfig = string.Format("{0}\\App.cfg", AppDomain.CurrentDomain.BaseDirectory);
        public static ConnectionInfo ConnectionInfo = new ConnectionInfo();
        public static string UserGUID = string.Empty;
        public static string LogonGUID = string.Empty;
        public static string Fullname = string.Empty;
        public static string Password = string.Empty;
        public static StaffType StaffType = Common.StaffType.Admin;
        public static bool AllowShowServiePrice = true;
        public static bool AllowPrintReceipt = true;
        public static bool AllowExportReceipt = true;
        public static bool AllowExportInvoice = true;
        public static bool AllowPrintInvoice = true;
        public static string PrintLabelConfigPath = string.Format("{0}\\PrintLabelConfig.xml", AppDomain.CurrentDomain.BaseDirectory);
        public static PrintLabelConfig PrintLabelConfig = new PrintLabelConfig();
    }
}
