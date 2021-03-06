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
using MM.Common;
using MM.Bussiness;
using MM.Databasae;
using MM.Dialogs;

namespace MM.Controls
{
    public partial class uMapMauHoSoVoiDichVu : uBase
    {
        #region Members
        private string _mauHoSoGUID = string.Empty;
        private string _hopDongGUID = string.Empty;
        #endregion

        #region Constructor
        public uMapMauHoSoVoiDichVu()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowEdit;
            btnDelete.Enabled = AllowEdit;
        }

        private void OnDisplayHopDongList()
        {
            Result result = CompanyContractBus.GetContractList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    cboMaHopDong.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyContractBus.GetNoCompletedContractList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetNoCompletedContractList"));
            }
        }

        private string GetTenHopDong(string hopDongGUID)
        {
            string tenHopDong = string.Empty;

            DataTable dt = cboMaHopDong.DataSource as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] rows = dt.Select(string.Format("CompanyContractGUID='{0}'", hopDongGUID));
                if (rows == null || rows.Length <= 0) return string.Empty;

                tenHopDong = rows[0]["ContractName"].ToString();
            }

            return tenHopDong;
        }

        public void DisplayInfo()
        {
            UpdateGUI();

            OnDisplayHopDongList();

            Result result = MauHoSoBus.GetMauHoSoList();
            if (result.IsOK)
            {
                ClearData();
                dgMauHoSo.DataSource = result.QueryResult;
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("MauHoSoBus.GetMauHoSoList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("MauHoSoBus.GetMauHoSoList"));
            }
        }

        public void ClearData()
        {
            ClearDetailData();

            DataTable dt = dgMauHoSo.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgService.DataSource = null;
            }
        }

        private void ClearDetailData()
        {
            DataTable dt = dgService.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgService.DataSource = null;
            }
        }

        private void DisplayChiTietMauHoSo(string mauHoSoGUID)
        {
            Result result = MauHoSoBus.GetChiTietMauHoSoList(mauHoSoGUID, _hopDongGUID);
            if (result.IsOK)
            {
                ClearDetailData();
                dgService.DataSource = result.QueryResult;
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("MauHoSoBus.GetChiTietMauHoSoList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("MauHoSoBus.GetChiTietMauHoSoList"));
            }
        }

        private List<string> GetAddedServices()
        {
            List<string> addedServices = new List<string>();
            DataTable dt = dgService.DataSource as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    addedServices.Add(row["ServiceGUID"].ToString());
                }
            }

            return addedServices;
        }


        private void OnAddService()
        {
            if (dgMauHoSo.SelectedRows == null || dgMauHoSo.SelectedRows.Count <= 0) return;
            if (cboMaHopDong.SelectedValue == null) return;

            List<string> addedServices = GetAddedServices();
            dlgServices dlg = new dlgServices(addedServices, _hopDongGUID);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                List<DataRow> rows = dlg.Services;
                Result result = MauHoSoBus.AddServices(_mauHoSoGUID, _hopDongGUID, rows);
                if (result.IsOK)
                {
                    DisplayChiTietMauHoSo(_mauHoSoGUID);
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("MauHoSoBus.AddServices"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("MauHoSoBus.AddServices"));
                }
            }
        }

        private void OnDeleteService()
        {
            if (dgMauHoSo.SelectedRows == null || dgMauHoSo.SelectedRows.Count <= 0) return;
            if (cboMaHopDong.SelectedValue == null) return;

            List<string> keys = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgService.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    keys.Add(row["ServiceGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (keys.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những dịch vụ mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = MauHoSoBus.DeleteServices(_mauHoSoGUID, _hopDongGUID, keys);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("MauHoSoBus.DeleteServices"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("MauHoSoBus.DeleteServices"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những dịch vụ cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgService.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddService();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteService();
        }

        private void dgMauHoSo_SelectionChanged(object sender, EventArgs e)
        {
            if (dgMauHoSo.SelectedRows == null || dgMauHoSo.SelectedRows.Count <= 0) return;
            DataRow row = (dgMauHoSo.SelectedRows[0].DataBoundItem as DataRowView).Row;
            _mauHoSoGUID = row["MauHoSoGUID"].ToString();

            if (cboMaHopDong.SelectedValue == null || cboMaHopDong.SelectedValue == DBNull.Value) return;
            DisplayChiTietMauHoSo(_mauHoSoGUID);
        }

        private void cboMaHopDong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaHopDong.SelectedValue == null || cboMaHopDong.SelectedValue == DBNull.Value) return;
            _hopDongGUID = cboMaHopDong.SelectedValue.ToString();
            txtTenHopDong.Text = GetTenHopDong(_hopDongGUID);

            if (dgMauHoSo.SelectedRows == null || dgMauHoSo.SelectedRows.Count <= 0) return;
            DisplayChiTietMauHoSo(_mauHoSoGUID);
        }
        #endregion
    }
}
