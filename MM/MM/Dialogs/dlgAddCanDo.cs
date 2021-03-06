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
using MM.Bussiness;
using MM.Common;
using MM.Databasae;

namespace MM.Dialogs
{
    public partial class dlgAddCanDo : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private string _patientGUID = string.Empty;
        private CanDo _canDo = new CanDo();
        private DataRow _drCanDo = null;
        private bool _allowEdit = true;
        #endregion

        #region Constructor
        public dlgAddCanDo(string patientGUID)
        {
            InitializeComponent();
            _patientGUID = patientGUID;
        }

        public dlgAddCanDo(string patientGUID, DataRow drCanDo, bool allowEdit)
        {
            InitializeComponent();
            _isNew = false;
            _allowEdit = allowEdit;
            this.Text = "Sua can do";
            _patientGUID = patientGUID;
            _drCanDo = drCanDo;
        }
        #endregion

        #region Properties
        public CanDo CanDo
        {
            get { return _canDo; }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            dtpkNgayCanDo.Value = DateTime.Now;

            //DocStaff
            List<byte> staffTypes = new List<byte>();
            staffTypes.Add((byte)StaffType.DieuDuong);
            Result result = DocStaffBus.GetDocStaffList(staffTypes);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("DocStaffBus.GetDocStaffList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.GetDocStaffList"));
                return;
            }
            else
            {
                cboDocStaff.DataSource = result.QueryResult;
            }

            if (Global.StaffType == StaffType.DieuDuong)
            {
                cboDocStaff.SelectedValue = Global.UserGUID;
                cboDocStaff.Enabled = false;
            }
        }

        private void DisplayInfo(DataRow drCanDo)
        {
            try
            {
                _canDo.CanDoGuid = Guid.Parse(drCanDo["CanDoGuid"].ToString());
                dtpkNgayCanDo.Value = Convert.ToDateTime(drCanDo["NgayCanDo"]);
                cboDocStaff.SelectedValue = drCanDo["DocStaffGUID"].ToString();

                if (drCanDo["TimMach"] != null && drCanDo["TimMach"] != DBNull.Value)
                    txtTimMach.Text = drCanDo["TimMach"].ToString();

                if (drCanDo["HuyetAp"] != null && drCanDo["HuyetAp"] != DBNull.Value)
                    txtHuyetAp.Text = drCanDo["HuyetAp"].ToString();

                if (drCanDo["ChieuCao"] != null && drCanDo["ChieuCao"] != DBNull.Value)
                    txtChieuCao.Text = drCanDo["ChieuCao"].ToString();

                if (drCanDo["HoHap"] != null && drCanDo["HoHap"] != DBNull.Value)
                    txtHoHap.Text = drCanDo["HoHap"].ToString();

                if (drCanDo["ChieuCao"] != null && drCanDo["ChieuCao"] != DBNull.Value)
                    txtChieuCao.Text = drCanDo["ChieuCao"].ToString();

                if (drCanDo["CanNang"] != null && drCanDo["CanNang"] != DBNull.Value)
                    txtCanNang.Text = drCanDo["CanNang"].ToString();

                if (drCanDo["BMI"] != null && drCanDo["BMI"] != DBNull.Value)
                    txtBMI.Text = drCanDo["BMI"].ToString();

                if (drCanDo["MuMau"] != null && drCanDo["MuMau"] != DBNull.Value)
                    txtMuMau.Text = drCanDo["MuMau"].ToString();

                if (drCanDo["MatPhai"] != null && drCanDo["MatPhai"] != DBNull.Value)
                    txtMatPhai.Text = drCanDo["MatPhai"].ToString();

                if (drCanDo["MatTrai"] != null && drCanDo["MatTrai"] != DBNull.Value)
                    txtMatTrai.Text = drCanDo["MatTrai"].ToString();

                if (drCanDo["HieuChinh"] != null && drCanDo["HieuChinh"] != DBNull.Value)
                {
                    bool isHieuChinh = Convert.ToBoolean(drCanDo["HieuChinh"]);
                    raHieuChinh.Checked = isHieuChinh;
                }

                if (drCanDo["CreatedDate"] != null && drCanDo["CreatedDate"] != DBNull.Value)
                    _canDo.CreatedDate = Convert.ToDateTime(drCanDo["CreatedDate"]);

                if (drCanDo["CreatedBy"] != null && drCanDo["CreatedBy"] != DBNull.Value)
                    _canDo.CreatedBy = Guid.Parse(drCanDo["CreatedBy"].ToString());

                if (drCanDo["UpdatedDate"] != null && drCanDo["UpdatedDate"] != DBNull.Value)
                    _canDo.UpdatedDate = Convert.ToDateTime(drCanDo["UpdatedDate"]);

                if (drCanDo["UpdatedBy"] != null && drCanDo["UpdatedBy"] != DBNull.Value)
                    _canDo.UpdatedBy = Guid.Parse(drCanDo["UpdatedBy"].ToString());

                if (drCanDo["DeletedDate"] != null && drCanDo["DeletedDate"] != DBNull.Value)
                    _canDo.DeletedDate = Convert.ToDateTime(drCanDo["DeletedDate"]);

                if (drCanDo["DeletedBy"] != null && drCanDo["DeletedBy"] != DBNull.Value)
                    _canDo.DeletedBy = Guid.Parse(drCanDo["DeletedBy"].ToString());

                _canDo.Status = Convert.ToByte(drCanDo["Status"]);

                if (!_allowEdit)
                {
                    btnOK.Enabled = _allowEdit;
                    groupBox1.Enabled = _allowEdit;
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
            if (cboDocStaff.SelectedValue == null || cboDocStaff.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn người khám.", IconType.Information);
                cboDocStaff.Focus();
                return false;
            }

            return true;
        }

        private void OnSaveInfo()
        {
            try
            {
                if (_isNew)
                {
                    _canDo.CreatedDate = DateTime.Now;
                    _canDo.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _canDo.UpdatedDate = DateTime.Now;
                    _canDo.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                _canDo.PatientGUID = Guid.Parse(_patientGUID);
                _canDo.TimMach = txtTimMach.Text;
                _canDo.HuyetAp = txtHuyetAp.Text;
                _canDo.HoHap = txtHoHap.Text;
                _canDo.ChieuCao = txtChieuCao.Text;
                _canDo.CanNang = txtCanNang.Text;
                _canDo.BMI = txtBMI.Text;
                _canDo.MuMau = txtMuMau.Text;
                _canDo.MatPhai = txtMatPhai.Text;
                _canDo.MatTrai = txtMatTrai.Text;
                _canDo.HieuChinh = raHieuChinh.Checked;
                _canDo.CanDoKhac = string.Empty;

                MethodInvoker method = delegate
                {
                    _canDo.NgayCanDo = dtpkNgayCanDo.Value;
                    _canDo.DocStaffGUID = Guid.Parse(cboDocStaff.SelectedValue.ToString());

                    Result result = CanDoBus.InsertCanDo(_canDo);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("CanDoBus.InsertCanDo"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("CanDoBus.InsertCanDo"));
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

        private void CalculateBMI()
        {
            if (txtChieuCao.Text.Trim() == string.Empty || txtCanNang.Text.Trim() == string.Empty)
            {
                txtBMI.Text = string.Empty;
                return;
            }

            double canNang = 0;
            if (!Double.TryParse(txtCanNang.Text, out canNang))
            {
                txtBMI.Text = string.Empty;
                return;
            }

            double chieuCao = 0;
            if (!Double.TryParse(txtChieuCao.Text, out chieuCao))
            {
                txtBMI.Text = string.Empty;
                return;
            }

            chieuCao = chieuCao / 100;

            double BMI = canNang / (chieuCao * chieuCao);
            BMI = Math.Round(BMI, 2);
            txtBMI.Text = BMI.ToString();
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddCanDo_Load(object sender, EventArgs e)
        {
            InitData();
            if (!_isNew) DisplayInfo(_drCanDo);
        }


        private void dlgAddCanDo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
            }
            else if (_allowEdit)
            {
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin cân đo ?") == System.Windows.Forms.DialogResult.Yes)
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

        private void txtChieuCao_TextChanged(object sender, EventArgs e)
        {
            CalculateBMI();
        }

        private void txtCanNang_TextChanged(object sender, EventArgs e)
        {
            CalculateBMI();
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
