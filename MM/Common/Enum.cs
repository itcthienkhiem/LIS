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

namespace MM.Common
{
    public enum LoaiPT : byte
    {
        DichVu = 0,
        Thuoc,
        HopDong,
        CapCuu
    }

    public enum ServiceType : byte
    {
        CanLamSang = 0,
        LamSang
    };

    public enum ActionType : byte
    {
        Add = 0,
        Edit,
        Delete
    };

    public enum TrackingType : byte
    {
        None = 0,
        Price,
        Count
    };

    public enum DrawType
    {
        None,
        Line,
        Pencil
    };

    public enum ErrorCode : int
    {
        OK = 0,
        NO_DATA,
        INVALID_SQL_STATEMENT,
        NO_AUTHORIZED,
        NO_DATA_TO_ANALYZE,
        SQL_QUERY_TIMEOUT,
        UNKNOWN_ERROR,
        INVALID_CONNECTION_INFO,
        INVALID_SERVERNAME,
        INVALID_DATABASENAME,
        INVALID_USERNAME,
        INVALID_PASSWORD,
        DATA_INCONSISTENT,
        LOCK,
        DELETED,
        NO_UPDATED,
        CANCEL_UPDATED,
        EXIST,
        NOT_EXIST,
        CONNECT_FTP_FAIL,
        UPLOAD_FTP_FAIL, 
        DELETE_FTP_FAIL,
        FILE_NOT_FOUND
    };

    public enum Gender : int
    {
        Male = 0,
        Female,
        None
    };

    public enum IconType : int
    {
        Information,
        Question,
        Error
    };

    public enum LoaiToaThuoc : byte
    {
        Chung = 0,
        SanKhoa
    };

    public enum MsgBoxType
    {
        OK,
        YesNo
    };

    public enum StaffType : int
    {
        BacSi = 0,
        DieuDuong,
        LeTan,
        BenhNhan,
        Admin,
        XetNghiem,
        ThuKyYKhoa,
        Sale,
        KeToan,
        None,
        BacSiSieuAm,
        BacSiNgoaiTongQuat,
        BacSiNoiTongQuat,
        BacSiPhuKhoa
    };

    public enum Status : byte
    {
        Actived = 0,
        Deactived
    };

    public enum WorkType : int
    {
        FullTime = 0,
        PartTime
    };

    public enum PaymentType : int
    {
        TienMat = 0,
        ChuyenKhoan,
        TienMat_ChuyenKhoan,
        BaoHiem,
        CaThe
    };

    public enum CoQuan : int
    {
        Mat = 0,
        TaiMuiHong,
        RangHamMat,
        HoHap,
        TimMach,
        TieuHoa,
        TietNieuSinhDuc,
        CoXuongKhop,
        DaLieu,
        ThanKinh,
        NoiTiet,
        Khac, 
        KhamPhuKhoa
    };

    public enum LoaiNoiSoi : byte
    {
        Tai = 0,
        Mui,
        Hong_ThanhQuan,
        TaiMuiHong,
        TongQuat,
        DaDay, 
        TrucTrang
    };

    public enum BookMarkType : int
    {
        KetLuanNoiSoiTai = 0,
        KetLuanNoiSoiMui,
        KetLuanNoiSoiHongThanhQuan,
        KetLuanNoiSoiTaiMuiHong,
        KetLuanNoiSoiTongQuat,

        DeNghiNoiSoiTai,
        DeNghiNoiSoiMui,
        DeNghiNoiSoiHongThanhQuan,
        DeNghiNoiSoiTaiMuiHong,
        DeNghiNoiSoiTongQuat,

        KetQuaNoiSoiOngTai,
        KetQuaNoiSoiMangNhi,
        KetQuaNoiSoiCanBua,
        KetQuaNoiSoiHomNhi,
        KetQuaNoiSoiValsava,

        KetQuaNoiSoiNiemMac,
        KetQuaNoiSoiVachNgan,
        KetQuaNoiSoiKheTren,
        KetQuaNoiSoiKheGiua,
        KetQuaNoiSoiCuonGiua,
        KetQuaNoiSoiCuonDuoi,
        KetQuaNoiSoiMomMoc,
        KetQuaNoiSoiBongSang,
        KetQuaNoiSoiVom,

