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

namespace SonoOnlineResult.Dialogs
{
    public partial class dlgDeleteUploadFile : Form
    {
        #region Members

        #endregion

        #region Constructor
        public dlgDeleteUploadFile()
        {
            InitializeComponent();

            DateTime dt = DateTime.Now.AddMonths(-1);
            dtpkFrom.Value = new DateTime(dt.Year, dt.Month, 1, 0, 0, 0);
            dtpkTo.Value = new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month), 23, 59, 59);
        }
        #endregion

        #region Properties
        public DateTime From
        {
            get { return dtpkFrom.Value; }
        }

        public DateTime To
        {
            get { return dtpkTo.Value; }
        }
        #endregion

        #region UI Command
        private bool CheckInfo()
        {
            if (dtpkFrom.Value > dtpkTo.Value)
            {
                MessageBox.Show("Please input From Date is less than or equal To Date.", 
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpkFrom.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region Window Event Handlers
        private void dlgDeleteUploadFile_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
                else if (MessageBox.Show("Do you want to delele upload file ?", 
                    this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    e.Cancel = true;
            }
        }
        #endregion
    }
}
