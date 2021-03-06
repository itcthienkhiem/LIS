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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;

namespace MM.Dialogs
{
    public partial class dlgAddPhieuThuHopDong : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private PhieuThuHopDong _phieuThuHopDong = new PhieuThuHopDong();
        private DataRow _drPhieuThu = null;
        private bool _isExportedInvoice = false;
        private string _hopDongGUID = string.Empty;
        #endregion

        #region Constructor
        public dlgAddPhieuThuHopDong()
        {
            InitializeComponent();
            GenerateCode();
        }

        public dlgAddPhieuThuHopDong(DataRow drPhieuThu)
        {
            InitializeComponent();
            _drPhieuThu = drPhieuThu;
            _isNew = false;
            this.Text = "Xem phieu thu";
        }
        #endregion

        #region Properties
        public PhieuThuHopDong PhieuThuHopDong
        {
            get { return _phieuThuHopDong; }
        }

        public bool IsExportedInvoice
        {
            get { return _isExportedInvoice; }
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            if (_isNew)
                btnExportInvoice.Enabled = false;
            else
            {
                bool isExportedInvoice = Convert.ToBoolean(_drPhieuThu["IsExported"]);
                btnExportInvoice.Enabled = Global.AllowExportHoaDonHopDong && !isExportedInvoice;
            }
        }

        private void GenerateCode()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = PhieuThuHopDongBus.GetPhieuThuHopDongCount();
            if (result.IsOK)
            {
                int count = Convert.ToInt32(result.QueryResult);
                txtMaPhieuThu.Text = Utility.GetCode("PTHD", count + 1, 7);
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("PhieuThuHopDongBus.GetPhieuThuHopDongCount"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuHopDongBus.GetPhieuThuHopDongCount"));
            }
        }

        private string GetGenerateCode()
        {
            Result result = PhieuThuHopDongBus.GetPhieuThuHopDongCount();
            if (result.IsOK)
            {
                int count = Convert.ToInt32(result.QueryResult);
                return Utility.GetCode("PTHD", count + 1, 7);
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("PhieuThuHopDongBus.GetPhieuThuHopDongCount"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuHopDongBus.GetPhieuThuHopDongCount"));
                return string.Empty;
            }
        }

        private void InitData()
        {
            cboHinhThucThanhToan.SelectedIndex = 0;
            dtpkNgayThu.Value = DateTime.Now;
            OnDisplayHopDongList();
        }

        private void OnDisplayHopDongList()
        {
            Result result = CompanyContractBus.GetContractList();
            if (result.IsOK)
                cboMaHopDong.DataSource = result.QueryResult as DataTable;
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.GetContractList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetContractList"));
            }
        }

        private DataRow GetHopDong()
        {
            DataTable dt = cboMaHopDong.DataSource as DataTable;
            if (dt == null) return null;
            DataRow[] rows = dt.Select(string.Format("CompanyContractGUID='{0}'", _hopDongGUID));
            if (rows != null && rows.Length > 0)
                return rows[0];

            return null;
        }

