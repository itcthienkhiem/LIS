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
using MM.Databasae;
using MM.Bussiness;

namespace MM.Dialogs
{
    public partial class dlgChangePassword : dlgBase
    {
        #region Members
        private string _newPassword = string.Empty;
        #endregion

        #region Constructor
        public dlgChangePassword()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private bool CheckInfo()
        {
            if (txtOldPassword.Text != Global.Password)
            {
                MsgBox.Show(this.Text, "Mật khẩu cũ không chính xác. Vui lòng nhập lại.", IconType.Information);
                txtOldPassword.Focus();
                return false;
            }

            if (txtNewPassword.Text == txtOldPassword.Text)
            {
                MsgBox.Show(this.Text, "Mật khẩu mới không được trùng với mật khẩu cũ. Vui lòng nhập lại.", IconType.Information);
                txtNewPassword.Focus();
                return false;
            }

            if (!Utility.IsValidPassword(txtNewPassword.Text))
            {
                MsgBox.Show(this.Text, "Mật khẩu mới không hợp lệ (4-12 kí tự). Vui lòng nhập lại.", IconType.Information);
                txtNewPassword.Focus();
                return false;
            }

            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                MsgBox.Show(this.Text, "Xác nhận lại mật khẩu không khớp với mật khẩu mới đã nhập. Vui lòng nhập lại.", IconType.Information);
                txtConfirmPassword.Focus();
                return false;
            }

            return true;
        }

        private void ChangePasswordAsThread()
        {
            try
            {
                _newPassword = txtNewPassword.Text;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnChangePasswordProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnChangePassword()
        {
            Result result = LogonBus.ChangePassword(_newPassword);
            if (result.IsOK)
                Global.Password = _newPassword;
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("LogonBus.ChangePassword"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("LogonBus.ChangePassword"));
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgChangePassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                {
                    ChangePasswordAsThread();
                }
                else 
                    e.Cancel = true;
            }
        }
        #endregion

        #region Working Thread
        private void OnChangePasswordProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnChangePassword();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion
    }
}
