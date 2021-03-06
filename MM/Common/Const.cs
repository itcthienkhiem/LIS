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
    public class Const
    {
        public static string ServerNameKey = "ServerNameKey";
        public static string DatabaseNameKey = "DatabaseNameKey";
        public static string AuthenticationKey = "AuthenticationKey";
        public static string UserNameKey = "UserNameKey";
        public static string PasswordKey = "PasswordKey";

        public static string FTPServerNameKey = "FTPServerNameKey";
        public static string FTPUserNameKey = "FTPUserNameKey";
        public static string FTPPasswordKey = "FTPPasswordKey";

        public static string AlertDayKey = "AlertDayKey";
        public static string AlertSoNgayHetHanCapCuuKey = "AlertSoNgayHetHanCapCuuKey";
        public static string AlertSoLuongHetTonKhoCapCuuKey = "AlertSoLuongHetTonKhoCapCuuKey";

        public static string TVHomePathKey = "TVHomePathKey";
        public static string TVHomeSoiCTCKey = "TVHomeSoiCTCKey";
        public static string TVHomeSieuAmKey = "TVHomeSieuAmKey";
        public static string TVHomeFormatKey = "TVHomeFormatKey";

        public static string UsernameKey = "UsernameKey";

        public static string ShareFolderKey = "ShareFolderKey";

        public static string XoaDichVuKhiXoaPhieuThuKey = "XoaDichVuKhiXoaPhieuThuKey";

        public static string AdminGUID = "00000000-0000-0000-0000-000000000000";

        public static string Contract = "Contract";
        public static string OpenPatient = "OpenPatient";
        public static string Patient = "Patient";
        public static string Permission = "Permission";
        public static string ServicePrice = "ServicePrice";
        public static string Services = "Services";
        public static string Company = "Company";
        public static string Symptom = "Symptom";
        public static string DocStaff = "DocStaff";
        public static string Speciality = "Speciality";
        public static string PrintLabel = "PrintLabel";
        public static string Receipt = "Receipt";
        public static string Invoice = "Invoice";
        public static string DuplicatePatient = "DuplicatePatient";
        public static string DoanhThuNhanVien = "DoanhThuNhanVien";
        public static string DichVuHopDong = "DichVuHopDong";
        public static string Thuoc = "Thuoc";
        public static string NhomThuoc = "NhomThuoc";
        public static string LoThuoc = "LoThuoc";
        public static string GiaThuoc = "GiaThuoc";
        public static string KeToa = "KeToa";
        public static string ThuocHetHan = "ThuocHetHan";
        public static string ThuocTonKho = "ThuocTonKho";
        public static string PhieuThuThuoc = "PhieuThuThuoc";
        public static string DichVuTuTuc = "DichVuTuTuc";
        public static string ChiDinh = "ChiDinh";
        public static string Tracking = "Tracking";
        public static string ServiceGroup = "ServiceGroup";
        public static string InKetQuaKhamSucKhoeTongQuat = "InKetQuaKhamSucKhoeTongQuat";
        public static string GiaVonDichVu = "GiaVonDichVu";
        public static string DoanhThuTheoNgay = "DoanhThuTheoNgay";
        public static string PhongCho = "PhongCho";
        public static string DichVuChuaXuatPhieuThu = "DichVuChuaXuatPhieuThu";
        public static string HoaDonThuoc = "HoaDonThuoc";
        public static string HoaDonXuatTruoc = "HoaDonXuatTruoc";
        public static string DangKyHoaDonXuatTruoc = "DangKyHoaDonXuatTruoc";
        public static string ThongKeHoaDon = "ThongKeHoaDon";
        public static string PhucHoiBenhNhan = "PhucHoiBenhNhan";
        public static string PhieuThuHopDong = "PhieuThuHopDong";
        public static string HoaDonHopDong = "HoaDonHopDong";
        public static string YKienKhachHang = "YKienKhachHang";
        public static string TuVanKhachHang = "TuVanKhachHang";
        public static string NhatKyLienHeCongTy = "NhatKyLienHeCongTy";
        public static string DichVuDaSuDung = "DichVuDaSuDung";
        public static string CanDo = "CanDo";
        public static string KhamLamSang = "KhamLamSang";
        public static string LoiKhuyen = "LoiKhuyen";
        public static string KetLuan = "KetLuan";
        public static string KhamNoiSoi = "KhamNoiSoi";
        public static string KhamCTC = "KhamCTC";
        public static string Booking = "Booking";
        public static string XetNghiem = "XetNghiem";
        public static string BaoCaoKhachHangMuaThuoc = "BaoCaoKhachHangMuaThuoc";
        public static string BaoCaoSoLuongKham = "BaoCaoSoLuongKham";
        public static string ThayDoiSoHoaDon = "ThayDoiSoHoaDon";
        public static string ThayDoiSoHoaDonXetNghiem = "ThayDoiSoHoaDonXetNghiem";
        public static string TraCuuThongTinKhachHang = "TraCuuThongTinKhachHang";
        public static string DiaChiCongTy = "DiaChiCongTy";
        public static string ChiTietPhieuThuDichVu = "ChiTietPhieuThuDichVu";
        public static string CauHinTrangIn = "CauHinhTrangIn";
        public static string BenhNhanThanThuoc = "BenhNhanThanThuoc";
        public static string LoaiSieuAm = "LoaiSieuAm";
        public static string KetQuaSieuAm = "KetQuaSieuAm";
        public static string TiemNgua = "TiemNgua";
        public static string CauHinhSoNgayBaoTiemNgua = "CauHinhSoNgayBaoTiemNgua";
        public static string CongTacNgoaiGio = "CongTacNgoaiGio";
        public static string LichKham = "LichKham";
        public static string KhoCapCuu = "KhoCapCuu";
        public static string NhapKhoCapCuu = "NhapKhoCapCuu";
        public static string XuatKhoCapCuu = "XuatKhoCapCuu";
        public static string BaoCaoTonKhoCapCuu = "BaoCaoTonKhoCapCuu";
        public static string BaoCaoCapCuuHetHan = "BaoCaoCapCuuHetHan";
        public static string CauHinhKhoCapCuu = "CauHinhKhoCapCuu";
        public static string ThongBao = "ThongBao";
        public static string BenhNhanNgoaiGoiKham = "BenhNhanNgoaiGoiKham";
        public static string PhieuThuCapCuu = "PhieuThuCapCuu";
        public static string GiaCapCuu = "GiaCapCuu";
        public static string NguoiSuDung = "NguoiSuDung";
        public static string NhomNguoiSuDung = "NhomNguoiSuDung";
        public static string KetQuaCanLamSang = "KetQuaCanLamSang";
        public static string TaoHoSo = "TaoHoSo";
        public static string UploadHoSo = "UploadHoSo";
        public static string KeToaCapCuu = "KeToaCapCuu";
        public static string TaoMatKhauHoSo = "TaoMatKhauHoSo";
        public static string TVHomeConfig = "TVHomeConfig";
        public static string KhamHopDong = "KhamHopDong";
        public static string TinNhanMau = "TinNhanMau";
        public static string GuiSMS = "GuiSMS";
        public static string BaoCaoCongNoHopDong = "BaoCaoCongNoHopDong";
        public static string SMSLog = "SMSLog";
        public static string CapNhatNhanhChecklist = "CapNhatNhanhChecklist";
        public static string ToaThuocTrongNgay = "ToaThuocTrongNgay";
        public static string NhanVienTrungLap = "NhanVienTrungLap";
        public static string ChuyenBenhAn = "ChuyenBenhAn";
        public static string DichVuXetNghiem = "DichVuXetNghiem";
        public static string ThongKeThuocXuatHoaDon = "ThongKeThuocXuatHoaDon";
        public static string MapMauHoSoVoiDichVu = "MapMauHoSoVoiDichVu";
        public static string InMauHoSo = "InMauHoSo";
        public static string CauHinhDichVuXetNghiem = "CauHinhDichVuXetNghiem";
        public static string TraHoSo = "TraHoSo";
        public static string DoanhThuTheoNhomDichVu = "DoanhThuTheoNhomDichVu";
        public static string HuyThuoc = "HuyThuoc";
        public static string ThongKeHoaDonDichVuVaThuoc = "ThongKeHoaDonDichVuVaThuoc";
        public static string ThongKePhieuThuDichVuVaThuoc = "ThongKePhieuThuDichVuVaThuoc";
        public static string HoaDonXetNghiem = "HoaDonXetNghiem";
        public static string BaoCaoDoanhThuThuocTheoPhieuThu = "BaoCaoDoanhThuThuocTheoPhieuThu";
        public static string ThongKeChiDinhDuocXuatHoaDon = "ThongKeChiDinhDuocXuatHoaDon";
        public static string ThongKeChiDinhCuaBacSi = "ThongKeChiDinhCuaBacSi";
        public static string GhiNhanTraNo = "GhiNhanTraNo";
        public static string NhanXetKhamLamSang = "NhanXetKhamLamSang";
        public static string PhucHoiKetQuaKhamBenh = "PhucHoiKetQuaKhamBenh";

        public static string CheckListTemplate = "Theo dõi thực hiện";
        public static string ChiTietPhieuThuDichVuTemplate = "Chi tiết phiếu thu dịch vụ";
        public static string ChiTietPhieuThuThuocTemplate = "Chi tiết phiếu thu thuốc";
        public static string ChiTietPhieuThuCapCuuTemplate = "Chi tiết phiếu thu cấp cứu";
        public static string DanhSachBenhNhanDenKhamTemplate = "Danh sách bệnh nhân đến khám";
        public static string DanhSachBenhNhanTemplate = "Danh sách bệnh nhân";
        public static string DanhSachBenhNhan2Template = "Danh sách bệnh nhân";
        public static string DichVuHopDongTemplate = "Dịch vụ hợp đồng";
        public static string DichVuTuTucTemplate = "Dịch vụ tự túc";
        public static string DoanhThuTheoNgayTemplate = "Doanh thu theo ngày";
        public static string GiaVonDichVuTemplate = "Giá vốn dịch vụ";
        public static string HDGTGTTemplate = "Hóa đơn giá trị gia tăng";
        public static string KetQuaNoiSoiHongThanhQuanTemplate = "Kết quả nội soi thanh quản";
        public static string KetQuaNoiSoiMuiTemplate = "Kết quả nội soi mũi";
        public static string KetQuaNoiSoiTaiMuiHongTemplate = "Kết quả nội soi tai mũi họng";
        public static string KetQuaNoiSoiTaiTemplate = "Kết quả nội soi tai";
        public static string KetQuaNoiSoiTongQuatTemplate = "Kết quả nội soi tổng quát";
        public static string KetQuaNoiSoiDaDayTemplate = "Kết quả nội soi dạ dày";
        public static string KetQuaNoiSoiTrucTrangTemplate = "Kết quả nội soi trực tràng";
        public static string KetQuaSoiCTCTemplate = "Kết quả nội soi cổ tử cung";
        public static string KhamSucKhoeTongQuatTemplate = "Khám sức khỏe tổng quát";
        public static string NhatKyLienHeCongTyTemplate = "Nhật kí liên hệ công ty";
        public static string PhieuThuThuocTemplate = "Phiếu thu thuốc";
        public static string PhieuThuCapCuuTemplate = "Phiếu thu cấp cứu";
        public static string PhieuThuDichVuTemplate = "Phiếu thu dịch vụ";
        public static string TrieuChungTemplate = "Triệu chứng";
        public static string ThuocTonKhoTheoKhoangThoiGianTemplate = "Thuốc tồn kho theo khoảng thời gian";
        public static string ToaThuocTemplate = "Toa thuốc";
        public static string ToaThuocChungTemplate = "Toa thuốc chung";
        public static string ToaThuocSanKhoaTemplate = "Toa thuốc sản khoa";
        public static string YKienKhachHangTemplate = "Ý kiến khách hàng";
        public static string TuVanKhachHangTemplate = "Tư vấn khách hàng";
        public static string KetQuaXetNghiemCellDyn3200Template = "Kết quả xét nghiệm CellDyn3200";
        public static string KetQuaXetNghiemSinhHoaTemplate = "Kết quả xét nghiệm sinh hóa";
        public static string DanhSachDichVuXuatPhieuThuTemplate = "Danh sách dịch vụ xuất phiếu thu";
        public static string DanhSachDichVuTemplate = "Danh sách dịch vụ";
        public static string DanhSachThuocTemplate = "Danh sách thuốc";
        public static string DanhSachNhanVienTemplate = "Danh sách nhân viên";
        public static string CongTacNgoaiGioTemplate = "Công tác ngoài giờ";
        public static string LichKhamTemplate = "Lịch khám";
        public static string BaoCaoTonKhoCapCuuTemplate = "Báo cáo tồn kho cấp cứu";
        public static string BenhNhanNgoaiGoiKhamTemplate = "Bệnh nhân ngoài gói khám";
        public static string BaoCaoCongNoHopDongTongHopTemplate = "Báo cáo công nợ hợp đồng tổng hợp";
        public static string BaoCaoCongNoHopDongChiTietTemplate = "Báo cáo công nợ hợp đồng chi tiết";
        public static string ChiDinhTemplate = "Chỉ định";
        public static string DichVuXetNghiemTemplate = "Dịch vụ xét nghiệm";
        public static string ThongKeThuocXuatHoaDonTemplate = "Thống kê thuốc xuất hóa đơn";
        public static string DoanhThuTheoNhomDichVuTemplate = "Doanh thu theo nhóm dịch vụ";
        public static string GiaThuocTemplate = "Giá thuốc";
        public static string LoThuocTemplate = "Lô thuốc";
        public static string HoaDonXetNghiemYKhoaTemplate = "Hóa đơn xét nghiệm";
        
        public static string TVHomeProcessName = "TVHome Media2";

        public static int DeplayCount = 10000;
    }
}
