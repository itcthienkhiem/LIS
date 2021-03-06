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
namespace MM.Dialogs
{
    partial class dlgTVHomeConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgTVHomeConfig));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.raSieuAm_Co = new System.Windows.Forms.RadioButton();
            this.raSieuAm_Khong = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.raSoiCTC_Co = new System.Windows.Forms.RadioButton();
            this.raSoiCTC_Khong = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.pImageFormat = new System.Windows.Forms.Panel();
            this.raJPG = new System.Windows.Forms.RadioButton();
            this.raBMP = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pImageFormat.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pImageFormat);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnBrowser);
            this.groupBox1.Controls.Add(this.txtPath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(7, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(519, 82);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.raSieuAm_Co);
            this.panel2.Controls.Add(this.raSieuAm_Khong);
            this.panel2.Location = new System.Drawing.Point(360, 52);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(148, 25);
            this.panel2.TabIndex = 8;
            this.panel2.Visible = false;
            // 
            // raSieuAm_Co
            // 
            this.raSieuAm_Co.AutoSize = true;
            this.raSieuAm_Co.Checked = true;
            this.raSieuAm_Co.Location = new System.Drawing.Point(3, 4);
            this.raSieuAm_Co.Name = "raSieuAm_Co";
            this.raSieuAm_Co.Size = new System.Drawing.Size(38, 17);
            this.raSieuAm_Co.TabIndex = 5;
            this.raSieuAm_Co.TabStop = true;
            this.raSieuAm_Co.Text = "Có";
            this.raSieuAm_Co.UseVisualStyleBackColor = true;
            // 
            // raSieuAm_Khong
            // 
            this.raSieuAm_Khong.AutoSize = true;
            this.raSieuAm_Khong.Location = new System.Drawing.Point(86, 4);
            this.raSieuAm_Khong.Name = "raSieuAm_Khong";
            this.raSieuAm_Khong.Size = new System.Drawing.Size(56, 17);
            this.raSieuAm_Khong.TabIndex = 6;
            this.raSieuAm_Khong.Text = "Không";
            this.raSieuAm_Khong.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.raSoiCTC_Co);
            this.panel1.Controls.Add(this.raSoiCTC_Khong);
            this.panel1.Location = new System.Drawing.Point(391, 81);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(148, 25);
            this.panel1.TabIndex = 7;
            this.panel1.Visible = false;
            // 
            // raSoiCTC_Co
            // 
            this.raSoiCTC_Co.AutoSize = true;
            this.raSoiCTC_Co.Checked = true;
            this.raSoiCTC_Co.Location = new System.Drawing.Point(3, 4);
            this.raSoiCTC_Co.Name = "raSoiCTC_Co";
            this.raSoiCTC_Co.Size = new System.Drawing.Size(38, 17);
            this.raSoiCTC_Co.TabIndex = 5;
            this.raSoiCTC_Co.TabStop = true;
            this.raSoiCTC_Co.Text = "Có";
            this.raSoiCTC_Co.UseVisualStyleBackColor = true;
            // 
            // raSoiCTC_Khong
            // 
            this.raSoiCTC_Khong.AutoSize = true;
            this.raSoiCTC_Khong.Location = new System.Drawing.Point(86, 4);
            this.raSoiCTC_Khong.Name = "raSoiCTC_Khong";
            this.raSoiCTC_Khong.Size = new System.Drawing.Size(56, 17);
            this.raSoiCTC_Khong.TabIndex = 6;
            this.raSoiCTC_Khong.Text = "Không";
            this.raSoiCTC_Khong.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(308, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Siêu Âm:";
            this.label3.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(282, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Soi CTC:";
            this.label2.Visible = false;
            // 
            // btnBrowser
            // 
            this.btnBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowser.Location = new System.Drawing.Point(478, 18);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(28, 23);
            this.btnBrowser.TabIndex = 2;
            this.btnBrowser.Text = "...";
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(120, 20);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(353, 20);
            this.txtPath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Đường dẫn TVHome:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(268, 90);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(189, 90);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // pImageFormat
            // 
            this.pImageFormat.Controls.Add(this.raJPG);
            this.pImageFormat.Controls.Add(this.raBMP);
            this.pImageFormat.Location = new System.Drawing.Point(120, 47);
            this.pImageFormat.Name = "pImageFormat";
            this.pImageFormat.Size = new System.Drawing.Size(148, 25);
            this.pImageFormat.TabIndex = 10;
            // 
            // raJPG
            // 
            this.raJPG.AutoSize = true;
            this.raJPG.Checked = true;
            this.raJPG.Location = new System.Drawing.Point(3, 4);
            this.raJPG.Name = "raJPG";
            this.raJPG.Size = new System.Drawing.Size(45, 17);
            this.raJPG.TabIndex = 5;
            this.raJPG.TabStop = true;
            this.raJPG.Text = "JPG";
            this.raJPG.UseVisualStyleBackColor = true;
            // 
            // raBMP
            // 
            this.raBMP.AutoSize = true;
            this.raBMP.Location = new System.Drawing.Point(86, 4);
            this.raBMP.Name = "raBMP";
            this.raBMP.Size = new System.Drawing.Size(48, 17);
            this.raBMP.TabIndex = 6;
            this.raBMP.Text = "BMP";
            this.raBMP.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Định dạng hình ảnh:";
            // 
            // dlgTVHomeConfig
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(532, 120);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgTVHomeConfig";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cau hinh TVHome";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgTVHomeConfig_FormClosing);
            this.Load += new System.EventHandler(this.dlgTVHomeConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pImageFormat.ResumeLayout(false);
            this.pImageFormat.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton raSieuAm_Co;
        private System.Windows.Forms.RadioButton raSieuAm_Khong;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton raSoiCTC_Co;
        private System.Windows.Forms.RadioButton raSoiCTC_Khong;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pImageFormat;
        private System.Windows.Forms.RadioButton raJPG;
        private System.Windows.Forms.RadioButton raBMP;
        private System.Windows.Forms.Label label4;
    }
}
