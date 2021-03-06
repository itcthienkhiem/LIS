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
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Transactions;
using MM.Common;
using MM.Databasae;


namespace MM.Bussiness
{
    public class HoaDonXuatTruocBus : BusBase
    {
        public static Result GetHoaDonXuatTruocList()
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM HoaDonXuatTruocView WITH(NOLOCK) WHERE Status={0} ORDER BY NgayXuatHoaDon DESC", (byte)Status.Actived);
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

        public static Result GetSoHoaDonXuatTruocList()
        {
            Result result = new Result();

            try
            {

                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM QuanLySoHoaDon WITH(NOLOCK) WHERE XuatTruoc='True' AND NgayBatDau >= '{0}' ORDER BY SoHoaDon",
                    Global.NgayThayDoiSoHoaDonSauCung.ToString("yyyy-MM-dd HH:mm:ss"));
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

        public static Result GetSoHoaDonXuatTruocList(DateTime fromDate, DateTime toDate)
        {
            Result result = new Result();

            try
            {

                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM QuanLySoHoaDon WITH(NOLOCK) WHERE XuatTruoc='True' AND NgayBatDau >= '{0}' AND NgayBatDau < '{1}' ORDER BY SoHoaDon",
                    fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"));
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

        public static Result GetHoaDonXuatTruocList(bool isFromDateToDate, DateTime fromDate, DateTime toDate, string tenBenhNhan, int type)
        {
            Result result = new Result();

            try
            {
                string query = string.Empty;
                if (isFromDateToDate)
                {
                    if (type == 0) //Tất cả
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM HoaDonXuatTruocView WITH(NOLOCK) WHERE NgayXuatHoaDon BETWEEN '{0}' AND '{1}' ORDER BY NgayXuatHoaDon DESC",
                           fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"));
                    }
                    else if (type == 1) //Chưa xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM HoaDonXuatTruocView WITH(NOLOCK) WHERE Status={0} AND NgayXuatHoaDon BETWEEN '{1}' AND '{2}' ORDER BY NgayXuatHoaDon DESC",
                        (byte)Status.Actived, fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"));
                    }
                    else //Đã xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM HoaDonXuatTruocView WITH(NOLOCK) WHERE Status={0} AND NgayXuatHoaDon BETWEEN '{1}' AND '{2}' ORDER BY NgayXuatHoaDon DESC",
                        (byte)Status.Deactived, fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"));
                    }

                }
                else
                {
                    if (type == 0) //Tất cả
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM HoaDonXuatTruocView WITH(NOLOCK) WHERE TenNguoiMuaHang LIKE N'%{0}%' ORDER BY NgayXuatHoaDon DESC", tenBenhNhan);
                    }
                    else if (type == 1) //Chưa xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM HoaDonXuatTruocView WITH(NOLOCK) WHERE Status={0} AND TenNguoiMuaHang LIKE N'%{1}%' ORDER BY NgayXuatHoaDon DESC",
                        (byte)Status.Actived, tenBenhNhan);
                    }
                    else //Đã xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM HoaDonXuatTruocView WITH(NOLOCK) WHERE Status={0} AND TenNguoiMuaHang LIKE N'%{1}%' ORDER BY NgayXuatHoaDon DESC",
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

