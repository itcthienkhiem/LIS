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
    public partial class uDocStaffList : uBase
    {
        #region Members
        private bool _isAscending = true;
        private int _type = 1; //0: Tất cả; 1: Chưa xóa; 2: Đã xóa
        #endregion

        #region Constructor
        public uDocStaffList()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            btnDelete.Enabled = AllowDelete && !raDaXoa.Checked;
            btnExportExcel.Enabled = AllowExport;

            addToolStripMenuItem.Enabled = AllowAdd;
            deleteToolStripMenuItem.Enabled = AllowDelete && !raDaXoa.Checked;
            exportExcelToolStripMenuItem.Enabled = AllowExport;
        }

        public void ClearData()
        {
            DataTable dt = dgDocStaff.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgDocStaff.DataSource = null;
            }
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;

                if (raTatCa.Checked) _type = 0;
                else if (raChuaXoa.Checked) _type = 1;
                else if (raDaXoa.Checked) _type = 2;

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

        private void OnDisplayDocStaffList()
        {
            Result result = DocStaffBus.GetDocStaffList(_type);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    dgDocStaff.DataSource = result.QueryResult;
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

        private void OnAddDocStaff()
        {
            dlgAddDocStaff dlg = new dlgAddDocStaff();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DisplayAsThread();
                //DataTable dt = dgDocStaff.DataSource as DataTable;
                //if (dt == null) return;
                //DataRow newRow = dt.NewRow();
                //newRow["Checked"] = false;
                //newRow["DocStaffGUID"] = dlg.DocStaff.DocStaffGUID.ToString();
                //newRow["ContactGUID"] = dlg.Contact.ContactGUID.ToString();
                //newRow["FullName"] = dlg.Contact.FullName;
                //newRow["SurName"] = dlg.Contact.SurName;
                //newRow["MiddleName"] = dlg.Contact.MiddleName;
                //newRow["FirstName"] = dlg.Contact.FirstName;
                //newRow["KnownAs"] = dlg.Contact.KnownAs;
                //newRow["PreferredName"] = dlg.Contact.PreferredName;
                //newRow["Gender"] = dlg.Contact.Gender;

                //if (dlg.Contact.Gender == 0) newRow["GenderAsStr"] = "Nam";//dlg.Contact.Gender == 0 ? "Nam" : "Nữ";
                //else if (dlg.Contact.Gender == 1) newRow["GenderAsStr"] = "Nữ";
                //else newRow["GenderAsStr"] = "Không xác định";

                //newRow["DobStr"] = dlg.Contact.DobStr;
                //newRow["IdentityCard"] = dlg.Contact.IdentityCard;
                //newRow["HomePhone"] = dlg.Contact.HomePhone;
                //newRow["WorkPhone"] = dlg.Contact.WorkPhone;
                //newRow["Mobile"] = dlg.Contact.Mobile;
                //newRow["Email"] = dlg.Contact.Email;
                //newRow["FAX"] = dlg.Contact.FAX;
                //newRow["Address"] = dlg.Contact.Address;
                //newRow["Ward"] = dlg.Contact.Ward;
                //newRow["District"] = dlg.Contact.District;
                //newRow["City"] = dlg.Contact.City;

                //newRow["Qualifications"] = dlg.DocStaff.Qualifications;
                //newRow["SpecialityGUID"] = dlg.DocStaff.SpecialityGUID.ToString();
                //newRow["WorkType"] = dlg.DocStaff.WorkType;
                //newRow["StaffType"] = dlg.DocStaff.StaffType;

                //if (dlg.Contact.CreatedDate.HasValue)
                //    newRow["CreatedDate"] = dlg.Contact.CreatedDate;

                //if (dlg.Contact.CreatedBy.HasValue)
                //    newRow["CreatedBy"] = dlg.Contact.CreatedBy.ToString();

                //if (dlg.Contact.UpdatedDate.HasValue)
                //    newRow["UpdatedDate"] = dlg.Contact.UpdatedDate;

                //if (dlg.Contact.UpdatedBy.HasValue)
                //    newRow["UpdatedBy"] = dlg.Contact.UpdatedBy.ToString();

                //if (dlg.Contact.DeletedDate.HasValue)
                //    newRow["DeletedDate"] = dlg.Contact.DeletedDate;

                //if (dlg.Contact.DeletedBy.HasValue)
                //    newRow["DeletedBy"] = dlg.Contact.DeletedBy.ToString();
                //dt.Rows.Add(newRow);
                //SelectLastedRow();
            }
        }

        private void SelectLastedRow()
        {
            dgDocStaff.CurrentCell = dgDocStaff[1, dgDocStaff.RowCount - 1];
            dgDocStaff.Rows[dgDocStaff.RowCount - 1].Selected = true;
        }

        private void OnEditDocStaff()
        {
            if (dgDocStaff.SelectedRows == null || dgDocStaff.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 nhân viên.", IconType.Information);
                return;
            }

            DataRow drDocStaff = (dgDocStaff.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddDocStaff dlg = new dlgAddDocStaff(drDocStaff, AllowEdit);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DisplayAsThread();
                //drDocStaff["FullName"] = dlg.Contact.FullName;
                //drDocStaff["SurName"] = dlg.Contact.SurName;
                //drDocStaff["MiddleName"] = dlg.Contact.MiddleName;
                //drDocStaff["FirstName"] = dlg.Contact.FirstName;
                //drDocStaff["KnownAs"] = dlg.Contact.KnownAs;
                //drDocStaff["PreferredName"] = dlg.Contact.PreferredName;
                //drDocStaff["Gender"] = dlg.Contact.Gender;

                //if (dlg.Contact.Gender == 0) drDocStaff["GenderAsStr"] = "Nam";//dlg.Contact.Gender == 0 ? "Nam" : "Nữ";
                //else if (dlg.Contact.Gender == 1) drDocStaff["GenderAsStr"] = "Nữ";
                //else drDocStaff["GenderAsStr"] = "Không xác định";

                //drDocStaff["DobStr"] = dlg.Contact.DobStr;
                //drDocStaff["IdentityCard"] = dlg.Contact.IdentityCard;
                //drDocStaff["HomePhone"] = dlg.Contact.HomePhone;
                //drDocStaff["WorkPhone"] = dlg.Contact.WorkPhone;
                //drDocStaff["Mobile"] = dlg.Contact.Mobile;
                //drDocStaff["Email"] = dlg.Contact.Email;
                //drDocStaff["FAX"] = dlg.Contact.FAX;
                //drDocStaff["Address"] = dlg.Contact.Address;
                //drDocStaff["Ward"] = dlg.Contact.Ward;
                //drDocStaff["District"] = dlg.Contact.District;
                //drDocStaff["City"] = dlg.Contact.City;

                //drDocStaff["Qualifications"] = dlg.DocStaff.Qualifications;
                //drDocStaff["SpecialityGUID"] = dlg.DocStaff.SpecialityGUID.ToString();
                //drDocStaff["WorkType"] = dlg.DocStaff.WorkType;
                //drDocStaff["StaffType"] = dlg.DocStaff.StaffType;

                //if (dlg.Contact.CreatedDate.HasValue)
                //    drDocStaff["CreatedDate"] = dlg.Contact.CreatedDate;

                //if (dlg.Contact.CreatedBy.HasValue)
                //    drDocStaff["CreatedBy"] = dlg.Contact.CreatedBy.ToString();

                //if (dlg.Contact.UpdatedDate.HasValue)
                //    drDocStaff["UpdatedDate"] = dlg.Contact.UpdatedDate;

                //if (dlg.Contact.UpdatedBy.HasValue)
                //    drDocStaff["UpdatedBy"] = dlg.Contact.UpdatedBy.ToString();

                //if (dlg.Contact.DeletedDate.HasValue)
                //    drDocStaff["DeletedDate"] = dlg.Contact.DeletedDate;

                //if (dlg.Contact.DeletedBy.HasValue)
                //    drDocStaff["DeletedBy"] = dlg.Contact.DeletedBy.ToString();
            }
        }

        private void OnDeleteDocStaff()
        {
            List<string> deletedDocStaffList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgDocStaff.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedDocStaffList.Add(row["DocStaffGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedDocStaffList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những nhân viên mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = DocStaffBus.DeleteDocStaff(deletedDocStaffList);
                    if (result.IsOK)
                    {
                        //foreach (DataRow row in deletedRows)
                        //{
                        //    dt.Rows.Remove(row);
                        //}
                        DisplayAsThread();
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("DocStaffBus.DeleteDocStaff"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.DeleteDocStaff"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những nhân viên cần xóa.", IconType.Information);
        }

        private void OnExportExcel()
        {
            List<DataRow> checkedRows = new List<DataRow>();
            DataTable dt = dgDocStaff.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                    checkedRows.Add(row);
            }

            if (checkedRows.Count <= 0)
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những nhân viên cần xuất excel.", IconType.Information);
            else
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Export Excel";
                dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    ExportExcel.ExportDanhSachNhanVienToExcel(dlg.FileName, checkedRows);
                }
            }
        }
        #endregion

        #region Window Event Handlers
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddDocStaff();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditDocStaff();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteDocStaff();
        }

        private void dgDocStaff_DoubleClick(object sender, EventArgs e)
        {
            //if (!AllowEdit) return;
            OnEditDocStaff();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgDocStaff.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void dgDocStaff_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                _isAscending = !_isAscending;
                DataTable dt = dgDocStaff.DataSource as DataTable;

                List<DataRow> results = null;

                if (_isAscending)
                {
                    results = (from p in dt.AsEnumerable()
                               orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                               select p).ToList<DataRow>();
                }
                else
                {
                    results = (from p in dt.AsEnumerable()
                               orderby p.Field<string>("FirstName") descending, p.Field<string>("FullName") descending
                               select p).ToList<DataRow>();
                }


                DataTable newDataSource = dt.Clone();

                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                dgDocStaff.DataSource = newDataSource;
            }
            else
                _isAscending = false;
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            OnExportExcel();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnAddDocStaff();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnEditDocStaff();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDeleteDocStaff();
        }

        private void exportExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnExportExcel();
        }

        private void raTatCa_CheckedChanged(object sender, EventArgs e)
        {
            if (raTatCa.Checked) DisplayAsThread();
        }

        private void raChuaXoa_CheckedChanged(object sender, EventArgs e)
        {
            if (raChuaXoa.Checked) DisplayAsThread();
        }

        private void raDaXoa_CheckedChanged(object sender, EventArgs e)
        {
            if (raDaXoa.Checked) DisplayAsThread();
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
        #endregion
    }
}
