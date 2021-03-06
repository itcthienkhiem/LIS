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
    public class GiaVonDichVuBus : BusBase
    {
        public static Result GetGiaVonDichVuList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM GiaVonDichVuView WITH(NOLOCK) WHERE GiaVonDichVuStatus={0} AND ServiceStatus={0} ORDER BY Name ASC, NgayApDung DESC",
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

        public static Result GetGiaVonDichVuList(bool isFromDateToDate, DateTime fromDate, DateTime toDate, string tenDichVu)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                if (isFromDateToDate)
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM GiaVonDichVuView WITH(NOLOCK) WHERE GiaVonDichVuStatus={0} AND ServiceStatus={0} AND NgayApDung BETWEEN '{1}' AND '{2}' ORDER BY Name ASC, NgayApDung DESC",
                    (byte)Status.Actived, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM GiaVonDichVuView WITH(NOLOCK) WHERE GiaVonDichVuStatus={0} AND ServiceStatus={0} AND Name LIKE N'%{1}%' ORDER BY Name ASC, NgayApDung DESC",
                   (byte)Status.Actived, tenDichVu);
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

        public static Result GetGiaVonDichVuMoiNhat(string serviceGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT TOP 1 * FROM GiaVonDichVu WITH(NOLOCK) WHERE ServiceGUID = '{0}' AND Status = {1} AND NgayApDung <= '{2}' ORDER BY NgayApDung DESC",
                    serviceGUID, (byte)Status.Actived, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
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

        public static Result GetGiaVonDichVuMoiNhat(string serviceGUID, DateTime ngayThu)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT TOP 1 * FROM GiaVonDichVu WITH(NOLOCK) WHERE ServiceGUID = '{0}' AND Status = {1} AND NgayApDung <= '{2}' ORDER BY NgayApDung DESC",
                    serviceGUID, (byte)Status.Actived, ngayThu.ToString("yyyy-MM-dd 23:59:59"));
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

        public static Result DeleteGiaVonDichVu(List<string> giaVonDichVuKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    foreach (string key in giaVonDichVuKeys)
                    {
                        GiaVonDichVu giaVon = db.GiaVonDichVus.SingleOrDefault<GiaVonDichVu>(g => g.GiaVonDichVuGUID.ToString() == key);
                        if (giaVon != null)
                        {
                            giaVon.DeletedDate = DateTime.Now;
                            giaVon.DeletedBy = Guid.Parse(Global.UserGUID);
                            giaVon.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Dịch vụ: '{1}', Giá vốn: '{2}', Ngày áp dụng: '{3}'\n",
                                giaVon.GiaVonDichVuGUID.ToString(), giaVon.Service.Name, giaVon.GiaVon, giaVon.NgayApDung.ToString("dd/MM/yyyy HH:mm:ss"));
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin giá vốn dịch vụ";
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

        public static Result InsertGiaVonDichVu(GiaVonDichVu giaVonDichVu)
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
                    if (giaVonDichVu.GiaVonDichVuGUID == null || giaVonDichVu.GiaVonDichVuGUID == Guid.Empty)
                    {
                        giaVonDichVu.GiaVonDichVuGUID = Guid.NewGuid();
                        db.GiaVonDichVus.InsertOnSubmit(giaVonDichVu);
                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Dịch vụ: '{1}', Giá vốn: '{2}', Ngày áp dụng: '{3}'",
                                giaVonDichVu.GiaVonDichVuGUID.ToString(), giaVonDichVu.Service.Name, giaVonDichVu.GiaVon, 
                                giaVonDichVu.NgayApDung.ToString("dd/MM/yyyy HH:mm:ss"));

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin giá vốn dịch vụ";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.Price;
                        tk.ComputerName = Utility.GetDNSHostName();
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        GiaVonDichVu gvdv = db.GiaVonDichVus.SingleOrDefault<GiaVonDichVu>(g => g.GiaVonDichVuGUID.ToString() == giaVonDichVu.GiaVonDichVuGUID.ToString());
                        if (gvdv != null)
                        {
                            double giaCu = gvdv.GiaVon;

                            gvdv.ServiceGUID = giaVonDichVu.ServiceGUID;
                            gvdv.GiaVon = giaVonDichVu.GiaVon;
                            gvdv.NgayApDung = giaVonDichVu.NgayApDung;
                            gvdv.Note = giaVonDichVu.Note;
                            gvdv.CreatedDate = giaVonDichVu.CreatedDate;
                            gvdv.CreatedBy = giaVonDichVu.CreatedBy;
                            gvdv.UpdatedDate = giaVonDichVu.UpdatedDate;
                            gvdv.UpdatedBy = giaVonDichVu.UpdatedBy;
                            gvdv.DeletedDate = giaVonDichVu.DeletedDate;
                            gvdv.DeletedBy = giaVonDichVu.DeletedBy;
                            gvdv.Status = giaVonDichVu.Status;
                            db.SubmitChanges();

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Dịch vụ: '{1}', Giá vốn: cũ: '{2}' - mới: '{3}', Ngày áp dụng: '{4}'",
                                    gvdv.GiaVonDichVuGUID.ToString(), gvdv.Service.Name, giaCu, gvdv.GiaVon, gvdv.NgayApDung.ToString("dd/MM/yyyy HH:mm:ss"));

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin giá vốn dịch vụ";
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
    }
}