        private void DisplayCongNoHopDong()
        {
            if (_hopDongGUID == string.Empty) return;

            Result result = PhieuThuHopDongBus.GetCongNoTheoHopDong(_hopDongGUID);
            if (result.IsOK)
            {
                double congNo = Convert.ToDouble(result.QueryResult);
                numCongNo.Value = (Decimal)congNo;
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("PhieuThuHopDongBus.GetCongNoTheoHopDong"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuHopDongBus.GetCongNoTheoHopDong"));
            }
        }

        private void DisplayInfo(DataRow drPhieuThu)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                txtMaPhieuThu.Text = drPhieuThu["MaPhieuThuHopDong"] as string;
                cboMaHopDong.SelectedValue = drPhieuThu["HopDongGUID"].ToString();
                dtpkNgayThu.Value = Convert.ToDateTime(drPhieuThu["NgayThu"]);
                txtTenKhachHang.Text = drPhieuThu["TenNguoiNop"].ToString();
                txtTenCongTy.Text = drPhieuThu["TenCongTy"] as string;
                txtDiaChi.Text = drPhieuThu["DiaChi"] as string;
                chkDaThuTien.Checked = Convert.ToBoolean(drPhieuThu["DaThuTien"]);
                chkDaXuatHD.Checked = Convert.ToBoolean(drPhieuThu["IsExported"]);
                cboHinhThucThanhToan.SelectedIndex = Convert.ToInt32(drPhieuThu["HinhThucThanhToan"]);
                txtGhiChu.Text = drPhieuThu["Notes"] as string;

                _phieuThuHopDong.PhieuThuHopDongGUID = Guid.Parse(drPhieuThu["PhieuThuHopDongGUID"].ToString());

                if (drPhieuThu["CreatedDate"] != null && drPhieuThu["CreatedDate"] != DBNull.Value)
                    _phieuThuHopDong.CreatedDate = Convert.ToDateTime(drPhieuThu["CreatedDate"]);

                if (drPhieuThu["CreatedBy"] != null && drPhieuThu["CreatedBy"] != DBNull.Value)
                    _phieuThuHopDong.CreatedBy = Guid.Parse(drPhieuThu["CreatedBy"].ToString());

                if (drPhieuThu["UpdatedDate"] != null && drPhieuThu["UpdatedDate"] != DBNull.Value)
                    _phieuThuHopDong.UpdatedDate = Convert.ToDateTime(drPhieuThu["UpdatedDate"]);

                if (drPhieuThu["UpdatedBy"] != null && drPhieuThu["UpdatedBy"] != DBNull.Value)
                    _phieuThuHopDong.UpdatedBy = Guid.Parse(drPhieuThu["UpdatedBy"].ToString());

                if (drPhieuThu["DeletedDate"] != null && drPhieuThu["DeletedDate"] != DBNull.Value)
                    _phieuThuHopDong.DeletedDate = Convert.ToDateTime(drPhieuThu["DeletedDate"]);

                if (drPhieuThu["DeletedBy"] != null && drPhieuThu["DeletedBy"] != DBNull.Value)
                    _phieuThuHopDong.DeletedBy = Guid.Parse(drPhieuThu["DeletedBy"].ToString());

                _phieuThuHopDong.Status = Convert.ToByte(drPhieuThu["Status"]);

                Result result = PhieuThuHopDongBus.GetChiTietPhieuThuHopDong(_phieuThuHopDong.PhieuThuHopDongGUID.ToString());
                if (result.IsOK)
                {
                    DataTable dtChiTiet = result.QueryResult as DataTable;
                    if (dtChiTiet != null && dtChiTiet.Rows.Count > 0)
                    {
                        DataRow row = dtChiTiet.Rows[0];
                        txtDichVu.Text = row["DichVu"].ToString();
                        numThu.Value = (Decimal)Convert.ToDouble(row["ThanhTien"]);
                    }
                }
                else
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("PhieuThuHopDongBus.GetChiTietPhieuThuHopDong"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuHopDongBus.GetChiTietPhieuThuHopDong"));
                }
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (txtMaPhieuThu.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập mã phiếu thu.", IconType.Information);
                txtMaPhieuThu.Focus();
                return false;
            }

            if (cboMaHopDong.SelectedValue == null || cboMaHopDong.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn hợp đồng cần xuất phiếu thu.", IconType.Information);
                cboMaHopDong.Focus();
                return false;
            }

            if (txtTenKhachHang.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập tên khách hàng.", IconType.Information);
                txtTenKhachHang.Focus();
                return false;
            }

            //string phieuThuHopDongGUID = _isNew ? string.Empty : _phieuThuHopDong.PhieuThuHopDongGUID.ToString();
            //Result result = PhieuThuHopDongBus.CheckPhieuThuHopDongExistCode(phieuThuHopDongGUID, txtMaPhieuThu.Text);

            //if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            //{
            //    if (result.Error.Code == ErrorCode.EXIST)
            //    {
            //        MsgBox.Show(this.Text, "Mã phiếu thu này đã tồn tại rồi. Vui lòng nhập mã khác.", IconType.Information);
            //        txtMaPhieuThu.Focus();
            //        return false;
            //    }
            //}
            //else
            //{
            //    MsgBox.Show(this.Text, result.GetErrorAsString("PhieuThuHopDongBus.CheckPhieuThuHopDongExistCode"), IconType.Error);
            //    return false;
            //}

            if (txtDichVu.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập dịch vụ cần xuất phiếu thu.", IconType.Information);
                txtDichVu.Focus();
                return false;
            }

            if (numThu.Value <= 0)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập số tiền cần thu.", IconType.Information);
                numThu.Focus();
                return false;
            }

