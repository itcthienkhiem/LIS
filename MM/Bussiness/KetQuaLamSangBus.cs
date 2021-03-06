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
using System.Collections;
using MM.Common;
using MM.Databasae;

namespace MM.Bussiness
{
    public class KetQuaLamSangBus : BusBase
    {
        public static Result GetKetQuaLamSangList(string patientGUID, DateTime fromDate, DateTime toDate)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                //if (Global.StaffType != StaffType.BacSi && Global.StaffType != StaffType.BacSiSieuAm &&
                //    Global.StaffType != StaffType.BacSiNgoaiTongQuat && Global.StaffType != StaffType.BacSiNoiTongQuat && 
                //    Global.StaffType != StaffType.BacSiPhuKhoa)
                //    query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, *, '' AS KetQua FROM KetQuaLamSangView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND NgayKham BETWEEN '{1}' AND '{2}' AND Status = {3} AND Archived = 'False' ORDER BY NgayKham DESC",
                //        patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), (byte)Status.Actived);
                //else
                //    query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, *, '' AS KetQua FROM KetQuaLamSangView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND NgayKham BETWEEN '{1}' AND '{2}' AND Status = {3} AND Archived = 'False' AND DocStaffGUID = '{4}' ORDER BY NgayKham DESC",
                //        patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), (byte)Status.Actived, Global.UserGUID);

                query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, *, '' AS KetQua FROM KetQuaLamSangView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND NgayKham BETWEEN '{1}' AND '{2}' AND Status = {3} ORDER BY NgayKham DESC",
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

        public static Result DeleteKetQuaLamSang(List<String> ketQuaLamSangKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    foreach (string key in ketQuaLamSangKeys)
                    {
                        KetQuaLamSang kqls = db.KetQuaLamSangs.SingleOrDefault<KetQuaLamSang>(k => k.KetQuaLamSangGUID.ToString() == key);
                        if (kqls != null)
                        {
                            kqls.DeletedDate = DateTime.Now;
                            kqls.DeletedBy = Guid.Parse(Global.UserGUID);
                            kqls.Status = (byte)Status.Deactived;

                            string ketQua = string.Empty;
                            if (kqls.Normal) ketQua += "Bình thường, ";
                            if (kqls.Abnormal) ketQua += "Bất thường, ";

                            if (ketQua != string.Empty) ketQua = ketQua.Substring(0, ketQua.Length - 2);


                            if (kqls.CoQuan != (byte)CoQuan.KhamPhuKhoa)
                            {
                                desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Cơ quan: '{3}', Kết quả: '{4}', Nhận xét: '{5}'\n",
                                kqls.KetQuaLamSangGUID.ToString(), kqls.Patient.Contact.FullName, kqls.DocStaff.Contact.FullName,
                                Utility.ParseCoQuanEnumToName((CoQuan)kqls.CoQuan), ketQua, kqls.Note);
                            }
                            else
                            {
                                if (kqls.NgayKinhChot.HasValue)
                                {
                                    desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Cơ quan: '{3}', PARA: '{4}', Ngày kinh chót: '{5}', Ghi chú: '{6}', Kết quả khám phụ khoa: '{7}', Soi tươi huyết trắng: '{8}', Kết quả Pap: '{9}'\n",
                                    kqls.KetQuaLamSangGUID.ToString(), kqls.Patient.Contact.FullName, kqls.DocStaff.Contact.FullName,
                                    Utility.ParseCoQuanEnumToName((CoQuan)kqls.CoQuan), kqls.PARA, kqls.NgayKinhChot.Value.ToString("dd/MM/yyyy"),
                                    kqls.PhuKhoaNote, kqls.Note, kqls.SoiTuoiHuyetTrang, ketQua);
                                }
                                else
                                {
                                    desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Cơ quan: '{3}', PARA: '{4}', Ghi chú: '{5}', Kết quả khám phụ khoa: '{6}', Soi tươi huyết trắng: '{7}', Kết quả Pap: '{8}'\n",
                                    kqls.KetQuaLamSangGUID.ToString(), kqls.Patient.Contact.FullName, kqls.DocStaff.Contact.FullName,
                                    Utility.ParseCoQuanEnumToName((CoQuan)kqls.CoQuan), kqls.PARA, kqls.PhuKhoaNote, kqls.Note, kqls.SoiTuoiHuyetTrang, ketQua);
                                }
                            }
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin khám lâm sàng";
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

