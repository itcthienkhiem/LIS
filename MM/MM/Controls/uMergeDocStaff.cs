﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;
using MM.Bussiness;

namespace MM.Controls
{
    public partial class uMergeDocStaff : UserControl
    {
        private DataTable _dataSource = null;
        private bool _isAscending = true;

        public uMergeDocStaff()
        {
            InitializeComponent();
        }

        public DataTable DataSource
        {
            get
            {
                return _dataSource;
            }
            set
            {
                _dataSource = value;
            }
        }

        public void BindData()
        {
            dgMergePatient.DataSource = DataSource;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.Close();
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            if (dgMergePatient.SelectedRows == null || dgMergePatient.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn nhân viên cần giữ lại.", IconType.Information);
                return;
            }

            string keepPatientGUID = (dgMergePatient.SelectedRows[0].DataBoundItem as DataRowView).Row["DocStaffGUID"].ToString();
            foreach(DataGridViewRow row in dgMergePatient.Rows)
            {
                DataRow dr = (row.DataBoundItem as DataRowView).Row;
                if (dr["DocStaffGUID"].ToString() != keepPatientGUID)
                {
                    string mergePatientGUID = dr["DocStaffGUID"].ToString();
                    DocStaffBus.Merge2DocStaffs(keepPatientGUID, mergePatientGUID);
                }
            }

            MsgBox.Show("Merge nhan vien", "Merge kết thúc", IconType.Information);
            if(Form.ActiveForm!=null)
                Form.ActiveForm.Close();
        }

        private void dgMergePatient_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                _isAscending = !_isAscending;

                DataTable dt = dgMergePatient.DataSource as DataTable;
                if (dt == null || dt.Rows.Count <= 0) return;
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

                dgMergePatient.DataSource = newDataSource;
            }
            else
                _isAscending = false;
        }
    }
}