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
using MM.Dialogs;

namespace MM.Controls
{
    public partial class uThongBaoList : uBase
    {
        #region Members
        private DateTime _tuNgay = DateTime.Now;
        private DateTime _denNgay = DateTime.Now;
        private string _tenNguoiTao = string.Empty;
        #endregion

        #region Constructor
        public uThongBaoList()
        {
            InitializeComponent();

            dtpkDenNgay.Value = DateTime.Now;
            dtpkTuNgay.Value = DateTime.Now.AddDays(-7);
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            btnEdit.Enabled = AllowEdit;
            btnDelete.Enabled = AllowDelete;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                _tuNgay = dtpkTuNgay.Value;
                _denNgay = dtpkDenNgay.Value;
                _tenNguoiTao = txtTenNguoiTao.Text.Trim();
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayThongBaoListProc));
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

        private void OnDisplayThongBaoList()
        {
            bool isAll = (AllowAdd || AllowEdit || AllowDelete) ? true : false;

            Result result = ThongBaoBus.GetThongBaoList(_tuNgay, _denNgay, _tenNguoiTao, isAll);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgThongBao.DataSource = result.QueryResult as DataTable;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ThongBaoBus.GetThongBaoList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ThongBaoBus.GetThongBaoList"));
            }
        }

        private void OnAdd()
        {
            dlgAddThongBao dlg = new dlgAddThongBao();
            if (dlg.ShowDialog(this) == DialogResult.OK)
                DisplayAsThread();
        }

        private void OnEdit()
        {
            if (dgThongBao.SelectedRows == null || dgThongBao.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 thông báo.", IconType.Information);
                return;
            }

            DataRow drThongBao = (dgThongBao.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string nguoiTaoGUID = drThongBao["CreatedBy"].ToString();
            if (nguoiTaoGUID != Global.UserGUID)
            {
                MsgBox.Show(Application.ProductName, "Bạn không thể sửa thông báo do người khác tạo. Vui lòng kiểm tra lại.", IconType.Information);
                return;
            }

            dlgAddThongBao dlg = new dlgAddThongBao(drThongBao);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnDelete()
        {
            List<string> deletedKeysList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgThongBao.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedKeysList.Add(row["ThongBaoGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedKeysList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những thông báo mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    foreach (DataRow row in deletedRows)
                    {
                        string nguoiTaoGUID = row["CreatedBy"].ToString();
                        if (nguoiTaoGUID != Global.UserGUID)
                        {
                            MsgBox.Show(Application.ProductName, "Bạn không thể xóa thông báo do người khác tạo. Vui lòng kiểm tra lại.", IconType.Information);
                            return;
                        }
                    }

                    Result result = ThongBaoBus.DeleteThongBao(deletedKeysList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("ThongBaoBus.DeleteThongBao"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ThongBaoBus.DeleteThongBao"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những thông báo cần xóa.", IconType.Information);
        }

        private void ExecuteThongBao(byte[] buff)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
            dlg.FileName = string.Format("ThongBao_{0}.xls", DateTime.Now.ToString("yyyy_MM_dd"));
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                Utility.SaveFileFromBytes(dlg.FileName, buff);
                Utility.ExecuteFile(dlg.FileName);
            }
        }

        private void OnXemThongBao()
        {
            try
            {
                if (dgThongBao.SelectedRows == null || dgThongBao.SelectedRows.Count <= 0)
                {
                    MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 thông báo.", IconType.Information);
                    return;
                }

                DataRow drThongBao = (dgThongBao.SelectedRows[0].DataBoundItem as DataRowView).Row;

                if (drThongBao["ThongBaoBuff3"] != null && drThongBao["ThongBaoBuff3"] != DBNull.Value)
                {
                    byte[] buff = (byte[])drThongBao["ThongBaoBuff3"];
                    ExecuteThongBao(buff);
                    return;
                }

                if (drThongBao["ThongBaoBuff2"] != null && drThongBao["ThongBaoBuff2"] != DBNull.Value)
                {
                    byte[] buff = (byte[])drThongBao["ThongBaoBuff2"];
                    ExecuteThongBao(buff);
                    return;
                }

                if (drThongBao["ThongBaoBuff1"] != null && drThongBao["ThongBaoBuff1"] != DBNull.Value)
                {
                    byte[] buff = (byte[])drThongBao["ThongBaoBuff1"];
                    ExecuteThongBao(buff);
                    return;
                }

                if (drThongBao["ThongBaoBuff"] != null && drThongBao["ThongBaoBuff"] != DBNull.Value)
                {
                    byte[] buff = (byte[])drThongBao["ThongBaoBuff"];
                    ExecuteThongBao(buff);
                }
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }
        #endregion
        
        #region Window Event Handlers
        private void btnView_Click(object sender, EventArgs e)
        {
            if (dtpkTuNgay.Value > dtpkDenNgay.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập từ ngày nhỏ hơn hoặc bằng đến ngày.", IconType.Information);
                dtpkTuNgay.Focus();
                return;
            }

            DisplayAsThread();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAdd();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDelete();
        }

        private void btnXemThongBao_Click(object sender, EventArgs e)
        {
            OnXemThongBao();
        }

        private void dgThongBao_DoubleClick(object sender, EventArgs e)
        {
            if (!AllowEdit) return;
            OnEdit();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgThongBao.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplayThongBaoListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayThongBaoList();
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
