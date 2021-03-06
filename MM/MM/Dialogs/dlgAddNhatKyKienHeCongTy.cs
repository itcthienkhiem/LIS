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
using MM.Databasae;
using MM.Bussiness;

namespace MM.Dialogs
{
    public partial class dlgAddNhatKyKienHeCongTy : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private NhatKyLienHeCongTy _nhatKyLienHeCongTy = new NhatKyLienHeCongTy();
        private DataRow _drNhatKyLienHeCongTy = null;
        private bool _isView = false;
        #endregion

        #region Constructor
        public dlgAddNhatKyKienHeCongTy()
        {
            InitializeComponent();
        }

        public dlgAddNhatKyKienHeCongTy(DataRow drNhatKyLienHeCongTy)
        {
            InitializeComponent();
            _drNhatKyLienHeCongTy = drNhatKyLienHeCongTy;
            _isNew = false;
            dtpkNgayGioLienHe.Enabled = false;
            this.Text = "Sua nhat ky lien he cong ty";
        }
        #endregion

        #region Properties
        public NhatKyLienHeCongTy NhatKyLienHeCongTy
        {
            get { return _nhatKyLienHeCongTy; }
            set { _nhatKyLienHeCongTy = value; }
        }
        #endregion

        #region UI Command
        private void DisplayCongTyLienHeList()
        {
            dtpkNgayGioLienHe.Value = DateTime.Now;
            Result result = NhatKyLienHeCongTyBus.GetCongTyLienHeList();
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    cboCongTyLienHe.Items.Add(row["CongTyLienHe"].ToString());
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("NhatKyLienHeCongTyBus.GetCongTyLienHeList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("NhatKyLienHeCongTyBus.GetCongTyLienHeList"));
            }
        }

        private void DisplayInfo(DataRow drNhatKyLienHeCongTy)
        {
            try
            {
                dtpkNgayGioLienHe.Value = Convert.ToDateTime(drNhatKyLienHeCongTy["NgayGioLienHe"]);
                cboCongTyLienHe.Text = drNhatKyLienHeCongTy["CongTyLienHe"] as string;
                txtNoiDungLienHe.Text = drNhatKyLienHeCongTy["NoiDungLienHe"] as string;
                txtNguoiLienHe.Text = drNhatKyLienHeCongTy["TenNguoiLienHe"] as string;
                txtSoDienThoaiLienHe.Text = drNhatKyLienHeCongTy["SoDienThoaiLienHe"] as string;
                txtEmail.Text = drNhatKyLienHeCongTy["Email"] as string;
                txtDiaChi.Text = drNhatKyLienHeCongTy["DiaChi"] as string;
                txtSoNguoiKham.Text = drNhatKyLienHeCongTy["SoNguoiKham"].ToString();
                if (drNhatKyLienHeCongTy["ThangKham"] != null && drNhatKyLienHeCongTy["ThangKham"] != DBNull.Value)
                    txtThangKham.Text = drNhatKyLienHeCongTy["ThangKham"].ToString();

                chkHighlight.Checked = Convert.ToBoolean(drNhatKyLienHeCongTy["Highlight"]);

                _nhatKyLienHeCongTy.NhatKyLienHeCongTyGUID = Guid.Parse(drNhatKyLienHeCongTy["NhatKyLienHeCongTyGUID"].ToString());

                if (drNhatKyLienHeCongTy["CreatedDate"] != null && drNhatKyLienHeCongTy["CreatedDate"] != DBNull.Value)
                    _nhatKyLienHeCongTy.CreatedDate = Convert.ToDateTime(drNhatKyLienHeCongTy["CreatedDate"]);

                if (drNhatKyLienHeCongTy["CreatedBy"] != null && drNhatKyLienHeCongTy["CreatedBy"] != DBNull.Value)
                    _nhatKyLienHeCongTy.CreatedBy = Guid.Parse(drNhatKyLienHeCongTy["CreatedBy"].ToString());

                if (drNhatKyLienHeCongTy["UpdatedDate"] != null && drNhatKyLienHeCongTy["UpdatedDate"] != DBNull.Value)
                    _nhatKyLienHeCongTy.UpdatedDate = Convert.ToDateTime(drNhatKyLienHeCongTy["UpdatedDate"]);

                if (drNhatKyLienHeCongTy["UpdatedBy"] != null && drNhatKyLienHeCongTy["UpdatedBy"] != DBNull.Value)
                    _nhatKyLienHeCongTy.UpdatedBy = Guid.Parse(drNhatKyLienHeCongTy["UpdatedBy"].ToString());

                if (drNhatKyLienHeCongTy["DeletedDate"] != null && drNhatKyLienHeCongTy["DeletedDate"] != DBNull.Value)
                    _nhatKyLienHeCongTy.DeletedDate = Convert.ToDateTime(drNhatKyLienHeCongTy["DeletedDate"]);

                if (drNhatKyLienHeCongTy["DeletedBy"] != null && drNhatKyLienHeCongTy["DeletedBy"] != DBNull.Value)
                    _nhatKyLienHeCongTy.DeletedBy = Guid.Parse(drNhatKyLienHeCongTy["DeletedBy"].ToString());

                _nhatKyLienHeCongTy.Status = Convert.ToByte(drNhatKyLienHeCongTy["Status"]);

                string userGUID = drNhatKyLienHeCongTy["CreatedBy"].ToString();
                if (userGUID != Global.UserGUID)
                {
                    dtpkNgayGioLienHe.Enabled = false;
                    cboCongTyLienHe.Enabled = false;
                    txtNguoiLienHe.Enabled = false;
                    txtSoDienThoaiLienHe.Enabled = false;
                    txtSoNguoiKham.Enabled = false;
                    txtThangKham.Enabled = false;
                    txtNoiDungLienHe.Enabled = false;
                    txtDiaChi.Enabled = false;
                    txtEmail.Enabled = false;
                    btnOK.Enabled = false;
                    chkHighlight.Enabled = false;
                    _isView = true;
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
            if (cboCongTyLienHe.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập công ty liên hệ.", IconType.Information);
                cboCongTyLienHe.Focus();
                return false;
            }

            //if (txtNguoiLienHe.Text.Trim() == string.Empty)
            //{
            //    MsgBox.Show(this.Text, "Vui lòng nhập tên người liên hệ.", IconType.Information);
            //    txtNguoiLienHe.Focus();
            //    return false;
            //}

            if (txtSoDienThoaiLienHe.Text.Trim() == string.Empty && txtEmail.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập số điện thoại hoặc email liên hệ.", IconType.Information);
                txtSoDienThoaiLienHe.Focus();
                return false;
            }

            //if (txtThangKham.Text.Trim() == string.Empty)
            //{
            //    MsgBox.Show(this.Text, "Vui lòng nhập tháng khám.", IconType.Information);
            //    txtThangKham.Focus();
            //    return false;
            //}

            string nhatKyLienHeCongTyGUID = string.Empty;
            if (!_isNew) nhatKyLienHeCongTyGUID = _nhatKyLienHeCongTy.NhatKyLienHeCongTyGUID.ToString();

            Result result = NhatKyLienHeCongTyBus.CheckCongTyLienHeExist(cboCongTyLienHe.Text, nhatKyLienHeCongTyGUID);
            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, string.Format("Công ty: '{0}' đã liên hệ rồi. Vui lòng xem lại thông tin.", cboCongTyLienHe.Text), 
                        IconType.Information);

                    _isView = true;
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("NhatKyLienHeCongTyBus.CheckCongTyLienHeExist"), IconType.Error);
                return false;
            }

