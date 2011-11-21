﻿using System;
using System.Collections;
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
    public partial class dlgAddContract : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private CompanyContract _contract = new CompanyContract();
        //private List<string> _addedMembers = new List<string>();
        //private List<string> _deletedMembers = new List<string>();
        private List<string> _addedServices = new List<string>();
        private List<string> _deletedServices = new List<string>();
        private Hashtable _htCompanyMember = new Hashtable();
        private CompanyMember _selectedCompanyMember = null;
        #endregion

        #region Constructor
        public dlgAddContract()
        {
            InitializeComponent();
            InitData();
            DisplayDetailAsThread(Guid.Empty.ToString());
        }

        public dlgAddContract(DataRow drContract)
        {
            InitializeComponent();
            InitData();
            _isNew = false;
            this.Text = "Sua hop dong";
            DisplayInfo(drContract);
        }
        #endregion

        #region Properties
        public CompanyContract Contract
        {
            get { return _contract; }
        }

        public string ComName
        {
            get { return cboCompany.Text; }
        }
        #endregion

        #region UI Command
        private void DisplayInfo(DataRow drContract)
        {
            try
            {
                txtCode.Text = drContract["ContractCode"] as string;
                txtName.Text = drContract["ContractName"] as string;
                cboCompany.SelectedValue = drContract["CompanyGUID"].ToString();
                dtpkBeginDate.Value = Convert.ToDateTime(drContract["BeginDate"]);
                chkCompleted.Checked = Convert.ToBoolean(drContract["Completed"]);

                _contract.CompanyContractGUID = Guid.Parse(drContract["CompanyContractGUID"].ToString());
                _contract.CompanyGUID = Guid.Parse(drContract["CompanyGUID"].ToString());

                if (drContract["CreatedDate"] != null && drContract["CreatedDate"] != DBNull.Value)
                    _contract.CreatedDate = Convert.ToDateTime(drContract["CreatedDate"]);

                if (drContract["CreatedBy"] != null && drContract["CreatedBy"] != DBNull.Value)
                    _contract.CreatedBy = Guid.Parse(drContract["CreatedBy"].ToString());

                if (drContract["UpdatedDate"] != null && drContract["UpdatedDate"] != DBNull.Value)
                    _contract.UpdatedDate = Convert.ToDateTime(drContract["UpdatedDate"]);

                if (drContract["UpdatedBy"] != null && drContract["UpdatedBy"] != DBNull.Value)
                    _contract.UpdatedBy = Guid.Parse(drContract["UpdatedBy"].ToString());

                if (drContract["DeletedDate"] != null && drContract["DeletedDate"] != DBNull.Value)
                    _contract.DeletedDate = Convert.ToDateTime(drContract["DeletedDate"]);

                if (drContract["DeletedBy"] != null && drContract["DeletedBy"] != DBNull.Value)
                    _contract.DeletedBy = Guid.Parse(drContract["DeletedBy"].ToString());

                _contract.Status = Convert.ToByte(drContract["ContractStatus"]);

                DisplayDetailAsThread(_contract.CompanyContractGUID.ToString());
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (txtCode.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập mã hợp đồng.");
                tabContract.SelectedTabIndex = 0;
                txtCode.Focus();
                return false;
            }

            if (txtName.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập tên hợp đồng.");
                tabContract.SelectedTabIndex = 0;
                txtName.Focus();
                return false;
            }

            if (cboCompany.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn công ty.");
                tabContract.SelectedTabIndex = 0;
                cboCompany.Focus();
                return false;
            }

            string conGUID = _isNew ? string.Empty : _contract.CompanyContractGUID.ToString();
            Result result = CompanyContractBus.CheckContractExistCode(conGUID, txtCode.Text);

            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Mã hợp đồng này đã tồn tại rồi. Vui lòng nhập mã khác.");
                    tabContract.SelectedTabIndex = 0;
                    txtCode.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.CheckContractExistCode"));
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
                MsgBox.Show(this.Text, e.Message);
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
                _contract.ContractCode = txtCode.Text;
                _contract.ContractName = txtName.Text;
                _contract.BeginDate = dtpkBeginDate.Value;
                _contract.Completed = chkCompleted.Checked;
                _contract.Status = (byte)Status.Actived;

                if (_isNew)
                {
                    _contract.CreatedDate = DateTime.Now;
                    _contract.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _contract.UpdatedDate = DateTime.Now;
                    _contract.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                MethodInvoker method = delegate
                {
                    _contract.CompanyGUID = Guid.Parse(cboCompany.SelectedValue.ToString());

                    Result result = CompanyContractBus.InsertContract(_contract, _selectedCompanyMember.AddedMembers, 
                        _selectedCompanyMember.DeletedMembers, _addedServices, _deletedServices);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.InsertContract"));
                        Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.InsertContract"));
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    }
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void InitData()
        {
            //Company
            Result result = CompanyBus.GetCompanyList();
            if (result.IsOK)
            {
                cboCompany.DataSource = result.QueryResult as DataTable;
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyBus.GetCompanyList"));
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyBus.GetCompanyList"));
            }
        }

        private void DisplayDetailAsThread(string contractGUID)
        {
            try
            {
                chkCheckedMember.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayDetailProc), contractGUID);
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(this.Text, e.Message);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnDisplayMembers(string contractGUID)
        {
            Result result = CompanyContractBus.GetContractMemberList(contractGUID);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgMembers.DataSource = result.QueryResult;

                    if (cboCompany.Text == string.Empty) return;
                    string companyGUID = cboCompany.SelectedValue.ToString();
                    if (!_htCompanyMember.ContainsKey(companyGUID))
                    {
                        _selectedCompanyMember = new CompanyMember();
                        _selectedCompanyMember.CompanyGUID = companyGUID;
                        _selectedCompanyMember.DataSource = result.QueryResult as DataTable;
                        _htCompanyMember.Add(companyGUID, _selectedCompanyMember);
                    }
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.GetContractMemberList"));
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetContractMemberList"));
            }
        }

        private void OnDisplayCheckList(string contractGUID)
        {
            Result result = CompanyContractBus.GetCheckList(contractGUID);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgService.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.GetCheckList"));
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetCheckList"));
            }
        }

        private void OnAddMember()
        {
            if (cboCompany.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn công ty.");
                tabContract.SelectedTabIndex = 0;
                cboCompany.Focus();
                return;
            }
            dlgMembers dlg = new dlgMembers(cboCompany.SelectedValue.ToString());
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                List<DataRow> checkedRows = dlg.Members;
                DataTable dataSource = dgMembers.DataSource as DataTable;
                foreach (DataRow row in checkedRows)
                {
                    string companyMemberGUID = row["CompanyMemberGUID"].ToString();
                    DataRow[] rows = dataSource.Select(string.Format("CompanyMemberGUID='{0}'", companyMemberGUID));
                    if (rows == null || rows.Length <= 0)
                    {
                        DataRow newRow = dataSource.NewRow();
                        newRow["Checked"] = false;
                        newRow["CompanyMemberGUID"] = companyMemberGUID;
                        newRow["FileNum"] = row["FileNum"];
                        newRow["FullName"] = row["FullName"];
                        newRow["DobStr"] = row["DobStr"];
                        newRow["GenderAsStr"] = row["GenderAsStr"];
                        dataSource.Rows.Add(newRow);

                        if (!_selectedCompanyMember.AddedMembers.Contains(companyMemberGUID))
                            _selectedCompanyMember.AddedMembers.Add(companyMemberGUID);

                        _selectedCompanyMember.DeletedMembers.Remove(companyMemberGUID);
                    }
                }
            }
        }

        private void OnDeleteMember()
        {
            List<string> deletedMemList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgMembers.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedMemList.Add(row["CompanyMemberGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedMemList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những nhân viên mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    foreach (DataRow row in deletedRows)
                    {
                        string companyMemberGUID = row["CompanyMemberGUID"].ToString();
                        if (!_selectedCompanyMember.DeletedMembers.Contains(companyMemberGUID))
                            _selectedCompanyMember.DeletedMembers.Add(companyMemberGUID);

                        _selectedCompanyMember.AddedMembers.Remove(companyMemberGUID);

                        dt.Rows.Remove(row);
                    }
                }
            }
            else
                MsgBox.Show(this.Text, "Vui lòng đánh dấu những nhân viên cần xóa.");
        }

        private void OnAddService()
        {
            dlgServices dlg = new dlgServices();
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                List<DataRow> checkedRows = dlg.Services;
                DataTable dataSource = dgService.DataSource as DataTable;
                foreach (DataRow row in checkedRows)
                {
                    string serviceGUID = row["ServiceGUID"].ToString();
                    DataRow[] rows = dataSource.Select(string.Format("ServiceGUID='{0}'", serviceGUID));
                    if (rows == null || rows.Length <= 0)
                    {
                        DataRow newRow = dataSource.NewRow();
                        newRow["Checked"] = false;
                        newRow["ServiceGUID"] = serviceGUID;
                        newRow["Code"] = row["Code"];
                        newRow["Name"] = row["Name"];
                        newRow["Price"] = row["Price"];
                        newRow["Description"] = row["Description"];
                        dataSource.Rows.Add(newRow);

                        if (!_addedServices.Contains(serviceGUID))
                            _addedServices.Add(serviceGUID);

                        _deletedServices.Remove(serviceGUID);
                    }
                }
            }
        }

        private void OnDeleteService()
        {
            List<string> deletedSrvList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgService.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedSrvList.Add(row["ServiceGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedSrvList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những dịch vụ mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    foreach (DataRow row in deletedRows)
                    {
                        string serviceGUID = row["ServiceGUID"].ToString();
                        if (!_deletedServices.Contains(serviceGUID))
                            _deletedServices.Add(serviceGUID);

                        _addedServices.Remove(serviceGUID);

                        dt.Rows.Remove(row);
                    }
                }
            }
            else
                MsgBox.Show(this.Text, "Vui lòng đánh dấu những dịch vụ cần xóa.");
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddContract_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
            }
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            OnAddMember();
        }

        private void btnDeleteMember_Click(object sender, EventArgs e)
        {
            OnDeleteMember();
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            OnAddService();
        }

        private void btnDeleteService_Click(object sender, EventArgs e)
        {
            OnDeleteService();
        }

        private void chkCheckedMember_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgMembers.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkCheckedMember.Checked;
            }
        }

        private void chkCheckedService_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgService.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkCheckedService.Checked;
            }
        }

        private void cboCompany_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboCompany.Text == string.Empty) return;
            DataTable dt = dgMembers.DataSource as DataTable;
            if (dt == null) return;

            string companyGUID = cboCompany.SelectedValue.ToString();
            if (_htCompanyMember.ContainsKey(companyGUID))
            {
                _selectedCompanyMember = _htCompanyMember[companyGUID] as CompanyMember;
                dgMembers.DataSource = _selectedCompanyMember.DataSource;
            }
            else
            {
                _selectedCompanyMember = new CompanyMember();
                _selectedCompanyMember.CompanyGUID = companyGUID;
                _selectedCompanyMember.DataSource = dt.Clone();
                _htCompanyMember.Add(companyGUID, _selectedCompanyMember);
                dgMembers.DataSource = _selectedCompanyMember.DataSource;
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
                MsgBox.Show(this.Text, e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnDisplayDetailProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayMembers(state.ToString());
                OnDisplayCheckList(state.ToString());
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(this.Text, e.Message);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion
    }

    public class CompanyMember
    {
        public string CompanyGUID = string.Empty;
        public DataTable DataSource = null;
        public List<string> AddedMembers = new List<string>();
        public List<string> DeletedMembers = new List<string>();
    }
}