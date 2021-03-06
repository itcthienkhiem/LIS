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
using MM.Dialogs;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;
using MM.Exports;

namespace MM.Controls
{
    public partial class uServicesList : uBase
    {
        #region Members
        private DataTable _dtTemp = null;
        private Dictionary<string, DataRow> _dictServices = new Dictionary<string,DataRow>();
        private string _name = string.Empty;
        #endregion

        #region Constructor
        public uServicesList()
        {
            InitializeComponent();
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
            priceDataGridViewTextBoxColumn.Visible = Global.AllowShowServiePrice;
            btnExportExcel.Enabled = AllowExport;

            addToolStripMenuItem.Enabled = AllowAdd;
            editToolStripMenuItem.Enabled = AllowEdit;
            deleteToolStripMenuItem.Enabled = AllowDelete;
            exportExcelToolStripMenuItem.Enabled = AllowExport;
        }

        public void ClearData()
        {
            DataTable dt = dgService.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
            }

            dgService.DataSource = null;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                _name = txtTenDichVu.Text;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayServicesListProc));
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

        public override void SearchAsThread()
        {
            try
            {
                chkChecked.Checked = false;
                _name = txtTenDichVu.Text;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSearchProc));
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnDisplayServicesList()
        {
            lock (ThisLock)
            {
                Result result = ServicesBus.GetServicesList(_name);
                if (result.IsOK)
                {
                    dgService.Invoke(new MethodInvoker(delegate()
                    {
                        ClearData();

                        DataTable dt = result.QueryResult as DataTable;
                        if (_dtTemp == null) _dtTemp = dt.Clone();
                        UpdateChecked(dt);
                        dgService.DataSource = dt;
                    }));
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServicesBus.GetServicesList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ServicesBus.GetServicesList"));
                }
            }
        }

        private void UpdateChecked(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                string key = row["ServiceGUID"].ToString();
                if (_dictServices.ContainsKey(key))
                    row["Checked"] = true;
            }
        }

        private void OnAddService()
        {
            dlgAddServices dlg = new dlgAddServices();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                SearchAsThread();
            }
        }

        private void OnEditService()
        {
            if (dgService.SelectedRows == null || dgService.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 dịch vụ.", IconType.Information);
                return;
            }
            
            DataRow drService = (dgService.SelectedRows[0].DataBoundItem as DataRowView).Row;
            if (drService == null) return;
            dlgAddServices dlg = new dlgAddServices(drService);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SearchAsThread();
            }
        }

        private void OnDeleteService()
        {
            if (_dictServices == null) return;

            List<DataRow> checkedRows = _dictServices.Values.ToList<DataRow>();

            if (checkedRows.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những dịch vụ mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    List<string> noteList = new List<string>();
                    List<string> deletedServiceList = new List<string>();

                    foreach (DataRow row in checkedRows)
                    {
                        string serviceCode = row["Code"].ToString();
                        string serviceGUID = row["ServiceGUID"].ToString();

                        dlgLyDoXoa dlg = new dlgLyDoXoa(serviceCode, 2);
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            noteList.Add(dlg.Notes);
                            deletedServiceList.Add(serviceGUID);
                        }
                    }

                    if (deletedServiceList.Count > 0)
                    {
                        Result result = ServicesBus.DeleteServices(deletedServiceList, noteList);
                        if (result.IsOK)
                        {
                            DataTable dt = dgService.DataSource as DataTable;
                            if (dt == null || dt.Rows.Count <= 0) return;
                            foreach (string key in deletedServiceList)
                            {
                                DataRow[] rows = dt.Select(string.Format("ServiceGUID='{0}'", key));
                                if (rows == null || rows.Length <= 0) continue;
                                dt.Rows.Remove(rows[0]);
                            }

                            _dictServices.Clear();
                            _dtTemp.Rows.Clear();
                        }
                        else
                        {
                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServicesBus.DeleteServices"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("ServicesBus.DeleteServices"));
                        }
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những dịch vụ cần xóa.", IconType.Information);
        }

        private void OnExportExcel()
        {
            if (_dictServices == null) return;

            List<DataRow> checkedRows = _dictServices.Values.ToList();
            if (checkedRows.Count <= 0)
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những dịch vụ cần xuất excel.", IconType.Information);
            else
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Export Excel";
                dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    ExportExcel.ExportDanhSachDichVuToExcel(dlg.FileName, checkedRows);
                }
            }
        }
        #endregion

        #region Window Event Handlers
        private void dgService_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;
            
            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgService.Rows[e.RowIndex].Cells[0];
            DataRow row = (dgService.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string serviceGUID = row["ServiceGUID"].ToString();
            bool isChecked = Convert.ToBoolean(cell.EditingCellFormattedValue);

            if (isChecked)
            {
                if (!_dictServices.ContainsKey(serviceGUID))
                {
                    _dtTemp.ImportRow(row);
                    _dictServices.Add(serviceGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                }
            }
            else
            {
                if (_dictServices.ContainsKey(serviceGUID))
                {
                    _dictServices.Remove(serviceGUID);

                    DataRow[] rows = _dtTemp.Select(string.Format("ServiceGUID='{0}'", serviceGUID));
                    if (rows != null && rows.Length > 0)
                        _dtTemp.Rows.Remove(rows[0]);
                }
            }
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgService.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
                string serviceGUID = row["ServiceGUID"].ToString();

                if (chkChecked.Checked)
                {
                    if (!_dictServices.ContainsKey(serviceGUID))
                    {
                        _dtTemp.ImportRow(row);
                        _dictServices.Add(serviceGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                    }
                }
                else
                {
                    if (_dictServices.ContainsKey(serviceGUID))
                    {
                        _dictServices.Remove(serviceGUID);

                        DataRow[] rows = _dtTemp.Select(string.Format("ServiceGUID='{0}'", serviceGUID));
                        if (rows != null && rows.Length > 0)
                            _dtTemp.Rows.Remove(rows[0]);
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddService();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditService();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteService();
        }

        private void dgService_DoubleClick(object sender, EventArgs e)
        {
            if (!AllowEdit) return;
            OnEditService();
        }

        private void txtTenDichVu_TextChanged(object sender, EventArgs e)
        {
            StartTimer();
        }

        private void txtTenDichVu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgService.Focus();

                if (dgService.SelectedRows != null && dgService.SelectedRows.Count > 0)
                {
                    int index = dgService.SelectedRows[0].Index;
                    if (index < dgService.RowCount - 1)
                    {
                        index++;
                        dgService.CurrentCell = dgService[1, index];
                        dgService.Rows[index].Selected = true;
                    }
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                dgService.Focus();

                if (dgService.SelectedRows != null && dgService.SelectedRows.Count > 0)
                {
                    int index = dgService.SelectedRows[0].Index;
                    if (index > 0)
                    {
                        index--;
                        dgService.CurrentCell = dgService[1, index];
                        dgService.Rows[index].Selected = true;
                    }
                }
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            OnExportExcel();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnAddService();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnEditService();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDeleteService();
        }

        private void exportExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnExportExcel();
        }
        #endregion

        #region Working Thread
        private void OnDisplayServicesListProc(object state)
        {
            try
            {
                OnDisplayServicesList();
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

        private void OnSearchProc(object state)
        {
            try
            {
                OnDisplayServicesList();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }
        #endregion
    }
}
