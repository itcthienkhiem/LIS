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
using System.Linq;
using System.Data;
using System.Data.Linq;
using System.Text;
using System.Transactions;
using MM.Common;
using MM.Databasae;

namespace MM.Bussiness
{
    public class HoaDonThuocBus : BusBase
    {
        public static Result GetHoaDonThuocList()
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM HoaDonThuocView WITH(NOLOCK) WHERE Status={0} ORDER BY NgayXuatHoaDon DESC", (byte)Status.Actived);
                return ExcuteQuery(query);
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }

        public static Result GetHoaDonThuocList(bool isFromDateToDate, DateTime fromDate, DateTime toDate, string tenBenhNhan, int type)
        {
            Result result = new Result();

            try
            {
                string query = string.Empty;
                if (isFromDateToDate)
                {
                    if (type == 0) //Tất cả
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM HoaDonThuocView WITH(NOLOCK) WHERE NgayXuatHoaDon BETWEEN '{0}' AND '{1}' ORDER BY NgayXuatHoaDon DESC",
                           fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"));
                    }
                    else if (type == 1) //Chưa xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM HoaDonThuocView WITH(NOLOCK) WHERE Status={0} AND NgayXuatHoaDon BETWEEN '{1}' AND '{2}' ORDER BY NgayXuatHoaDon DESC",
                        (byte)Status.Actived, fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"));
                    }
                    else //Đã xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM HoaDonThuocView WITH(NOLOCK) WHERE Status={0} AND NgayXuatHoaDon BETWEEN '{1}' AND '{2}' ORDER BY NgayXuatHoaDon DESC",
                        (byte)Status.Deactived, fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"));
                    }

                }
                else
                {
                    if (type == 0) //Tất cả
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM HoaDonThuocView WITH(NOLOCK) WHERE TenNguoiMuaHang LIKE N'%{0}%' ORDER BY NgayXuatHoaDon DESC", tenBenhNhan);
                    }
                    else if (type == 1) //Chưa xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM HoaDonThuocView WITH(NOLOCK) WHERE Status={0} AND TenNguoiMuaHang LIKE N'%{1}%' ORDER BY NgayXuatHoaDon DESC",
                        (byte)Status.Actived, tenBenhNhan);
                    }
                    else //Đã xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM HoaDonThuocView WITH(NOLOCK) WHERE Status={0} AND TenNguoiMuaHang LIKE N'%{1}%' ORDER BY NgayXuatHoaDon DESC",
                        (byte)Status.Deactived, tenBenhNhan);
                    }

                }

                return ExcuteQuery(query);
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }

        public static Result GetHoaDonThuocList(DateTime fromDate, DateTime toDate, string tenBenhNhan, string tenDonVi, string maSoThue, int type)
        {
            Result result = new Result();

            try
            {
                string subQuery = string.Empty;
                if (type == 0) subQuery = "1=1";
                else if (type == 1) subQuery = string.Format("Status={0}", (byte)Status.Actived);
                else subQuery = string.Format("Status={0}", (byte)Status.Deactived);

                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM HoaDonThuocView WITH(NOLOCK) WHERE NgayXuatHoaDon BETWEEN '{0}' AND '{1}' AND TenNguoiMuaHang LIKE N'%{2}%' AND TenDonVi LIKE N'%{3}%' AND MaSoThue LIKE N'%{4}%' AND {5} ORDER BY NgayXuatHoaDon DESC",
                           fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"), tenBenhNhan, tenDonVi, maSoThue, subQuery);

                return ExcuteQuery(query);
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }

        public static Result GetChiTietHoaDonThuoc(string hoaDonThuocGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT TenThuoc, SoLuong, DonViTinh, DonGia, ThanhTien FROM ChiTietHoaDonThuoc WITH(NOLOCK) WHERE HoaDonThuocGUID='{0}' AND Status={1} ORDER BY TenThuoc",
                    hoaDonThuocGUID, (byte)Status.Actived);
                return ExcuteQuery(query);
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }

        public static Result GetChiTietPhieuThuThuoc(string phieuThuThuocGUID)
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT TenThuoc, DonViTinh, SoLuong, DonGia, ThanhTien FROM ChiTietPhieuThuThuocView WITH(NOLOCK) WHERE PhieuThuThuocGUID='{0}' AND CTPTTStatus={1} ORDER BY TenThuoc",
                    phieuThuThuocGUID, (byte)Status.Actived);
                return ExcuteQuery(query);
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }

        public static Result GetChiTietPhieuThuThuoc(List<DataRow> phieuThuThuocList)
        {
            Result result = new Result();

            try
            {
                if (phieuThuThuocList == null || phieuThuThuocList.Count <= 0) return result;
                DataTable dtAll = null;

                foreach (DataRow row in phieuThuThuocList)
                {
                    string phieuThuThuocGUID = row["PhieuThuThuocGUID"].ToString();
                    string query = string.Format("SELECT TenThuoc, DonViTinh, SoLuong, DonGia, ThanhTien FROM ChiTietPhieuThuThuocView WITH(NOLOCK) WHERE PhieuThuThuocGUID='{0}' AND CTPTTStatus={1} ORDER BY TenThuoc",
                    phieuThuThuocGUID, (byte)Status.Actived);

                    result = ExcuteQuery(query);

                    if (!result.IsOK) return result;

                    DataTable dt = result.QueryResult as DataTable;
                    if (dtAll == null)
                    {
                        dtAll = new DataTable();
                        dtAll = dt;
                    }
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            dtAll.ImportRow(dr);
                        }
                    }
                }

                result.QueryResult = dtAll;
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }

        public static Result GetHoaDonThuoc(string hoaDonThuocGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                HoaDonThuocView hdt = db.HoaDonThuocViews.SingleOrDefault<HoaDonThuocView>(h => h.HoaDonThuocGUID.ToString() == hoaDonThuocGUID && 
                    h.Status == (byte)Status.Actived);
                result.QueryResult = hdt;
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }

        public static Result GetNgayXuatHoaDon(string soHoaDon)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                HoaDonThuoc hdt = (from i in db.HoaDonThuocs
                                   where i.SoHoaDon == soHoaDon &&
                                   i.Status == (byte)Status.Deactived &&
                                   i.NgayXuatHoaDon >= Global.NgayThayDoiSoHoaDonSauCung
                                   orderby i.NgayXuatHoaDon descending
                                   select i).FirstOrDefault();

                if (hdt != null)
                    result.QueryResult = hdt.NgayXuatHoaDon.Value;
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }

        public static Result CheckHoaDonThuocExistCode(int soHoaDon)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                QuanLySoHoaDon qlshd = db.QuanLySoHoaDons.SingleOrDefault<QuanLySoHoaDon>(q => q.SoHoaDon == soHoaDon &&
                   (q.DaXuat == true || q.XuatTruoc == true) && q.NgayBatDau.Value >= Global.NgayThayDoiSoHoaDonSauCung);

                if (qlshd == null)
                    result.Error.Code = ErrorCode.NOT_EXIST;
                else
                    result.Error.Code = ErrorCode.EXIST;
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }

        public static Result CheckHoaDonThuocExistCode(int soHoaDon, DateTime fromDate, DateTime toDate)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                QuanLySoHoaDon qlshd = db.QuanLySoHoaDons.SingleOrDefault<QuanLySoHoaDon>(q => q.SoHoaDon == soHoaDon &&
                   (q.DaXuat == true || q.XuatTruoc == true) && q.NgayBatDau.Value >= fromDate && q.NgayBatDau < toDate);

                if (qlshd == null)
                    result.Error.Code = ErrorCode.NOT_EXIST;
                else
                    result.Error.Code = ErrorCode.EXIST;
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }

        public static Result DeleteHoaDonThuoc(List<string> keys, List<string> noteList)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    int index = 0;
                    foreach (string key in keys)
                    {
                        HoaDonThuoc hdt = db.HoaDonThuocs.SingleOrDefault<HoaDonThuoc>(i => i.HoaDonThuocGUID.ToString() == key);
                        if (hdt != null)
                        {
                            hdt.DeletedDate = DateTime.Now;
                            hdt.DeletedBy = Guid.Parse(Global.UserGUID);
                            hdt.Status = (byte)Status.Deactived;
                            if (hdt.Notes == null || hdt.Notes.Trim() == string.Empty)
                                hdt.Notes = noteList[index];
                            else
                                hdt.Notes += string.Format(" - {0}", noteList[index]);

                            //Update Exported Invoice
                            if (hdt.PhieuThuThuocGUIDList != null && hdt.PhieuThuThuocGUIDList.Trim() != string.Empty)
                            {
                                string[] pttkeys = hdt.PhieuThuThuocGUIDList.Split(',');
                                foreach (string pttKey in pttkeys)
                                {
                                    PhieuThuThuoc ptt = db.PhieuThuThuocs.SingleOrDefault<PhieuThuThuoc>(r => r.PhieuThuThuocGUID.ToString() == pttKey);
                                    if (ptt != null) ptt.IsExported = false;
                                }

                            }

                            int soHoaDon = Convert.ToInt32(hdt.SoHoaDon);

                            DateTime fromDate = Global.MinDateTime;
                            DateTime toDate = Global.MaxDateTime;
                            var ngayThayDoi = (from n in db.NgayBatDauLamMoiSoHoaDons
                                               where n.KiHieu.ToLower() == hdt.KiHieu &&
                                               n.MauSo.ToLower() == hdt.MauSo.ToLower()
                                               select n).FirstOrDefault();

                            if (ngayThayDoi != null)
                            {
                                fromDate = ngayThayDoi.NgayBatDau;

                                var nextThayDoi = (from n in db.NgayBatDauLamMoiSoHoaDons
                                                   where n.NgayBatDau > ngayThayDoi.NgayBatDau
                                                   orderby n.NgayBatDau ascending
                                                   select n).FirstOrDefault();

                                if (nextThayDoi != null) toDate = nextThayDoi.NgayBatDau;

                                QuanLySoHoaDon qlshd = db.QuanLySoHoaDons.SingleOrDefault<QuanLySoHoaDon>(q => q.SoHoaDon == soHoaDon &&
                                q.NgayBatDau.Value >= fromDate && q.NgayBatDau < toDate);
                                if (qlshd != null) qlshd.DaXuat = false;
                                else
                                {
                                    qlshd = new QuanLySoHoaDon();
                                    qlshd.QuanLySoHoaDonGUID = Guid.NewGuid();
                                    qlshd.SoHoaDon = soHoaDon;
                                    qlshd.DaXuat = false;
                                    qlshd.XuatTruoc = false;
                                    qlshd.NgayBatDau = fromDate;
                                    db.QuanLySoHoaDons.InsertOnSubmit(qlshd);
                                }
                            }

                            string htttStr = Utility.ParseHinhThucThanhToanToStr((PaymentType)hdt.HinhThucThanhToan);

                            desc += string.Format("- GUID: '{0}', Mã hóa đơn: '{1}', Ngày xuất HĐ: '{2}', Người mua hàng: '{3}', Tên đơn vị: '{4}', Địa chỉ: '{5}', STK: '{6}', Hình thức thanh toán: '{7}', Ghi chú: '{8}', Đã thu tiền: '{9}'\n",
                                hdt.HoaDonThuocGUID.ToString(), hdt.SoHoaDon, hdt.NgayXuatHoaDon.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                hdt.TenNguoiMuaHang, hdt.TenDonVi, hdt.DiaChi, hdt.SoTaiKhoan, htttStr, noteList[index], !hdt.ChuaThuTien);
                        }

                        index++;
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin hóa đơn thuốc";
                    tk.Description = desc;
                    tk.TrackingType = (byte)TrackingType.Price;
                    tk.ComputerName = Utility.GetDNSHostName();
                    db.Trackings.InsertOnSubmit(tk);

                    db.SubmitChanges();
                    t.Complete();
                }
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }

        public static Result InsertHoaDonThuoc(HoaDonThuoc hdt, List<ChiTietHoaDonThuoc> addedDetails)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    hdt.HoaDonThuocGUID = Guid.NewGuid();
                    db.HoaDonThuocs.InsertOnSubmit(hdt);
                    db.SubmitChanges();

                    string htttStr = Utility.ParseHinhThucThanhToanToStr((PaymentType)hdt.HinhThucThanhToan);

                    desc += string.Format("- Hóa đơn thuốc: GUID: '{0}', Mã hóa đơn: '{1}', Ngày xuất HĐ: '{2}', Người mua hàng: '{3}', Tên đơn vị: '{4}', Địa chỉ: '{5}', STK: '{6}', Hình thức thanh toán: '{7}', Ghi chú: '{8}', Đã thu tiền: '{9}'\n",
                                hdt.HoaDonThuocGUID.ToString(), hdt.SoHoaDon, hdt.NgayXuatHoaDon.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                hdt.TenNguoiMuaHang, hdt.TenDonVi, hdt.DiaChi, hdt.SoTaiKhoan, htttStr, hdt.Notes, !hdt.ChuaThuTien);

                    if (addedDetails != null && addedDetails.Count > 0)
                    {
                        desc += "- Chi tiết hóa đơn:\n";

                        foreach (ChiTietHoaDonThuoc detail in addedDetails)
                        {
                            detail.ChiTietHoaDonThuocGUID = Guid.NewGuid();
                            detail.HoaDonThuocGUID = hdt.HoaDonThuocGUID;
                            db.ChiTietHoaDonThuocs.InsertOnSubmit(detail);

                            desc += string.Format("  + GUID: '{0}', Dịch vụ: '{1}', ĐVT: '{2}', Số lượng: '{3}', Đơn giá: '{4}', Thành tiền: '{5}', Loai: '{6}'\n",
                                detail.ChiTietHoaDonThuocGUID.ToString(), detail.TenThuoc, detail.DonViTinh, detail.SoLuong, detail.DonGia, detail.ThanhTien, detail.Loai);
                        }

                        db.SubmitChanges();
                    }

                    //Update Exported Invoice
                    if (hdt.PhieuThuThuocGUIDList != null && hdt.PhieuThuThuocGUIDList.Trim() != string.Empty)
                    {
                        string[] pttkeys = hdt.PhieuThuThuocGUIDList.Split(',');
                        foreach (string pttKey in pttkeys)
                        {
                            PhieuThuThuoc ptt = db.PhieuThuThuocs.SingleOrDefault<PhieuThuThuoc>(r => r.PhieuThuThuocGUID.ToString() == pttKey);
                            if (ptt != null) ptt.IsExported = true;
                        }

                    }

                    int soHoaDon = Convert.ToInt32(hdt.SoHoaDon);
                    QuanLySoHoaDon qlshd = db.QuanLySoHoaDons.SingleOrDefault<QuanLySoHoaDon>(q => q.SoHoaDon == soHoaDon && 
                        q.NgayBatDau.Value >= Global.NgayThayDoiSoHoaDonSauCung);
                    if (qlshd != null) qlshd.DaXuat = true;
                    else
                    {
                        qlshd = new QuanLySoHoaDon();
                        qlshd.QuanLySoHoaDonGUID = Guid.NewGuid();
                        qlshd.SoHoaDon = soHoaDon;
                        qlshd.DaXuat = true;
                        qlshd.XuatTruoc = false;
                        qlshd.NgayBatDau = Global.NgayThayDoiSoHoaDonSauCung;
                        db.QuanLySoHoaDons.InsertOnSubmit(qlshd);
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Add;
                    tk.Action = "Thêm thông tin hóa đơn thuốc";
                    tk.Description = desc;
                    tk.TrackingType = (byte)TrackingType.Price;
                    tk.ComputerName = Utility.GetDNSHostName();
                    db.Trackings.InsertOnSubmit(tk);

                    db.SubmitChanges();
                    t.Complete();
                }

            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }

        public static Result UpdateDaThuTienInvoice(string hoaDonThuocGUID, bool daThuTien)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    HoaDonThuoc invoice = db.HoaDonThuocs.SingleOrDefault<HoaDonThuoc>(i => i.HoaDonThuocGUID.ToString() == hoaDonThuocGUID);
                    if (invoice != null)
                    {
                        invoice.UpdatedDate = DateTime.Now;
                        invoice.UpdatedBy = Guid.Parse(Global.UserGUID);
                        invoice.ChuaThuTien = !daThuTien;

                        string htttStr = Utility.ParseHinhThucThanhToanToStr((PaymentType)invoice.HinhThucThanhToan);

                        desc += string.Format("- Hóa đơn thuốc: GUID: '{0}', Mã hóa đơn: '{1}', Ngày xuất HĐ: '{2}', Người mua hàng: '{3}', Tên đơn vị: '{4}', Địa chỉ: '{5}', STK: '{6}', Hình thức thanh toán: '{7}', Ghi chú: '{8}', Đã thu tiền: '{9}'\n",
                                    invoice.HoaDonThuocGUID.ToString(), invoice.SoHoaDon, invoice.NgayXuatHoaDon.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                    invoice.TenNguoiMuaHang, invoice.TenDonVi, invoice.DiaChi, invoice.SoTaiKhoan, htttStr, invoice.Notes, !invoice.ChuaThuTien);

                        //Tracking
                        desc = desc.Substring(0, desc.Length - 1);
                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Cập nhật thông tin hóa đơn thuốc";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        tk.ComputerName = Utility.GetDNSHostName();
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }


                    t.Complete();
                }

            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }
    }
}
