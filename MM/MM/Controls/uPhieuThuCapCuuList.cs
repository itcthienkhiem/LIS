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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;
using MM.Dialogs;
using MM.Exports;

namespace MM.Controls
{
    public partial class uPhieuThuCapCuuList : uBase
    {
        #region Members
        private bool _isFromDateToDate = true;
        private string _tenBenhNhan = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        private int _type = 1; //0: TatCa; 1: ChuaXoa; 2: DaXoa
        private int _type2 = 0;//0: TatCa; 1: DaThuTien; 2: ChuaThuTien
        #endregion

        #region Constructor
        public uPhieuThuCapCuuList()
        {
            InitializeComponent();
            dtpkDenNgay.Value = DateTime.Now;
            dtpkTuNgay.Value = DateTime.Now.AddDays(-7);
        }
        #endregion

        #region Properties
        public List<DataRow> CheckedPTRows
        {
            get
            {
                if (dgPhieuThu.RowCount <= 0) return null;
                List<DataRow> checkedRows = new List<DataRow>();
                DataTable dt = dgPhieuThu.DataSource as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    if (Boolean.Parse(row["Checked"].ToString()))
                        checkedRows.Add(row);
                }

                return checkedRows;
            }
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            btnDelete.Enabled = AllowDelete;
            btnPrint.Enabled = AllowPrint;
            btnPrintPreview.Enabled = AllowPrint;
            btnExportExcel.Enabled = AllowExport;
            //btnExportInvoice.Enabled = Global.AllowEx;
            btnGhiNhanTraNo.Enabled = Global.AllowViewGhiNhanTraNo;

            addToolStripMenuItem.Enabled = AllowAdd;
            deleteToolStripMenuItem.Enabled = AllowDelete;
            printPreviewToolStripMenuItem.Enabled = AllowPrint;
            printToolStripMenuItem.Enabled = AllowPrint;
            exportExcelToolStripMenuItem.Enabled = AllowExport;
            ghiNhanTraNoToolStripMenuItem.Enabled = Global.AllowViewGhiNhanTraNo;
        }

        public void ClearData()
        {
            DataTable dt = dgPhieuThu.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgPhieuThu.DataSource = null;
            }
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                lbKetQuaTimDuoc.Text = "Kết quả tìm được: 0";
                _isFromDateToDate = raTuNgayToiNgay.Checked;
                _fromDate = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                _toDate = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
                _tenBenhNhan = txtTenBenhNhan.Text;

                if (raTatCa.Checked) _type = 0;
                else if (raChuaXoa.Checked) _type = 1;
                else _type = 2;