        public static Result InsertKetQuaLamSang(KetQuaLamSang ketQuaLamSang)
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
                    if (ketQuaLamSang.KetQuaLamSangGUID == null || ketQuaLamSang.KetQuaLamSangGUID == Guid.Empty)
                    {
                        ketQuaLamSang.KetQuaLamSangGUID = Guid.NewGuid();
                        db.KetQuaLamSangs.InsertOnSubmit(ketQuaLamSang);
                        db.SubmitChanges();

                        //Tracking
                        string ketQua = string.Empty;
                        if (ketQuaLamSang.Normal) ketQua += "Bình thường, ";
                        if (ketQuaLamSang.Abnormal) ketQua += "Bất thường, ";

                        if (ketQua != string.Empty) ketQua = ketQua.Substring(0, ketQua.Length - 2);

                        if (ketQuaLamSang.CoQuan != (byte)CoQuan.KhamPhuKhoa)
                        {
                            desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Cơ quan: '{3}', Kết quả: '{4}', Nhận xét: '{5}'",
                            ketQuaLamSang.KetQuaLamSangGUID.ToString(), ketQuaLamSang.Patient.Contact.FullName, ketQuaLamSang.DocStaff.Contact.FullName,
                            Utility.ParseCoQuanEnumToName((CoQuan)ketQuaLamSang.CoQuan), ketQua, ketQuaLamSang.Note);
                        }
                        else
                        {
                            if (ketQuaLamSang.NgayKinhChot != null && ketQuaLamSang.NgayKinhChot.HasValue)
                            {
                                desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Cơ quan: '{3}', PARA: '{4}', Ngày kinh chót: '{5}', Ghi chú: '{6}', Kết quả khám phụ khoa: '{7}', Soi tươi huyết trắng: '{8}', Kết quả Pap: '{9}'",
                                ketQuaLamSang.KetQuaLamSangGUID.ToString(), ketQuaLamSang.Patient.Contact.FullName, ketQuaLamSang.DocStaff.Contact.FullName,
                                Utility.ParseCoQuanEnumToName((CoQuan)ketQuaLamSang.CoQuan), ketQuaLamSang.PARA, ketQuaLamSang.NgayKinhChot.Value.ToString("dd/MM/yyyy"),
                                ketQuaLamSang.PhuKhoaNote, ketQuaLamSang.Note, ketQuaLamSang.SoiTuoiHuyetTrang, ketQua);
                            }
                            else
                            {
                                desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Cơ quan: '{3}', PARA: '{4}', Ghi chú: '{5}', Kết quả khám phụ khoa: '{6}', Soi tươi huyết trắng: '{7}', Kết quả Pap: '{8}'",
                                ketQuaLamSang.KetQuaLamSangGUID.ToString(), ketQuaLamSang.Patient.Contact.FullName, ketQuaLamSang.DocStaff.Contact.FullName,
                                Utility.ParseCoQuanEnumToName((CoQuan)ketQuaLamSang.CoQuan), ketQuaLamSang.PARA, ketQuaLamSang.PhuKhoaNote, ketQuaLamSang.Note, 
                                ketQuaLamSang.SoiTuoiHuyetTrang, ketQua);
                            }
                        }
                        

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin khám lâm sàng";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        tk.ComputerName = Utility.GetDNSHostName();
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        KetQuaLamSang kqls = db.KetQuaLamSangs.SingleOrDefault<KetQuaLamSang>(k => k.KetQuaLamSangGUID == ketQuaLamSang.KetQuaLamSangGUID);
                        if (kqls != null)
                        {
                            kqls.NgayKham = ketQuaLamSang.NgayKham;
                            kqls.PatientGUID = ketQuaLamSang.PatientGUID;
                            kqls.DocStaffGUID = ketQuaLamSang.DocStaffGUID;
                            kqls.CoQuan = ketQuaLamSang.CoQuan;
                            kqls.Normal = ketQuaLamSang.Normal;
                            kqls.Abnormal = ketQuaLamSang.Abnormal;
                            kqls.Note = ketQuaLamSang.Note;
                            kqls.PARA = ketQuaLamSang.PARA;
                            kqls.NgayKinhChot = ketQuaLamSang.NgayKinhChot;
                            kqls.PhuKhoaNote = ketQuaLamSang.PhuKhoaNote;
                            kqls.SoiTuoiHuyetTrang = ketQuaLamSang.SoiTuoiHuyetTrang;
                            kqls.CreatedBy = ketQuaLamSang.CreatedBy;
                            kqls.CreatedDate = ketQuaLamSang.CreatedDate;
                            kqls.DeletedBy = ketQuaLamSang.DeletedBy;
                            kqls.DeletedDate = ketQuaLamSang.DeletedDate;
                            kqls.UpdatedBy = ketQuaLamSang.UpdatedBy;
                            kqls.UpdatedDate = ketQuaLamSang.UpdatedDate;
                            kqls.Status = ketQuaLamSang.Status;
                            db.SubmitChanges();

                            //Tracking
                            string ketQua = string.Empty;
                            if (ketQuaLamSang.Normal) ketQua += "Bình thường, ";
                            if (ketQuaLamSang.Abnormal) ketQua += "Bất thường, ";

                            if (ketQua != string.Empty) ketQua = ketQua.Substring(0, ketQua.Length - 2);

                            if (kqls.CoQuan != (byte)CoQuan.KhamPhuKhoa)
                            {
                                desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Cơ quan: '{3}', Kết quả: '{4}', Nhận xét: '{5}'",
                                kqls.KetQuaLamSangGUID.ToString(), kqls.Patient.Contact.FullName, kqls.DocStaff.Contact.FullName,
                                Utility.ParseCoQuanEnumToName((CoQuan)kqls.CoQuan), ketQua, kqls.Note);
                            }
                            else
                            {
                                if (kqls.NgayKinhChot != null && kqls.NgayKinhChot.HasValue)
                                {
                                    desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Cơ quan: '{3}', PARA: '{4}', Ngày kinh chót: '{5}', Ghi chú: '{6}', Kết quả khám phụ khoa: '{7}', Soi tươi huyết trắng: '{8}', Kết quả Pap: '{9}'",
                                    kqls.KetQuaLamSangGUID.ToString(), kqls.Patient.Contact.FullName, kqls.DocStaff.Contact.FullName,
                                    Utility.ParseCoQuanEnumToName((CoQuan)kqls.CoQuan), kqls.PARA, kqls.NgayKinhChot.Value.ToString("dd/MM/yyyy"),
                                    kqls.PhuKhoaNote, kqls.Note, kqls.SoiTuoiHuyetTrang, ketQua);
                                }
                                else
                                {
                                    desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Cơ quan: '{3}', PARA: '{4}', Ghi chú: '{5}', Kết quả khám phụ khoa: '{6}', Soi tươi huyết trắng: '{7}', Kết quả Pap: '{8}'",
                                    kqls.KetQuaLamSangGUID.ToString(), kqls.Patient.Contact.FullName, kqls.DocStaff.Contact.FullName,
                                    Utility.ParseCoQuanEnumToName((CoQuan)kqls.CoQuan), kqls.PARA, kqls.PhuKhoaNote, kqls.Note, kqls.SoiTuoiHuyetTrang, ketQua);
                                }
                            }

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin khám lâm sàng";
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

        public static Result InsertKetQuaLamSang(List<KetQuaLamSang> ketQuaLamSangList)
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
                    foreach (var ketQuaLamSang in ketQuaLamSangList)
                    {
                        ketQuaLamSang.KetQuaLamSangGUID = Guid.NewGuid();
                        db.KetQuaLamSangs.InsertOnSubmit(ketQuaLamSang);
                        db.SubmitChanges();

                        //Tracking
                        string ketQua = string.Empty;
                        if (ketQuaLamSang.Normal) ketQua += "Bình thường, ";
                        if (ketQuaLamSang.Abnormal) ketQua += "Bất thường, ";

                        if (ketQua != string.Empty) ketQua = ketQua.Substring(0, ketQua.Length - 2);

                        if (ketQuaLamSang.CoQuan != (byte)CoQuan.KhamPhuKhoa)
                        {
                            desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Cơ quan: '{3}', Kết quả: '{4}', Nhận xét: '{5}'\n",
                            ketQuaLamSang.KetQuaLamSangGUID.ToString(), ketQuaLamSang.Patient.Contact.FullName, ketQuaLamSang.DocStaff.Contact.FullName,
                            Utility.ParseCoQuanEnumToName((CoQuan)ketQuaLamSang.CoQuan), ketQua, ketQuaLamSang.Note);
                        }
                        else
                        {
                            if (ketQuaLamSang.NgayKham != null && ketQuaLamSang.NgayKinhChot.HasValue)
                            {
                                desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Cơ quan: '{3}', PARA: '{4}', Ngày kinh chót: '{5}', Ghi chú: '{6}', Kết quả khám phụ khoa: '{7}', Soi tươi huyết trắng: '{8}', Kết quả Pap: '{9}'",
                                ketQuaLamSang.KetQuaLamSangGUID.ToString(), ketQuaLamSang.Patient.Contact.FullName, ketQuaLamSang.DocStaff.Contact.FullName,
                                Utility.ParseCoQuanEnumToName((CoQuan)ketQuaLamSang.CoQuan), ketQuaLamSang.PARA, ketQuaLamSang.NgayKinhChot.Value.ToString("dd/MM/yyyy"),
                                ketQuaLamSang.PhuKhoaNote, ketQuaLamSang.Note, ketQuaLamSang.SoiTuoiHuyetTrang, ketQua);
                            }
                            else
                            {
                                desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Cơ quan: '{3}', PARA: '{4}', Ghi chú: '{5}', Kết quả khám phụ khoa: '{6}', Soi tươi huyết trắng: '{7}', Kết quả Pap: '{8}'",
                                ketQuaLamSang.KetQuaLamSangGUID.ToString(), ketQuaLamSang.Patient.Contact.FullName, ketQuaLamSang.DocStaff.Contact.FullName,
                                Utility.ParseCoQuanEnumToName((CoQuan)ketQuaLamSang.CoQuan), ketQuaLamSang.PARA, ketQuaLamSang.PhuKhoaNote, ketQuaLamSang.Note,
                                ketQuaLamSang.SoiTuoiHuyetTrang, ketQua);
                            }
                        }    
                    }

                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Add;
                    tk.Action = "Thêm thông tin khám lâm sàng";
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

        public static Result GetLastKetQuaLamSang(string patientGUID, DateTime fromDate, DateTime toDate)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                Hashtable htKetQuaLamSang = new Hashtable();

                CoQuan[] coQuanList = (CoQuan[])Enum.GetValues(typeof(CoQuan));
                foreach (CoQuan coQuan in coQuanList)
                {
                    KetQuaLamSang kq = (from k in db.KetQuaLamSangs
                                        where k.PatientGUID.ToString() == patientGUID &&
                                        k.NgayKham >= fromDate && k.NgayKham <= toDate &&
                                        k.CoQuan == (byte)coQuan &&
                                        k.Status == (byte)Status.Actived
                                        orderby k.NgayKham descending
                                        select k).FirstOrDefault<KetQuaLamSang>();
                    if (kq != null) htKetQuaLamSang.Add(coQuan, kq);    
                }

                result.QueryResult = htKetQuaLamSang;
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

        public static Result GetNgayKhamLamSangCuoiCung(string patientGUID)
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT Max(NgayKham) FROM KetQuaLamSang WITH(NOLOCK) WHERE PatientGUID = '{0}' AND Status = {1}",
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
                        string ketQuaLamSangGUID = row["KetQuaLamSangGUID"].ToString();
                        KetQuaLamSang ketQuaLamSang = (from s in db.KetQuaLamSangs
                                                       where s.KetQuaLamSangGUID.ToString() == ketQuaLamSangGUID
                                                       select s).FirstOrDefault();

                        if (ketQuaLamSang == null) continue;

                        //Tracking
                        string desc = string.Format("- KetQuaLamSangGUID: '{0}': PatientGUID: '{1}' ==> '{2}' (KetQuaLamSang)",
                            ketQuaLamSangGUID, ketQuaLamSang.PatientGUID.ToString(), patientGUID);

                        ketQuaLamSang.PatientGUID = Guid.Parse(patientGUID);

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
