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
    partial class dlgAddServiceHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddServiceHistory));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.raNormal = new System.Windows.Forms.RadioButton();
            this.raNegative = new System.Windows.Forms.RadioButton();
            this.gbNormal = new System.Windows.Forms.GroupBox();
            this.chkAbnormal = new System.Windows.Forms.CheckBox();
            this.chkNormal = new System.Windows.Forms.CheckBox();
            this.gbNegative = new System.Windows.Forms.GroupBox();
            this.chkPositive = new System.Windows.Forms.CheckBox();
            this.chkNegative = new System.Windows.Forms.CheckBox();
            this.raKhamTheoHopDong = new System.Windows.Forms.RadioButton();
            this.raKhamTuTuc = new System.Windows.Forms.RadioButton();
            this.chkChuyenNhuong = new System.Windows.Forms.CheckBox();
            this.txtChuyenNhuong = new System.Windows.Forms.TextBox();
            this.btnChonBenhNhan = new System.Windows.Forms.Button();
            this.btnChonDichVu = new System.Windows.Forms.Button();
            this.chkBSCD = new System.Windows.Forms.CheckBox();
            this.cboBacSiChiDinh = new System.Windows.Forms.ComboBox();
            this.docStaffViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.numDiscount = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpkActiveDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.cboDocStaff = new System.Windows.Forms.ComboBox();
            this.cboService = new System.Windows.Forms.ComboBox();
            this.serviceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lbUnit = new System.Windows.Forms.Label();
            this.numPrice = new System.Windows.Forms.NumericUpDown();
            this.lbPrice = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.patientViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbNormal.SuspendLayout();
            this.gbNegative.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serviceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.patientViewBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numSoLuong);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.raKhamTheoHopDong);
            this.groupBox1.Controls.Add(this.raKhamTuTuc);
            this.groupBox1.Controls.Add(this.chkChuyenNhuong);
            this.groupBox1.Controls.Add(this.txtChuyenNhuong);
            this.groupBox1.Controls.Add(this.btnChonBenhNhan);
            this.groupBox1.Controls.Add(this.btnChonDichVu);
            this.groupBox1.Controls.Add(this.chkBSCD);
            this.groupBox1.Controls.Add(this.cboBacSiChiDinh);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numDiscount);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtpkActiveDate);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cboDocStaff);
            this.groupBox1.Controls.Add(this.cboService);
            this.groupBox1.Controls.Add(this.lbUnit);
            this.groupBox1.Controls.Add(this.numPrice);
            this.groupBox1.Controls.Add(this.lbPrice);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(532, 341);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin dịch vụ";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(135, 232);
            this.txtDescription.MaxLength = 4000;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(269, 96);
            this.txtDescription.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(76, 235);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Nhận xét:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.raNormal);
            this.panel1.Controls.Add(this.raNegative);
            this.panel1.Controls.Add(this.gbNormal);
            this.panel1.Controls.Add(this.gbNegative);
            this.panel1.Location = new System.Drawing.Point(117, 233);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(360, 108);
            this.panel1.TabIndex = 13;
            this.panel1.Visible = false;
            // 
            // raNormal
            // 
            this.raNormal.AutoSize = true;
            this.raNormal.Checked = true;
            this.raNormal.Location = new System.Drawing.Point(18, 5);
            this.raNormal.Name = "raNormal";
            this.raNormal.Size = new System.Drawing.Size(14, 13);
            this.raNormal.TabIndex = 13;
            this.raNormal.TabStop = true;
            this.raNormal.UseVisualStyleBackColor = true;
            this.raNormal.CheckedChanged += new System.EventHandler(this.raNormal_CheckedChanged);
            // 
            // raNegative
            // 
            this.raNegative.AutoSize = true;
            this.raNegative.Location = new System.Drawing.Point(18, 57);
            this.raNegative.Name = "raNegative";
            this.raNegative.Size = new System.Drawing.Size(14, 13);
            this.raNegative.TabIndex = 15;
            this.raNegative.UseVisualStyleBackColor = true;
            this.raNegative.CheckedChanged += new System.EventHandler(this.raNegative_CheckedChanged);
            // 
            // gbNormal
            // 
            this.gbNormal.Controls.Add(this.chkAbnormal);
            this.gbNormal.Controls.Add(this.chkNormal);
            this.gbNormal.Location = new System.Drawing.Point(23, 3);
            this.gbNormal.Name = "gbNormal";
            this.gbNormal.Size = new System.Drawing.Size(269, 47);
            this.gbNormal.TabIndex = 14;
            this.gbNormal.TabStop = false;
            // 
            // chkAbnormal
            // 
            this.chkAbnormal.AutoSize = true;
            this.chkAbnormal.Location = new System.Drawing.Point(128, 19);
            this.chkAbnormal.Name = "chkAbnormal";
            this.chkAbnormal.Size = new System.Drawing.Size(78, 17);
            this.chkAbnormal.TabIndex = 1;
            this.chkAbnormal.Text = "Bất thường";
            this.chkAbnormal.UseVisualStyleBackColor = true;
            this.chkAbnormal.CheckedChanged += new System.EventHandler(this.chkAbnormal_CheckedChanged);
            // 
            // chkNormal
            // 
            this.chkNormal.AutoSize = true;
            this.chkNormal.Checked = true;
            this.chkNormal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNormal.Location = new System.Drawing.Point(20, 19);
            this.chkNormal.Name = "chkNormal";
            this.chkNormal.Size = new System.Drawing.Size(83, 17);
            this.chkNormal.TabIndex = 0;
            this.chkNormal.Text = "Bình thường";
            this.chkNormal.UseVisualStyleBackColor = true;
            this.chkNormal.CheckedChanged += new System.EventHandler(this.chkNormal_CheckedChanged);
            // 
            // gbNegative
            // 
            this.gbNegative.Controls.Add(this.chkPositive);
            this.gbNegative.Controls.Add(this.chkNegative);
            this.gbNegative.Enabled = false;
            this.gbNegative.Location = new System.Drawing.Point(23, 57);
            this.gbNegative.Name = "gbNegative";
            this.gbNegative.Size = new System.Drawing.Size(269, 47);
            this.gbNegative.TabIndex = 16;
            this.gbNegative.TabStop = false;
            // 
            // chkPositive
            // 
            this.chkPositive.AutoSize = true;
            this.chkPositive.Location = new System.Drawing.Point(128, 19);
            this.chkPositive.Name = "chkPositive";
            this.chkPositive.Size = new System.Drawing.Size(80, 17);
            this.chkPositive.TabIndex = 3;
            this.chkPositive.Text = "Dương tính";
            this.chkPositive.UseVisualStyleBackColor = true;
            this.chkPositive.CheckedChanged += new System.EventHandler(this.chkPositive_CheckedChanged);
            // 
            // chkNegative
            // 
            this.chkNegative.AutoSize = true;
            this.chkNegative.Checked = true;
            this.chkNegative.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNegative.Location = new System.Drawing.Point(20, 19);
            this.chkNegative.Name = "chkNegative";
            this.chkNegative.Size = new System.Drawing.Size(63, 17);
            this.chkNegative.TabIndex = 2;
            this.chkNegative.Text = "Âm tính";
            this.chkNegative.UseVisualStyleBackColor = true;
            this.chkNegative.CheckedChanged += new System.EventHandler(this.chkNegative_CheckedChanged);
            // 
            // raKhamTheoHopDong
            // 
            this.raKhamTheoHopDong.AutoSize = true;
            this.raKhamTheoHopDong.Location = new System.Drawing.Point(233, 70);
            this.raKhamTheoHopDong.Name = "raKhamTheoHopDong";
            this.raKhamTheoHopDong.Size = new System.Drawing.Size(125, 17);
            this.raKhamTheoHopDong.TabIndex = 4;
            this.raKhamTheoHopDong.Text = "Khám theo hợp đồng";
            this.raKhamTheoHopDong.UseVisualStyleBackColor = true;
            this.raKhamTheoHopDong.CheckedChanged += new System.EventHandler(this.raKhamTheoHopDong_CheckedChanged);
            // 
            // raKhamTuTuc
            // 
            this.raKhamTuTuc.AutoSize = true;
            this.raKhamTuTuc.Checked = true;
            this.raKhamTuTuc.Location = new System.Drawing.Point(135, 70);
            this.raKhamTuTuc.Name = "raKhamTuTuc";
            this.raKhamTuTuc.Size = new System.Drawing.Size(82, 17);
            this.raKhamTuTuc.TabIndex = 3;
            this.raKhamTuTuc.TabStop = true;
            this.raKhamTuTuc.Text = "Khám tự túc";
            this.raKhamTuTuc.UseVisualStyleBackColor = true;
            this.raKhamTuTuc.CheckedChanged += new System.EventHandler(this.raKhamTuTuc_CheckedChanged);
            // 
            // chkChuyenNhuong
            // 
            this.chkChuyenNhuong.AutoSize = true;
            this.chkChuyenNhuong.Location = new System.Drawing.Point(13, 141);
            this.chkChuyenNhuong.Name = "chkChuyenNhuong";
            this.chkChuyenNhuong.Size = new System.Drawing.Size(116, 17);
            this.chkChuyenNhuong.TabIndex = 8;
            this.chkChuyenNhuong.Text = "Chuyển nhượng từ:";
            this.chkChuyenNhuong.UseVisualStyleBackColor = true;
            this.chkChuyenNhuong.CheckedChanged += new System.EventHandler(this.chkChuyenNhuong_CheckedChanged);
            // 
            // txtChuyenNhuong
            // 
            this.txtChuyenNhuong.Enabled = false;
            this.txtChuyenNhuong.Location = new System.Drawing.Point(135, 139);
            this.txtChuyenNhuong.Name = "txtChuyenNhuong";
            this.txtChuyenNhuong.Size = new System.Drawing.Size(269, 20);
            this.txtChuyenNhuong.TabIndex = 9;
            // 
            // btnChonBenhNhan
            // 
            this.btnChonBenhNhan.Enabled = false;
            this.btnChonBenhNhan.Location = new System.Drawing.Point(408, 138);
            this.btnChonBenhNhan.Name = "btnChonBenhNhan";
            this.btnChonBenhNhan.Size = new System.Drawing.Size(110, 23);
            this.btnChonBenhNhan.TabIndex = 10;
            this.btnChonBenhNhan.Text = "Chọn bệnh nhân...";
            this.btnChonBenhNhan.UseVisualStyleBackColor = true;
            this.btnChonBenhNhan.Click += new System.EventHandler(this.btnChonBenhNhan_Click);
            // 
            // btnChonDichVu
            // 
            this.btnChonDichVu.Location = new System.Drawing.Point(408, 20);
            this.btnChonDichVu.Name = "btnChonDichVu";
            this.btnChonDichVu.Size = new System.Drawing.Size(110, 23);
            this.btnChonDichVu.TabIndex = 1;
            this.btnChonDichVu.Text = "Chọn dịch vụ...";
            this.btnChonDichVu.UseVisualStyleBackColor = true;
            this.btnChonDichVu.Click += new System.EventHandler(this.btnChonDichVu_Click);
            // 
            // chkBSCD
            // 
            this.chkBSCD.AutoSize = true;
            this.chkBSCD.Location = new System.Drawing.Point(71, 117);
            this.chkBSCD.Name = "chkBSCD";
            this.chkBSCD.Size = new System.Drawing.Size(58, 17);
            this.chkBSCD.TabIndex = 6;
            this.chkBSCD.Text = "BSCĐ:";
            this.chkBSCD.UseVisualStyleBackColor = true;
            this.chkBSCD.CheckedChanged += new System.EventHandler(this.chkBSCD_CheckedChanged);
            // 
            // cboBacSiChiDinh
            // 
            this.cboBacSiChiDinh.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboBacSiChiDinh.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboBacSiChiDinh.DataSource = this.docStaffViewBindingSource;
            this.cboBacSiChiDinh.DisplayMember = "Fullname";
            this.cboBacSiChiDinh.Enabled = false;
            this.cboBacSiChiDinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cboBacSiChiDinh.FormattingEnabled = true;
            this.cboBacSiChiDinh.Location = new System.Drawing.Point(135, 115);
            this.cboBacSiChiDinh.Name = "cboBacSiChiDinh";
            this.cboBacSiChiDinh.Size = new System.Drawing.Size(269, 21);
            this.cboBacSiChiDinh.TabIndex = 7;
            this.cboBacSiChiDinh.ValueMember = "DocStaffGUID";
            // 
            // docStaffViewBindingSource
            // 
            this.docStaffViewBindingSource.DataSource = typeof(MM.Databasae.DocStaffView);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(209, 188);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "(%)";
            // 
            // numDiscount
            // 
            this.numDiscount.DecimalPlaces = 1;
            this.numDiscount.Location = new System.Drawing.Point(135, 185);
            this.numDiscount.Name = "numDiscount";
            this.numDiscount.Size = new System.Drawing.Size(69, 20);
            this.numDiscount.TabIndex = 12;
            this.numDiscount.ThousandsSeparator = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(95, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Giảm:";
            // 
            // dtpkActiveDate
            // 
            this.dtpkActiveDate.CustomFormat = "dd/MM/yyyy";
            this.dtpkActiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkActiveDate.Location = new System.Drawing.Point(135, 45);
            this.dtpkActiveDate.Name = "dtpkActiveDate";
            this.dtpkActiveDate.Size = new System.Drawing.Size(122, 20);
            this.dtpkActiveDate.TabIndex = 2;
            this.dtpkActiveDate.ValueChanged += new System.EventHandler(this.dtpkActiveDate_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(53, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Ngày sử dụng:";
            // 
            // cboDocStaff
            // 
            this.cboDocStaff.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboDocStaff.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDocStaff.DataSource = this.docStaffViewBindingSource;
            this.cboDocStaff.DisplayMember = "Fullname";
            this.cboDocStaff.FormattingEnabled = true;
            this.cboDocStaff.Location = new System.Drawing.Point(135, 91);
            this.cboDocStaff.Name = "cboDocStaff";
            this.cboDocStaff.Size = new System.Drawing.Size(269, 21);
            this.cboDocStaff.TabIndex = 5;
            this.cboDocStaff.ValueMember = "DocStaffGUID";
            // 
            // cboService
            // 
            this.cboService.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboService.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboService.DataSource = this.serviceBindingSource;
            this.cboService.DisplayMember = "Name";
            this.cboService.FormattingEnabled = true;
            this.cboService.Location = new System.Drawing.Point(135, 21);
            this.cboService.Name = "cboService";
            this.cboService.Size = new System.Drawing.Size(269, 21);
            this.cboService.TabIndex = 0;
            this.cboService.ValueMember = "ServiceGUID";
            this.cboService.SelectedValueChanged += new System.EventHandler(this.cboService_SelectedValueChanged);
            // 
            // serviceBindingSource
            // 
            this.serviceBindingSource.DataSource = typeof(MM.Databasae.Service);
            // 
            // lbUnit
            // 
            this.lbUnit.AutoSize = true;
            this.lbUnit.Location = new System.Drawing.Point(260, 165);
            this.lbUnit.Name = "lbUnit";
            this.lbUnit.Size = new System.Drawing.Size(36, 13);
            this.lbUnit.TabIndex = 8;
            this.lbUnit.Text = "(VNĐ)";
            // 
            // numPrice
            // 
            this.numPrice.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numPrice.Location = new System.Drawing.Point(135, 162);
            this.numPrice.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numPrice.Name = "numPrice";
            this.numPrice.Size = new System.Drawing.Size(121, 20);
            this.numPrice.TabIndex = 11;
            this.numPrice.ThousandsSeparator = true;
            this.numPrice.Value = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            // 
            // lbPrice
            // 
            this.lbPrice.AutoSize = true;
            this.lbPrice.Location = new System.Drawing.Point(103, 165);
            this.lbPrice.Name = "lbPrice";
            this.lbPrice.Size = new System.Drawing.Size(26, 13);
            this.lbPrice.TabIndex = 2;
            this.lbPrice.Text = "Giá:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Bác sĩ thực hiện:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dịch vụ:";
            // 
            // patientViewBindingSource
            // 
            this.patientViewBindingSource.DataSource = typeof(MM.Databasae.PatientView);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(267, 351);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(188, 351);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // numSoLuong
            // 
            this.numSoLuong.Location = new System.Drawing.Point(135, 208);
            this.numSoLuong.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numSoLuong.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSoLuong.Name = "numSoLuong";
            this.numSoLuong.Size = new System.Drawing.Size(69, 20);
            this.numSoLuong.TabIndex = 13;
            this.numSoLuong.ThousandsSeparator = true;
            this.numSoLuong.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(77, 211);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Số lượng:";
            // 
            // dlgAddServiceHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(545, 379);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddServiceHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them su dung dich vu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddServiceHistory_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddServiceHistory_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbNormal.ResumeLayout(false);
            this.gbNormal.PerformLayout();
            this.gbNegative.ResumeLayout(false);
            this.gbNegative.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serviceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.patientViewBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboDocStaff;
        private System.Windows.Forms.ComboBox cboService;
        private System.Windows.Forms.Label lbUnit;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.NumericUpDown numPrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbPrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.BindingSource docStaffViewBindingSource;
        private System.Windows.Forms.BindingSource serviceBindingSource;
        private System.Windows.Forms.DateTimePicker dtpkActiveDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numDiscount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton raNegative;
        private System.Windows.Forms.RadioButton raNormal;
        private System.Windows.Forms.GroupBox gbNegative;
        private System.Windows.Forms.CheckBox chkPositive;
        private System.Windows.Forms.CheckBox chkNegative;
        private System.Windows.Forms.GroupBox gbNormal;
        private System.Windows.Forms.CheckBox chkAbnormal;
        private System.Windows.Forms.CheckBox chkNormal;
        private System.Windows.Forms.ComboBox cboBacSiChiDinh;
        private System.Windows.Forms.CheckBox chkBSCD;
        private System.Windows.Forms.Button btnChonDichVu;
        private System.Windows.Forms.BindingSource patientViewBindingSource;
        private System.Windows.Forms.Button btnChonBenhNhan;
        private System.Windows.Forms.TextBox txtChuyenNhuong;
        private System.Windows.Forms.CheckBox chkChuyenNhuong;
        private System.Windows.Forms.RadioButton raKhamTheoHopDong;
        private System.Windows.Forms.RadioButton raKhamTuTuc;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown numSoLuong;
        private System.Windows.Forms.Label label7;
    }
}
