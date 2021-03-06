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
    public class ServiceHistoryBus : BusBase
    {
        public static Result GetDichVuLamThemList(string patientGUID, string hopDongGUID)
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, S.*, CAST(((S.FixedPrice - (S.FixedPrice * S.Discount)/100) * S.SoLuong) AS float) AS Amount, CASE ISNULL(R.ChuaThuTien, CAST(1 AS bit)) WHEN 1 THEN 0 ELSE 1 END AS DaThuTien FROM ReceiptDetail D WITH(NOLOCK) INNER JOIN Receipt R WITH(NOLOCK) ON D.ReceiptGUID = R.ReceiptGUID RIGHT OUTER JOIN ServiceHistoryView S WITH(NOLOCK) ON D.ServiceHistoryGUID = S.ServiceHistoryGUID WHERE S.PatientGUID = '{0}' AND S.Status = {1} AND S.ServiceStatus = {1} AND S.HopDongGUID='{2}' AND (R.Status IS NULL OR R.Status={1}) AND (D.Status IS NULL OR D.Status={1}) ORDER BY S.[Name]",
                            patientGUID, (byte)Status.Actived, hopDongGUID);

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

        public static Result GetServiceHistory(string patientGUID, bool isAll, DateTime fromDate, DateTime toDate)
        {
            Result result = new Result();

            try
            {
                string query = string.Empty;
                if (isAll)
                {
                    if (Global.StaffType != StaffType.BacSi && Global.StaffType != StaffType.BacSiSieuAm &&
                        Global.StaffType != StaffType.BacSiNgoaiTongQuat && Global.StaffType != StaffType.BacSiNoiTongQuat &&
                        Global.StaffType != StaffType.BacSiPhuKhoa && Global.StaffType != StaffType.XetNghiem)
                        query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, *, CAST(((FixedPrice - (FixedPrice * Discount)/100) * SoLuong) AS float) AS Amount FROM ServiceHistoryView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND Status = {1} AND ServiceStatus = {1} ORDER BY Name", 
                            patientGUID, (byte)Status.Actived);
                    else
                        query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, *, CAST(((FixedPrice - (FixedPrice * Discount)/100) * SoLuong) AS float) AS Amount FROM ServiceHistoryView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND Status = {1} AND ServiceStatus = {1} AND (DocStaffGUID = '{2}' OR DocStaffGUID IS NULL) ORDER BY Name", 
                            patientGUID, (byte)Status.Actived, Global.UserGUID);
                }
                else
                {
                    if (Global.StaffType != StaffType.BacSi && Global.StaffType != StaffType.BacSiSieuAm &&
                        Global.StaffType != StaffType.BacSiNgoaiTongQuat && Global.StaffType != StaffType.BacSiNoiTongQuat &&
                        Global.StaffType != StaffType.BacSiPhuKhoa && Global.StaffType != StaffType.XetNghiem)
                        query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, *, CAST(((FixedPrice - (FixedPrice * Discount)/100) * SoLuong) AS float) AS Amount FROM ServiceHistoryView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND ActivedDate BETWEEN '{1}' AND '{2}' AND Status = {3} AND ServiceStatus = {3} ORDER BY Name",
                            patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), (byte)Status.Actived);
                    else
                        query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, *, CAST(((FixedPrice - (FixedPrice * Discount)/100) * SoLuong) AS float) AS Amount FROM ServiceHistoryView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND ActivedDate BETWEEN '{1}' AND '{2}' AND Status = {3} AND ServiceStatus = {3} AND (DocStaffGUID = '{4}' OR DocStaffGUID IS NULL) ORDER BY Name",
                            patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), (byte)Status.Actived, Global.UserGUID);
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

        public static Result GetServiceHistory(string patientGUID, DateTime fromDate, DateTime toDate, ServiceType type)
        {
            Result result = new Result();
            MMOverride db = null;
            try
            {
                db = new MMOverride();
                List<ServiceHistoryView> serviceHistoryList = (from s in db.ServiceHistoryViews
                                                               where s.PatientGUID.ToString() == patientGUID &&
                                                               s.ActivedDate.Value >= fromDate && s.ActivedDate.Value <= toDate &&
                                                               s.Status == (byte)Status.Actived && s.Type == (byte)type &&
                                                               s.ServiceStatus == (byte)Status.Actived
                                                               orderby s.Name ascending
                                                               select s).ToList<ServiceHistoryView>();
                result.QueryResult = serviceHistoryList;
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

        public static Result GetDSDichVuChuaXuatPhieu(DateTime fromDate, DateTime toDate)
        {
            Result result = new Result();
            MMOverride db = null;
            try
            {
                string query = string.Format("SELECT S.*, P.FullName AS TenBenhNhan, CAST(((S.FixedPrice - (S.FixedPrice * S.Discount)/100) * SoLuong) AS float) AS Amount FROM ServiceHistoryView S WITH(NOLOCK), PatientView P WITH(NOLOCK) WHERE S.PatientGUID = P.PatientGUID AND S.ActivedDate BETWEEN '{0}' AND '{1}' AND S.IsExported = 'False' AND S.Status = {2} AND ServiceStatus = {2} AND KhamTuTuc = 'True' ORDER BY S.ActivedDate",
                    fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), (byte)Status.Actived);

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

        public static Result CheckDichVuExist(string serviceHistoryGUID, string patientGUID, string serviceGUID, DateTime ngayKham)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                DateTime tuNgay = new DateTime(ngayKham.Year, ngayKham.Month, ngayKham.Day, 0, 0, 0);
                DateTime denNgay = new DateTime(ngayKham.Year, ngayKham.Month, ngayKham.Day, 23, 59, 59);

                ServiceHistory srvHistory = null;
                if (serviceHistoryGUID == string.Empty)
                {
                    srvHistory = (from s in db.ServiceHistories
                                  where s.Status == (byte)Status.Actived &&
                                  s.ActivedDate.Value >= tuNgay &&
                                  s.ActivedDate.Value <= denNgay &&
                                  s.ServiceGUID.Value.ToString() == serviceGUID &&
                                  s.PatientGUID.Value.ToString() == patientGUID &&
                                  !s.IsExported
                                  select s).FirstOrDefault();
                }
                else
                {
                    srvHistory = (from s in db.ServiceHistories
                                  where s.Status == (byte)Status.Actived &&
                                  s.ActivedDate.Value >= tuNgay &&
                                  s.ActivedDate.Value <= denNgay &&
                                  s.ServiceGUID.Value.ToString() == serviceGUID &&
                                  s.PatientGUID.Value.ToString() == patientGUID &&
                                  !s.IsExported &&
                                  s.ServiceHistoryGUID.ToString() != serviceHistoryGUID
                                  select s).FirstOrDefault();
                }

                if (srvHistory == null) result.Error.Code = ErrorCode.NOT_EXIST;
                else result.Error.Code = ErrorCode.EXIST;
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

        public static Result DeleteServiceHistory(List<String> serviceHistoryKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    foreach (string key in serviceHistoryKeys)
                    {
                        ServiceHistory srvHistory = db.ServiceHistories.SingleOrDefault<ServiceHistory>(s => s.ServiceHistoryGUID.ToString() == key);
                        if (srvHistory != null)
                        {
                            srvHistory.DeletedDate = DateTime.Now;
                            srvHistory.DeletedBy = Guid.Parse(Global.UserGUID);
                            srvHistory.Status = (byte)Status.Deactived;

                            string bacSiThucHien = string.Empty;
                            if (srvHistory.DocStaff != null)
                                bacSiThucHien = srvHistory.DocStaff.Contact.FullName;

                            string nguoiChuyenNhuong = string.Empty;
                            if (srvHistory.RootPatientGUID != null && srvHistory.RootPatientGUID.HasValue)
                            {
                                PatientView patient = db.PatientViews.FirstOrDefault(p => p.PatientGUID == srvHistory.RootPatientGUID.Value);
                                if (patient != null) nguoiChuyenNhuong = patient.FullName;
                            }

                            string hopDongGUID = string.Empty;
                            if (srvHistory.HopDongGUID != null && srvHistory.HopDongGUID.HasValue)
                                hopDongGUID = srvHistory.HopDongGUID.Value.ToString();

                            desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Dịch vụ: '{3}', Giá: '{4}', Giảm: '{5}', Giá vốn: '{6}', Nguuời chuyển nhượng: '{7}', Khám tự túc: '{8}', Hợp đồng GUID: '{9}', Số lượng: '{10}'\n",
                                srvHistory.ServiceHistoryGUID.ToString(), srvHistory.Patient.Contact.FullName, bacSiThucHien,
                                srvHistory.Service.Name, srvHistory.Price.Value, srvHistory.Discount, srvHistory.GiaVon, nguoiChuyenNhuong, srvHistory.KhamTuTuc, hopDongGUID, srvHistory.SoLuong);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin dịch vụ sử dụng";
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

        public static Result InsertServiceHistory(ServiceHistory serviceHistory)
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
                    if (serviceHistory.ServiceHistoryGUID == null || serviceHistory.ServiceHistoryGUID == Guid.Empty)
                    {
                        if (serviceHistory.HopDongGUID == null || !serviceHistory.HopDongGUID.HasValue)
                        {
                            serviceHistory.ServiceHistoryGUID = Guid.NewGuid();
                            db.ServiceHistories.InsertOnSubmit(serviceHistory);
                            db.SubmitChanges();

                            string bacSiThucHien = string.Empty;
                            if (serviceHistory.DocStaff != null)
                                bacSiThucHien = serviceHistory.DocStaff.Contact.FullName;

                            string nguoiChuyenNhuong = string.Empty;
                            if (serviceHistory.RootPatientGUID != null && serviceHistory.RootPatientGUID.HasValue)
                            {
                                PatientView patient = db.PatientViews.FirstOrDefault(p => p.PatientGUID == serviceHistory.RootPatientGUID.Value);
                                if (patient != null) nguoiChuyenNhuong = patient.FullName;
                            }

                            //Tracking
                            string hopDongGUID = string.Empty;
                            if (serviceHistory.HopDongGUID != null && serviceHistory.HopDongGUID.HasValue)
                                hopDongGUID = serviceHistory.HopDongGUID.Value.ToString();

                            desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Dịch vụ: '{3}', Giá: '{4}', Giảm: '{5}', Giá vốn: '{6}', Người chuyển nhượng: '{7}', Khám tự túc: '{8}', Hợp đồng GUID: '{9}', Số lượng: '{10}'",
                                    serviceHistory.ServiceHistoryGUID.ToString(), serviceHistory.Patient.Contact.FullName, bacSiThucHien,
                                    serviceHistory.Service.Name, serviceHistory.Price.Value, serviceHistory.Discount, serviceHistory.GiaVon,
                                    nguoiChuyenNhuong, serviceHistory.KhamTuTuc, hopDongGUID, serviceHistory.SoLuong);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Add;
                            tk.Action = "Thêm thông tin dịch vụ sử dụng";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.Price;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);
                        }
                        else
                        {
                            DateTime tuNgay = new DateTime(serviceHistory.ActivedDate.Value.Year, serviceHistory.ActivedDate.Value.Month, serviceHistory.ActivedDate.Value.Day, 0, 0, 0);
                            DateTime denNgay = new DateTime(serviceHistory.ActivedDate.Value.Year, serviceHistory.ActivedDate.Value.Month, serviceHistory.ActivedDate.Value.Day, 23, 59, 59);
                            ServiceHistory srvHistory = (from s in db.ServiceHistories
                                                        where s.Status == (byte)Status.Actived &&
                                                        s.ActivedDate.Value >= tuNgay &&
                                                        s.ActivedDate.Value <= denNgay &&
                                                        s.ServiceGUID.Value.ToString() == serviceHistory.ServiceGUID.Value.ToString() &&
                                                        s.PatientGUID.Value.ToString() == serviceHistory.PatientGUID.Value.ToString() &&
                                                        !s.IsExported
                                                        select s).FirstOrDefault();

                            if (srvHistory == null)
                            {
                                serviceHistory.ServiceHistoryGUID = Guid.NewGuid();
                                db.ServiceHistories.InsertOnSubmit(serviceHistory);
                                db.SubmitChanges();

                                string bacSiThucHien = string.Empty;
                                if (serviceHistory.DocStaff != null)
                                    bacSiThucHien = serviceHistory.DocStaff.Contact.FullName;

                                string nguoiChuyenNhuong = string.Empty;
                                if (serviceHistory.RootPatientGUID != null && serviceHistory.RootPatientGUID.HasValue)
                                {
                                    PatientView patient = db.PatientViews.FirstOrDefault(p => p.PatientGUID == serviceHistory.RootPatientGUID.Value);
                                    if (patient != null) nguoiChuyenNhuong = patient.FullName;
                                }

                                //Tracking
                                string hopDongGUID = string.Empty;
                                if (serviceHistory.HopDongGUID != null && serviceHistory.HopDongGUID.HasValue)
                                    hopDongGUID = serviceHistory.HopDongGUID.Value.ToString();

                                desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Dịch vụ: '{3}', Giá: '{4}', Giảm: '{5}', Giá vốn: '{6}', Người chuyển nhượng: '{7}', Khám tự túc: '{8}', Hợp đồng GUID: '{9}', Số lượng: '{10}'",
                                        serviceHistory.ServiceHistoryGUID.ToString(), serviceHistory.Patient.Contact.FullName, bacSiThucHien,
                                        serviceHistory.Service.Name, serviceHistory.Price.Value, serviceHistory.Discount, serviceHistory.GiaVon,
                                        nguoiChuyenNhuong, serviceHistory.KhamTuTuc, hopDongGUID, serviceHistory.SoLuong);

                                Tracking tk = new Tracking();
                                tk.TrackingGUID = Guid.NewGuid();
                                tk.TrackingDate = DateTime.Now;
                                tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                                tk.ActionType = (byte)ActionType.Add;
                                tk.Action = "Thêm thông tin dịch vụ sử dụng";
                                tk.Description = desc;
                                tk.TrackingType = (byte)TrackingType.Price;
                                tk.ComputerName = Utility.GetDNSHostName();
                                db.Trackings.InsertOnSubmit(tk);
                            }
                            else
                            {
                                double giaCu = srvHistory.Price.Value;
                                double giamCu = srvHistory.Discount;
                                double giaVonCu = srvHistory.GiaVon;

                                srvHistory.ActivedDate = serviceHistory.ActivedDate;
                                srvHistory.CreatedBy = serviceHistory.CreatedBy;
                                srvHistory.CreatedDate = serviceHistory.CreatedDate;
                                srvHistory.DeletedBy = serviceHistory.DeletedBy;
                                srvHistory.DeletedDate = serviceHistory.DeletedDate;
                                srvHistory.DocStaffGUID = serviceHistory.DocStaffGUID;
                                srvHistory.Note = serviceHistory.Note;
                                srvHistory.Price = serviceHistory.Price;
                                srvHistory.Discount = serviceHistory.Discount;
                                srvHistory.GiaVon = serviceHistory.GiaVon;
                                srvHistory.ServiceGUID = serviceHistory.ServiceGUID;
                                srvHistory.UpdatedBy = serviceHistory.UpdatedBy;
                                srvHistory.UpdatedDate = serviceHistory.UpdatedDate;
                                srvHistory.Status = serviceHistory.Status;
                                srvHistory.IsNormalOrNegative = serviceHistory.IsNormalOrNegative;
                                srvHistory.Normal = serviceHistory.Normal;
                                srvHistory.Abnormal = serviceHistory.Abnormal;
                                srvHistory.Negative = serviceHistory.Negative;
                                srvHistory.Positive = serviceHistory.Positive;
                                srvHistory.RootPatientGUID = serviceHistory.RootPatientGUID;
                                srvHistory.KhamTuTuc = serviceHistory.KhamTuTuc;
                                srvHistory.HopDongGUID = serviceHistory.HopDongGUID;
                                srvHistory.SoLuong = serviceHistory.SoLuong;

                                string bacSiThucHien = string.Empty;
                                if (srvHistory.DocStaff != null)
                                    bacSiThucHien = srvHistory.DocStaff.Contact.FullName;

                                string nguoiChuyenNhuong = string.Empty;
                                if (srvHistory.RootPatientGUID != null && srvHistory.RootPatientGUID.HasValue)
                                {
                                    PatientView patient = db.PatientViews.FirstOrDefault(p => p.PatientGUID == srvHistory.RootPatientGUID.Value);
                                    if (patient != null) nguoiChuyenNhuong = patient.FullName;
                                }

                                //Tracking
                                string hopDongGUID = string.Empty;
                                if (serviceHistory.HopDongGUID != null && serviceHistory.HopDongGUID.HasValue)
                                    hopDongGUID = serviceHistory.HopDongGUID.Value.ToString();

                                desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Dịch vụ: '{3}', Giá: cũ: '{4}' - mới: '{5}', Giảm: cũ: '{6}' - mới: '{7}', Giá vốn cũ: '{8}' - mới: '{9}', Người chuyển nhượng: '{10}', Khám tự túc: '{11}', Hợp đồng GUID: '{12}', Số lượng: '{13}'",
                                        srvHistory.ServiceHistoryGUID.ToString(), srvHistory.Patient.Contact.FullName, bacSiThucHien,
                                        srvHistory.Service.Name, giaCu, srvHistory.Price.Value, giamCu, srvHistory.Discount, giaVonCu, srvHistory.GiaVon, nguoiChuyenNhuong, srvHistory.KhamTuTuc, hopDongGUID, srvHistory.SoLuong);

                                Tracking tk = new Tracking();
                                tk.TrackingGUID = Guid.NewGuid();
                                tk.TrackingDate = DateTime.Now;
                                tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                                tk.ActionType = (byte)ActionType.Edit;
                                tk.Action = "Sửa thông tin dịch vụ sử dụng";
                                tk.Description = desc;
                                tk.TrackingType = (byte)TrackingType.Price;
                                tk.ComputerName = Utility.GetDNSHostName();
                                db.Trackings.InsertOnSubmit(tk);

                                serviceHistory.ServiceHistoryGUID = srvHistory.ServiceHistoryGUID;
                            }
                        }

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        ServiceHistory srvHistory = db.ServiceHistories.SingleOrDefault<ServiceHistory>(s => s.ServiceHistoryGUID.ToString() == serviceHistory.ServiceHistoryGUID.ToString());
                        if (srvHistory != null)
                        {
                            double giaCu = srvHistory.Price.Value;
                            double giamCu = srvHistory.Discount;
                            double giaVonCu = srvHistory.GiaVon;

                            srvHistory.ActivedDate = serviceHistory.ActivedDate;
                            srvHistory.CreatedBy = serviceHistory.CreatedBy;
                            srvHistory.CreatedDate = serviceHistory.CreatedDate;
                            srvHistory.DeletedBy = serviceHistory.DeletedBy;
                            srvHistory.DeletedDate = serviceHistory.DeletedDate;
                            srvHistory.DocStaffGUID = serviceHistory.DocStaffGUID;
                            srvHistory.Note = serviceHistory.Note;
                            srvHistory.Price = serviceHistory.Price;
                            srvHistory.Discount = serviceHistory.Discount;
                            srvHistory.GiaVon = serviceHistory.GiaVon;
                            srvHistory.ServiceGUID = serviceHistory.ServiceGUID;
                            srvHistory.UpdatedBy = serviceHistory.UpdatedBy;
                            srvHistory.UpdatedDate = serviceHistory.UpdatedDate;
                            srvHistory.Status = serviceHistory.Status;
                            srvHistory.IsNormalOrNegative = serviceHistory.IsNormalOrNegative;
                            srvHistory.Normal = serviceHistory.Normal;
                            srvHistory.Abnormal = serviceHistory.Abnormal;
                            srvHistory.Negative = serviceHistory.Negative;
                            srvHistory.Positive = serviceHistory.Positive;
                            srvHistory.RootPatientGUID = serviceHistory.RootPatientGUID;
                            srvHistory.KhamTuTuc = serviceHistory.KhamTuTuc;
                            srvHistory.HopDongGUID = serviceHistory.HopDongGUID;
                            srvHistory.SoLuong = serviceHistory.SoLuong;

                            string bacSiThucHien = string.Empty;
                            if (srvHistory.DocStaff != null)
                                bacSiThucHien = srvHistory.DocStaff.Contact.FullName;

                            string nguoiChuyenNhuong = string.Empty;
                            if (srvHistory.RootPatientGUID != null && srvHistory.RootPatientGUID.HasValue)
                            {
                                PatientView patient = db.PatientViews.FirstOrDefault(p => p.PatientGUID == srvHistory.RootPatientGUID.Value);
                                if (patient != null) nguoiChuyenNhuong = patient.FullName;
                            }

                            //Tracking
                            string hopDongGUID = string.Empty;
                            if (serviceHistory.HopDongGUID != null && serviceHistory.HopDongGUID.HasValue)
                                hopDongGUID = serviceHistory.HopDongGUID.Value.ToString();

                            desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Dịch vụ: '{3}', Giá: cũ: '{4}' - mới: '{5}', Giảm: cũ: '{6}' - mới: '{7}', Giá vốn cũ: '{8}' - mới: '{9}', Người chuyển nhượng: '{10}', Khám tự túc: '{11}', Hợp đồng GUID: '{12}', Số lượng: '{13}'",
                                    srvHistory.ServiceHistoryGUID.ToString(), srvHistory.Patient.Contact.FullName, bacSiThucHien,
                                    srvHistory.Service.Name, giaCu, srvHistory.Price.Value, giamCu, srvHistory.Discount, giaVonCu, srvHistory.GiaVon, nguoiChuyenNhuong, srvHistory.KhamTuTuc, hopDongGUID, srvHistory.SoLuong);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin dịch vụ sử dụng";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.Price;
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

        public static Result GetPhieuThuByServiceHistoryGUID(string serviceHistoryGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                ServiceHistory srvHistory = db.ServiceHistories.SingleOrDefault<ServiceHistory>(s => s.ServiceHistoryGUID.ToString() == serviceHistoryGUID);
                if (srvHistory != null)
                {
                    if (srvHistory.ReceiptDetails != null && srvHistory.ReceiptDetails.Count > 0)
                        result.QueryResult = srvHistory.ReceiptDetails[0].Receipt;
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

        public static Result UpdateChecklist(string patientGUID, string hopDongGUID, DateTime beginDate, DateTime endDate, List<DataRow> checklistRows)
        {
            Result result = new Result();

            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (DataRow row in checklistRows)
                    {
                        string desc = string.Empty;
                        bool isUsing = Convert.ToBoolean(row["Using"]);
                        string serviceGUID = row["ServiceGUID"].ToString();

                        List<ServiceHistory> serviceHistoryList = (from s in db.ServiceHistories
                                                                    where s.ServiceGUID.Value.ToString() == serviceGUID &&
                                                                    s.Status == (byte)Status.Actived &&
                                                                    !s.KhamTuTuc && s.PatientGUID.Value.ToString() == patientGUID &&
                                                                    s.ActivedDate.Value >= beginDate && s.ActivedDate.Value <= endDate
                                                                    select s).ToList();

                        if (!isUsing)
                        {
                            if (serviceHistoryList != null && serviceHistoryList.Count > 0)
                            {
                                foreach (var srvHistory in serviceHistoryList)
                                {
                                    srvHistory.Status = (byte)Status.Deactived;

                                    string bacSiThucHien = string.Empty;
                                    if (srvHistory.DocStaff != null)
                                        bacSiThucHien = srvHistory.DocStaff.Contact.FullName;

                                    string nguoiChuyenNhuong = string.Empty;
                                    if (srvHistory.RootPatientGUID != null && srvHistory.RootPatientGUID.HasValue)
                                    {
                                        PatientView patient = db.PatientViews.FirstOrDefault(p => p.PatientGUID == srvHistory.RootPatientGUID.Value);
                                        if (patient != null) nguoiChuyenNhuong = patient.FullName;
                                    }

                                    desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Dịch vụ: '{3}', Giá: '{4}', Giảm: '{5}', Giá vốn: '{6}', Nguuời chuyển nhượng: '{7}', Khám tự túc: '{8}', Số lượng: '{9}'\n",
                                        srvHistory.ServiceHistoryGUID.ToString(), srvHistory.Patient.Contact.FullName, bacSiThucHien,
                                        srvHistory.Service.Name, srvHistory.Price.Value, srvHistory.Discount, srvHistory.GiaVon, nguoiChuyenNhuong, srvHistory.KhamTuTuc, srvHistory.SoLuong);
                                }


                                //Tracking
                                desc = desc.Substring(0, desc.Length - 1);
                                Tracking tk = new Tracking();
                                tk.TrackingGUID = Guid.NewGuid();
                                tk.TrackingDate = DateTime.Now;
                                tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                                tk.ActionType = (byte)ActionType.Delete;
                                tk.Action = "Xóa thông tin dịch vụ sử dụng";
                                tk.Description = desc;
                                tk.TrackingType = (byte)TrackingType.Price;
                                tk.ComputerName = Utility.GetDNSHostName();
                                db.Trackings.InsertOnSubmit(tk);
                            }
                        }
                        else
                        {
                            if (serviceHistoryList == null || serviceHistoryList.Count <= 0)
                            {
                                List<ServiceHistory> svh = (from s in db.ServiceHistories
                                                            where s.ServiceGUID.Value.ToString() == serviceGUID &&
                                                            s.Status == (byte)Status.Actived &&
                                                            s.KhamTuTuc && s.RootPatientGUID.HasValue &&
                                                            s.RootPatientGUID.Value.ToString() == patientGUID &&
                                                            s.ActivedDate.Value >= beginDate && s.ActivedDate.Value <= endDate
                                                            select s).ToList();

                                if (svh == null || svh.Count <= 0)
                                {
                                    ServiceHistory srv = new ServiceHistory();
                                    srv.ServiceHistoryGUID = Guid.NewGuid();
                                    srv.ServiceGUID = Guid.Parse(serviceGUID);
                                    srv.PatientGUID = Guid.Parse(patientGUID);
                                    srv.Status = (byte)Status.Actived;
                                    srv.CreatedDate = DateTime.Now;
                                    srv.CreatedBy = Guid.Parse(Global.UserGUID);
                                    srv.ActivedDate = DateTime.Now;
                                    srv.IsExported = false;
                                    srv.IsNormalOrNegative = true;
                                    srv.Normal = true;
                                    srv.Abnormal = false;
                                    srv.Positive = false;
                                    srv.Negative = false;
                                    srv.KhamTuTuc = false;
                                    srv.Discount = 0;
                                    srv.Price = 0;
                                    srv.GiaVon = 0;
                                    srv.SoLuong = 1;

                                    GiaDichVuHopDong giaDichVuHopDong = db.GiaDichVuHopDongs.SingleOrDefault(g => g.HopDongGUID.ToString() == hopDongGUID &&
                                                                        g.ServiceGUID.ToString() == serviceGUID && g.Status == 0);

                                    if (giaDichVuHopDong != null) srv.Price = giaDichVuHopDong.Gia;

                                    GiaVonDichVu giaVonDichVu = (from g in db.GiaVonDichVus
                                                                 where g.ServiceGUID.ToString() == serviceGUID && g.Status == (byte)Status.Actived &&
                                                                 g.NgayApDung <= DateTime.Now
                                                                 orderby g.NgayApDung descending
                                                                 select g).FirstOrDefault();

                                    if (giaVonDichVu != null) srv.GiaVon = giaVonDichVu.GiaVon;

                                    db.ServiceHistories.InsertOnSubmit(srv);
                                    db.SubmitChanges();

                                    string bacSiThucHien = string.Empty;
                                    if (srv.DocStaff != null)
                                        bacSiThucHien = srv.DocStaff.Contact.FullName;

                                    string nguoiChuyenNhuong = string.Empty;
                                    if (srv.RootPatientGUID != null && srv.RootPatientGUID.HasValue)
                                    {
                                        PatientView patient = db.PatientViews.FirstOrDefault(p => p.PatientGUID == srv.RootPatientGUID.Value);
                                        if (patient != null) nguoiChuyenNhuong = patient.FullName;
                                    }

                                    //Tracking
                                    desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Dịch vụ: '{3}', Giá: '{4}', Giảm: '{5}', Giá vốn: '{6}', Người chuyển nhượng: '{7}', Khám tự túc: '{8}', Số lượng: '{9}'",
                                            srv.ServiceHistoryGUID.ToString(), srv.Patient.Contact.FullName, bacSiThucHien, srv.Service.Name, srv.Price.Value, 
                                            srv.Discount, srv.GiaVon, nguoiChuyenNhuong, srv.KhamTuTuc, srv.SoLuong);

                                    Tracking tk = new Tracking();
                                    tk.TrackingGUID = Guid.NewGuid();
                                    tk.TrackingDate = DateTime.Now;
                                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                                    tk.ActionType = (byte)ActionType.Add;
                                    tk.Action = "Thêm thông tin dịch vụ sử dụng";
                                    tk.Description = desc;
                                    tk.TrackingType = (byte)TrackingType.Price;
                                    tk.ComputerName = Utility.GetDNSHostName();
                                    db.Trackings.InsertOnSubmit(tk);
                                }
                            }
                        }
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
                        string serviceHistoryGUID = row["ServiceHistoryGUID"].ToString();
                        ServiceHistory srvHistory = (from s in db.ServiceHistories
                                                    where s.ServiceHistoryGUID.ToString() == serviceHistoryGUID
                                                    select s).FirstOrDefault();

                        if (srvHistory == null) continue;

                        //Tracking
                        string desc = string.Format("- ServiceHistoryGUID: '{0}': PatientGUID: '{1}' ==> '{2}' (ServiceHistory)",
                            serviceHistoryGUID, srvHistory.PatientGUID.ToString(), patientGUID);

                        srvHistory.PatientGUID = Guid.Parse(patientGUID);

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