            nhatKyLienHeCongTyGUID = string.Empty;
            if (!_isNew) nhatKyLienHeCongTyGUID = _nhatKyLienHeCongTy.NhatKyLienHeCongTyGUID.ToString();

            result = NhatKyLienHeCongTyBus.CheckCongTyLienHeExist2(cboCongTyLienHe.Text, nhatKyLienHeCongTyGUID);
            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    string sChoice =string.Format("Công ty: '{0}' đã liên hệ rồi. Bạn có muốn tạo liên hệ với công ty này nữa không?", cboCongTyLienHe.Text);
                    if (MsgBox.Question(this.Text, sChoice) == DialogResult.Yes)
                    {
                        return true;
                    }

                    _isView = true;
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("NhatKyLienHeCongTyBus.CheckCongTyLienHeExist2"), IconType.Error);
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

                _nhatKyLienHeCongTy.Status = (byte)Status.Actived;

                if (_isNew)
                {
                    _nhatKyLienHeCongTy.CreatedDate = DateTime.Now;
                    _nhatKyLienHeCongTy.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _nhatKyLienHeCongTy.UpdatedDate = DateTime.Now;
                    _nhatKyLienHeCongTy.UpdatedBy = Guid.Parse(Global.UserGUID);
                    
                }

                MethodInvoker method = delegate
                {
                    _nhatKyLienHeCongTy.NgayGioLienHe = dtpkNgayGioLienHe.Value;
                    if (!_isNew)
                    {
                        int soNgay = DateTime.Now.Subtract(dtpkNgayGioLienHe.Value).Days;
                        int thang = soNgay / 30;
                        int ngay = soNgay % 30;

                        if (thang > 0)
                            _nhatKyLienHeCongTy.SoNgay = string.Format("{0} tháng {1} ngày", thang, ngay);
                        else
                            _nhatKyLienHeCongTy.SoNgay = string.Format("{0} ngày", ngay);
                    }

                    if (Global.StaffType != StaffType.Admin)
                        _nhatKyLienHeCongTy.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    else
                        _nhatKyLienHeCongTy.DocStaffGUID = null;

                    _nhatKyLienHeCongTy.CongTyLienHe = cboCongTyLienHe.Text;
                    _nhatKyLienHeCongTy.NoiDungLienHe = txtNoiDungLienHe.Text;
                    _nhatKyLienHeCongTy.Note = string.Empty;
                    _nhatKyLienHeCongTy.TenNguoiLienHe = txtNguoiLienHe.Text;
                    _nhatKyLienHeCongTy.SoDienThoaiLienHe = txtSoDienThoaiLienHe.Text;
                    _nhatKyLienHeCongTy.SoNguoiKham = txtSoNguoiKham.Text;
                    _nhatKyLienHeCongTy.DiaChi = txtDiaChi.Text;
                    _nhatKyLienHeCongTy.Email = txtEmail.Text;
                    _nhatKyLienHeCongTy.ThangKham = txtThangKham.Text;
                    _nhatKyLienHeCongTy.Highlight = chkHighlight.Checked;

                    Result result = NhatKyLienHeCongTyBus.InsertNhatKyLienHeCongTy(_nhatKyLienHeCongTy);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("NhatKyLienHeCongTyBus.InsertNhatKyLienHeCongTy"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("NhatKyLienHeCongTyBus.InsertNhatKyLienHeCongTy"));
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
        #endregion

        #region Window Event Handlers
        private void dlgAddNhatKyKienHeCongTy_Load(object sender, EventArgs e)
        {
            DisplayCongTyLienHeList();
            if (!_isNew) DisplayInfo(_drNhatKyLienHeCongTy);
        }

        private void dlgAddNhatKyKienHeCongTy_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else if (!_isView)
                    e.Cancel = true;
            }
            else if (!_isView)
            {
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu nhật ký liên hệ công ty ?") == System.Windows.Forms.DialogResult.Yes)
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