                if (raAll.Checked) _type2 = 0;
                else if (raDaThuTien.Checked) _type2 = 1;
                else _type2 = 2;

                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayPhieuThuThuocListProc));
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

        //public void HighlightExportedInvoice()
        //{
        //    foreach (DataGridViewRow row in dgPhieuThu.Rows)
        //    {
        //        DataRow dr = (row.DataBoundItem as DataRowView).Row;
        //        bool isExported = Convert.ToBoolean(dr["IsExported"]);
        //        if (isExported)
        //            row.DefaultCellStyle.BackColor = Color.LightSeaGreen;
        //    }
        //}

        private void ShowTongTien()
        {
            if (!chkTongTien.Checked)
                chkTongTien.Text = "Tổng tiền:";
            else
            {
                Result result = PhieuThuCapCuuBus.GetTongTien(_isFromDateToDate, _fromDate, _toDate, _tenBenhNhan, _type, _type2);
                if (result.IsOK)
                {
                    chkTongTien.Text = string.Format("Tổng tiền: {0:N0} VNĐ", result.QueryResult);
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhieuThuCapCuuBus.GetTongTien"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuCapCuuBus.GetTongTien"));
                }
            }
        }

        private void OnDisplayPhieuThuThuocList()
        {
            Result result = PhieuThuCapCuuBus.GetPhieuThuCapCuuList(_isFromDateToDate, _fromDate, _toDate, _tenBenhNhan, _type, _type2);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    DataTable dt = result.QueryResult as DataTable;
                    dgPhieuThu.DataSource = result.QueryResult;
                    //HighlightExportedInvoice();
                    lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dt.Rows.Count);

                    ShowTongTien();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhieuThuCapCuuBus.GetPhieuThuCapCuuList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuCapCuuBus.GetPhieuThuCapCuuList"));
            }
        }

        private void SelectLastedRow()
        {
            dgPhieuThu.CurrentCell = dgPhieuThu[1, dgPhieuThu.RowCount - 1];
            dgPhieuThu.Rows[dgPhieuThu.RowCount - 1].Selected = true;
        }

        private void OnAddPhieuThu()
        {
            dlgAddPhieuThuCapCuu dlg = new dlgAddPhieuThuCapCuu();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnDeletePhieuThu()
        {
            List<string> deletedPTThuocList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgPhieuThu.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedRows.Add(row);
                }
            }

            if (deletedRows.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những phiếu thu mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    List<string> noteList = new List<string>();

                    foreach (DataRow row in deletedRows)
                    {
                        string maPhieuThuThuoc = row["MaPhieuThuCapCuu"].ToString();
                        string phieuThuThuocGUID = row["PhieuThuCapCuuGUID"].ToString();

                        dlgLyDoXoa dlg = new dlgLyDoXoa(maPhieuThuThuoc, 0);
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            noteList.Add(dlg.Notes);
                            deletedPTThuocList.Add(phieuThuThuocGUID);
                        }
                    }

                    if (deletedPTThuocList.Count > 0)
                    {
                        Result result = PhieuThuCapCuuBus.DeletePhieuThuCapCuu(deletedPTThuocList, noteList);
                        if (result.IsOK)
                        {
                            foreach (DataRow row in deletedRows)
                            {
                                string phieuThuThuocGUID = row["PhieuThuCapCuuGUID"].ToString();
                                if (deletedPTThuocList.Contains(phieuThuThuocGUID))
                                    dt.Rows.Remove(row);
                            }
                        }
                        else
                        {
                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhieuThuCapCuuBus.DeletePhieuThuCapCuu"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuCapCuuBus.DeletePhieuThuCapCuu"));
                        }
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những phiếu thu cần xóa.", IconType.Information);
        }

        private void OnPrint(bool isPreview)
        {
            Cursor.Current = Cursors.WaitCursor;
            List<DataRow> checkedRows = new List<DataRow>();
            DataTable dt = dgPhieuThu.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    checkedRows.Add(row);
                }
            }

            if (checkedRows.Count > 0)
            {
                string exportFileName = string.Format("{0}\\Temp\\PhieuThuCapCuu.xls", Application.StartupPath);
                if (isPreview)
                {
                    foreach (DataRow row in checkedRows)
                    {
                        string phieuThuThuocGUID = row["PhieuThuCapCuuGUID"].ToString();
                        if (ExportExcel.ExportPhieuThuCapCuuToExcel(exportFileName, phieuThuThuocGUID))
                        {
                            try
                            {
                                ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(Const.PhieuThuCapCuuTemplate));
                            }
                            catch (Exception ex)
                            {
                                MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                return;
                            }
                        }
                        else
                            return;
                    }
                }
                else
                {
                    if (_printDialog.ShowDialog() == DialogResult.OK)
                    {
                        foreach (DataRow row in checkedRows)
                        {
                            string phieuThuThuocGUID = row["PhieuThuCapCuuGUID"].ToString();
                            if (ExportExcel.ExportPhieuThuCapCuuToExcel(exportFileName, phieuThuThuocGUID))
                            {
                                try
                                {
                                    ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.PhieuThuCapCuuTemplate));
                                }
                                catch (Exception ex)
                                {
                                    MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                    return;
                                }
                            }
                            else
                                return;
                        }
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những phiếu thu cần in.", IconType.Information);
        }

        private void OnViewPhieuThuThuoc()
        {
            if (dgPhieuThu.SelectedRows == null || dgPhieuThu.SelectedRows.Count <= 0)
                return;

            DataRow drPhieuThu = (dgPhieuThu.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddPhieuThuCapCuu dlg = new dlgAddPhieuThuCapCuu(drPhieuThu, AllowEdit);
            dlg.ShowDialog(this);
            //if (dlg.ShowDialog(this) == DialogResult.Cancel)
            //{
            //    if (dlg.IsExportedInvoice)
            //        HighlightExportedInvoice();
            //}
            //else
            //    HighlightExportedInvoice();
        }

        private void OnExportExcel()
        {
            Cursor.Current = Cursors.WaitCursor;
            List<string> checkedPhieuThuThuocKeys = new List<string>();
            DataTable dt = dgPhieuThu.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    checkedPhieuThuThuocKeys.Add(row["PhieuThuCapCuuGUID"].ToString());
                }
            }

            if (checkedPhieuThuThuocKeys.Count > 0)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Export Excel";
                dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ExportExcel.ExportChiTietPhieuThuCapCuuToExcel(dlg.FileName, checkedPhieuThuThuocKeys);
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những phiếu thu cần xuất Excel.", IconType.Information);
        }

        private void OnExportInvoice()
        {
            //List<DataRow> exportedInvoiceList = new List<DataRow>();
            //List<DataRow> noExportedInvoiceList = new List<DataRow>();
            //List<DataRow> checkedRows = CheckedPTRows;

            //foreach (DataRow row in checkedRows)
            //{

            //    bool isExported = Convert.ToBoolean(row["IsExported"]);
            //    if (!isExported)
            //        noExportedInvoiceList.Add(row);
            //    else
            //        exportedInvoiceList.Add(row);
            //}

            //if (exportedInvoiceList.Count > 0)
            //{
            //    MsgBox.Show(Application.ProductName, "(Một số) phiếu thu đã xuất hóa đơn rồi. Vui lòng kiểm tra lại.", IconType.Information);
            //    return;
            //}

            //if (MsgBox.Question(Application.ProductName, "Bạn có muốn xuất hóa đơn ?") == DialogResult.No) return;

            //dlgHoaDonThuoc dlg = new dlgHoaDonThuoc(noExportedInvoiceList);
            //dlg.ShowDialog();

            //HighlightExportedInvoice();
        }

        private void GhiNhanTraNo()
        {
            if (dgPhieuThu.SelectedRows == null || dgPhieuThu.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 phiếu thu.", IconType.Information);
                return;
            }

            DataRow drReceipt = (dgPhieuThu.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string phieuThuGUID = drReceipt["PhieuThuCapCuuGUID"].ToString();
            bool daThuTien = Convert.ToBoolean(drReceipt["DaThuTien"]);

            dlgGhiNhanTraNo dlg = new dlgGhiNhanTraNo(LoaiPT.CapCuu, phieuThuGUID, daThuTien);
            dlg.ShowDialog();
            if (dlg.IsDataChange) DisplayAsThread();
        }
        #endregion

        #region Window Event Handlers
        private void ghiNhanTraNoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GhiNhanTraNo();
        }

        private void btnGhiNhanTraNo_Click(object sender, EventArgs e)
        {
            GhiNhanTraNo();
        }

        private void chkTongTien_CheckedChanged(object sender, EventArgs e)
        {
            ShowTongTien();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddPhieuThu();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeletePhieuThu();
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OnPrint(false);
        }
        
        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgPhieuThu.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void dgPhieuThu_DoubleClick(object sender, EventArgs e)
        {
            OnViewPhieuThuThuoc();
        }

        private void raTuNgayToiNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpkTuNgay.Enabled = raTuNgayToiNgay.Checked;
            dtpkDenNgay.Enabled = raTuNgayToiNgay.Checked;
            txtTenBenhNhan.ReadOnly = raTuNgayToiNgay.Checked;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (raTuNgayToiNgay.Checked && dtpkTuNgay.Value > dtpkDenNgay.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập từ ngày nhỏ hơn hoặc bằng đến ngày.", IconType.Information);
                dtpkTuNgay.Focus();
                return;
            }

            if (raTenBenhNhan.Checked && txtTenBenhNhan.Text.Trim() == string.Empty)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập tên bệnh nhân.", IconType.Information);
                txtTenBenhNhan.Focus();
                return;
            }

            DisplayAsThread();
        }

        private void txtTenBenhNhan_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            OnExportExcel();
        }

        private void btnExportInvoice_Click(object sender, EventArgs e)
        {
            if (dgPhieuThu.RowCount <= 0 || CheckedPTRows == null || CheckedPTRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu ít nhất 1 phiếu thu cần xuất hóa đơn.", IconType.Information);
                return;
            }

            OnExportInvoice();
        }

        private void dgPhieuThu_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //HighlightExportedInvoice();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnAddPhieuThu();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDeletePhieuThu();
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPrint(false);
        }

        private void exportExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnExportExcel();
        }

        private void xuatHoaDonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgPhieuThu.RowCount <= 0 || CheckedPTRows == null || CheckedPTRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu ít nhất 1 phiếu thu cần xuất hóa đơn.", IconType.Information);
                return;
            }

            OnExportInvoice();
        }

        private void raTatCa_CheckedChanged(object sender, EventArgs e)
        {
            DisplayAsThread();
        }

        private void raChuaXoa_CheckedChanged(object sender, EventArgs e)
        {
            DisplayAsThread();
        }

        private void raDaXoa_CheckedChanged(object sender, EventArgs e)
        {
            DisplayAsThread();
        }

        private void raAll_CheckedChanged(object sender, EventArgs e)
        {
            DisplayAsThread();
        }

        private void raDaThuTien_CheckedChanged(object sender, EventArgs e)
        {
            DisplayAsThread();
        }

        private void raChuaThuTien_CheckedChanged(object sender, EventArgs e)
        {
            DisplayAsThread();
        }
        #endregion

        #region Working Thread
        private void OnDisplayPhieuThuThuocListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayPhieuThuThuocList();
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