            return true;
        }

        private void SaveInfoAsThread()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSaveInfoProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnSaveInfo()
        {
            try
            {
                MethodInvoker method = delegate
                {
                    _phieuThuHopDong.MaPhieuThuHopDong = txtMaPhieuThu.Text;
                    _phieuThuHopDong.HopDongGUID = Guid.Parse(cboMaHopDong.SelectedValue.ToString());
                    _phieuThuHopDong.NgayThu = dtpkNgayThu.Value;
                    _phieuThuHopDong.TenNguoiNop = txtTenKhachHang.Text;
                    _phieuThuHopDong.TenCongTy = txtTenCongTy.Text;
                    _phieuThuHopDong.DiaChi = txtDiaChi.Text;
                    _phieuThuHopDong.Status = (byte)Status.Actived;
                    _phieuThuHopDong.ChuaThuTien = !chkDaThuTien.Checked;
                    _phieuThuHopDong.HinhThucThanhToan = (byte)cboHinhThucThanhToan.SelectedIndex;
                    _phieuThuHopDong.Notes = txtGhiChu.Text;

                    if (_isNew)
                    {
                        _phieuThuHopDong.CreatedDate = DateTime.Now;
                        _phieuThuHopDong.CreatedBy = Guid.Parse(Global.UserGUID);
                    }

                    List<ChiTietPhieuThuHopDong> addedList = new List<ChiTietPhieuThuHopDong>();
                    ChiTietPhieuThuHopDong ctpthd = new ChiTietPhieuThuHopDong();
                    ctpthd.CreatedDate = DateTime.Now;
                    ctpthd.CreatedBy = Guid.Parse(Global.UserGUID);
                    ctpthd.DichVu = txtDichVu.Text;
                    ctpthd.DonGia = (double)numThu.Value;
                    ctpthd.SoLuong = 1;
                    ctpthd.Giam = 0;
                    ctpthd.ThanhTien = ctpthd.DonGia;
                    ctpthd.Status = (byte)Status.Actived;
                    addedList.Add(ctpthd);

                    string maPhieuThu = GetGenerateCode();
                    if (maPhieuThu == string.Empty) return;
                    _phieuThuHopDong.MaPhieuThuHopDong = maPhieuThu;

                    Result result = PhieuThuHopDongBus.InsertPhieuThuHopDong(_phieuThuHopDong, addedList);

                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("PhieuThuHopDongBus.InsertPhieuThuHopDong"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuHopDongBus.InsertPhieuThuHopDong"));
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    }
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnExportInvoice()
        {
            List<DataRow> phieuThuHopDongList = new List<DataRow>();
            phieuThuHopDongList.Add(_drPhieuThu);
            dlgHoaDonHopDong dlg = new dlgHoaDonHopDong(phieuThuHopDongList);
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _isExportedInvoice = true;
                btnExportInvoice.Enabled = false;
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddPhieuThuHopDong_Load(object sender, EventArgs e)
        {
            InitData();

            if (!_isNew)
            {
                if (Global.StaffType != StaffType.Admin)
                {
                    //btnOK.Enabled = false;
                    chkDaThuTien.Enabled = false;
                    chkDaXuatHD.Enabled = false;
                    cboHinhThucThanhToan.Enabled = false;
                }
                else
                {
                    chkDaThuTien.Enabled = true;
                    chkDaXuatHD.Enabled = true;
                    cboHinhThucThanhToan.Enabled = true;
                }

                DisplayInfo(_drPhieuThu);
                txtMaPhieuThu.ReadOnly = true;
                cboMaHopDong.Enabled = false;
                btnChonHopDong.Enabled = false;
                txtTenKhachHang.ReadOnly = true;
                txtTenCongTy.ReadOnly = true;
                txtDiaChi.ReadOnly = true;
                txtDichVu.ReadOnly = true;
                numCongNo.Enabled = false;
                numThu.Enabled = false;
                //btnOK.Enabled = false;
                dtpkNgayThu.Enabled = false;
                //chkDaThuTien.Enabled = false;
                //txtGhiChu.ReadOnly = true;
            }

            UpdateGUI();
        }

        private void cboMaHopDong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaHopDong.SelectedValue == null || cboMaHopDong.SelectedValue == DBNull.Value) return;

            _hopDongGUID = cboMaHopDong.SelectedValue.ToString();

            DataRow row = GetHopDong();
            if (row != null)
                txtTenCongTy.Text = row["TenCty"].ToString();
            else
                txtTenCongTy.Text = string.Empty;

            DisplayCongNoHopDong();
        }

        private void btnChonHopDong_Click(object sender, EventArgs e)
        {
            DataTable dt = cboMaHopDong.DataSource as DataTable;
            if (dt == null) return;
            dlgSelectSingleHopDong dlg = new dlgSelectSingleHopDong(dt);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                cboMaHopDong.SelectedValue = dlg.HopDongGUID;
            }
        }

        private void btnExportInvoice_Click(object sender, EventArgs e)
        {
            OnExportInvoice();
        }

        private void dlgAddPhieuThuHopDong_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (_isNew)
                {
                    if (CheckInfo())
                        SaveInfoAsThread();
                    else
                        e.Cancel = true;
                }
                else //if (Global.StaffType == StaffType.Admin)
                {
                    Result result = PhieuThuHopDongBus.CapNhatTrangThaiPhieuThu(_phieuThuHopDong.PhieuThuHopDongGUID.ToString(), 
                        chkDaXuatHD.Checked, chkDaThuTien.Checked, (byte)cboHinhThucThanhToan.SelectedIndex, txtGhiChu.Text);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhieuThuHopDongBus.CapNhatTrangThaiPhieuThu"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuHopDongBus.CapNhatTrangThaiPhieuThu"));
                        e.Cancel = true;
                    }
                    else
                    {
                        _drPhieuThu["IsExported"] = chkDaXuatHD.Checked;
                        _drPhieuThu["DaThuTien"] = chkDaThuTien.Checked;
                        _drPhieuThu["HinhThucThanhToan"] = (byte)cboHinhThucThanhToan.SelectedIndex;
                        _drPhieuThu["HinhThucThanhToanStr"] = cboHinhThucThanhToan.Text;
                        _drPhieuThu["Notes"] = txtGhiChu.Text;
                    }
                }
            }
            else
            {
                if (_isNew)
                {
                    if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin phiếu thu hợp đồng ?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (CheckInfo())
                        {
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            SaveInfoAsThread();
                        }
                        else
                            e.Cancel = true;
                    }
                }
            }
        }
        #endregion

        #region Working Thread
        private void OnSaveInfoProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnSaveInfo();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion
    }
}
