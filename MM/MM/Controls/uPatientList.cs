﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Dialogs;
using MM.Bussiness;
using MM.Common;
using MM.Databasae;

namespace MM.Controls
{
    public partial class uPatientList : uBase
    {
        #region Members
        //private Color _defaultBackColor;
        private Color _highLightBackColor;
        private DataTable _dataSource = null;
        #endregion

        #region Constructor
        public uPatientList()
        {
            InitializeComponent();
            _highLightBackColor = Color.YellowGreen;
        }
        #endregion

        #region Properties
        public object DataSource
        {
            get
            {
                if (dgPatient.RowCount <= 0)
                    DisplayAsThread();

                return dgPatient.DataSource;
            }
        }
        #endregion

        #region UI Command
        public void ClearData()
        {
            dgPatient.DataSource = null;
        }

        public void DisplayAsThread()
        {
            try
            {
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayPatientListProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnDisplayPatientList()
        {
            Result result = PatientBus.GetPatientList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    //dgPatient.DataSource = result.QueryResult;
                    _dataSource = result.QueryResult as DataTable;
                    OnSearchPatient();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.GetPatientList"));
                Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.GetPatientList"));
            }
        }

        private void OnAddPatient()
        {
            if (_dataSource == null) return;
            dlgAddPatient dlg = new dlgAddPatient();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DataTable dt = _dataSource;//dgPatient.DataSource as DataTable;
                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["PatientGUID"] = dlg.Patient.PatientGUID.ToString();
                newRow["ContactGUID"] = dlg.Contact.ContactGUID.ToString();
                newRow["FileNum"] = dlg.Patient.FileNum;
                newRow["SurName"] = dlg.Contact.SurName;
                newRow["MiddleName"] = dlg.Contact.MiddleName;
                newRow["FirstName"] = dlg.Contact.FirstName;
                newRow["KnownAs"] = dlg.Contact.KnownAs;
                newRow["PreferredName"] = dlg.Contact.PreferredName;
                newRow["Gender"] = dlg.Contact.Gender;
                newRow["GenderAsStr"] = dlg.Contact.Gender == 0 ? "Nam" : "Nữ";
                newRow["Dob"] = dlg.Contact.Dob;
                newRow["IdentityCard"] = dlg.Contact.IdentityCard;
                newRow["HomePhone"] = dlg.Contact.HomePhone;
                newRow["WorkPhone"] = dlg.Contact.WorkPhone;
                newRow["Mobile"] = dlg.Contact.Mobile;
                newRow["Email"] = dlg.Contact.Email;
                newRow["FAX"] = dlg.Contact.FAX;
                newRow["Address"] = dlg.Contact.Address;
                newRow["Ward"] = dlg.Contact.Ward;
                newRow["District"] = dlg.Contact.District;
                newRow["City"] = dlg.Contact.City;
                newRow["Fullname"] = string.Format("{0} {1} {2}", dlg.Contact.SurName, dlg.Contact.MiddleName, dlg.Contact.FirstName);
                newRow["FullAddress"] = string.Format("{0}, {1}, {2}, {3}", dlg.Contact.Address, dlg.Contact.Ward, dlg.Contact.District, dlg.Contact.City);
                newRow["Occupation"] = dlg.Contact.Occupation;

                if (dlg.Contact.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.Contact.CreatedDate;

                if (dlg.Contact.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.Contact.CreatedBy.ToString();

                if (dlg.Contact.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.Contact.UpdatedDate;

                if (dlg.Contact.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.Contact.UpdatedBy.ToString();

                if (dlg.Contact.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.Contact.DeletedDate;

                if (dlg.Contact.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.Contact.DeletedBy.ToString();

                dt.Rows.Add(newRow);
            }

            OnSearchPatient();
        }

        private DataRow GetDataRow(string patientGUID)
        {
            DataRow[] rows = _dataSource.Select(string.Format("PatientGUID = '{0}'", patientGUID));
            if (rows == null || rows.Length <= 0) return null;

            return rows[0];
        }

        private void OnEditPatient()
        {
            if (_dataSource == null) return;

            if (dgPatient.SelectedRows == null || dgPatient.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 bệnh nhân.");
                return;
            }

            //DataRow drPatient = (dgPatient.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string patientGUID = (dgPatient.SelectedRows[0].DataBoundItem as DataRowView).Row["PatientGUID"].ToString();
            DataRow drPatient = GetDataRow(patientGUID);
            if (drPatient == null) return;

            dlgAddPatient dlg = new dlgAddPatient(drPatient);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                drPatient["FileNum"] = dlg.Patient.FileNum;
                drPatient["SurName"] = dlg.Contact.SurName;
                drPatient["MiddleName"] = dlg.Contact.MiddleName;
                drPatient["FirstName"] = dlg.Contact.FirstName;
                drPatient["KnownAs"] = dlg.Contact.KnownAs;
                drPatient["PreferredName"] = dlg.Contact.PreferredName;
                drPatient["Gender"] = dlg.Contact.Gender;
                drPatient["GenderAsStr"] = dlg.Contact.Gender == 0 ? "Nam" : "Nữ";
                drPatient["Dob"] = dlg.Contact.Dob;
                drPatient["IdentityCard"] = dlg.Contact.IdentityCard;
                drPatient["HomePhone"] = dlg.Contact.HomePhone;
                drPatient["WorkPhone"] = dlg.Contact.WorkPhone;
                drPatient["Mobile"] = dlg.Contact.Mobile;
                drPatient["Email"] = dlg.Contact.Email;
                drPatient["FAX"] = dlg.Contact.FAX;
                drPatient["Address"] = dlg.Contact.Address;
                drPatient["Ward"] = dlg.Contact.Ward;
                drPatient["District"] = dlg.Contact.District;
                drPatient["City"] = dlg.Contact.City;
                drPatient["Fullname"] = string.Format("{0} {1} {2}", dlg.Contact.SurName, dlg.Contact.MiddleName, dlg.Contact.FirstName);
                drPatient["FullAddress"] = string.Format("{0}, {1}, {2}, {3}", dlg.Contact.Address, dlg.Contact.Ward, dlg.Contact.District, dlg.Contact.City);
                drPatient["Occupation"] = dlg.Contact.Occupation;

                if (dlg.Contact.CreatedDate.HasValue)
                    drPatient["CreatedDate"] = dlg.Contact.CreatedDate;

                if (dlg.Contact.CreatedBy.HasValue)
                    drPatient["CreatedBy"] = dlg.Contact.CreatedBy.ToString();

                if (dlg.Contact.UpdatedDate.HasValue)
                    drPatient["UpdatedDate"] = dlg.Contact.UpdatedDate;

                if (dlg.Contact.UpdatedBy.HasValue)
                    drPatient["UpdatedBy"] = dlg.Contact.UpdatedBy.ToString();

                if (dlg.Contact.DeletedDate.HasValue)
                    drPatient["DeletedDate"] = dlg.Contact.DeletedDate;

                if (dlg.Contact.DeletedBy.HasValue)
                    drPatient["DeletedBy"] = dlg.Contact.DeletedBy.ToString();
            }

            OnSearchPatient();
        }

        private void OnDeletePatient()
        {
            if (_dataSource == null) return;
            List<string> deletedPatientList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            List<DataRow> deletedRows2 = new List<DataRow>();
            DataTable dt = dgPatient.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    string patientGUID = row["PatientGUID"].ToString();
                    deletedPatientList.Add(patientGUID);
                    deletedRows.Add(row);
                    DataRow r = GetDataRow(patientGUID);
                    if (r != null) deletedRows2.Add(r);
                }
            }

            if (deletedPatientList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những bệnh nhân mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = PatientBus.DeletePatient(deletedPatientList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }

                        foreach (DataRow row in deletedRows2)
                        {
                            _dataSource.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.DeletePatient"));
                        Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.DeletePatient"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân cần xóa.");
        }

        private void OnOpentPatient()
        {
            if (dgPatient.SelectedRows == null || dgPatient.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 bệnh nhân.");
                return;
            }

            DataRow patientRow = (dgPatient.SelectedRows[0].DataBoundItem as DataRowView).Row;
            base.RaiseOpentPatient(patientRow);
        }

        private void ClearHighLight()
        {
            foreach (DataGridViewRow row in dgPatient.Rows)
            {
                row.DefaultCellStyle.BackColor = SystemColors.Window;
            }
        }

        private void OnSearchPatient()
        {
            chkChecked.Checked = false;
            //ClearHighLight();
            if (txtSearchPatient.Text.Trim() == string.Empty)
            {
                dgPatient.DataSource = _dataSource;
                if (dgPatient.RowCount > 0) dgPatient.Rows[0].Selected = true;
                return;
            }

            string str = txtSearchPatient.Text.ToLower();

            //Fullname
            var results = from p in _dataSource.AsEnumerable()
                          where (p.Field<string>("Fullname").ToLower().IndexOf(str) >= 0 ||
                          str.IndexOf(p.Field<string>("Fullname").ToLower()) >= 0) &&
                          p.Field<string>("Fullname") != null &&
                          p.Field<string>("Fullname").Trim() != string.Empty
                          select p;

            DataTable newDataSource = _dataSource.Clone();
            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            if (newDataSource.Rows.Count > 0)
            {
                dgPatient.DataSource = newDataSource;
                return;
            }


            //FileNum
            results = from p in _dataSource.AsEnumerable()
                      where (p.Field<string>("FileNum").ToLower().IndexOf(str) >= 0 ||
                      str.IndexOf(p.Field<string>("FileNum").ToLower()) >= 0) &&
                          p.Field<string>("FileNum") != null &&
                          p.Field<string>("FileNum").Trim() != string.Empty
                      select p;

            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            if (newDataSource.Rows.Count > 0)
            {
                dgPatient.DataSource = newDataSource;
                return;
            }

            //HomePhone
            results = from p in _dataSource.AsEnumerable()
                      where (p.Field<string>("HomePhone").ToLower().IndexOf(str) >= 0 ||
                      str.IndexOf(p.Field<string>("HomePhone").ToLower()) >= 0) &&
                      p.Field<string>("HomePhone") != null &&
                      p.Field<string>("HomePhone").Trim() != string.Empty
                      select p;

            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            if (newDataSource.Rows.Count > 0)
            {
                dgPatient.DataSource = newDataSource;
                return;
            }

            //WorkPhone
            results = from p in _dataSource.AsEnumerable()
                      where (p.Field<string>("WorkPhone").ToLower().IndexOf(str) >= 0 ||
                      str.IndexOf(p.Field<string>("WorkPhone").ToLower()) >= 0) &&
                          p.Field<string>("WorkPhone") != null &&
                          p.Field<string>("WorkPhone").Trim() != string.Empty
                      select p;

            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            if (newDataSource.Rows.Count > 0)
            {
                dgPatient.DataSource = newDataSource;
                return;
            }

            //Mobile
            results = from p in _dataSource.AsEnumerable()
                      where (p.Field<string>("Mobile").ToLower().IndexOf(str) >= 0 ||
                      str.IndexOf(p.Field<string>("Mobile").ToLower()) >= 0) &&
                          p.Field<string>("Mobile") != null &&
                          p.Field<string>("Mobile").Trim() != string.Empty
                      select p;

            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            dgPatient.DataSource = newDataSource;

            //Fullname
            /*foreach (DataGridViewRow row in dgPatient.Rows)
            {
                DataRow r = (row.DataBoundItem as DataRowView).Row;
                string fullName = (r["Fullname"] as string).ToLower();
                if (fullName.IndexOf(str) >= 0 || str.IndexOf(fullName) >= 0)
                {
                    dgPatient.FirstDisplayedScrollingRowIndex = row.Index;
                    row.DefaultCellStyle.BackColor = _highLightBackColor;
                    //row.Selected = true;
                    return;
                }
            }

            //File Num
            foreach (DataGridViewRow row in dgPatient.Rows)
            {
                DataRow r = (row.DataBoundItem as DataRowView).Row;
                string fileNum = (r["FileNum"] as string).ToLower();
                if (fileNum.IndexOf(str) >= 0 || str.IndexOf(fileNum) >= 0)
                {
                    dgPatient.FirstDisplayedScrollingRowIndex = row.Index;
                    row.DefaultCellStyle.BackColor = _highLightBackColor;
                    //row.Selected = true;
                    return;
                }
            }

            //Home Phone
            foreach (DataGridViewRow row in dgPatient.Rows)
            {
                DataRow r = (row.DataBoundItem as DataRowView).Row;
                if (r["HomePhone"] != null && r["HomePhone"].ToString().Trim() != string.Empty)
                {
                    string homePhone = (r["HomePhone"] as string).ToLower();
                    if (homePhone.IndexOf(str) >= 0 || str.IndexOf(homePhone) >= 0)
                    {
                        dgPatient.FirstDisplayedScrollingRowIndex = row.Index;
                        row.DefaultCellStyle.BackColor = _highLightBackColor;
                        //row.Selected = true;
                        return;
                    }
                }
            }

            //Work Phone
            foreach (DataGridViewRow row in dgPatient.Rows)
            {
                DataRow r = (row.DataBoundItem as DataRowView).Row;
                if (r["WorkPhone"] != null && r["WorkPhone"].ToString().Trim() != string.Empty)
                {
                    string workPhone = (r["WorkPhone"] as string).ToLower();
                    if (workPhone.IndexOf(str) >= 0 || str.IndexOf(workPhone) >= 0)
                    {
                        dgPatient.FirstDisplayedScrollingRowIndex = row.Index;
                        row.DefaultCellStyle.BackColor = _highLightBackColor;
                        //row.Selected = true;
                        return;
                    }
                }
            }

            //Mobie
            foreach (DataGridViewRow row in dgPatient.Rows)
            {
                DataRow r = (row.DataBoundItem as DataRowView).Row;
                if (r["Mobile"] != null && r["Mobile"].ToString().Trim() != string.Empty)
                {
                    string mobile = (r["Mobile"] as string).ToLower();
                    if (mobile.IndexOf(str) >= 0 || str.IndexOf(mobile) >= 0)
                    {
                        dgPatient.FirstDisplayedScrollingRowIndex = row.Index;
                        row.DefaultCellStyle.BackColor = _highLightBackColor;
                        //row.Selected = true;
                        return;
                    }
                }
            }*/

        }
        #endregion

        #region Window Event Handlers
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddPatient();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditPatient();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeletePatient();
        }

        private void btnOpenPatient_Click(object sender, EventArgs e)
        {
            OnOpentPatient();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgPatient.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void dgDocStaff_DoubleClick(object sender, EventArgs e)
        {
            OnEditPatient();
        }

        private void txtSearchPatient_TextChanged(object sender, EventArgs e)
        {
            OnSearchPatient();
        }

        private void txtSearchPatient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgPatient.Focus();

                if (dgPatient.SelectedRows != null && dgPatient.SelectedRows.Count > 0)
                {
                    int index = dgPatient.SelectedRows[0].Index;
                    if (index < dgPatient.RowCount - 1)
                    {
                        index++;
                        dgPatient.CurrentCell = dgPatient[3, index];
                        dgPatient.Rows[index].Selected = true;
                    }
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                dgPatient.Focus();

                if (dgPatient.SelectedRows != null && dgPatient.SelectedRows.Count > 0)
                {
                    int index = dgPatient.SelectedRows[0].Index;
                    if (index > 0)
                    {
                        index--;
                        dgPatient.CurrentCell = dgPatient[3, index];
                        dgPatient.Rows[index].Selected = true;
                    }
                }
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplayPatientListProc(object state)
        {
            try
            {
                //Thread.Sleep(1000);
                OnDisplayPatientList();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message);
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