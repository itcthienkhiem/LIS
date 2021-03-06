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
using System.Data.Linq;
using System.Transactions;
using MM.Common;
using MM.Databasae;
namespace MM.Bussiness
{
    public class KetLuanBus : BusBase
    {
        public static Result GetKetLuanList(string patientGUID, DateTime fromDate, DateTime toDate)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                //if (Global.StaffType != StaffType.BacSi && Global.StaffType != StaffType.BacSiSieuAm &&
                //    Global.StaffType != StaffType.BacSiNgoaiTongQuat && Global.StaffType != StaffType.BacSiNoiTongQuat && 
                //    Global.StaffType != StaffType.BacSiPhuKhoa)
                //    query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, * FROM KetLuanView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND NgayKetLuan BETWEEN '{1}' AND '{2}' AND Status = {3} AND Archived = 'False' ORDER BY NgayKetLuan DESC",
                //        patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), (byte)Status.Actived);
                //else
                //    query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, * FROM KetLuanView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND NgayKetLuan BETWEEN '{1}' AND '{2}' AND Status = {3} AND Archived = 'False' AND DocStaffGUID = '{4}' ORDER BY NgayKetLuan DESC",
                //        patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), (byte)Status.Actived, Global.UserGUID);

                query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, * FROM KetLuanView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND NgayKetLuan BETWEEN '{1}' AND '{2}' AND Status = {3} ORDER BY NgayKetLuan DESC",
                        patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), (byte)Status.Actived);

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

        public static Result DeleteKetLuan(List<String> ketLuanKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    foreach (string key in ketLuanKeys)
                    {
                        KetLuan ketLuan = db.KetLuans.SingleOrDefault<KetLuan>(k => k.KetLuanGUID.ToString() == key);
                        if (ketLuan != null)
                        {
                            ketLuan.DeletedDate = DateTime.Now;
                            ketLuan.DeletedBy = Guid.Parse(Global.UserGUID);
                            ketLuan.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', '{3}', Các xét nghiệm làm thêm: '{4}', '{5}', Lý do: '{6}', '{7}', Lý do: '{8}'\n",
                                ketLuan.KetLuanGUID.ToString(), ketLuan.Patient.Contact.FullName, ketLuan.DocStaff.Contact.FullName,
                                ketLuan.HasLamThemXetNghiem ? "Có làm thêm xét nghiệm" : "Không làm thêm xét nghiệm", ketLuan.CacXetNghiemLamThem,
                                ketLuan.HasLamDuCanLamSang ? "Đã làm đủ cận lâm sàng trong gói khám" : "Chưa làm đủ cận lâm sàng trong gói khám", ketLuan.LyDo_CanLamSang,
                                ketLuan.HasDuSucKhoe ? "Đủ sức khỏe làm việc" : "Không đủ sức khỏe làm việc", ketLuan.LyDo_SucKhoe);
                        }


                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin kết luận";
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

        public static Result InsertKetLuan(KetLuan ketLuan)
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
                    if (ketLuan.KetLuanGUID == null || ketLuan.KetLuanGUID == Guid.Empty)
                    {
                        ketLuan.KetLuanGUID = Guid.NewGuid();
                        db.KetLuans.InsertOnSubmit(ketLuan);
                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', '{3}', Các xét nghiệm làm thêm: '{4}', '{5}', Lý do: '{6}', '{7}', Lý do: '{8}'",
                                ketLuan.KetLuanGUID.ToString(), ketLuan.Patient.Contact.FullName, ketLuan.DocStaff.Contact.FullName,
                                ketLuan.HasLamThemXetNghiem ? "Có làm thêm xét nghiệm" : "Không làm thêm xét nghiệm", ketLuan.CacXetNghiemLamThem,
                                ketLuan.HasLamDuCanLamSang ? "Đã làm đủ cận lâm sàng trong gói khám" : "Chưa làm đủ cận lâm sàng trong gói khám", ketLuan.LyDo_CanLamSang,
                                ketLuan.HasDuSucKhoe ? "Đủ sức khỏe làm việc" : "Không đủ sức khỏe làm việc", ketLuan.LyDo_SucKhoe);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin kết luận";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        tk.ComputerName = Utility.GetDNSHostName();
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        KetLuan kl = db.KetLuans.SingleOrDefault<KetLuan>(k => k.KetLuanGUID.ToString() == ketLuan.KetLuanGUID.ToString());
                        if (kl != null)
                        {
                            kl.NgayKetLuan = ketLuan.NgayKetLuan;
                            kl.PatientGUID = ketLuan.PatientGUID;
                            kl.DocStaffGUID = ketLuan.DocStaffGUID;
                            kl.HasLamThemXetNghiem = ketLuan.HasLamThemXetNghiem;
                            kl.CacXetNghiemLamThem = ketLuan.CacXetNghiemLamThem;
                            kl.HasLamDuCanLamSang = ketLuan.HasLamDuCanLamSang;
                            kl.LyDo_CanLamSang = ketLuan.LyDo_CanLamSang;
                            kl.HasDuSucKhoe = ketLuan.HasDuSucKhoe;
                            kl.LyDo_SucKhoe = ketLuan.LyDo_SucKhoe;
                            kl.CreatedBy = ketLuan.CreatedBy;
                            kl.CreatedDate = ketLuan.CreatedDate;
                            kl.DeletedBy = ketLuan.DeletedBy;
                            kl.DeletedDate = ketLuan.DeletedDate;
                            kl.UpdatedBy = ketLuan.UpdatedBy;
                            kl.UpdatedDate = ketLuan.UpdatedDate;
                            kl.Status = ketLuan.Status;
                            db.SubmitChanges();

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', '{3}', Các xét nghiệm làm thêm: '{4}', '{5}', Lý do: '{6}', '{7}', Lý do: '{8}'",
                                kl.KetLuanGUID.ToString(), kl.Patient.Contact.FullName, kl.DocStaff.Contact.FullName,
                                kl.HasLamThemXetNghiem ? "Có làm thêm xét nghiệm" : "Không làm thêm xét nghiệm", kl.CacXetNghiemLamThem,
                                kl.HasLamDuCanLamSang ? "Đã làm đủ cận lâm sàng trong gói khám" : "Chưa làm đủ cận lâm sàng trong gói khám", kl.LyDo_CanLamSang,
                                kl.HasDuSucKhoe ? "Đủ sức khỏe làm việc" : "Không đủ sức khỏe làm việc", kl.LyDo_SucKhoe);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin kết luận";
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

        public static Result GetLastKetLuan(string patientGUID, DateTime fromDate, DateTime toDate)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                KetLuan ketLuan = (from kl in db.KetLuans
                                   where kl.PatientGUID.ToString() == patientGUID &&
                                   kl.NgayKetLuan >= fromDate && kl.NgayKetLuan <= toDate &&
                                   kl.Status == (byte)Status.Actived
                                   orderby kl.NgayKetLuan descending
                                   select kl).FirstOrDefault<KetLuan>();

                result.QueryResult = ketLuan;
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

        public static Result GetNgayKetLuanCuoiCung(string patientGUID)
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT Max(NgayKetLuan) FROM KetLuan WITH(NOLOCK) WHERE PatientGUID = '{0}' AND Status = {1}",
                    patientGUID, (byte)Status.Actived);

                result = ExcuteQuery(query);

                if (!result.IsOK) return result;

                DataTable dt = result.QueryResult as DataTable;
                if (dt == null || dt.Rows.Count <= 0)
                    result.QueryResult = Global.MinDateTime;
                else
                {
                    if (dt.Rows[0][0] != null && dt.Rows[0][0] != DBNull.Value)
                        result.QueryResult = Convert.ToDateTime(dt.Rows[0][0]);
                    else result.QueryResult = Global.MinDateTime;
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

        public static Result ChuyenBenhAn(string patientGUID, List<DataRow> rows)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (DataRow row in rows)
                    {
                        string ketLuanGUID = row["KetLuanGUID"].ToString();
                        KetLuan ketLuan = (from s in db.KetLuans
                                             where s.KetLuanGUID.ToString() == ketLuanGUID
                                             select s).FirstOrDefault();

                        if (ketLuan == null) continue;

                        //Tracking
                        string desc = string.Format("- KetLuanGUID: '{0}': PatientGUID: '{1}' ==> '{2}' (KetLuan)",
                            ketLuanGUID, ketLuan.PatientGUID.ToString(), patientGUID);

                        ketLuan.PatientGUID = Guid.Parse(patientGUID);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Edit;
                        tk.Action = "Chuyển bệnh án";
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
    }
}
