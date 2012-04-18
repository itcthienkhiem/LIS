﻿using System;
using System.Collections.Generic;
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
    public partial class uKetQuaXetNghiem_Hitachi917 : uBase
    {
        #region Members
        private bool _isFromDateToDate = true;
        private string _tenBenhNhan = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        #endregion

        #region Constructor
        public uKetQuaXetNghiem_Hitachi917()
        {
            InitializeComponent();
            dtpkDenNgay.Value = DateTime.Now;
            dtpkTuNgay.Value = DateTime.Now;
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnDelete.Enabled = AllowDelete;
            btnEdit.Enabled = AllowEdit;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();

                _isFromDateToDate = raTuNgayToiNgay.Checked;
                _fromDate = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                _toDate = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
                _tenBenhNhan = txtTenBenhNhan.Text;

                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayKetQuaXetNghiemListProc));
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

        private void OnDisplayKetQuaXetNghiemList()
        {
            Result result = XetNghiem_Hitachi917Bus.GetKetQuaXetNghiemList(_isFromDateToDate, _fromDate, _toDate, _tenBenhNhan);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgXetNghiem.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiem_Hitachi917Bus.GetKetQuaXetNghiemList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_Hitachi917Bus.GetKetQuaXetNghiemList"));
            }
        }

        private void OnDisplayChiTietKetQuaXetNghiem(string ketQuaXetNghiemGUID)
        {
            Result result = XetNghiem_Hitachi917Bus.GetChiTietKetQuaXetNghiem(ketQuaXetNghiemGUID);
            if (result.IsOK)
                dgChiTietKQXN.DataSource = result.QueryResult;
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiem_Hitachi917Bus.GetChiTietKetQuaXetNghiem"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_Hitachi917Bus.GetChiTietKetQuaXetNghiem"));
            }
        }

        private void OnCapNhatBenhNhan()
        {
            if (dgXetNghiem.SelectedRows == null || dgXetNghiem.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 xét nghiệm để cập nhật bệnh nhân.", IconType.Information);
                return;
            }

            DataRow row = (dgXetNghiem.SelectedRows[0].DataBoundItem as DataRowView).Row;
            if (row == null) return;

            dlgSelectPatient dlg = new dlgSelectPatient();
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                DataRow patientRow = dlg.PatientRow;
                if (patientRow != null)
                {
                    KetQuaXetNghiem_Hitachi917 kqxn = new KetQuaXetNghiem_Hitachi917();
                    kqxn.KQXN_Hitachi917GUID = Guid.Parse(row["KQXN_Hitachi917GUID"].ToString());
                    kqxn.PatientGUID = Guid.Parse(patientRow["PatientGUID"].ToString());
                    Result result = XetNghiem_Hitachi917Bus.UpdatePatient(kqxn);
                    if (result.IsOK)
                    {
                        row["PatientGUID"] = patientRow["PatientGUID"];
                        row["FileNum"] = patientRow["FileNum"];
                        row["FullName"] = patientRow["FullName"];
                        row["DobStr"] = patientRow["DobStr"];
                        row["GenderAsStr"] = patientRow["GenderAsStr"];
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiem_Hitachi917Bus.UpdatePatient"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_Hitachi917Bus.UpdatePatient"));
                    }
                }
            }
        }

        private void OnDeleteKQXN()
        {
            List<string> deletedKQXNList = new List<string>();
            DataTable dt = dgXetNghiem.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedKQXNList.Add(row["KQXN_Hitachi917GUID"].ToString());
                }
            }

            if (deletedKQXNList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những xét nghiệm bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = XetNghiem_Hitachi917Bus.DeleteXetNghiem(deletedKQXNList);
                    if (result.IsOK)
                    {
                        DisplayAsThread();
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiem_Hitachi917Bus.DeleteXetNghiem"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_Hitachi917Bus.DeleteXetNghiem"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những xét nghiệm cần xóa.", IconType.Information);
        }

        
        #endregion

        #region Window Event Handlers
        private void raTuNgayToiNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpkTuNgay.Enabled = raTuNgayToiNgay.Checked;
            dtpkDenNgay.Enabled = raTuNgayToiNgay.Checked;
            txtTenBenhNhan.ReadOnly = raTuNgayToiNgay.Checked;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DisplayAsThread();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnCapNhatBenhNhan();
        }

        private void dgXetNghiem_DoubleClick(object sender, EventArgs e)
        {
            if (!AllowEdit) return;
            OnCapNhatBenhNhan();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteKQXN();
        }

        private void dtpkTuNgay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                DisplayAsThread();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgXetNghiem.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void chkCTKQXNChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgChiTietKQXN.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void dgXetNghiem_SelectionChanged(object sender, EventArgs e)
        {
            if (dgXetNghiem.SelectedRows == null || dgXetNghiem.SelectedRows.Count <= 0)
            {
                dgChiTietKQXN.DataSource = null;
                return;
            }

            DataRow row = (dgXetNghiem.SelectedRows[0].DataBoundItem as DataRowView).Row;
            if (row == null)
            {
                dgChiTietKQXN.DataSource = null;
                return;
            }

            OnDisplayChiTietKetQuaXetNghiem(row["KQXN_Hitachi917GUID"].ToString());
        }
        #endregion

        #region Working Thread
        private void OnDisplayKetQuaXetNghiemListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayKetQuaXetNghiemList();
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
