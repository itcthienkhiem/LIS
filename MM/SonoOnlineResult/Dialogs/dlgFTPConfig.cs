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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using SonoOnlineResult.Dialogs;

namespace SonoOnlineResult.Dialogs
{
    public partial class dlgFTPConfig : dlgBase
    {
        #region Memnbers

        #endregion

        #region Constructor
        public dlgFTPConfig()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public bool IsChangeConnectionInfo
        {
            get
            {
                if (Global.FTPConnectionInfo.ServerName.ToLower() != txtServerName.Text.ToLower()) return true;
                if (Global.FTPConnectionInfo.Username.ToLower() != txtUserName.Text.ToLower()) return true;
                if (Global.FTPConnectionInfo.Password != txtPassword.Text) return true;
                return false;
            }
        }
        #endregion

        #region UI Command
        private bool CheckInfo()
        {
            bool result = true;

            if (txtServerName.Text == string.Empty)
            {
                result = false;
                MessageBox.Show("Please enter server.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtServerName.Focus();
            }
            else if (txtUserName.Text == string.Empty)
            {
                result = false;
                MessageBox.Show("Please enter user.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUserName.Focus();
            }
            else if (txtPassword.Text == string.Empty)
            {
                result = false;
                MessageBox.Show("Please enter password.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword.Focus();
            }

            return result;
        }

        private Result TestConnection()
        {
            Cursor.Current = Cursors.WaitCursor;
            FTPConnectionInfo connectionInfo = new FTPConnectionInfo();
            connectionInfo.ServerName = txtServerName.Text;
            connectionInfo.Username = txtUserName.Text;
            connectionInfo.Password = txtPassword.Text;

            return connectionInfo.TestConnection();
        }

        public void SetAppConfig()
        {
            //Connection Info
            Global.FTPConnectionInfo.ServerName = txtServerName.Text;
            Global.FTPConnectionInfo.Username = txtUserName.Text;
            Global.FTPConnectionInfo.Password = txtPassword.Text;
        }
        #endregion

        #region Window Event Handlers
        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            if (!CheckInfo()) return;

            Result result = TestConnection();

            if (result.IsOK)
                MessageBox.Show("Connection successfully.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Connection failure.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dlgDatabaseConfig_Load(object sender, EventArgs e)
        {
            //Connnection Info
            txtServerName.Text = Global.FTPConnectionInfo.ServerName;
            txtUserName.Text = Global.FTPConnectionInfo.Username;
            txtPassword.Text = Global.FTPConnectionInfo.Password;
        }

        private void dlgDatabaseConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
                if (!CheckInfo()) e.Cancel = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
