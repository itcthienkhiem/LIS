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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Reporting.WinForms;
using MM.Databasae;
using MM.Common;
using MM.Bussiness;

namespace MM.Controls
{
    public partial class uDoanhThuNhanVien : uBase
    {
        #region Members
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        private string _docStaffGUID = string.Empty;
        private byte _type = 0;
        private bool _isTongHop = true;
        #endregion

        #region Constructor
        public uDoanhThuNhanVien()
        {
            InitializeComponent();
            dtpkToDate.Value = DateTime.Now;
            dtpkFromDate.Value = DateTime.Now.AddDays(-1);
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            _ucReportViewer.ShowPrintButton = AllowPrint;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayDocStaffListProc));
                base.ShowWaiting();
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

        public void ClearData()
        {
            DataTable dt = cboNhanVien.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                //dt = null;
                //cboNhanVien.DataSource = null;
            }
        }

        private void OnDisplayDocStaffList()
        {
            Result result = DocStaffBus.GetDocStaffList(1);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    DataTable dt = result.QueryResult as DataTable;
                    DataRow newRow = dt.NewRow();
                    newRow["DocStaffGUID"] = Guid.Empty.ToString();
                    newRow["FullName"] = "--------Tất cả--------";
                    dt.Rows.InsertAt(newRow, 0);

                    cboNhanVien.DataSource = dt;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("DocStaffBus.GetDocStaffList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.GetDocStaffList"));
            }
        }

        private bool CheckInfo()
        {
            if (dtpkFromDate.Value > dtpkToDate.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập từ ngày nhỏ hơn hoặc bằng đến ngày.", IconType.Information);
                dtpkFromDate.Focus();
                return false;
            }

            return true;
        }

        private void OnView()
        {
            Result result = null;
            if (_isTongHop)
            {
                result = ReportBus.GetDoanhThuNhanVienTongHop(_fromDate, _toDate, _docStaffGUID, _type);
                if (result.IsOK)
                {
                    ReportDataSource reportDataSource = new ReportDataSource("spDoanhThuNhanVienTongHopResult", 
                        (List<spDoanhThuNhanVienTongHopResult>)result.QueryResult);

                    MethodInvoker method = delegate
                    {
                        if (result == null || result.QueryResult == null)
                            txtKetQua.Text = "0";
                        else
                            txtKetQua.Text = ((List<spDoanhThuNhanVienTongHopResult>)result.QueryResult).Count.ToString();

                        _ucReportViewer.ViewReport("MM.Templates.rptDoanhThuNhanVienTongHop.rdlc", reportDataSource);
                    };

                    if (InvokeRequired) BeginInvoke(method);
                    else method.Invoke();
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReportBus.GetDoanhThuNhanVienTongHop"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ReportBus.GetDoanhThuNhanVienTongHop"));
                }
            }
            else
            {
                result = ReportBus.GetDoanhThuNhanVienChiTiet(_fromDate, _toDate, _docStaffGUID, _type);
                if (result.IsOK)
                {
                    ReportDataSource reportDataSource = new ReportDataSource("spDoanhThuNhanVienChiTietResult",
                        (List<spDoanhThuNhanVienChiTietResult>)result.QueryResult);

                    MethodInvoker method = delegate
                    {
                        if (result == null || result.QueryResult == null)
                        {
                            txtKetQua.Text = "0";
                        }
                        else
                        {
                            List<spDoanhThuNhanVienChiTietResult> doanhThuChiTietList = (List<spDoanhThuNhanVienChiTietResult>)result.QueryResult;
                            int count = 0;
                            string fullName = string.Empty;
                            foreach (var doanhThu in doanhThuChiTietList)
                            {
                                if (fullName.ToLower() != doanhThu.FullName.ToLower())
                                {
                                    count++;
                                    fullName = doanhThu.FullName;
                                }
                            }

                            txtKetQua.Text = count.ToString();
                        }

                        _ucReportViewer.ViewReport("MM.Templates.rptDoanhThuNhanVienChiTiet.rdlc", reportDataSource);
                    };

                    if (InvokeRequired) BeginInvoke(method);
                    else method.Invoke();
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReportBus.GetDoanhThuNhanVienChiTiet"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ReportBus.GetDoanhThuNhanVienChiTiet"));
                }
            }
        }

        private void ViewAsThread()
        {
            try
            {
                if (!CheckInfo()) return;
                _fromDate = new DateTime(dtpkFromDate.Value.Year, dtpkFromDate.Value.Month, dtpkFromDate.Value.Day, 0, 0, 0);
                _toDate = new DateTime(dtpkToDate.Value.Year, dtpkToDate.Value.Month, dtpkToDate.Value.Day, 23, 59, 59);
                _docStaffGUID = cboNhanVien.SelectedValue.ToString();
                _type = raServiceHistory.Checked ? (byte)0 : (byte)1;
                _isTongHop = raTongHop.Checked;

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnViewProc));
                base.ShowWaiting();
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

        #region Window Event Handlers
        private void btnView_Click(object sender, EventArgs e)
        {
            ViewAsThread();
        }
        #endregion

        #region Working Thread
        private void OnDisplayDocStaffListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayDocStaffList();
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

        private void OnViewProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnView();
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
