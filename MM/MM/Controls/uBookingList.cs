﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;
using MM.Dialogs;

namespace MM.Controls
{
    public partial class uBookingList : uBase
    {
        #region Members
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        #endregion

        #region Constructor
        public uBookingList()
        {
            InitializeComponent();
            dtpkTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            dtpkDenNgay.Value = DateTime.Now;
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            btnEdit.Enabled = AllowEdit;
            btnDelete.Enabled = AllowDelete;
            btnPrint.Enabled = AllowPrint;
            btnPrintPreview.Enabled = AllowPrint;
            btnExportExcel.Enabled = AllowExport;
        }

        public void DisplayAsThread()
        {
            UpdateGUI();
            _fromDate = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
            _toDate = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
            
            ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayBookingListProc));
            base.ShowWaiting();
        }

        private void OnDisplayBookingList()
        {
            Result result = BookingBus.GetBookingList(_fromDate, _toDate);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    DataTable dt = result.QueryResult as DataTable;

                    Hashtable htBooking = new Hashtable();
                    Hashtable htBloodTaking = new Hashtable();
                    int saturdayCount = 0;
                    int count1 = 0;
                    int count2 = 0;
                    string dateStr = string.Empty;
                    List<string> keys = new List<string>();

                    foreach (DataRow row in dt.Rows)
                    {
                        BookingType bookingType = (BookingType)Convert.ToInt32(row["BookingType"]);
                        DateTime bookingDate = Convert.ToDateTime(row["BookingDate"]);
                        string bookingDateStr = Convert.ToDateTime(row["BookingDate"]).ToString("yyyy/MM/dd");

                        if (!keys.Contains(bookingDateStr)) keys.Add(bookingDateStr);

                        if (dateStr != bookingDateStr)
                        {
                            dateStr = bookingDateStr;
                            if (bookingDate.DayOfWeek == DayOfWeek.Saturday) saturdayCount++;
                        }

                        if (bookingType == BookingType.Monitor)
                        {
                            if (!htBooking.ContainsKey(bookingDateStr))
                            {
                                List<DataRow> rows = new List<DataRow>();
                                rows.Add(row);
                                htBooking.Add(bookingDateStr, rows);
                            }
                            else
                            {
                                List<DataRow> rows = (List<DataRow>)htBooking[bookingDateStr];
                                rows.Add(row);
                            }

                            count1++;
                        }
                        else
                        {
                            if (!htBloodTaking.ContainsKey(bookingDateStr))
                            {
                                List<DataRow> rows = new List<DataRow>();
                                rows.Add(row);
                                htBloodTaking.Add(bookingDateStr, rows);
                            }
                            else
                            {
                                List<DataRow> rows = (List<DataRow>)htBloodTaking[bookingDateStr];
                                rows.Add(row);
                            }

                            count2++;
                        }
                    }

                    int colCount = 15;
                    int rowCount = count1 > count2 ? count1 : count2;
                    rowCount += 3;

                    if (saturdayCount > 0)
                    {
                        DateTime dateTime = Convert.ToDateTime(dt.Rows[dt.Rows.Count - 1]["BookingDate"]);
                        if (dateTime.DayOfWeek == DayOfWeek.Saturday)
                            rowCount += (saturdayCount - 1);
                        else
                            rowCount += saturdayCount;
                    }

                    ClearData();       

                    dgBooking.Redim(rowCount, colCount);

                    InitHeader();
                    if (keys.Count > 0) FillData(htBooking, htBloodTaking, keys);
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("BookingBus.GetBookingList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("BookingBus.GetBookingList"));
            }
        }

        private void ClearData()
        {
            for (int i = 3; i < dgBooking.RowsCount; i++)
            {
                for (int j = 0; j < dgBooking.ColumnsCount; j++)
                {
                    dgBooking[i, j] = null;
                }
            }
        }

        private void FillData(Hashtable htBooking, Hashtable htBloodTaking, List<string> keys)
        {
            SourceGrid2.RectangleBorder borderRB = new SourceGrid2.RectangleBorder(new SourceGrid2.Border(Color.Black), new SourceGrid2.Border(Color.Black));
            SourceGrid2.RectangleBorder borderRB2 = new SourceGrid2.RectangleBorder(new SourceGrid2.Border(Color.Black), 
                new SourceGrid2.Border(Color.Black, 2));

            SourceGrid2.RectangleBorder borderR = new SourceGrid2.RectangleBorder();
            borderR.Right = new SourceGrid2.Border(Color.Black);
            borderR.Bottom = new SourceGrid2.Border(Color.LightGray);

            SourceGrid2.RectangleBorder borderRT = new SourceGrid2.RectangleBorder();
            borderRT.Top = new SourceGrid2.Border(Color.Black);
            borderRT.Right = new SourceGrid2.Border(Color.Black);
            borderRT.Bottom = new SourceGrid2.Border(Color.LightGray);

            SourceGrid2.RectangleBorder borderTB = new SourceGrid2.RectangleBorder();
            borderTB.Top = new SourceGrid2.Border(Color.Black);
            borderTB.Right = new SourceGrid2.Border(Color.LightGray);
            borderTB.Bottom = new SourceGrid2.Border(Color.Black);

            SourceGrid2.RectangleBorder borderRBT = new SourceGrid2.RectangleBorder();
            borderRBT.Top = new SourceGrid2.Border(Color.Black);
            borderRBT.Right = new SourceGrid2.Border(Color.Black);
            borderRBT.Bottom = new SourceGrid2.Border(Color.Black);

            SourceGrid2.RectangleBorder borderRBT2 = new SourceGrid2.RectangleBorder();
            borderRBT2.Top = new SourceGrid2.Border(Color.Black);
            borderRBT2.Right = new SourceGrid2.Border(Color.Black);
            borderRBT2.Bottom = new SourceGrid2.Border(Color.Black, 2);

            SourceGrid2.RectangleBorder borderLRBT = new SourceGrid2.RectangleBorder();
            borderLRBT.Top = new SourceGrid2.Border(Color.Black);
            borderLRBT.Right = new SourceGrid2.Border(Color.Black);
            borderLRBT.Bottom = new SourceGrid2.Border(Color.Black);
            borderLRBT.Left = new SourceGrid2.Border(Color.Black);

            SourceGrid2.RectangleBorder borderLRBT2 = new SourceGrid2.RectangleBorder();
            borderLRBT2.Top = new SourceGrid2.Border(Color.Black);
            borderLRBT2.Right = new SourceGrid2.Border(Color.Black);
            borderLRBT2.Bottom = new SourceGrid2.Border(Color.Black, 2);
            borderLRBT2.Left = new SourceGrid2.Border(Color.Black);

            SourceGrid2.RectangleBorder borderLR = new SourceGrid2.RectangleBorder();
            borderLR.Left = new SourceGrid2.Border(Color.Black);
            borderLR.Right = new SourceGrid2.Border(Color.Black);
            borderLR.Bottom = new SourceGrid2.Border(Color.LightGray);

            SourceGrid2.RectangleBorder borderLRB = new SourceGrid2.RectangleBorder();
            borderLRB.Right = new SourceGrid2.Border(Color.Black);
            borderLRB.Bottom = new SourceGrid2.Border(Color.Black);
            borderLRB.Left = new SourceGrid2.Border(Color.Black);

            SourceGrid2.RectangleBorder borderLRB2 = new SourceGrid2.RectangleBorder();
            borderLRB2.Right = new SourceGrid2.Border(Color.Black);
            borderLRB2.Bottom = new SourceGrid2.Border(Color.Black, 2);
            borderLRB2.Left = new SourceGrid2.Border(Color.Black);

            SourceGrid2.RectangleBorder borderRBT3 = new SourceGrid2.RectangleBorder();
            borderRBT3.Top = new SourceGrid2.Border(Color.Black);
            borderRBT3.Right = new SourceGrid2.Border(Color.Black, 2);
            borderRBT3.Bottom = new SourceGrid2.Border(Color.Black);

            SourceGrid2.RectangleBorder borderRBT4 = new SourceGrid2.RectangleBorder();
            borderRBT4.Top = new SourceGrid2.Border(Color.Black);
            borderRBT4.Right = new SourceGrid2.Border(Color.Black, 2);
            borderRBT4.Bottom = new SourceGrid2.Border(Color.Black, 2);

            SourceGrid2.RectangleBorder borderRB3 = new SourceGrid2.RectangleBorder(new SourceGrid2.Border(Color.Black, 2), 
                new SourceGrid2.Border(Color.Black));
            SourceGrid2.RectangleBorder borderRB4 = new SourceGrid2.RectangleBorder(new SourceGrid2.Border(Color.Black, 2),
                new SourceGrid2.Border(Color.Black, 2));
            
            Font fontBold = new Font("Tahoma", 8, FontStyle.Bold);
            Font fontNormal = new Font("Tahoma", 8);

            int rowIndex = 3;
            SourceGrid2.Cells.Real.Cell cell = null;

            for (int j = 0; j < keys.Count; j++)
            {
                string key = keys[j];
                int totalMorning = 0;
                int totalAfternoon = 0;
                int totalEvening = 0;

                List<DataRow> bookingList = htBooking[key] as List<DataRow>;
                List<DataRow> bloodTakingList = htBloodTaking[key] as List<DataRow>;
                int bookingCount = bookingList != null ? bookingList.Count : 0;
                int bloodTakingCount = bloodTakingList != null ? bloodTakingList.Count : 0;
                int count = bookingCount > bloodTakingCount ? bookingCount : bloodTakingCount;
                DateTime bookingDate;
                if (bookingList != null) bookingDate = Convert.ToDateTime(bookingList[0]["BookingDate"]);
                else bookingDate = Convert.ToDateTime(bloodTakingList[0]["BookingDate"]);

                Color foreColor = bookingDate.DayOfWeek == DayOfWeek.Saturday ? Color.Red : Color.Black;
                
                for (int i = 0; i < count; i++)
                {
                    if (i == 0)
                    {
                        //Day
                        cell = NewCell(GetDayOfWeek(bookingDate), Color.White, foreColor,
                            ContentAlignment.MiddleCenter, fontBold, false, string.Empty);

                        if (j < keys.Count - 1)
                            cell.Border = i < count - 1 ? borderRT : borderRBT;
                        else
                            cell.Border = i < count - 1 ? borderRT : borderRBT2;

                        dgBooking[rowIndex, 0] = cell;

                        //Date
                        cell = NewCell(bookingDate.ToString("dd/MM/yyyy"), Color.White, foreColor,
                            ContentAlignment.MiddleCenter, fontBold, false, string.Empty);

                        if (j < keys.Count - 1)
                            cell.Border = i < count - 1 ? borderRT : borderRBT;
                        else
                            cell.Border = i < count - 1 ? borderRT : borderRBT2;

                        dgBooking[rowIndex, 1] = cell;
                    }
                    else
                    {
                        cell = NewCell(string.Empty, Color.White, foreColor,
                            ContentAlignment.MiddleCenter, fontBold, false, string.Empty);
                        if (j < keys.Count - 1)
                            cell.Border = i < count - 1 ? borderR : borderRB;
                        else
                            cell.Border = i < count - 1 ? borderR : borderRB2;

                        dgBooking[rowIndex, 0] = cell;

                        cell = NewCell(string.Empty, Color.White, foreColor,
                            ContentAlignment.MiddleCenter, fontBold, false, string.Empty);
                        if (j < keys.Count - 1)
                            cell.Border = i < count - 1 ? borderR : borderRB;
                        else
                            cell.Border = i < count - 1 ? borderR : borderRB2;

                        dgBooking[rowIndex, 1] = cell;
                    }

                    string company = string.Empty;
                    int morningCount = 0;
                    int afternoonCount = 0;
                    int eveningCount = 0;
                    string sales = string.Empty;
                    DataRow row = null;

                    //Booking Monitor
                    if (i < bookingCount)
                    {
                        row = bookingList[i];
                        company = bookingList[i]["Company"].ToString();
                        morningCount = Convert.ToInt32(bookingList[i]["MorningCount"]);
                        afternoonCount = Convert.ToInt32(bookingList[i]["AfternoonCount"]);
                        eveningCount = Convert.ToInt32(bookingList[i]["EveningCount"]);
                        sales = bookingList[i]["Sales"].ToString();

                        totalMorning += morningCount;
                        totalAfternoon += afternoonCount;
                        totalEvening += eveningCount;
                    }

                    //Company
                    cell = NewCell(company, Color.White, foreColor, ContentAlignment.MiddleLeft, fontNormal, false, string.Empty);
                    if (j < keys.Count - 1) cell.Border = i == 0 ? borderRBT : borderRB;
                    else if (i == 0) cell.Border = i == count - 1 ? borderRBT2 : borderRBT;
                    else cell.Border = i == count - 1 ? borderRB2 : borderRB;
                    cell.Tag = row;
                    dgBooking[rowIndex, 2] = cell;

                    //Morning
                    cell = NewCell(morningCount == 0 ? string.Empty : morningCount.ToString(), Color.White, foreColor, 
                        ContentAlignment.MiddleCenter, fontNormal, false, string.Empty);
                    if (j < keys.Count - 1) cell.Border = i == 0 ? borderRBT : borderRB;
                    else if (i == 0) cell.Border = i == count - 1 ? borderRBT2 : borderRBT;
                    else cell.Border = i == count - 1 ? borderRB2 : borderRB;
                    dgBooking[rowIndex, 3] = cell;

                    //Afternoon
                    cell = NewCell(afternoonCount == 0 ? string.Empty : afternoonCount.ToString(), Color.White, foreColor,
                        ContentAlignment.MiddleCenter, fontNormal, false, string.Empty);
                    if (j < keys.Count - 1) cell.Border = i == 0 ? borderRBT : borderRB;
                    else if (i == 0) cell.Border = i == count - 1 ? borderRBT2 : borderRBT;
                    else cell.Border = i == count - 1 ? borderRB2 : borderRB;
                    dgBooking[rowIndex, 4] = cell;

                    //Evening
                    cell = NewCell(eveningCount == 0 ? string.Empty : eveningCount.ToString(), Color.White, foreColor,
                        ContentAlignment.MiddleCenter, fontNormal, false, string.Empty);
                    if (j < keys.Count - 1) cell.Border = i == 0 ? borderRBT : borderRB;
                    else if (i == 0) cell.Border = i == count - 1 ? borderRBT2 : borderRBT;
                    else cell.Border = i == count - 1 ? borderRB2 : borderRB;
                    dgBooking[rowIndex, 5] = cell;

                    //Owner
                    cell = NewCell(sales, Color.White, foreColor,
                        ContentAlignment.MiddleLeft, fontNormal, false, string.Empty);
                    if (j < keys.Count - 1) cell.Border = i == 0 ? borderRBT3 : borderRB3;
                    else if (i == 0) cell.Border = i == count - 1 ? borderRBT4 : borderRBT3;
                    else cell.Border = i == count - 1 ? borderRB4 : borderRB3;
                    dgBooking[rowIndex, 9] = cell;

                    //Empty Cell
                    cell = NewCell(string.Empty, Color.White, foreColor,
                        ContentAlignment.MiddleRight, fontNormal, false, string.Empty);
                    cell.Border = borderR;
                    dgBooking[rowIndex, 10] = cell;

                    //Blood Taking
                    company = string.Empty;
                    int pax = 0;
                    string time = string.Empty;
                    sales = string.Empty;
                    row = null;

                    if (i < bloodTakingCount)
                    {
                        row = bloodTakingList[i];
                        company = bloodTakingList[i]["Company"].ToString();
                        pax = Convert.ToInt32(bloodTakingList[i]["Pax"]);
                        time = Convert.ToDateTime(bloodTakingList[i]["BookingDate"]).ToString("hh:mm tt");
                        sales = bloodTakingList[i]["Sales"].ToString();
                    }

                    //Company
                    cell = NewCell(company, Color.White, foreColor, ContentAlignment.MiddleLeft, fontNormal, false, string.Empty);
                    if (j < keys.Count - 1) cell.Border = i == 0 ? borderLRBT : borderLRB;
                    else if (i == 0) cell.Border = i == count - 1 ? borderLRBT2 : borderLRBT;
                    else cell.Border = i == count - 1 ? borderLRB2 : borderLRB;
                    cell.Tag = row;
                    dgBooking[rowIndex, 11] = cell;

                    //Pax
                    cell = NewCell(pax == 0 ? string.Empty : pax.ToString(), Color.White, foreColor,
                        ContentAlignment.MiddleCenter, fontNormal, false, string.Empty);
                    if (j < keys.Count - 1) cell.Border = i == 0 ? borderRBT : borderRB;
                    else if (i == 0) cell.Border = i == count - 1 ? borderRBT2 : borderRBT;
                    else cell.Border = i == count - 1 ? borderRB2 : borderRB;
                    dgBooking[rowIndex, 12] = cell;

                    //Time
                    cell = NewCell(time, Color.White, foreColor,
                        ContentAlignment.MiddleCenter, fontNormal, false, string.Empty);
                    if (j < keys.Count - 1) cell.Border = i == 0 ? borderRBT : borderRB;
                    else if (i == 0) cell.Border = i == count - 1 ? borderRBT2 : borderRBT;
                    else cell.Border = i == count - 1 ? borderRB2 : borderRB;
                    dgBooking[rowIndex, 13] = cell;

                    //Sales
                    cell = NewCell(sales, Color.White, foreColor,
                        ContentAlignment.MiddleLeft, fontNormal, false, string.Empty);
                    if (j < keys.Count - 1) cell.Border = i == 0 ? borderRBT3 : borderRB3;
                    else if (i == 0) cell.Border = i == count - 1 ? borderRBT4 : borderRBT3;
                    else cell.Border = i == count - 1 ? borderRB4 : borderRB3;
                    dgBooking[rowIndex, 14] = cell;

                    rowIndex++;
                }

                //Total Morning
                cell = NewCell(totalMorning == 0 ? string.Empty : totalMorning.ToString(), Color.White, foreColor,
                    ContentAlignment.MiddleCenter, fontNormal, false, string.Empty);
                cell.Border = j < keys.Count - 1 ? borderRBT : borderRBT2;
                dgBooking[rowIndex - count, 6] = cell;
                dgBooking[rowIndex - count, 6].RowSpan = count;

                //Total Afternoon
                cell = NewCell(totalAfternoon == 0 ? string.Empty : totalAfternoon.ToString(), Color.White, foreColor,
                    ContentAlignment.MiddleCenter, fontNormal, false, string.Empty);
                cell.Border = j < keys.Count - 1 ? borderRBT : borderRBT2;
                dgBooking[rowIndex - count, 7] = cell;
                dgBooking[rowIndex - count, 7].RowSpan = count;

                //Total Evening
                cell = NewCell(totalEvening == 0 ? string.Empty : totalEvening.ToString(), Color.White, foreColor,
                    ContentAlignment.MiddleCenter, fontNormal, false, string.Empty);
                //cell.Border = j < keys.Count - 1 ? borderRBT3 : borderRBT4;
                cell.Border = j < keys.Count - 1 ? borderRBT : borderRBT2;
                dgBooking[rowIndex - count, 8] = cell;
                dgBooking[rowIndex - count, 8].RowSpan = count;

                //Saturday
                if (bookingDate.DayOfWeek == DayOfWeek.Saturday && j < keys.Count - 1)
                {
                    cell = NewCell(string.Empty, Color.White, foreColor,
                            ContentAlignment.MiddleCenter, fontBold, false, string.Empty);
                    cell.Border = borderTB;
                    dgBooking[rowIndex, 0] = cell;
                    dgBooking[rowIndex, 0].ColumnSpan = 10;

                    cell = NewCell(string.Empty, Color.White, foreColor,
                            ContentAlignment.MiddleCenter, fontBold, false, string.Empty);
                    dgBooking[rowIndex, 10] = cell;

                    cell = NewCell(string.Empty, Color.White, foreColor,
                            ContentAlignment.MiddleCenter, fontBold, false, string.Empty);
                    cell.Border = borderTB;
                    dgBooking[rowIndex, 11] = cell;
                    dgBooking[rowIndex, 11].ColumnSpan = 4;

                    rowIndex++;
                }
            }
        }

        private string GetDayOfWeek(DateTime dt)
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

        private void InitHeader()
        {
            if (dgBooking[0, 0] != null) return;
            Font font = new System.Drawing.Font("Tohama", 9, FontStyle.Bold);
            SourceGrid2.RectangleBorder borderRB = new SourceGrid2.RectangleBorder(new SourceGrid2.Border(Color.Black), new SourceGrid2.Border(Color.Black));
            SourceGrid2.RectangleBorder borderRB2 = new SourceGrid2.RectangleBorder(new SourceGrid2.Border(Color.Black, 2), new SourceGrid2.Border(Color.Black));

            SourceGrid2.RectangleBorder borderTRB = new SourceGrid2.RectangleBorder();
            borderTRB.Top = new SourceGrid2.Border(Color.Black);
            borderTRB.Right = new SourceGrid2.Border(Color.Black);
            borderTRB.Bottom = new SourceGrid2.Border(Color.Black);

            SourceGrid2.RectangleBorder borderTRB2 = new SourceGrid2.RectangleBorder();
            borderTRB2.Top = new SourceGrid2.Border(Color.Black);
            borderTRB2.Right = new SourceGrid2.Border(Color.Black, 2);
            borderTRB2.Bottom = new SourceGrid2.Border(Color.Black);

            SourceGrid2.RectangleBorder borderLTRB = new SourceGrid2.RectangleBorder();
            borderLTRB.Bottom = new SourceGrid2.Border(Color.Black);
            borderLTRB.Left = new SourceGrid2.Border(Color.Black);
            borderLTRB.Top = new SourceGrid2.Border(Color.Black);
            borderLTRB.Right = new SourceGrid2.Border(Color.Black);

            SourceGrid2.RectangleBorder borderR = new SourceGrid2.RectangleBorder();
            borderR.Left = new SourceGrid2.Border(Color.LightGray);
            borderR.Right = new SourceGrid2.Border(Color.Black);
            borderR.Bottom = new SourceGrid2.Border(Color.LightGray);
                        
            SourceGrid2.Cells.Real.Cell cell = NewCell("BOOKING MONITOR", Color.White, Color.Red, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[0, 0] = cell;
            dgBooking[0, 0].ColumnSpan = 10;
            
            cell = NewCell(string.Empty, Color.White, Color.Red, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[0, 10] = cell;

            cell = NewCell("BLOOD TAKING", Color.White, Color.Red, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[0, 11] = cell;
            dgBooking[0, 11].ColumnSpan = 4;

            cell = NewCell("Day", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderTRB;
            dgBooking[1, 0] = cell;
            dgBooking[1, 0].RowSpan = 2;

            cell = NewCell("Date", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderTRB;
            dgBooking[1, 1] = cell;
            dgBooking[1, 1].RowSpan = 2;
            dgBooking.AutoSizeColumn(1, 75);

            cell = NewCell("Company", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderTRB;
            dgBooking[1, 2] = cell;
            dgBooking[1, 2].RowSpan = 2;
            dgBooking.AutoSizeColumn(2, 150);

            cell = NewCell("Morning", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderTRB;
            dgBooking[1, 3] = cell;
            dgBooking[1, 3].RowSpan = 2;
            dgBooking.AutoSizeColumn(3, 60);

            cell = NewCell("Afternoon", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderTRB;
            dgBooking[1, 4] = cell;
            dgBooking[1, 4].RowSpan = 2;
            dgBooking.AutoSizeColumn(4, 60);

            cell = NewCell("Evening", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderTRB;
            dgBooking[1, 5] = cell;
            dgBooking[1, 5].RowSpan = 2;
            dgBooking.AutoSizeColumn(5, 60);

            cell = NewCell("Total", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderTRB;
            dgBooking[1, 6] = cell;
            dgBooking[1, 6].ColumnSpan = 3;

            cell = NewCell("Morning", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderRB;
            dgBooking[2, 6] = cell;
            dgBooking.AutoSizeColumn(6, 60);

            cell = NewCell("Afternoon", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderRB;
            dgBooking[2, 7] = cell;
            dgBooking.AutoSizeColumn(7, 60);

            cell = NewCell("Evening", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderRB;
            dgBooking[2, 8] = cell;
            dgBooking.AutoSizeColumn(8, 60);

            cell = NewCell("Owner", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderTRB2;
            dgBooking[1, 9] = cell;
            dgBooking[1, 9].RowSpan = 2;
            dgBooking.AutoSizeColumn(9, 150);

            cell = NewCell(string.Empty, Color.White, Color.Black, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[1, 10] = cell;
            dgBooking.AutoSizeColumn(10, 30);

            cell = NewCell(string.Empty, Color.White, Color.Black, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[1, 11] = cell;

            cell = NewCell(string.Empty, Color.White, Color.Black, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[1, 12] = cell;

            cell = NewCell(string.Empty, Color.White, Color.Black, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[1, 13] = cell;

            cell = NewCell(string.Empty, Color.White, Color.Black, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[1, 14] = cell;

            cell = NewCell(string.Empty, Color.White, Color.Black, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderR;
            dgBooking[2, 10] = cell;

            cell = NewCell("Company", Color.Yellow, Color.Black, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderLTRB;
            dgBooking[2, 11] = cell;
            dgBooking.AutoSizeColumn(11, 150);

            cell = NewCell("Pax", Color.Yellow, Color.Black, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderTRB;
            dgBooking[2, 12] = cell;
            dgBooking.AutoSizeColumn(12, 50);

            cell = NewCell("Time", Color.Yellow, Color.Black, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderTRB;
            dgBooking[2, 13] = cell;
            dgBooking.AutoSizeColumn(13, 60);

            cell = NewCell("Sales", Color.Yellow, Color.Black, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderTRB;
            dgBooking[2, 14] = cell;
            dgBooking.AutoSizeColumn(14, 150);
        }

        private SourceGrid2.Cells.Real.Cell NewCell(object value, Color backColor, Color foreColor, ContentAlignment textAlignment, Font font, bool isEnableEdit, string toolTip)
        {
            SourceGrid2.VisualModels.Common visualModel = new SourceGrid2.VisualModels.Common();
            visualModel.BackColor = backColor;
            visualModel.TextAlignment = textAlignment;
            visualModel.ForeColor = foreColor;
            visualModel.Font = font;
            
            SourceGrid2.DataModels.EditorTextBox editorModel = new SourceGrid2.DataModels.EditorTextBoxNumeric(typeof(string));
            editorModel.EnableEdit = isEnableEdit;

            SourceGrid2.Cells.Real.Cell cell = new SourceGrid2.Cells.Real.Cell(value, editorModel, visualModel);
            cell.ToolTipText = toolTip;

            return cell;
        }

        private void OnAdd()
        {
            dlgAddBooking dlg = new dlgAddBooking();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnEdit()
        {
            if (dgBooking.Selection == null || dgBooking.Selection.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 lịch hẹn để sửa.", IconType.Information);
                return;
            }

            DataRow row = null;
            foreach (SourceGrid2.Cells.Real.Cell cell in dgBooking.Selection.GetCells())
            {
                if (cell.Row < 3) continue;
                if (cell.Column < 0 || (cell.Column >= 6 && cell.Column <= 8) || cell.Column == 10) continue;

                if ((cell.Column >= 0 && cell.Column <= 5) || cell.Column == 9)
                    row = dgBooking[cell.Row, 2].Tag as DataRow;
                else
                    row = dgBooking[cell.Row, 11].Tag as DataRow;

                if (row == null) continue;

                break;
            }

            if (row == null)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 lịch hẹn để sửa.", IconType.Information);
                return;
            }

            string nguoiTao = row["CreatedBy"].ToString();
            if (nguoiTao != Global.UserGUID)
            {
                MsgBox.Show(Application.ProductName, string.Format("Bạn không thể sửa lịch hẹn của '{0}' tạo. Vui lòng kiểm tra lại",
                    row["NguoiTao"].ToString()), IconType.Information);
                return;
            }

            dlgEditBooking dlg = new dlgEditBooking(row);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnDelete()
        {
            if (dgBooking.Selection == null || dgBooking.Selection.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn ít nhất 1 lịch hẹn để xóa.", IconType.Information);
                return;
            }

            List<string> keys = new List<string>();
            foreach (SourceGrid2.Cells.Real.Cell cell in dgBooking.Selection.GetCells())
            {
                if (cell.Row < 3) continue;
                if (cell.Column < 0 || (cell.Column >= 6 && cell.Column <= 8) || cell.Column == 10) continue;

                DataRow row = null;
                if ((cell.Column >= 0 && cell.Column <= 5) || cell.Column == 9)
                    row = dgBooking[cell.Row, 2].Tag as DataRow;
                else
                    row = dgBooking[cell.Row, 11].Tag as DataRow;

                if (row == null) continue;
                string nguoiTao = row["CreatedBy"].ToString();
                if (nguoiTao != Global.UserGUID)
                {
                    MsgBox.Show(Application.ProductName, string.Format("Bạn không thể xóa lịch hẹn của '{0}' tạo. Vui lòng kiểm tra lại", 
                        row["NguoiTao"].ToString()), IconType.Information);
                    return;
                }

                keys.Add(row["BookingGUID"].ToString());
            }

            if (keys.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn ít nhất 1 lịch hẹn để xóa.", IconType.Information);
                return;
            }

            if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những lịch hẹn mà bạn đã chọn ?") == DialogResult.Yes)
            {
                Result result = BookingBus.DeleteBooking(keys);
                if (result.IsOK)
                {
                    DisplayAsThread();
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("BookingBus.DeleteBooking"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("BookingBus.DeleteBooking"));
                }
            }
            
        }
        #endregion

        #region Window Event Handlers
        private void btnView_Click(object sender, EventArgs e)
        {
            DisplayAsThread();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAdd();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDelete();
        }

        private void dgBooking_DoubleClick(object sender, EventArgs e)
        {
            OnEdit();
        }
        #endregion

        #region Working Thread
        private void OnDisplayBookingListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayBookingList();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion

        
    }
}
