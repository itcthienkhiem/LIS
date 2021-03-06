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
    partial class dlgAddKetQuaSieuAm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddKetQuaSieuAm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSaveAndPrint = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cboLoaiSieuAm = new System.Windows.Forms.ComboBox();
            this.loaiSieuAmBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.txtLamSang = new System.Windows.Forms.TextBox();
            this.cboBSSieuAm = new System.Windows.Forms.ComboBox();
            this.docStaffViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cboBSCD = new System.Windows.Forms.ComboBox();
            this.dtpkNgaySieuAm = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnSwap = new System.Windows.Forms.Button();
            this.btnHinh2 = new System.Windows.Forms.Button();
            this.picHinh2 = new System.Windows.Forms.PictureBox();
            this.ctmHinh2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.chọnHìnhTừBênNgoàiToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnHinh1 = new System.Windows.Forms.Button();
            this.picHinh1 = new System.Windows.Forms.PictureBox();
            this.ctmHinh1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.chọnHìnhTừBênNgoàiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.xóaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel5 = new System.Windows.Forms.Panel();
            this._textControl = new TXTextControl.TextControl();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loaiSieuAmBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHinh2)).BeginInit();
            this.ctmHinh2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHinh1)).BeginInit();
            this.ctmHinh1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 700);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(942, 35);
            this.panel1.TabIndex = 6;
            this.panel1.TabStop = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnSaveAndPrint);
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(696, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(246, 35);
            this.panel2.TabIndex = 19;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(164, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSaveAndPrint
            // 
            this.btnSaveAndPrint.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSaveAndPrint.Image = global::MM.Properties.Resources.save;
            this.btnSaveAndPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveAndPrint.Location = new System.Drawing.Point(85, 5);
            this.btnSaveAndPrint.Name = "btnSaveAndPrint";
            this.btnSaveAndPrint.Size = new System.Drawing.Size(75, 25);
            this.btnSaveAndPrint.TabIndex = 18;
            this.btnSaveAndPrint.Text = "     &Lưu && In";
            this.btnSaveAndPrint.UseVisualStyleBackColor = true;
            this.btnSaveAndPrint.Click += new System.EventHandler(this.btnSaveAndPrint_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(6, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cboLoaiSieuAm);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.txtLamSang);
            this.panel3.Controls.Add(this.cboBSSieuAm);
            this.panel3.Controls.Add(this.cboBSCD);
            this.panel3.Controls.Add(this.dtpkNgaySieuAm);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(942, 84);
            this.panel3.TabIndex = 7;
            // 
            // cboLoaiSieuAm
            // 
            this.cboLoaiSieuAm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboLoaiSieuAm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboLoaiSieuAm.DataSource = this.loaiSieuAmBindingSource;
            this.cboLoaiSieuAm.DisplayMember = "TenSieuAm";
            this.cboLoaiSieuAm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiSieuAm.FormattingEnabled = true;
            this.cboLoaiSieuAm.Location = new System.Drawing.Point(94, 8);
            this.cboLoaiSieuAm.Name = "cboLoaiSieuAm";
            this.cboLoaiSieuAm.Size = new System.Drawing.Size(223, 21);
            this.cboLoaiSieuAm.TabIndex = 26;
            this.cboLoaiSieuAm.ValueMember = "LoaiSieuAmGUID";
            this.cboLoaiSieuAm.SelectedIndexChanged += new System.EventHandler(this.cboLoaiSieuAm_SelectedIndexChanged);
            // 
            // loaiSieuAmBindingSource
            // 
            this.loaiSieuAmBindingSource.DataSource = typeof(MM.Databasae.LoaiSieuAm);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(22, 11);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Loại siêu âm:";
            // 
            // txtLamSang
            // 
            this.txtLamSang.Location = new System.Drawing.Point(94, 56);
            this.txtLamSang.MaxLength = 500;
            this.txtLamSang.Name = "txtLamSang";
            this.txtLamSang.Size = new System.Drawing.Size(541, 20);
            this.txtLamSang.TabIndex = 25;
            // 
            // cboBSSieuAm
            // 
            this.cboBSSieuAm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboBSSieuAm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboBSSieuAm.DataSource = this.docStaffViewBindingSource;
            this.cboBSSieuAm.DisplayMember = "Fullname";
            this.cboBSSieuAm.FormattingEnabled = true;
            this.cboBSSieuAm.Location = new System.Drawing.Point(412, 32);
            this.cboBSSieuAm.Name = "cboBSSieuAm";
            this.cboBSSieuAm.Size = new System.Drawing.Size(223, 21);
            this.cboBSSieuAm.TabIndex = 23;
            this.cboBSSieuAm.ValueMember = "DocStaffGUID";
            // 
            // docStaffViewBindingSource
            // 
            this.docStaffViewBindingSource.DataSource = typeof(MM.Databasae.DocStaffView);
            // 
            // cboBSCD
            // 
            this.cboBSCD.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboBSCD.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboBSCD.DataSource = this.docStaffViewBindingSource;
            this.cboBSCD.DisplayMember = "Fullname";
            this.cboBSCD.FormattingEnabled = true;
            this.cboBSCD.Location = new System.Drawing.Point(94, 32);
            this.cboBSCD.Name = "cboBSCD";
            this.cboBSCD.Size = new System.Drawing.Size(223, 21);
            this.cboBSCD.TabIndex = 20;
            this.cboBSCD.ValueMember = "DocStaffGUID";
            // 
            // dtpkNgaySieuAm
            // 
            this.dtpkNgaySieuAm.CustomFormat = "dd/MM/yyyy";
            this.dtpkNgaySieuAm.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkNgaySieuAm.Location = new System.Drawing.Point(412, 9);
            this.dtpkNgaySieuAm.Name = "dtpkNgaySieuAm";
            this.dtpkNgaySieuAm.Size = new System.Drawing.Size(105, 20);
            this.dtpkNgaySieuAm.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Lâm sàng:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(328, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Bác sĩ siêu âm:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Bác sĩ chỉ định:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(335, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Ngày siêu âm:";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.btnSwap);
            this.panel4.Controls.Add(this.btnHinh2);
            this.panel4.Controls.Add(this.picHinh2);
            this.panel4.Controls.Add(this.btnHinh1);
            this.panel4.Controls.Add(this.picHinh1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(629, 84);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(313, 616);
            this.panel4.TabIndex = 8;
            // 
            // btnSwap
            // 
            this.btnSwap.Image = ((System.Drawing.Image)(resources.GetObject("btnSwap.Image")));
            this.btnSwap.Location = new System.Drawing.Point(253, 329);
            this.btnSwap.Name = "btnSwap";
            this.btnSwap.Size = new System.Drawing.Size(31, 31);
            this.btnSwap.TabIndex = 22;
            this.btnSwap.UseVisualStyleBackColor = true;
            this.btnSwap.Click += new System.EventHandler(this.btnSwap_Click);
            // 
            // btnHinh2
            // 
            this.btnHinh2.Location = new System.Drawing.Point(230, 401);
            this.btnHinh2.Name = "btnHinh2";
            this.btnHinh2.Size = new System.Drawing.Size(75, 23);
            this.btnHinh2.TabIndex = 21;
            this.btnHinh2.Text = "Hình 2";
            this.btnHinh2.UseVisualStyleBackColor = true;
            this.btnHinh2.Click += new System.EventHandler(this.btnHinh2_Click);
            // 
            // picHinh2
            // 
            this.picHinh2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picHinh2.ContextMenuStrip = this.ctmHinh2;
            this.picHinh2.Location = new System.Drawing.Point(6, 349);
            this.picHinh2.Name = "picHinh2";
            this.picHinh2.Size = new System.Drawing.Size(220, 130);
            this.picHinh2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHinh2.TabIndex = 20;
            this.picHinh2.TabStop = false;
            this.picHinh2.DoubleClick += new System.EventHandler(this.picHinh2_DoubleClick);
            // 
            // ctmHinh2
            // 
            this.ctmHinh2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chọnHìnhTừBênNgoàiToolStripMenuItem1,
            this.toolStripSeparator2,
            this.toolStripMenuItem1});
            this.ctmHinh2.Name = "ctmHinh1";
            this.ctmHinh2.Size = new System.Drawing.Size(201, 54);
            // 
            // chọnHìnhTừBênNgoàiToolStripMenuItem1
            // 
            this.chọnHìnhTừBênNgoàiToolStripMenuItem1.Name = "chọnHìnhTừBênNgoàiToolStripMenuItem1";
            this.chọnHìnhTừBênNgoàiToolStripMenuItem1.Size = new System.Drawing.Size(200, 22);
            this.chọnHìnhTừBênNgoàiToolStripMenuItem1.Text = "Chọn hình từ bên ngoài";
            this.chọnHìnhTừBênNgoàiToolStripMenuItem1.Click += new System.EventHandler(this.chọnHìnhTừBênNgoàiToolStripMenuItem1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(197, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(200, 22);
            this.toolStripMenuItem1.Text = "Xóa";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // btnHinh1
            // 
            this.btnHinh1.Location = new System.Drawing.Point(230, 268);
            this.btnHinh1.Name = "btnHinh1";
            this.btnHinh1.Size = new System.Drawing.Size(75, 23);
            this.btnHinh1.TabIndex = 19;
            this.btnHinh1.Text = "Hình 1";
            this.btnHinh1.UseVisualStyleBackColor = true;
            this.btnHinh1.Click += new System.EventHandler(this.btnHinh1_Click);
            // 
            // picHinh1
            // 
            this.picHinh1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picHinh1.ContextMenuStrip = this.ctmHinh1;
            this.picHinh1.Location = new System.Drawing.Point(5, 213);
            this.picHinh1.Name = "picHinh1";
            this.picHinh1.Size = new System.Drawing.Size(220, 130);
            this.picHinh1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHinh1.TabIndex = 18;
            this.picHinh1.TabStop = false;
            this.picHinh1.DoubleClick += new System.EventHandler(this.picHinh1_DoubleClick);
            // 
            // ctmHinh1
            // 
            this.ctmHinh1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chọnHìnhTừBênNgoàiToolStripMenuItem,
            this.toolStripSeparator1,
            this.xóaToolStripMenuItem1});
            this.ctmHinh1.Name = "ctmHinh1";
            this.ctmHinh1.Size = new System.Drawing.Size(201, 54);
            // 
            // chọnHìnhTừBênNgoàiToolStripMenuItem
            // 
            this.chọnHìnhTừBênNgoàiToolStripMenuItem.Name = "chọnHìnhTừBênNgoàiToolStripMenuItem";
            this.chọnHìnhTừBênNgoàiToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.chọnHìnhTừBênNgoàiToolStripMenuItem.Text = "Chọn hình từ bên ngoài";
            this.chọnHìnhTừBênNgoàiToolStripMenuItem.Click += new System.EventHandler(this.chọnHìnhTừBênNgoàiToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(197, 6);
            // 
            // xóaToolStripMenuItem1
            // 
            this.xóaToolStripMenuItem1.Name = "xóaToolStripMenuItem1";
            this.xóaToolStripMenuItem1.Size = new System.Drawing.Size(200, 22);
            this.xóaToolStripMenuItem1.Text = "Xóa";
            this.xóaToolStripMenuItem1.Click += new System.EventHandler(this.xóaToolStripMenuItem1_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this._textControl);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 84);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(629, 616);
            this.panel5.TabIndex = 9;
            // 
            // _textControl
            // 
            this._textControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._textControl.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._textControl.Location = new System.Drawing.Point(0, 0);
            this._textControl.Name = "_textControl";
            this._textControl.PageMargins.Bottom = 79;
            this._textControl.PageMargins.Left = 79;
            this._textControl.PageMargins.Right = 79;
            this._textControl.PageMargins.Top = 79;
            this._textControl.Size = new System.Drawing.Size(629, 616);
            this._textControl.TabIndex = 1;
            this._textControl.ViewMode = TXTextControl.ViewMode.Normal;
            // 
            // dlgAddKetQuaSieuAm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 735);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "dlgAddKetQuaSieuAm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them ket qua sieu am";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddKetQuaSieuAm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.dlgAddKetQuaSieuAm_FormClosed);
            this.Load += new System.EventHandler(this.dlgAddKetQuaSieuAm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loaiSieuAmBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picHinh2)).EndInit();
            this.ctmHinh2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picHinh1)).EndInit();
            this.ctmHinh1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSaveAndPrint;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ComboBox cboLoaiSieuAm;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtLamSang;
        private System.Windows.Forms.ComboBox cboBSSieuAm;
        private System.Windows.Forms.ComboBox cboBSCD;
        private System.Windows.Forms.DateTimePicker dtpkNgaySieuAm;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource docStaffViewBindingSource;
        private System.Windows.Forms.BindingSource loaiSieuAmBindingSource;
        private TXTextControl.TextControl _textControl;
        private System.Windows.Forms.Button btnHinh2;
        private System.Windows.Forms.PictureBox picHinh2;
        private System.Windows.Forms.Button btnHinh1;
        private System.Windows.Forms.PictureBox picHinh1;
        private System.Windows.Forms.Button btnSwap;
        private System.Windows.Forms.ContextMenuStrip ctmHinh2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip ctmHinh1;
        private System.Windows.Forms.ToolStripMenuItem xóaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem chọnHìnhTừBênNgoàiToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem chọnHìnhTừBênNgoàiToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}
