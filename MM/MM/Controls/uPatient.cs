﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MM.Controls
{
    public partial class uPatient : UserControl
    {
        #region Members
        private object _patientRow = null;
        #endregion

        #region Constructor
        public uPatient()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public object PatientRow
        {
            get { return _patientRow; }
            set 
            { 
                _patientRow = value;
                _uServiceHistory.PatientRow = value;
            }
        }
        #endregion

        #region UI Command
        public void DisplayInfo()
        {
            if (_patientRow == null) return;

            DataRow row = _patientRow as DataRow;

            txtFileNum.Text = row["FileNum"].ToString();
            txtFullname.Text = row["Fullname"].ToString();
            txtGender.Text = row["GenderAsStr"].ToString();
            txtDOB.Text = Convert.ToDateTime(row["Dob"]).ToString("dd/MM/yyyy");
            txtIdentityCard.Text = row["IdentityCard"].ToString();
            txtHomePhone.Text = row["HomePhone"].ToString();
            txtWorkPhone.Text = row["WorkPhone"].ToString();
            txtMobile.Text = row["Mobile"].ToString();
            txtEmail.Text = row["Email"].ToString();
            txtFullAddress.Text = row["FullAddress"].ToString();

            _uServiceHistory.DisplayAsThread();
        }
        #endregion

        #region Window Event Handlers
        private void uPatient_Load(object sender, EventArgs e)
        {

        }
        #endregion
    }
}