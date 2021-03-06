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
    public class LoThuocBus : BusBase
    {
        public static Result GetGiaNhapTrungBinh(string thuocGUID)
        {
            Result result = new Result();

            MMOverride db = null;
            try
            {
                DateTime dt = DateTime.Now;
                db = new MMOverride();
                var loThuocList = from l in db.LoThuocs
                                  where l.Status == (byte)Status.Actived &&
                                  l.ThuocGUID.ToString() == thuocGUID &&
                                  new DateTime(l.NgayHetHan.Year, l.NgayHetHan.Month, l.NgayHetHan.Day) > dt &&
                                  l.SoLuongNhap * l.SoLuongQuiDoi - l.SoLuongXuat > 0
                                  select l;

                double giaNhapTB = 0;
                if (loThuocList != null)
                {
                    double tongGiaNhap = 0;
                    int soLuong = 0;
                    foreach (var lt in loThuocList)
                    {
                        int soLuongTon = lt.SoLuongNhap * lt.SoLuongQuiDoi - lt.SoLuongXuat;
                        soLuong += soLuongTon;
                        tongGiaNhap += (soLuongTon * lt.GiaNhapQuiDoi);
                    }

                    if (soLuong > 0)
                        giaNhapTB = Math.Round(tongGiaNhap / soLuong, 0);
                }

                result.QueryResult = giaNhapTB;
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

        public static Result GetLoThuocByThuoc(string thuocGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT dbo.LoThuoc.MaLoThuoc, dbo.LoThuoc.TenLoThuoc, dbo.LoThuoc.GiaNhap, dbo.LoThuoc.CreatedDate, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS NguoiTao FROM dbo.LoThuoc LEFT OUTER JOIN dbo.DocStaffView ON dbo.LoThuoc.CreatedBy = dbo.DocStaffView.DocStaffGUID WHERE LoThuoc.Status = 0 AND LoThuoc.SoLuongNhap * LoThuoc.SoLuongQuiDoi > LoThuoc.SoLuongXuat AND LoThuoc.ThuocGUID = '{0}' ORDER BY LoThuoc.CreatedDate",
                    thuocGUID);
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

        public static Result GetLoThuocList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CAST(SoLuongNhap * GiaNhap AS float) AS TongTien FROM LoThuocView WITH(NOLOCK) WHERE LoThuocStatus={0} AND ThuocStatus={0} ORDER BY CreatedDate", 
                    (byte)Status.Actived);
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

        public static Result GetLoThuocList(string tenThuoc, DateTime tuNgay, DateTime denNgay, bool isTenThuoc)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                if (isTenThuoc)
                {
                    if (tenThuoc.Trim() == string.Empty)
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CAST(SoLuongNhap * GiaNhap AS float) AS TongTien FROM LoThuocView WITH(NOLOCK) WHERE LoThuocStatus={0} AND ThuocStatus={0} ORDER BY CreatedDate",
                        (byte)Status.Actived);
                    }
                    else
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CAST(SoLuongNhap * GiaNhap AS float) AS TongTien FROM LoThuocView WITH(NOLOCK) WHERE TenThuoc LIKE N'{0}%' AND LoThuocStatus={1} AND ThuocStatus={1} ORDER BY CreatedDate",
                        tenThuoc, (byte)Status.Actived);
                    }
                }
                else
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CAST(SoLuongNhap * GiaNhap AS float) AS TongTien FROM LoThuocView WITH(NOLOCK) WHERE CreatedDate BETWEEN '{0}' AND '{1}' AND LoThuocStatus={2} AND ThuocStatus={2} ORDER BY CreatedDate",
                        tuNgay.ToString("yyyy-MM-dd 00:00:00"), denNgay.ToString("yyyy-MM-dd 23:59:59"), (byte)Status.Actived);
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

        public static Result CheckThuocHetHan(string thuocGUID)
        {
            Result result = new Result();
            MMOverride db = null;
            try
            {
                DateTime dt = DateTime.Now;
                db = new MMOverride();
                List<Thuoc> thuocResults = (from t in db.Thuocs
                                            join l in db.LoThuocs on t.ThuocGUID equals l.ThuocGUID
                                            where t.Status == (byte)Status.Actived && l.Status == (byte)Status.Actived &&
                                            l.SoLuongNhap * l.SoLuongQuiDoi - l.SoLuongXuat > 0 &&
                                            new DateTime(l.NgayHetHan.Year, l.NgayHetHan.Month, l.NgayHetHan.Day) > dt && t.ThuocGUID.ToString() == thuocGUID
                                            select t).ToList<Thuoc>();

                if (thuocResults != null && thuocResults.Count > 0)
                    result.QueryResult = false;
                else
                    result.QueryResult = true;
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

        public static Result GetNgayHetHanCuaThuoc(string thuocGUID)
        {
            Result result = new Result();
            MMOverride db = null;
            try
            {
                DateTime dt = DateTime.Now;
                db = new MMOverride();
                LoThuoc loThuoc = (from t in db.Thuocs
                                        join l in db.LoThuocs on t.ThuocGUID equals l.ThuocGUID
                                        where t.Status == (byte)Status.Actived && l.Status == (byte)Status.Actived &&
                                        t.ThuocGUID.ToString() == thuocGUID
                                        orderby new DateTime(l.NgayHetHan.Year, l.NgayHetHan.Month, l.NgayHetHan.Day) descending, l.CreatedDate descending
                                        select l).FirstOrDefault<LoThuoc>();

                if (loThuoc != null)
                    result.QueryResult = loThuoc.NgayHetHan;
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

        public static Result GetThuocTonKho(string thuocGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                DateTime dt = DateTime.Now;
                List<LoThuoc> loThuocList = (from l in db.LoThuocs
                                             where l.Status == (byte)Status.Actived &&
                                             l.ThuocGUID.ToString() == thuocGUID &&
                                             new DateTime(l.NgayHetHan.Year, l.NgayHetHan.Month, l.NgayHetHan.Day) > dt &&
                                             l.SoLuongNhap * l.SoLuongQuiDoi - l.SoLuongXuat > 0
                                             select l).ToList<LoThuoc>();

                if (loThuocList != null && loThuocList.Count > 0)
                {
                    int tongSLNhap = 0;
                    int tongSLXuat = 0;

                    foreach (LoThuoc lt in loThuocList)
                    {
                        tongSLNhap += lt.SoLuongNhap * lt.SoLuongQuiDoi;
                        tongSLXuat += lt.SoLuongXuat;
                    }

                    result.QueryResult = tongSLNhap - tongSLXuat;
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

        public static Result CheckThuocTonKho(string thuocGUID, int soLuong)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                DateTime dt = DateTime.Now;

                List<LoThuoc> loThuocList = (from l in db.LoThuocs
                                            where l.Status == (byte)Status.Actived && 
                                            l.ThuocGUID.ToString() == thuocGUID &&
                                            new DateTime(l.NgayHetHan.Year, l.NgayHetHan.Month, l.NgayHetHan.Day) > dt &&
                                            l.SoLuongNhap * l.SoLuongQuiDoi - l.SoLuongXuat > 0
                                            select l).ToList<LoThuoc>();

                if (loThuocList == null || loThuocList.Count <= 0)
                    result.QueryResult = false;
                else
                {
                    int tongSLNhap = 0;
                    int tongSLXuat = 0;

                    foreach (LoThuoc lt in loThuocList)
                    {
                        tongSLNhap += lt.SoLuongNhap * lt.SoLuongQuiDoi;
                        tongSLXuat += lt.SoLuongXuat;
                    }

                    if (tongSLNhap >= tongSLXuat + soLuong)
                        result.QueryResult = true;
                    else
                        result.QueryResult = false;
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

        public static Result GetNhaPhanPhoiList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT DISTINCT NhaPhanPhoi FROM LoThuoc WITH(NOLOCK) ORDER BY NhaPhanPhoi",
                    (byte)Status.Actived);
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

        public static Result GetLoThuocCount()
        {
            Result result = null;
            try
            {
                string query = "SELECT Count(*) FROM LoThuoc";
                result = ExcuteQuery(query);
                if (result.IsOK)
                {
                    DataTable dt = result.QueryResult as DataTable;
                    if (dt != null && dt.Rows.Count > 0)
                        result.QueryResult = Convert.ToInt32(dt.Rows[0][0]);
                    else result.QueryResult = 0;
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

            return result;
        }

        public static Result GetLoThuoc(string loThuocGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                LoThuoc loThuoc = db.LoThuocs.SingleOrDefault(l => l.LoThuocGUID.ToString() == loThuocGUID);
                result.QueryResult = loThuoc;
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

        public static Result DeleteLoThuoc(List<string> loThuocKeys, List<string> noteList)
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
                    foreach (string key in loThuocKeys)
                    {
                        LoThuoc loThuoc = db.LoThuocs.SingleOrDefault<LoThuoc>(l => l.LoThuocGUID.ToString() == key);
                        if (loThuoc != null)
                        {
                            if (loThuoc.Note != null && loThuoc.Note.Trim() != string.Empty)
                                loThuoc.Note += string.Format("\r\nXóa {0}: {1}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), noteList[index]);
                            else
                                loThuoc.Note = string.Format("Xóa {0}: {1}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), noteList[index]);

                            loThuoc.DeletedDate = DateTime.Now;
                            loThuoc.DeletedBy = Guid.Parse(Global.UserGUID);
                            loThuoc.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Mã lô: '{1}', Tên lô: '{2}', Thuốc: '{3}', Số đăng ký: '{4}', Hãng SX: '{5}', Ngày SX: '{6}', Ngày hết hạn: '{7}', Nhà phân phối: '{8}', SL nhập: '{9}', ĐVT nhập: '{10}', Giá nhập: '{11}', SL qui đổi: '{12}', ĐVT qui đổi: '{13}', Giá nhập qui đổi: '{14}', SL xuất: '{15}', Ghi chú: '{16}'\n",
                                loThuoc.LoThuocGUID.ToString(), loThuoc.MaLoThuoc, loThuoc.TenLoThuoc, loThuoc.Thuoc.TenThuoc, loThuoc.SoDangKy, loThuoc.HangSanXuat,
                                loThuoc.NgaySanXuat.ToString("dd/MM/yyyy"), loThuoc.NgayHetHan.ToString("dd/MM/yyyy"), loThuoc.NhaPhanPhoi, loThuoc.SoLuongNhap,
                                loThuoc.DonViTinhNhap, loThuoc.GiaNhap, loThuoc.SoLuongQuiDoi, loThuoc.DonViTinhQuiDoi, loThuoc.GiaNhapQuiDoi, loThuoc.SoLuongXuat, loThuoc.Note);
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
                    tk.Action = "Xóa thông tin lô thuốc";
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

        public static Result CheckLoThuocExistCode(string loThuocGUID, string code)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                LoThuoc lt = null;
                if (loThuocGUID == null || loThuocGUID == string.Empty)
                    lt = db.LoThuocs.SingleOrDefault<LoThuoc>(l => l.MaLoThuoc.ToLower() == code.ToLower());
                else
                    lt = db.LoThuocs.SingleOrDefault<LoThuoc>(l => l.MaLoThuoc.ToLower() == code.ToLower() &&
                                                                l.LoThuocGUID.ToString() != loThuocGUID);

                if (lt == null)
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

        public static Result InsertLoThuoc(LoThuoc loThuoc)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;

                using (TransactionScope tnx = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    //Insert
                    if (loThuoc.LoThuocGUID == null || loThuoc.LoThuocGUID == Guid.Empty)
                    {
                        loThuoc.LoThuocGUID = Guid.NewGuid();
                        db.LoThuocs.InsertOnSubmit(loThuoc);
                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Mã lô: '{1}', Tên lô: '{2}', Thuốc: '{3}', Số đăng ký: '{4}', Hãng SX: '{5}', Ngày SX: '{6}', Ngày hết hạn: '{7}', Nhà phân phối: '{8}', SL nhập: '{9}', ĐVT nhập: '{10}', Giá nhập: '{11}', SL qui đổi: '{12}', ĐVT qui đổi: '{13}', Giá nhập qui đổi: '{14}', SL xuất: '{15}', Ghi chú: '{16}'",
                                loThuoc.LoThuocGUID.ToString(), loThuoc.MaLoThuoc, loThuoc.TenLoThuoc, loThuoc.Thuoc.TenThuoc, loThuoc.SoDangKy, loThuoc.HangSanXuat,
                                loThuoc.NgaySanXuat.ToString("dd/MM/yyyy"), loThuoc.NgayHetHan.ToString("dd/MM/yyyy"), loThuoc.NhaPhanPhoi, loThuoc.SoLuongNhap,
                                loThuoc.DonViTinhNhap, loThuoc.GiaNhap, loThuoc.SoLuongQuiDoi, loThuoc.DonViTinhQuiDoi, loThuoc.GiaNhapQuiDoi, loThuoc.SoLuongXuat, loThuoc.Note);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin lô thuốc";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.Price;
                        tk.ComputerName = Utility.GetDNSHostName();
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        LoThuoc lt = db.LoThuocs.SingleOrDefault<LoThuoc>(l => l.LoThuocGUID.ToString() == loThuoc.LoThuocGUID.ToString());
                        if (lt != null)
                        {
                            double giaNhapCu = lt.GiaNhap;
                            int soLuongNhapCu = lt.SoLuongNhap;
                            double giaNhapQuiDoiCu = lt.GiaNhapQuiDoi;
                            int soLuongQuiDoiCu = lt.SoLuongQuiDoi;

                            lt.ThuocGUID = loThuoc.ThuocGUID;
                            lt.MaLoThuoc = loThuoc.MaLoThuoc;
                            lt.TenLoThuoc = loThuoc.TenLoThuoc;
                            lt.SoDangKy = loThuoc.SoDangKy;
                            lt.HangSanXuat = loThuoc.HangSanXuat;
                            lt.NgaySanXuat = loThuoc.NgaySanXuat;
                            lt.NgayHetHan = loThuoc.NgayHetHan;
                            lt.NhaPhanPhoi = loThuoc.NhaPhanPhoi;
                            lt.SoLuongNhap = loThuoc.SoLuongNhap;
                            lt.DonViTinhNhap = loThuoc.DonViTinhNhap;
                            lt.GiaNhap = loThuoc.GiaNhap;
                            lt.SoLuongQuiDoi = loThuoc.SoLuongQuiDoi;
                            lt.DonViTinhQuiDoi = loThuoc.DonViTinhQuiDoi;
                            lt.GiaNhapQuiDoi = loThuoc.GiaNhapQuiDoi;
                            lt.Note = loThuoc.Note;
                            lt.CreatedDate = loThuoc.CreatedDate;
                            lt.CreatedBy = loThuoc.CreatedBy;
                            lt.UpdatedDate = loThuoc.UpdatedDate;
                            lt.UpdatedBy = loThuoc.UpdatedBy;
                            lt.DeletedDate = loThuoc.DeletedDate;
                            lt.DeletedBy = loThuoc.DeletedBy;
                            lt.Status = loThuoc.Status;
                            lt.SystemDate = loThuoc.SystemDate;
                            db.SubmitChanges();

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Mã lô: '{1}', Tên lô: '{2}', Thuốc: '{3}', Số đăng ký: '{4}', Hãng SX: '{5}', Ngày SX: '{6}', Ngày hết hạn: '{7}', Nhà phân phối: '{8}', SL nhập: cũ: '{9}' - mới: '{10}', ĐVT nhập: '{11}', Giá nhập: cũ: '{12}' - mới: '{13}', SL qui đổi: cũ: '{14}' - mới: '{15}', ĐVT qui đổi: '{16}', Giá nhập qui đổi: cũ: '{17}' - mới: '{18}', SL xuất: '{19}', Ghi chú: '{20}'",
                                    lt.LoThuocGUID.ToString(), lt.MaLoThuoc, lt.TenLoThuoc, lt.Thuoc.TenThuoc, lt.SoDangKy, lt.HangSanXuat,
                                    lt.NgaySanXuat.ToString("dd/MM/yyyy"), lt.NgayHetHan.ToString("dd/MM/yyyy"), lt.NhaPhanPhoi, soLuongNhapCu,
                                    lt.SoLuongNhap, lt.DonViTinhNhap, giaNhapCu, lt.GiaNhap, soLuongQuiDoiCu, lt.SoLuongQuiDoi,
                                    lt.DonViTinhQuiDoi, giaNhapQuiDoiCu, lt.GiaNhapQuiDoi, lt.SoLuongXuat, lt.Note);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin lô thuốc";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.Price;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);

                            db.SubmitChanges();
                        }
                    }

                    tnx.Complete();
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

        public static Result GetGiaThuocNhap(string maThuocGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                LoThuoc loThuoc = (from l in db.LoThuocs
                                   where l.ThuocGUID.ToString() == maThuocGUID &&
                                   l.Status == (byte)Status.Actived
                                   orderby l.CreatedDate descending
                                   select l).FirstOrDefault();

                if (loThuoc != null)
                    result.QueryResult = loThuoc.GiaNhapQuiDoi;
                else
                    result.QueryResult = 0;
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