        public static Result GetHoaDonXuatTruocList(DateTime fromDate, DateTime toDate, string tenBenhNhan, string tenDonVi, string maSoThue, int type)
        {
            Result result = new Result();

            try
            {
                string subQuery = string.Empty;
                if (type == 0) subQuery = "1=1";
                else if (type == 1) subQuery = string.Format("Status={0}", (byte)Status.Actived);
                else subQuery = string.Format("Status={0}", (byte)Status.Deactived);

                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM HoaDonXuatTruocView WITH(NOLOCK) WHERE NgayXuatHoaDon BETWEEN '{0}' AND '{1}' AND TenNguoiMuaHang LIKE N'%{2}%' AND TenDonVi LIKE N'%{3}%' AND MaSoThue LIKE N'%{4}%' AND {5} ORDER BY NgayXuatHoaDon DESC",
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

        public static Result GetChiTietHoaDonXuatTruoc(string hoaDonXuatTruocGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT TenMatHang, SoLuong, DonViTinh, DonGia, ThanhTien FROM ChiTietHoaDonXuatTruoc WITH(NOLOCK) WHERE HoaDonXuatTruocGUID='{0}' AND Status={1} ORDER BY TenMatHang",
                    hoaDonXuatTruocGUID, (byte)Status.Actived);
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

        public static Result GetHoaDonXuatTruoc(string hoaDonXuatTruocGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                HoaDonXuatTruocView hdt = db.HoaDonXuatTruocViews.SingleOrDefault<HoaDonXuatTruocView>(h => h.HoaDonXuatTruocGUID.ToString() == hoaDonXuatTruocGUID &&
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
                HoaDonXuatTruoc hdt = (from i in db.HoaDonXuatTruocs
                                   where i.SoHoaDon == soHoaDon &&
                                   i.Status == (byte)Status.Deactived &&
                                   i.NgayXuatHoaDon >= Global.NgayThayDoiSoHoaDonSauCung
                                   orderby i.NgayXuatHoaDon descending
                                   select i).FirstOrDefault();

                if (hdt != null)
                    result.QueryResult = hdt.NgayXuatHoaDon;
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

        public static Result GetNgayXuatHoaDon(string soHoaDon, DateTime fromDate, DateTime toDate)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                HoaDonXuatTruoc hdt = (from i in db.HoaDonXuatTruocs
                                       where i.SoHoaDon == soHoaDon &&
                                       i.Status == (byte)Status.Deactived &&
                                       i.NgayXuatHoaDon >= fromDate && i.NgayXuatHoaDon < toDate
                                       orderby i.NgayXuatHoaDon descending
                                       select i).FirstOrDefault();

                if (hdt != null)
                    result.QueryResult = hdt.NgayXuatHoaDon;
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

        public static Result CheckHoaDonXuatTruocExistCode(int soHoaDon)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                QuanLySoHoaDon qlshd = db.QuanLySoHoaDons.SingleOrDefault<QuanLySoHoaDon>(q => q.SoHoaDon == soHoaDon && q.DaXuat == true &&
                    q.NgayBatDau >= Global.NgayThayDoiSoHoaDonSauCung);

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

        public static Result CheckHoaDonXuatTruocExistCode(int soHoaDon, DateTime fromDate, DateTime toDate)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                QuanLySoHoaDon qlshd = db.QuanLySoHoaDons.SingleOrDefault<QuanLySoHoaDon>(q => q.SoHoaDon == soHoaDon && q.DaXuat == true &&
                    q.NgayBatDau >= fromDate && q.NgayBatDau < toDate);

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

        public static Result DeleteHoaDonXuatTruoc(List<string> keys, List<string> noteList)
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
                        HoaDonXuatTruoc hdt = db.HoaDonXuatTruocs.SingleOrDefault<HoaDonXuatTruoc>(i => i.HoaDonXuatTruocGUID.ToString() == key);
                        if (hdt != null)
                        {
                            hdt.DeletedDate = DateTime.Now;
                            hdt.DeletedBy = Guid.Parse(Global.UserGUID);
                            hdt.Status = (byte)Status.Deactived;
                            if (hdt.Notes == null || hdt.Notes.Trim() == string.Empty)
                                hdt.Notes = noteList[index];
                            else
                                hdt.Notes += string.Format(" - {0}", noteList[index]);

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
                                    qlshd.XuatTruoc = true;
                                    qlshd.NgayBatDau = fromDate;
                                    db.QuanLySoHoaDons.InsertOnSubmit(qlshd);
                                }
                            }

                            string htttStr = Utility.ParseHinhThucThanhToanToStr((PaymentType)hdt.HinhThucThanhToan);
                            desc += string.Format("- GUID: '{0}', Mã hóa đơn: '{1}', Ngày xuất HĐ: '{2}', Người mua hàng: '{3}', Tên đơn vị: '{4}', Địa chỉ: '{5}', STK: '{6}', Hình thức thanh toán: '{7}', Ghi chú: '{8}', Đã thu tiền: '{9}'\n",
                                hdt.HoaDonXuatTruocGUID.ToString(), hdt.SoHoaDon, hdt.NgayXuatHoaDon.ToString("dd/MM/yyyy HH:mm:ss"),
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
                    tk.Action = "Xóa thông tin hóa đơn xuất trước";
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

        public static Result DeleteHoaDonXuatTruoc(List<string> keys, List<string> noteList, DateTime fromDate, DateTime toDate)
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
                        HoaDonXuatTruoc hdt = db.HoaDonXuatTruocs.SingleOrDefault<HoaDonXuatTruoc>(i => i.HoaDonXuatTruocGUID.ToString() == key);
                        if (hdt != null)
                        {
                            hdt.DeletedDate = DateTime.Now;
                            hdt.DeletedBy = Guid.Parse(Global.UserGUID);
                            hdt.Status = (byte)Status.Deactived;
                            if (hdt.Notes == null || hdt.Notes.Trim() == string.Empty)
                                hdt.Notes = noteList[index];
                            else
                                hdt.Notes += string.Format(" - {0}", noteList[index]);

                            int soHoaDon = Convert.ToInt32(hdt.SoHoaDon);
                            QuanLySoHoaDon qlshd = db.QuanLySoHoaDons.SingleOrDefault<QuanLySoHoaDon>(q => q.SoHoaDon == soHoaDon &&
                                q.NgayBatDau.Value >= fromDate && q.NgayBatDau < toDate);
                            if (qlshd != null) qlshd.DaXuat = false;
                            else
                            {
                                qlshd = new QuanLySoHoaDon();
                                qlshd.QuanLySoHoaDonGUID = Guid.NewGuid();
                                qlshd.SoHoaDon = soHoaDon;
                                qlshd.DaXuat = false;
                                qlshd.XuatTruoc = true;
                                qlshd.NgayBatDau = fromDate;
                                db.QuanLySoHoaDons.InsertOnSubmit(qlshd);
                            }

                            string htttStr = Utility.ParseHinhThucThanhToanToStr((PaymentType)hdt.HinhThucThanhToan);
                            desc += string.Format("- GUID: '{0}', Mã hóa đơn: '{1}', Ngày xuất HĐ: '{2}', Người mua hàng: '{3}', Tên đơn vị: '{4}', Địa chỉ: '{5}', STK: '{6}', Hình thức thanh toán: '{7}', Ghi chú: '{8}', Đã thu tiền: '{9}'\n",
                                hdt.HoaDonXuatTruocGUID.ToString(), hdt.SoHoaDon, hdt.NgayXuatHoaDon.ToString("dd/MM/yyyy HH:mm:ss"),
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
                    tk.Action = "Xóa thông tin hóa đơn xuất trước";
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

        public static Result InsertHoaDonXuatTruoc(HoaDonXuatTruoc hdt, List<ChiTietHoaDonXuatTruoc> addedDetails)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    hdt.HoaDonXuatTruocGUID = Guid.NewGuid();
                    db.HoaDonXuatTruocs.InsertOnSubmit(hdt);
                    db.SubmitChanges();

                    string htttStr = Utility.ParseHinhThucThanhToanToStr((PaymentType)hdt.HinhThucThanhToan);

                    desc += string.Format("- Hóa đơn xuất trước: GUID: '{0}', Mã hóa đơn: '{1}', Ngày xuất HĐ: '{2}', Người mua hàng: '{3}', Tên đơn vị: '{4}', Địa chỉ: '{5}', STK: '{6}', Hình thức thanh toán: '{7}', Ghi chú: '{8}', Đã thu tiền: '{9}'\n",
                                hdt.HoaDonXuatTruocGUID.ToString(), hdt.SoHoaDon, hdt.NgayXuatHoaDon.ToString("dd/MM/yyyy HH:mm:ss"),
                                hdt.TenNguoiMuaHang, hdt.TenDonVi, hdt.DiaChi, hdt.SoTaiKhoan, htttStr, hdt.Notes, !hdt.ChuaThuTien);

                    if (addedDetails != null && addedDetails.Count > 0)
                    {
                        desc += "- Chi tiết hóa đơn:\n";

                        foreach (ChiTietHoaDonXuatTruoc detail in addedDetails)
                        {
                            detail.ChiTietHoaDonXuatTruocGUID = Guid.NewGuid();
                            detail.HoaDonXuatTruocGUID = hdt.HoaDonXuatTruocGUID;
                            db.ChiTietHoaDonXuatTruocs.InsertOnSubmit(detail);

                            desc += string.Format("  + GUID: '{0}', Dịch vụ: '{1}', ĐVT: '{2}', Số lượng: '{3}', Đơn giá: '{4}', Thành tiền: '{5}'\n",
                                detail.ChiTietHoaDonXuatTruocGUID.ToString(), detail.TenMatHang, detail.DonViTinh, detail.SoLuong, detail.DonGia, detail.ThanhTien);
                        }

                        db.SubmitChanges();
                    }

                    int soHoaDon = Convert.ToInt32(hdt.SoHoaDon);
                    QuanLySoHoaDon qlshd = db.QuanLySoHoaDons.SingleOrDefault<QuanLySoHoaDon>(q => q.SoHoaDon == soHoaDon &&
                        q.NgayBatDau >= Global.NgayThayDoiSoHoaDonSauCung);
                    if (qlshd != null) qlshd.DaXuat = true;
                    else
                    {
                        qlshd = new QuanLySoHoaDon();
                        qlshd.QuanLySoHoaDonGUID = Guid.NewGuid();
                        qlshd.SoHoaDon = soHoaDon;
                        qlshd.DaXuat = true;
                        qlshd.XuatTruoc = true;
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
                    tk.Action = "Thêm thông tin hóa đơn xuất trước";
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

        public static Result InsertHoaDonXuatTruoc(HoaDonXuatTruoc hdt, List<ChiTietHoaDonXuatTruoc> addedDetails, DateTime fromDate, DateTime toDate)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    hdt.HoaDonXuatTruocGUID = Guid.NewGuid();
                    db.HoaDonXuatTruocs.InsertOnSubmit(hdt);
                    db.SubmitChanges();

                    string htttStr = Utility.ParseHinhThucThanhToanToStr((PaymentType)hdt.HinhThucThanhToan);

                    desc += string.Format("- Hóa đơn xuất trước: GUID: '{0}', Mã hóa đơn: '{1}', Ngày xuất HĐ: '{2}', Người mua hàng: '{3}', Tên đơn vị: '{4}', Địa chỉ: '{5}', STK: '{6}', Hình thức thanh toán: '{7}', Ghi chú: '{8}', Đã thu tiền: '{9}'\n",
                                hdt.HoaDonXuatTruocGUID.ToString(), hdt.SoHoaDon, hdt.NgayXuatHoaDon.ToString("dd/MM/yyyy HH:mm:ss"),
                                hdt.TenNguoiMuaHang, hdt.TenDonVi, hdt.DiaChi, hdt.SoTaiKhoan, htttStr, hdt.Notes, !hdt.ChuaThuTien);

                    if (addedDetails != null && addedDetails.Count > 0)
                    {
                        desc += "- Chi tiết hóa đơn:\n";

                        foreach (ChiTietHoaDonXuatTruoc detail in addedDetails)
                        {
                            detail.ChiTietHoaDonXuatTruocGUID = Guid.NewGuid();
                            detail.HoaDonXuatTruocGUID = hdt.HoaDonXuatTruocGUID;
                            db.ChiTietHoaDonXuatTruocs.InsertOnSubmit(detail);

                            desc += string.Format("  + GUID: '{0}', Dịch vụ: '{1}', ĐVT: '{2}', Số lượng: '{3}', Đơn giá: '{4}', Thành tiền: '{5}'\n",
                                detail.ChiTietHoaDonXuatTruocGUID.ToString(), detail.TenMatHang, detail.DonViTinh, detail.SoLuong, detail.DonGia, detail.ThanhTien);
                        }

                        db.SubmitChanges();
                    }

                    int soHoaDon = Convert.ToInt32(hdt.SoHoaDon);
                    QuanLySoHoaDon qlshd = db.QuanLySoHoaDons.SingleOrDefault<QuanLySoHoaDon>(q => q.SoHoaDon == soHoaDon &&
                        q.NgayBatDau >= fromDate && q.NgayBatDau < toDate);
                    if (qlshd != null) qlshd.DaXuat = true;
                    else
                    {
                        qlshd = new QuanLySoHoaDon();
                        qlshd.QuanLySoHoaDonGUID = Guid.NewGuid();
                        qlshd.SoHoaDon = soHoaDon;
                        qlshd.DaXuat = true;
                        qlshd.XuatTruoc = true;
                        qlshd.NgayBatDau = fromDate;
                        db.QuanLySoHoaDons.InsertOnSubmit(qlshd);
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Add;
                    tk.Action = "Thêm thông tin hóa đơn xuất trước";
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

        public static Result DeleteQuanLySoHoaDon(List<string> keys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    foreach (string key in keys)
                    {
                        QuanLySoHoaDon s = db.QuanLySoHoaDons.SingleOrDefault<QuanLySoHoaDon>(ss => ss.QuanLySoHoaDonGUID.ToString() == key);
                        if (s != null)
                        {
                            desc += string.Format("- GUID: '{0}', Số hóa dơn: '{1}', Đã xuất: '{2}', Xuất trước: '{3}'\n",
                                s.QuanLySoHoaDonGUID.ToString(), s.SoHoaDon, s.DaXuat, s.XuatTruoc);

                            s.DaXuat = false;
                            s.XuatTruoc = false;
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa số hóa đơn xuất trước";
                    tk.Description = desc;
                    tk.TrackingType = (byte)TrackingType.None;
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

        public static Result InsertQuanLySoHoaDon(QuanLySoHoaDon qlshd)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    //Insert
                    if (qlshd.QuanLySoHoaDonGUID == null || qlshd.QuanLySoHoaDonGUID == Guid.Empty)
                    {
                        qlshd.QuanLySoHoaDonGUID = Guid.NewGuid();
                        qlshd.NgayBatDau = Global.NgayThayDoiSoHoaDonSauCung;
                        db.QuanLySoHoaDons.InsertOnSubmit(qlshd);
                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Số hóa dơn: '{1}', Đã xuất: '{2}', Xuất trước: '{3}'",
                                 qlshd.QuanLySoHoaDonGUID.ToString(), qlshd.SoHoaDon, qlshd.DaXuat, qlshd.XuatTruoc);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm số hóa đơn xuất trước";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        tk.ComputerName = Utility.GetDNSHostName();
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        QuanLySoHoaDon q = db.QuanLySoHoaDons.SingleOrDefault<QuanLySoHoaDon>(s => s.QuanLySoHoaDonGUID.ToString() == qlshd.QuanLySoHoaDonGUID.ToString());
                        if (q != null)
                        {
                            q.SoHoaDon = qlshd.SoHoaDon;
                            q.DaXuat = qlshd.DaXuat;
                            q.XuatTruoc = qlshd.XuatTruoc;

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Số hóa dơn: '{1}', Đã xuất: '{2}', Xuất trước: '{3}'",
                                 q.QuanLySoHoaDonGUID.ToString(), q.SoHoaDon, q.DaXuat, q.XuatTruoc);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa số hóa đơn xuất trước";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);

                            db.SubmitChanges();
                        }
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

        public static Result InsertQuanLySoHoaDon(List<QuanLySoHoaDon> qlshdList)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (QuanLySoHoaDon qlshd in qlshdList)
                    {
                        QuanLySoHoaDon q = db.QuanLySoHoaDons.SingleOrDefault(qq => qq.SoHoaDon == qlshd.SoHoaDon &&
                            qq.NgayBatDau.Value >= Global.NgayThayDoiSoHoaDonSauCung);
                        if (q == null)
                        {
                            qlshd.QuanLySoHoaDonGUID = Guid.NewGuid();
                            qlshd.NgayBatDau = Global.NgayThayDoiSoHoaDonSauCung;
                            db.QuanLySoHoaDons.InsertOnSubmit(qlshd);
                        }
                        else
                        {
                            q.DaXuat = qlshd.DaXuat;
                            q.XuatTruoc = qlshd.XuatTruoc;
                        }
                        
                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Số hóa dơn: '{1}', Đã xuất: '{2}', Xuất trước: '{3}'",
                                 qlshd.QuanLySoHoaDonGUID.ToString(), qlshd.SoHoaDon, qlshd.DaXuat, qlshd.XuatTruoc);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm số hóa đơn xuất trước";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        tk.ComputerName = Utility.GetDNSHostName();
                        db.Trackings.InsertOnSubmit(tk);
                    }

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

        public static Result InsertQuanLySoHoaDon(List<QuanLySoHoaDon> qlshdList, DateTime fromDate, DateTime toDate)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (QuanLySoHoaDon qlshd in qlshdList)
                    {
                        QuanLySoHoaDon q = db.QuanLySoHoaDons.SingleOrDefault(qq => qq.SoHoaDon == qlshd.SoHoaDon &&
                            qq.NgayBatDau.Value >= fromDate && qq.NgayBatDau < toDate);
                        if (q == null)
                        {
                            qlshd.QuanLySoHoaDonGUID = Guid.NewGuid();
                            qlshd.NgayBatDau = fromDate;
                            db.QuanLySoHoaDons.InsertOnSubmit(qlshd);
                        }
                        else
                        {
                            q.DaXuat = qlshd.DaXuat;
                            q.XuatTruoc = qlshd.XuatTruoc;
                        }

                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Số hóa dơn: '{1}', Đã xuất: '{2}', Xuất trước: '{3}'",
                                 qlshd.QuanLySoHoaDonGUID.ToString(), qlshd.SoHoaDon, qlshd.DaXuat, qlshd.XuatTruoc);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm số hóa đơn xuất trước";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        tk.ComputerName = Utility.GetDNSHostName();
                        db.Trackings.InsertOnSubmit(tk);
                    }

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

        public static Result UpdateDaThuTienInvoice(string hoaDonXuatTruocGUID, bool daThuTien)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    HoaDonXuatTruoc invoice = db.HoaDonXuatTruocs.SingleOrDefault<HoaDonXuatTruoc>(i => i.HoaDonXuatTruocGUID.ToString() == hoaDonXuatTruocGUID);
                    if (invoice != null)
                    {
                        invoice.UpdatedDate = DateTime.Now;
                        invoice.UpdatedBy = Guid.Parse(Global.UserGUID);
                        invoice.ChuaThuTien = !daThuTien;

                        string htttStr = Utility.ParseHinhThucThanhToanToStr((PaymentType)invoice.HinhThucThanhToan);

                        desc += string.Format("- Hóa đơn dịch vụ: GUID: '{0}', Mã hóa đơn: '{1}', Ngày xuất HĐ: '{2}', Người mua hàng: '{3}', Tên đơn vị: '{4}', Địa chỉ: '{5}', STK: '{6}', Hình thức thanh toán: '{7}', Ghi chú: '{8}', Đã thu tiền: '{9}'\n",
                                    invoice.HoaDonXuatTruocGUID.ToString(), invoice.SoHoaDon, invoice.NgayXuatHoaDon.ToString("dd/MM/yyyy HH:mm:ss"),
                                    invoice.TenNguoiMuaHang, invoice.TenDonVi, invoice.DiaChi, invoice.SoTaiKhoan, htttStr, invoice.Notes, !invoice.ChuaThuTien);

                        //Tracking
                        desc = desc.Substring(0, desc.Length - 1);
                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Cập nhật thông tin hóa đơn xuất trước";
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
