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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;
using MM.Databasae;

namespace MM.Controls
{
    public partial class uNormal : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uNormal()
        {
            InitializeComponent();
            cboDoiTuong.SelectedIndex = 1;
        }
        #endregion

        #region Properties
        public DataTable DonViList
        {
            set
            {
                _uNormal_Chung.DonViList = value;
                _uNormal_Nam_Nu.DonViList = value;
                _uNormal_TreEm_NguoiLon_NguoiCaoTuoi.DonViList = value;
                _uNormal_HutThuoc_KhongHutThuoc.DonViList = value;
                _uNormal_Estradiol.DonViList = value;
                _uNormal_Sang_Chieu.DonViList = value;
            }
        }
        #endregion

        #region UI Command
        public List<ChiTietXetNghiem_Manual> GetChiTietXetNghiem_ManualList()
        {
            List<ChiTietXetNghiem_Manual> ctxns = null;
            switch (cboDoiTuong.Text)
            {
                case "Chung":
                    ctxns = new List<ChiTietXetNghiem_Manual>();
                    ctxns.Add(_uNormal_Chung.GetChiTietXetNghiem_Manual());
                    break;
                case "Nam - Nữ":
                    ctxns = _uNormal_Nam_Nu.GetChiTietXetNghiem_ManualList();
                    break;
                case "Trẻ em - Người lớn - Người cao tuổi":
                    ctxns = _uNormal_TreEm_NguoiLon_NguoiCaoTuoi.GetChiTietXetNghiem_ManualList();
                    break;
                case "Sáng - Chiều":
                    ctxns = _uNormal_Sang_Chieu.GetChiTietXetNghiem_ManualList();
                    break;
                case "Hút thuốc - Không hút thuốc":
                    ctxns = _uNormal_HutThuoc_KhongHutThuoc.GetChiTietXetNghiem_ManualList();
                    break;
                case "Âm tính - Dương tính":
                    ctxns = new List<ChiTietXetNghiem_Manual>();
                    ChiTietXetNghiem_Manual ct = _uNormal_Chung.GetChiTietXetNghiem_Manual();
                    ct.DoiTuong = (byte)DoiTuong.AmTinhDuongTinh;
                    ctxns.Add(ct);
                    break;
                case "Estradiol":
                    ctxns = _uNormal_Estradiol.GetChiTietXetNghiem_ManualList();
                    break;
                case "Khác":
                    ctxns = new List<ChiTietXetNghiem_Manual>();
                    ctxns.Add(_uNormal_SoiCanLangNuocTieu.GetChiTietXetNghiem_Manual());
                    break;
            }

            return ctxns;
        }

        public void SetChiTietXetNghiem_ManualList(List<ChiTietXetNghiem_Manual> ctxns)
        {
            if (ctxns == null || ctxns.Count <= 0)
            {
                cboDoiTuong.SelectedIndex = 0;
                return;
            }

            ChiTietXetNghiem_Manual ct = ctxns[0];

            switch ((DoiTuong)ct.DoiTuong)
            {
                case DoiTuong.Chung:
                    cboDoiTuong.Text = "Chung";
                    _uNormal_Chung.SetChiTietXetNghiem_Manual(ct);
                    break;
                case DoiTuong.Nam:
                case DoiTuong.Nu:
                    cboDoiTuong.Text = "Nam - Nữ";
                    _uNormal_Nam_Nu.SetChiTietXetNghiem_ManualList(ctxns);
                    break;
                case DoiTuong.TreEm:
                case DoiTuong.NguoiLon:
                case DoiTuong.NguoiCaoTuoi:
                    cboDoiTuong.Text = "Trẻ em - Người lớn - Người cao tuổi";
                    _uNormal_TreEm_NguoiLon_NguoiCaoTuoi.SetChiTietXetNghiem_ManualList(ctxns);
                    break;
                case DoiTuong.HutThuoc:
                case DoiTuong.KhongHutThuoc:
                    cboDoiTuong.Text = "Hút thuốc - Không hút thuốc";
                    _uNormal_HutThuoc_KhongHutThuoc.SetChiTietXetNghiem_ManualList(ctxns);
                    break;
                case DoiTuong.Sang_Chung:
                case DoiTuong.Chieu_Chung:
                case DoiTuong.Sang_Nam:
                case DoiTuong.Sang_Nu:
                case DoiTuong.Chieu_Nam:
                case DoiTuong.Chieu_Nu:
                    cboDoiTuong.Text = "Sáng - Chiều";
                    _uNormal_Sang_Chieu.SetChiTietXetNghiem_ManualList(ctxns);
                    break;
                case DoiTuong.FollicularPhase:
                case DoiTuong.Midcycle:
                case DoiTuong.LutelPhase:
                    cboDoiTuong.Text = "Estradiol";
                    _uNormal_Estradiol.SetChiTietXetNghiem_ManualList(ctxns);
                    break;
                case DoiTuong.AmTinhDuongTinh:
                    cboDoiTuong.Text = "Âm tính - Dương tính";
                    _uNormal_Chung.SetChiTietXetNghiem_Manual(ct);
                    break;
                case DoiTuong.Khac:
                    cboDoiTuong.Text = "Khác";
                    _uNormal_SoiCanLangNuocTieu.SetChiTietXetNghiem_Manual(ct);
                    break;
            }
        }

        public bool CheckInfo()
        {
            switch (cboDoiTuong.Text)
            {
                case "Chung":
                    return _uNormal_Chung.CheckInfo();
                case "Nam - Nữ":
                    return _uNormal_Nam_Nu.CheckInfo();
                case "Trẻ em - Người lớn - Người cao tuổi":
                    return _uNormal_TreEm_NguoiLon_NguoiCaoTuoi.CheckInfo();
                case "Sáng - Chiều":
                    return _uNormal_Sang_Chieu.CheckInfo();
                case "Hút thuốc - Không hút thuốc":
                    return _uNormal_HutThuoc_KhongHutThuoc.CheckInfo();
                case "Âm tính - Dương tính":
                    return _uNormal_Chung.CheckInfo();
                case "Estradiol":
                    return _uNormal_Estradiol.CheckInfo();
                case "Khác":
                    return _uNormal_SoiCanLangNuocTieu.CheckInfo();
            }

            return true;
        }

        private void ShowControl(string doiTuong)
        {
            foreach (Control ctrl in groupBox1.Controls)
            {
                if (ctrl.GetType() == typeof(Label) || ctrl.GetType() == typeof(ComboBox))
                    continue;

                ctrl.Visible = false;
            }

            switch (doiTuong)
            {
                case "Chung":
                    _uNormal_Chung.Visible = true;
                    break;
                case "Nam - Nữ":
                    _uNormal_Nam_Nu.Visible = true;
                    break;
                case "Trẻ em - Người lớn - Người cao tuổi":
                    _uNormal_TreEm_NguoiLon_NguoiCaoTuoi.Visible = true;
                    break;
                case "Sáng - Chiều":
                    _uNormal_Sang_Chieu.Visible = true;
                    break;
                case "Hút thuốc - Không hút thuốc":
                    _uNormal_HutThuoc_KhongHutThuoc.Visible = true;
                    break;
                case "Âm tính - Dương tính":
                    _uNormal_Chung.Visible = true;
                    break;
                case "Estradiol":
                    _uNormal_Estradiol.Visible = true;
                    break;
                case "Khác":
                    _uNormal_SoiCanLangNuocTieu.Visible = true;
                    break;
            }
        }
        #endregion

        #region Window Event Handlers
        private void cboDoiTuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowControl(cboDoiTuong.Text);
        }
        #endregion
    }
}
