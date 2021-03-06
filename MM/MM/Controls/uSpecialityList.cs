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
using MM.Bussiness;
using MM.Databasae;
using MM.Dialogs;

namespace MM.Controls
{
    public partial class uSpecialityList : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uSpecialityList()
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

            addToolStripMenuItem.Enabled = AllowAdd;
            editToolStripMenuItem.Enabled = AllowEdit;
            deleteToolStripMenuItem.Enabled = AllowDelete;
        }

        public void ClearData()
        {
            DataTable dt = dgSpeciality.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgSpeciality.DataSource = null;
            }
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplaySpecialityListProc));
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

        private void OnDisplaySpecialityList()
        {
            Result result = SpecialityBus.GetSpecialityList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    dgSpeciality.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("SpecialityBus.GetSpecialityList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("SpecialityBus.GetSpecialityList"));
            }
        }

        private void OnAddSpeciality()
        {
            dlgAddSpeciality dlg = new dlgAddSpeciality();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable dt = dgSpeciality.DataSource as DataTable;
                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["SpecialityGUID"] = dlg.Speciality.SpecialityGUID.ToString();
                newRow["Code"] = dlg.Speciality.Code;
                newRow["Name"] = dlg.Speciality.Name;
                newRow["Description"] = dlg.Speciality.Description;

                if (dlg.Speciality.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.Speciality.CreatedDate;

                if (dlg.Speciality.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.Speciality.CreatedBy.ToString();

                if (dlg.Speciality.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.Speciality.UpdatedDate;

                if (dlg.Speciality.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.Speciality.UpdatedBy.ToString();

                if (dlg.Speciality.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.Speciality.DeletedDate;

                if (dlg.Speciality.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.Speciality.DeletedBy.ToString();

                newRow["Status"] = dlg.Speciality.Status;
                dt.Rows.Add(newRow);
                //SelectLastedRow();
            }
        }

        private void SelectLastedRow()
        {
            dgSpeciality.CurrentCell = dgSpeciality[1, dgSpeciality.RowCount - 1];
            dgSpeciality.Rows[dgSpeciality.RowCount - 1].Selected = true;
        }

        private void OnEditSpeciality()
        {
            if (dgSpeciality.SelectedRows == null || dgSpeciality.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 chuyên khoa.", IconType.Information);
                return;
            }

            DataRow drSpec = (dgSpeciality.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddSpeciality dlg = new dlgAddSpeciality(drSpec);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                drSpec["Code"] = dlg.Speciality.Code;
                drSpec["Name"] = dlg.Speciality.Name;
                drSpec["Description"] = dlg.Speciality.Description;

                if (dlg.Speciality.CreatedDate.HasValue)
                    drSpec["CreatedDate"] = dlg.Speciality.CreatedDate;

                if (dlg.Speciality.CreatedBy.HasValue)
                    drSpec["CreatedBy"] = dlg.Speciality.CreatedBy.ToString();

                if (dlg.Speciality.UpdatedDate.HasValue)
                    drSpec["UpdatedDate"] = dlg.Speciality.UpdatedDate;

                if (dlg.Speciality.UpdatedBy.HasValue)
                    drSpec["UpdatedBy"] = dlg.Speciality.UpdatedBy.ToString();

                if (dlg.Speciality.DeletedDate.HasValue)
                    drSpec["DeletedDate"] = dlg.Speciality.DeletedDate;

                if (dlg.Speciality.DeletedBy.HasValue)
                    drSpec["DeletedBy"] = dlg.Speciality.DeletedBy.ToString();

                drSpec["Status"] = dlg.Speciality.Status;
            }
        }

        private void OnDeleteSpeciality()
        {
            List<string> deletedSpecList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgSpeciality.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedSpecList.Add(row["SpecialityGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedSpecList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những chuyên khoa mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = SpecialityBus.DeleteSpeciality(deletedSpecList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("SpecialityBus.DeleteSpeciality"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("SpecialityBus.DeleteSpeciality"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những chuyên khoa cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgSpeciality.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddSpeciality();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditSpeciality();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteSpeciality();
        }

        private void dgSpeciality_DoubleClick(object sender, EventArgs e)
        {
            if (!AllowEdit) return;
            OnEditSpeciality();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnAddSpeciality();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnEditSpeciality();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDeleteSpeciality();
        }
        #endregion

        #region Working Thread
        private void OnDisplaySpecialityListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplaySpecialityList();
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