        KetQuaNoiSoiAmydale,
        KetQuaNoiSoiXoangLe,
        KetQuaNoiSoiMiengThucQuan,
        KetQuaNoiSoiSunPheu,
        KetQuaNoiSoiDayThanh,
        KetQuaNoiSoiBangThanhThat,

        KetQuaNoiSoiMomMoc_BongSang,
        KetQuaNoiSoiThanhQuan,

        KetQuaSoiAmHo,
        KetQuaSoiAmDao,
        KetQuaSoiCTC,
        KetQuaSoiBieuMoLat,
        KetQuaSoiMoDem,
        KetQuaSoiRanhGioiLatTru,
        KetQuaSoiSauAcidAcetic,
        KetQuaSoiSauLugol,
        KetLuanSoiCTCT,

        KetLuanNoiSoiDaDay,
        KetLuanNoiSoiTrucTrang,

        DeNghiNoiSoiDaDay,
        DeNghiNoiSoiTrucTrang,

        KetQuaNoiSoiThucQuan,
        KetQuaNoiSoiDaDay,
        KetQuaNoiSoiHangVi,
        KetQuaNoiSoiMonVi,
        KetQuaNoiSoiHanhTaTrang,
        KetQuaNoiSoiClotest,

        KetQuaNoiSoiTrucTrang,
        KetQuaNoiSoiDaiTrangTrai,
        KetQuaNoiSoiDaiTrangGocLach,
        KetQuaNoiSoiDaiTrangNgang,
        KetQuaNoiSoiDaiTrangGocGan,
        KetQuaNoiSoiDaiTrangPhai,
        KetQuaNoiSoiManhTrang
    };

    public enum LoaiHoaDon : int
    {
        HoaDonThuoc = 0,
        HoaDonDichVu,
        HoaDonXuatTruoc,
        HoaDonHopDong
    };

    public enum LockType : int
    {
        HopDong = 0
    };

    public enum BookingType : int
    {
        Monitor = 0,
        BloodTaking
    };

    public enum DoiTuong : byte
    {
        Chung = 0,
        Chung_Sau2h,
        Nam,
        Nu,
        TreEm,
        NguoiLon,
        NguoiCaoTuoi,
        Nam_Sau2h,
        Nu_Sau2h,
        TreEm_Sau2h,
        NguoiLon_Sau2h,
        NguoiCaoTuoi_Sau2h,
        HutThuoc,
        KhongHutThuoc,
        Sang_Chung,
        Chieu_Chung,
        FollicularPhase,
        Midcycle,
        LutelPhase,
        AmTinhDuongTinh,
        Khac,
        Sang_Nam,
        Sang_Nu,
        Chieu_Nam,
        Chieu_Nu
    };

    public enum LoaiMayXN : byte
    {
        Hitachi917,
        CellDyn3200
    };

    public enum TinhTrang : byte
    {
        BinhThuong = 0,
        BatThuong
    };

    public enum AgeUnit : byte
    {
        Unknown = 0,
        Days,
        Months,
        Years
    };

    public enum LoaiXetNghiem : byte
    {
        Haematology = 0,//Huyết học
        Biochemistry, //Sinh hóa
        MienDich,
        Urine, //Nước tiểu
        Khac
        //SoiTuoiHuyetTrang,
        //Electrolytes, //Ion đồ
    }

    public enum LoaiLichKham : int
    {
        CongTySang = 0,
        CongTyChieu,
        BsNoiTongQuatSang,
        BsNoiTongQuatChieu,
        BsNgoaiTongQuatSang,
        BsNgoaiTongQuatChieu,
        BsSieuAmSang,
        BsSieuAmChieu,
        BsSanPhuKhoaSang,
        BsSanPhuKhoaChieu,
        BsRangHamMatSang,
        BsRangHamMatChieu,
        BsTaiMuiHongSang,
        BsTaiMuiHongChieu
    }

    public enum PatientSearchType : int
    {
        BenhNhan = 0,
        BenhNhanThanThuoc,
        BenhNhanKhongThanThuoc,
        NhanVienHopDong
    }

    public enum TVHomeImageFormat : int
    {
        BMP = 0,
        JPG
    }
}
