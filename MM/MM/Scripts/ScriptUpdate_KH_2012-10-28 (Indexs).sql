USE MM
GO
CREATE STATISTICS [_dta_stat_1893581784_1_15_4_3] ON [dbo].[Contact]([ContactGUID], [Archived], [FirstName], [FullName])
GO
CREATE NONCLUSTERED INDEX [_dta_index_Contact_10_1893581784__K4_K3_K15_1_2_5_6_7_8_9_10_11_12_13_14_16_17_18_19_20_21_22_23_24_25_26_27_28_29_30_31_32_33_] ON [dbo].[Contact] 
(
	[FirstName] ASC,
	[FullName] ASC,
	[Archived] ASC
)
INCLUDE ( [ContactGUID],
[Title],
[SurName],
[KnownAs],
[MiddleName],
[AliasFirstName],
[AliasSurName],
[Dob],
[DobStr],
[PreferredName],
[Occupation],
[IdentityCard],
[DateArchived],
[Note],
[HomePhone],
[WorkPhone],
[Mobile],
[Email],
[FAX],
[CreatedDate],
[CreatedBy],
[UpdatedDate],
[UpdatedBy],
[DeletedDate],
[DeletedBy],
[Gender],
[Address],
[Ward],
[District],
[City],
[Source],
[CompanyName]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [_dta_index_Patient_10_1957582012__K2_K1_3_4_5_6_7_8_9_10_11_12] ON [dbo].[Patient] 
(
	[ContactGUID] ASC,
	[PatientGUID] ASC
)
INCLUDE ( [FileNum],
[BarCode],
[Picture],
[HearFrom],
[Salutation],
[LastSeenDate],
[LastSeenDocGUID],
[DateDeceased],
[LastVisitGUID],
[NgayKham]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [_dta_index_PatientHistory_10_130099504__K2_1_3_4_5_6_7_8_9_10_11_12_13_14_15_16_17_18_19_20_21_22_23_24_25_26] ON [dbo].[PatientHistory] 
(
	[PatientGUID] ASC
)
INCLUDE ( [PatientHistoryGUID],
[Di_Ung_Thuoc],
[Thuoc_Di_Ung],
[Dot_Quy],
[Benh_Tim_Mach],
[Benh_Lao],
[Dai_Thao_Duong],
[Dai_Duong_Dang_Dieu_Tri],
[Viem_Gan_B],
[Viem_Gan_C],
[Viem_Gan_Dang_Dieu_Tri],
[Ung_Thu],
[Co_Quan_Ung_Thu],
[Dong_Kinh],
[Hen_Suyen],
[Benh_Khac],
[Benh_Gi],
[Thuoc_Dang_Dung],
[Hut_Thuoc],
[Uong_Ruou],
[Tinh_Trang_Gia_Dinh],
[Chich_Ngua_Viem_Gan_B],
[Chich_Ngua_Uon_Van],
[Chich_Ngua_Cum],
[Dang_Co_Thai]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_1893581784_3_1] ON [dbo].[Contact]([FullName], [ContactGUID])
GO
CREATE NONCLUSTERED INDEX [_dta_index_Contact_10_1893581784__K1_3_4_5_11_15_29_30] ON [dbo].[Contact] 
(
	[ContactGUID] ASC
)
INCLUDE ( [FullName],
[FirstName],
[SurName],
[DobStr],
[Archived],
[Gender],
[Address]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_772197801_2_4_3] ON [dbo].[Tracking]([TrackingDate], [ActionType], [DocStaffGUID])
GO
CREATE NONCLUSTERED INDEX [_dta_index_Tracking_10_772197801__K3_K2_K4_1_5_6_7] ON [dbo].[Tracking] 
(
	[DocStaffGUID] ASC,
	[TrackingDate] ASC,
	[ActionType] ASC
)
INCLUDE ( [TrackingGUID],
[Action],
[Description],
[TrackingType]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE CLUSTERED INDEX [_dta_index_Contact_c_10_1893581784__K1] ON [dbo].[Contact] 
(
	[ContactGUID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [_dta_index_Symptom_10_1829581556__K11_1_2_3_4_5_6_7_8_9_10] ON [dbo].[Symptom] 
(
	[Status] ASC
)
INCLUDE ( [SymptomGUID],
[Code],
[SymptomName],
[Advice],
[CreatedDate],
[CreatedBy],
[UpdatedDate],
[UpdatedBy],
[DeletedDate],
[DeletedBy]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [_dta_index_Services_10_1797581442__K12_K3_1_2_4_5_6_7_8_9_10_11_13_14_15] ON [dbo].[Services] 
(
	[Status] ASC,
	[Name] ASC
)
INCLUDE ( [ServiceGUID],
[Code],
[Price],
[Description],
[CreatedDate],
[CreatedBy],
[UpdatedDate],
[UpdatedBy],
[DeletedDate],
[DeletedBy],
[EnglishName],
[Type],
[StaffType]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_1286295642_4_2_12] ON [dbo].[GiaVonDichVu]([NgayApDung], [ServiceGUID], [Status])
GO
CREATE STATISTICS [_dta_stat_1286295642_2_12] ON [dbo].[GiaVonDichVu]([ServiceGUID], [Status])
GO
CREATE NONCLUSTERED INDEX [_dta_index_GiaVonDichVu_10_1286295642__K12_K4_K2_1_3_5_6_7_8_9_10_11] ON [dbo].[GiaVonDichVu] 
(
	[Status] ASC,
	[NgayApDung] ASC,
	[ServiceGUID] ASC
)
INCLUDE ( [GiaVonDichVuGUID],
[GiaVon],
[Note],
[CreatedDate],
[CreatedBy],
[UpdatedDate],
[UpdatedBy],
[DeletedDate],
[DeletedBy]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [_dta_index_Services_10_1797581442__K12_K1_K3_2_4_13_14] ON [dbo].[Services] 
(
	[Status] ASC,
	[ServiceGUID] ASC,
	[Name] ASC
)
INCLUDE ( [Code],
[Price],
[EnglishName],
[Type]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [_dta_index_GiaVonDichVu_10_1286295642__K12_K2_K4_1_3_5_6_7_8_9_10_11] ON [dbo].[GiaVonDichVu] 
(
	[Status] ASC,
	[ServiceGUID] ASC,
	[NgayApDung] ASC
)
INCLUDE ( [GiaVonDichVuGUID],
[GiaVon],
[Note],
[CreatedDate],
[CreatedBy],
[UpdatedDate],
[UpdatedBy],
[DeletedDate],
[DeletedBy]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [_dta_index_Contact_10_1893581784__K1_3_30_35] ON [dbo].[Contact] 
(
	[ContactGUID] ASC
)
INCLUDE ( [FullName],
[Address],
[CompanyName]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [_dta_index_Patient_10_1957582012__K1_K2_3] ON [dbo].[Patient] 
(
	[PatientGUID] ASC,
	[ContactGUID] ASC
)
INCLUDE ( [FileNum]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_2130106629_4_11_6] ON [dbo].[Receipt]([ReceiptDate], [Status], [CreatedBy])
GO
CREATE STATISTICS [_dta_stat_2130106629_6_4] ON [dbo].[Receipt]([CreatedBy], [ReceiptDate])
GO
CREATE STATISTICS [_dta_stat_2130106629_2_6_11_4] ON [dbo].[Receipt]([PatientGUID], [CreatedBy], [Status], [ReceiptDate])
GO
CREATE NONCLUSTERED INDEX [_dta_index_Receipt_10_2130106629__K11_K4_K2_K6_1_3_5_7_8_9_10_12_13_14_15] ON [dbo].[Receipt] 
(
	[Status] ASC,
	[ReceiptDate] ASC,
	[PatientGUID] ASC,
	[CreatedBy] ASC
)
INCLUDE ( [ReceiptGUID],
[ReceiptCode],
[CreatedDate],
[UpdatedDate],
[UpdatedBy],
[DeletedDate],
[DeletedBy],
[IsExportedInVoice],
[Notes],
[ChuaThuTien],
[LyDoGiam]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_2130106629_2_4_11] ON [dbo].[Receipt]([PatientGUID], [ReceiptDate], [Status])
GO
CREATE STATISTICS [_dta_stat_130099504_24_2] ON [dbo].[PatientHistory]([Chich_Ngua_Uon_Van], [PatientGUID])
GO
CREATE STATISTICS [_dta_stat_2130106629_2_11] ON [dbo].[Receipt]([PatientGUID], [Status])
GO
CREATE NONCLUSTERED INDEX [_dta_index_Receipt_10_2130106629__K11_K2_K6_K4_1_3_5_7_8_9_10_12_13_14_15] ON [dbo].[Receipt] 
(
	[Status] ASC,
	[PatientGUID] ASC,
	[CreatedBy] ASC,
	[ReceiptDate] ASC
)
INCLUDE ( [ReceiptGUID],
[ReceiptCode],
[CreatedDate],
[UpdatedDate],
[UpdatedBy],
[DeletedDate],
[DeletedBy],
[IsExportedInVoice],
[Notes],
[ChuaThuTien],
[LyDoGiam]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_958626458_8_14] ON [dbo].[PhieuThuThuoc]([CreatedBy], [Status])
GO
CREATE STATISTICS [_dta_stat_958626458_8_4] ON [dbo].[PhieuThuThuoc]([CreatedBy], [NgayThu])
GO
CREATE NONCLUSTERED INDEX [_dta_index_PhieuThuThuoc_10_958626458__K14_K4_K8_1_2_3_5_6_7_9_10_11_12_13_15_16_17_18_19] ON [dbo].[PhieuThuThuoc] 
(
	[Status] ASC,
	[NgayThu] ASC,
	[CreatedBy] ASC
)
INCLUDE ( [PhieuThuThuocGUID],
[ToaThuocGUID],
[MaPhieuThuThuoc],
[MaBenhNhan],
[TenBenhNhan],
[DiaChi],
[CreatedDate],
[UpdatedBy],
[UpdatedDate],
[DeletedBy],
[DeletedDate],
[TenCongTy],
[IsExported],
[Notes],
[ChuaThuTien],
[LyDoGiam]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_958626458_4_14_8] ON [dbo].[PhieuThuThuoc]([NgayThu], [Status], [CreatedBy])
GO
CREATE STATISTICS [_dta_stat_1893581784_3_15] ON [dbo].[Contact]([FullName], [Archived])
GO
CREATE STATISTICS [_dta_stat_1893581784_15_1_3] ON [dbo].[Contact]([Archived], [ContactGUID], [FullName])
GO
CREATE NONCLUSTERED INDEX [_dta_index_Contact_10_1893581784__K1_K15_K3_11_29_30] ON [dbo].[Contact] 
(
	[ContactGUID] ASC,
	[Archived] ASC,
	[FullName] ASC
)
INCLUDE ( [DobStr],
[Gender],
[Address]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_2130106629_1_2_11] ON [dbo].[Receipt]([ReceiptGUID], [PatientGUID], [Status])
GO
CREATE STATISTICS [_dta_stat_2130106629_4_11_1_2] ON [dbo].[Receipt]([ReceiptDate], [Status], [ReceiptGUID], [PatientGUID])
GO
CREATE NONCLUSTERED INDEX [_dta_index_Receipt_10_2130106629__K11_K4_K2_K1_3_12_13_14] ON [dbo].[Receipt] 
(
	[Status] ASC,
	[ReceiptDate] ASC,
	[PatientGUID] ASC,
	[ReceiptGUID] ASC
)
INCLUDE ( [ReceiptCode],
[IsExportedInVoice],
[Notes],
[ChuaThuTien]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_2130106629_1_11] ON [dbo].[Receipt]([ReceiptGUID], [Status])
GO
CREATE STATISTICS [_dta_stat_46623209_10_3] ON [dbo].[ReceiptDetail]([Status], [ServiceHistoryGUID])
GO
CREATE STATISTICS [_dta_stat_46623209_2_3] ON [dbo].[ReceiptDetail]([ReceiptGUID], [ServiceHistoryGUID])
GO
CREATE NONCLUSTERED INDEX [_dta_index_ReceiptDetail_10_46623209__K2_K10_K3_1_4_5_6_7_8_9] ON [dbo].[ReceiptDetail] 
(
	[ReceiptGUID] ASC,
	[Status] ASC,
	[ServiceHistoryGUID] ASC
)
INCLUDE ( [ReceiptDetailGUID],
[CreatedDate],
[CreatedBy],
[UpdatedDate],
[UpdatedBy],
[DeletedDate],
[DeletedBy]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_46623209_10_2_3] ON [dbo].[ReceiptDetail]([Status], [ReceiptGUID], [ServiceHistoryGUID])
GO
CREATE NONCLUSTERED INDEX [_dta_index_ServiceHistory_10_2117582582__K1_K3_6_7_8_15_22] ON [dbo].[ServiceHistory] 
(
	[ServiceHistoryGUID] ASC,
	[ServiceGUID] ASC
)
INCLUDE ( [Price],
[Discount],
[Note],
[Status],
[GiaVon]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [_dta_index_Services_10_1797581442__K12_1_2_3] ON [dbo].[Services] 
(
	[Status] ASC
)
INCLUDE ( [ServiceGUID],
[Code],
[Name]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_110623437_4_15] ON [dbo].[Invoice]([InvoiceDate], [Status])
GO
CREATE NONCLUSTERED INDEX [_dta_index_Invoice_10_110623437__K15_K4_1_3_5_6_7_8_9_10_11_12_13_14_16_17_18_19_20_21_22_23] ON [dbo].[Invoice] 
(
	[Status] ASC,
	[InvoiceDate] ASC
)
INCLUDE ( [InvoiceGUID],
[InvoiceCode],
[TenDonVi],
[SoTaiKhoan],
[HinhThucThanhToan],
[VAT],
[CreatedDate],
[CreatedBy],
[UpdatedDate],
[UpdatedBy],
[DeletedDate],
[DeletedBy],
[MaSoThue],
[TenNguoiMuaHang],
[DiaChi],
[ReceiptGUIDList],
[Notes],
[ChuaThuTien],
[MauSo],
[KiHieu]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_110623437_17_15] ON [dbo].[Invoice]([TenNguoiMuaHang], [Status])
GO
CREATE NONCLUSTERED INDEX [_dta_index_Invoice_10_110623437__K15_K17] ON [dbo].[Invoice] 
(
	[Status] ASC,
	[TenNguoiMuaHang] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [_dta_index_HoaDonThuoc_10_448160792__K18_K4_1_3_5_6_7_8_9_10_11_12_13_14_15_16_17_19_20_21_22_23] ON [dbo].[HoaDonThuoc] 
(
	[Status] ASC,
	[NgayXuatHoaDon] ASC
)
INCLUDE ( [HoaDonThuocGUID],
[SoHoaDon],
[TenNguoiMuaHang],
[DiaChi],
[TenDonVi],
[MaSoThue],
[SoTaiKhoan],
[HinhThucThanhToan],
[VAT],
[CreatedDate],
[CreatedBy],
[UpdatedDate],
[UpdatedBy],
[DeletedDate],
[DeletedBy],
[PhieuThuThuocGUIDList],
[Notes],
[ChuaThuTien],
[MauSo],
[KiHieu]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_1310783877_4_5] ON [dbo].[QuanLySoHoaDon]([XuatTruoc], [NgayBatDau])
GO
CREATE NONCLUSTERED INDEX [_dta_index_QuanLySoHoaDon_10_1310783877__K2_K5_K4_1_3] ON [dbo].[QuanLySoHoaDon] 
(
	[SoHoaDon] ASC,
	[NgayBatDau] ASC,
	[XuatTruoc] ASC
)
INCLUDE ( [QuanLySoHoaDonGUID],
[DaXuat]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [_dta_index_Thuoc_10_318624178__K15_K3_1_2_4_5_6_7_8_9_10_11_12_13_14] ON [dbo].[Thuoc] 
(
	[Status] ASC,
	[TenThuoc] ASC
)
INCLUDE ( [ThuocGUID],
[MaThuoc],
[BietDuoc],
[HamLuong],
[HoatChat],
[DonViTinh],
[Note],
[CreatedDate],
[CreatedBy],
[UpdatedDate],
[UpdatedBy],
[DeletedDate],
[DeletedBy]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_494624805_18_24] ON [dbo].[LoThuoc]([CreatedDate], [Status])
GO
CREATE NONCLUSTERED INDEX [_dta_index_LoThuoc_10_494624805__K24_K2_K18_1_3_4_5_6_7_8_9_10_11_12_13_14_15_16_17_19_20_21_22_23] ON [dbo].[LoThuoc] 
(
	[Status] ASC,
	[ThuocGUID] ASC,
	[CreatedDate] ASC
)
INCLUDE ( [LoThuocGUID],
[MaLoThuoc],
[TenLoThuoc],
[SoDangKy],
[NhaPhanPhoi],
[HangSanXuat],
[NgaySanXuat],
[NgayHetHan],
[SoLuongNhap],
[GiaNhap],
[DonViTinhNhap],
[SoLuongQuiDoi],
[DonViTinhQuiDoi],
[GiaNhapQuiDoi],
[SoLuongXuat],
[Note],
[CreatedBy],
[UpdatedDate],
[UpdatedBy],
[DeletedDate],
[DeletedBy]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_318624178_1_15] ON [dbo].[Thuoc]([ThuocGUID], [Status])
GO
CREATE STATISTICS [_dta_stat_606625204_4_12] ON [dbo].[GiaThuoc]([NgayApDung], [Status])
GO
CREATE STATISTICS [_dta_stat_606625204_2_12_4] ON [dbo].[GiaThuoc]([ThuocGUID], [Status], [NgayApDung])
GO
CREATE NONCLUSTERED INDEX [_dta_index_GiaThuoc_10_606625204__K12_K2_K4_1_3_5_6_7_8_9_10_11] ON [dbo].[GiaThuoc] 
(
	[Status] ASC,
	[ThuocGUID] ASC,
	[NgayApDung] ASC
)
INCLUDE ( [GiaThuocGUID],
[GiaBan],
[Note],
[CreatedDate],
[CreatedBy],
[UpdatedDate],
[UpdatedBy],
[DeletedDate],
[DeletedBy]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_1614941125_10_2_3_13] ON [dbo].[NhatKyLienHeCongTy]([UpdatedBy], [DocStaffGUID], [NgayGioLienHe], [Status])
GO
CREATE STATISTICS [_dta_stat_1614941125_3_13_10] ON [dbo].[NhatKyLienHeCongTy]([NgayGioLienHe], [Status], [UpdatedBy])
GO
CREATE NONCLUSTERED INDEX [_dta_index_NhatKyLienHeCongTy_10_1614941125__K13_K3_K10_K2_1_4_5_6_7_8_9_11_12_14_15_16_17_18_19_20_21] ON [dbo].[NhatKyLienHeCongTy] 
(
	[Status] ASC,
	[NgayGioLienHe] ASC,
	[UpdatedBy] ASC,
	[DocStaffGUID] ASC
)
INCLUDE ( [NhatKyLienHeCongTyGUID],
[CongTyLienHe],
[NoiDungLienHe],
[Note],
[CreatedDate],
[CreatedBy],
[UpdatedDate],
[DeletedDate],
[DeletedBy],
[TenNguoiLienHe],
[SoDienThoaiLienHe],
[SoNguoiKham],
[ThangKham],
[DiaChi],
[Email],
[Highlight],
[SoNgay]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_1614941125_10_2_13] ON [dbo].[NhatKyLienHeCongTy]([UpdatedBy], [DocStaffGUID], [Status])
GO
CREATE STATISTICS [_dta_stat_1614941125_13_4_10_2] ON [dbo].[NhatKyLienHeCongTy]([Status], [CongTyLienHe], [UpdatedBy], [DocStaffGUID])
GO
CREATE NONCLUSTERED INDEX [_dta_index_NhatKyLienHeCongTy_10_1614941125__K13_K4] ON [dbo].[NhatKyLienHeCongTy] 
(
	[Status] ASC,
	[CongTyLienHe] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_1614941125_4_13_2] ON [dbo].[NhatKyLienHeCongTy]([CongTyLienHe], [Status], [DocStaffGUID])
GO
CREATE STATISTICS [_dta_stat_1614941125_4_2] ON [dbo].[NhatKyLienHeCongTy]([CongTyLienHe], [DocStaffGUID])
GO
CREATE NONCLUSTERED INDEX [_dta_index_NhatKyLienHeCongTy_10_1614941125__K13_K4_K2_K10_1_3_5_6_7_8_9_11_12_14_15_16_17_18_19_20_21] ON [dbo].[NhatKyLienHeCongTy] 
(
	[Status] ASC,
	[CongTyLienHe] ASC,
	[DocStaffGUID] ASC,
	[UpdatedBy] ASC
)
INCLUDE ( [NhatKyLienHeCongTyGUID],
[NgayGioLienHe],
[NoiDungLienHe],
[Note],
[CreatedDate],
[CreatedBy],
[UpdatedDate],
[DeletedDate],
[DeletedBy],
[TenNguoiLienHe],
[SoDienThoaiLienHe],
[SoNguoiKham],
[ThangKham],
[DiaChi],
[Email],
[Highlight],
[SoNgay]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_1614941125_13_2] ON [dbo].[NhatKyLienHeCongTy]([Status], [DocStaffGUID])
GO
CREATE STATISTICS [_dta_stat_2117582582_3_12_10_4_23_2_15] ON [dbo].[ServiceHistory]([ServiceGUID], [UpdatedBy], [CreatedBy], [DocStaffGUID], [RootPatientGUID], [PatientGUID], [Status])
GO
CREATE STATISTICS [_dta_stat_2117582582_3_5_15] ON [dbo].[ServiceHistory]([ServiceGUID], [ActivedDate], [Status])
GO
CREATE STATISTICS [_dta_stat_2117582582_5_15_2_12_10_4_3_23] ON [dbo].[ServiceHistory]([ActivedDate], [Status], [PatientGUID], [UpdatedBy], [CreatedBy], [DocStaffGUID], [ServiceGUID], [RootPatientGUID])
GO
CREATE STATISTICS [_dta_stat_2117582582_23_5_15_2_3_12_10] ON [dbo].[ServiceHistory]([RootPatientGUID], [ActivedDate], [Status], [PatientGUID], [ServiceGUID], [UpdatedBy], [CreatedBy])
GO
CREATE NONCLUSTERED INDEX [_dta_index_ServiceHistory_10_2117582582__K15_K2_K5_K3_K12_K10_K4_K23_1_6_7_8_9_11_13_14_16_17_18_19_20_21_22_24] ON [dbo].[ServiceHistory] 
(
	[Status] ASC,
	[PatientGUID] ASC,
	[ActivedDate] ASC,
	[ServiceGUID] ASC,
	[UpdatedBy] ASC,
	[CreatedBy] ASC,
	[DocStaffGUID] ASC,
	[RootPatientGUID] ASC
)
INCLUDE ( [ServiceHistoryGUID],
[Price],
[Discount],
[Note],
[CreatedDate],
[UpdatedDate],
[DeletedDate],
[DeletedBy],
[IsExported],
[IsNormalOrNegative],
[Normal],
[Abnormal],
[Negative],
[Positive],
[GiaVon],
[KhamTuTuc]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_2117582582_12_10_4_5_15] ON [dbo].[ServiceHistory]([UpdatedBy], [CreatedBy], [DocStaffGUID], [ActivedDate], [Status])
GO
CREATE STATISTICS [_dta_stat_2117582582_2_15_5_3_12_10] ON [dbo].[ServiceHistory]([PatientGUID], [Status], [ActivedDate], [ServiceGUID], [UpdatedBy], [CreatedBy])
GO
CREATE STATISTICS [_dta_stat_811149935_22_3_17] ON [dbo].[CanDo]([DocStaffGUID], [NgayCanDo], [Status])
GO
CREATE STATISTICS [_dta_stat_811149935_3_17_2] ON [dbo].[CanDo]([NgayCanDo], [Status], [PatientGUID])
GO
CREATE STATISTICS [_dta_stat_811149935_22_17_2] ON [dbo].[CanDo]([DocStaffGUID], [Status], [PatientGUID])
GO
CREATE NONCLUSTERED INDEX [_dta_index_CanDo_10_811149935__K17_K2_K3_K22_1_4_5_6_7_8_9_10_11_12_13_14_15_16_18_19_20_21] ON [dbo].[CanDo] 
(
	[Status] ASC,
	[PatientGUID] ASC,
	[NgayCanDo] ASC,
	[DocStaffGUID] ASC
)
INCLUDE ( [CanDoGuid],
[TimMach],
[HuyetAp],
[HoHap],
[ChieuCao],
[CanNang],
[BMI],
[CanDoKhac],
[CreatedDate],
[CreatedBy],
[UpdatedDate],
[UpdatedBy],
[DeletedDate],
[DeletedBy],
[MuMau],
[MatTrai],
[MatPhai],
[HieuChinh]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_811149935_2_17_3_22] ON [dbo].[CanDo]([PatientGUID], [Status], [NgayCanDo], [DocStaffGUID])
GO
CREATE NONCLUSTERED INDEX [_dta_index_Contact_10_1893581784__K15_3_4_5_11_18_19_20_21_29_30] ON [dbo].[Contact] 
(
	[Archived] ASC
)
INCLUDE ( [FullName],
[FirstName],
[SurName],
[DobStr],
[HomePhone],
[WorkPhone],
[Mobile],
[Email],
[Gender],
[Address]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_1085246921_4_2_15] ON [dbo].[KetQuaLamSang]([DocStaffGUID], [NgayKham], [Status])
GO
CREATE STATISTICS [_dta_stat_1085246921_4_3_15_2] ON [dbo].[KetQuaLamSang]([DocStaffGUID], [PatientGUID], [Status], [NgayKham])
GO
CREATE NONCLUSTERED INDEX [_dta_index_KetQuaLamSang_10_1085246921__K15_K3_K2_K4_1_5_6_7_8_9_10_11_12_13_14_16_17_18_19] ON [dbo].[KetQuaLamSang] 
(
	[Status] ASC,
	[PatientGUID] ASC,
	[NgayKham] ASC,
	[DocStaffGUID] ASC
)
INCLUDE ( [KetQuaLamSangGUID],
[CoQuan],
[Normal],
[Abnormal],
[Note],
[CreatedDate],
[CreatedBy],
[UpdatedDate],
[UpdatedBy],
[DeletedDate],
[DeletedBy],
[PARA],
[SoiTuoiHuyetTrang],
[NgayKinhChot],
[PhuKhoaNote]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_1085246921_2_15_3] ON [dbo].[KetQuaLamSang]([NgayKham], [Status], [PatientGUID])
GO
CREATE STATISTICS [_dta_stat_973246522_4_2_13] ON [dbo].[LoiKhuyen]([DocStaffGUID], [Ngay], [Status])
GO
CREATE STATISTICS [_dta_stat_973246522_5_2_13_3] ON [dbo].[LoiKhuyen]([SymptomGUID], [Ngay], [Status], [PatientGUID])
GO
CREATE STATISTICS [_dta_stat_973246522_5_4_3_13_2] ON [dbo].[LoiKhuyen]([SymptomGUID], [DocStaffGUID], [PatientGUID], [Status], [Ngay])
GO
CREATE NONCLUSTERED INDEX [_dta_index_LoiKhuyen_10_973246522__K3_K13_K2_K5_K4_1_6_7_8_9_10_11_12] ON [dbo].[LoiKhuyen] 
(
	[PatientGUID] ASC,
	[Status] ASC,
	[Ngay] ASC,
	[SymptomGUID] ASC,
	[DocStaffGUID] ASC
)
INCLUDE ( [LoiKhuyenGUID],
[Note],
[CreatedDate],
[CreatedBy],
[UpdatedDate],
[UpdatedBy],
[DeletedDate],
[DeletedBy]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_973246522_13_3] ON [dbo].[LoiKhuyen]([Status], [PatientGUID])
GO
CREATE STATISTICS [_dta_stat_973246522_2_13_3_4] ON [dbo].[LoiKhuyen]([Ngay], [Status], [PatientGUID], [DocStaffGUID])
GO
CREATE STATISTICS [_dta_stat_1366295927_4_2_17] ON [dbo].[KetLuan]([DocStaffGUID], [NgayKetLuan], [Status])
GO
CREATE STATISTICS [_dta_stat_1366295927_4_3_17] ON [dbo].[KetLuan]([DocStaffGUID], [PatientGUID], [Status])
GO
CREATE NONCLUSTERED INDEX [_dta_index_KetLuan_10_1366295927__K3_K17_K2_K4_1_5_6_7_8_9_10_11_12_13_14_15_16] ON [dbo].[KetLuan] 
(
	[PatientGUID] ASC,
	[Status] ASC,
	[NgayKetLuan] ASC,
	[DocStaffGUID] ASC
)
INCLUDE ( [KetLuanGUID],
[HasLamThemXetNghiem],
[CacXetNghiemLamThem],
[HasLamDuCanLamSang],
[LyDo_CanLamSang],
[HasDuSucKhoe],
[LyDo_SucKhoe],
[CreatedDate],
[CreatedBy],
[UpdatedDate],
[UpdatedBy],
[DeletedDate],
[DeletedBy]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_1366295927_2_17_3] ON [dbo].[KetLuan]([NgayKetLuan], [Status], [PatientGUID])
GO
CREATE STATISTICS [_dta_stat_1961162132_6_7_3_64] ON [dbo].[KetQuaNoiSoi]([BacSiChiDinh], [BacSiSoi], [NgayKham], [Status])
GO
CREATE STATISTICS [_dta_stat_1961162132_6_7_4_64] ON [dbo].[KetQuaNoiSoi]([BacSiChiDinh], [BacSiSoi], [PatientGUID], [Status])
GO
CREATE NONCLUSTERED INDEX [_dta_index_KetQuaNoiSoi_10_1961162132__K4_K64_K3_K6_K7_1_2_5_8_9_10_11_12_13_14_15_16_17_18_19_20_21_22_23_24_25_26_27_28_29_] ON [dbo].[KetQuaNoiSoi] 
(
	[PatientGUID] ASC,
	[Status] ASC,
	[NgayKham] ASC,
	[BacSiChiDinh] ASC,
	[BacSiSoi] ASC
)
INCLUDE ( [KetQuaNoiSoiGUID],
[SoPhieu],
[LyDoKham],
[KetLuan],
[DeNghi],
[LoaiNoiSoi],
[OngTaiTrai],
[OngTaiPhai],
[MangNhiTrai],
[MangNhiPhai],
[CanBuaTrai],
[CanBuaPhai],
[HomNhiTrai],
[HomNhiPhai],
[ValsavaTrai],
[ValsavaPhai],
[NiemMacTrai],
[NiemMacPhai],
[VachNganTrai],
[VachNganPhai],
[KheTrenTrai],
[KheTrenPhai],
[KheGiuaTrai],
[KheGiuaPhai],
[CuonGiuaTrai],
[CuonGiuaPhai],
[CuonDuoiTrai],
[CuonDuoiPhai],
[MomMocTrai],
[MomMocPhai],
[BongSangTrai],
[BongSangPhai],
[VomTrai],
[VomPhai],
[Amydale],
[XoangLe],
[MiengThucQuan],
[SunPheu],
[DayThanh],
[BangThanhThat],
[OngTaiNgoai],
[MangNhi],
[NiemMac],
[VachNgan],
[KheTren],
[KheGiua],
[MomMoc_BongSang],
[Vom],
[ThanhQuan],
[CreatedDate],
[CreatedBy],
[UpdatedDate],
[UpdatedBy],
[DeletedDate],
[DeletedBy]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_1961162132_3_64] ON [dbo].[KetQuaNoiSoi]([NgayKham], [Status])
GO
CREATE STATISTICS [_dta_stat_649365678_3_4_23] ON [dbo].[KetQuaSoiCTC]([BacSiSoi], [NgayKham], [Status])
GO
CREATE STATISTICS [_dta_stat_649365678_4_23] ON [dbo].[KetQuaSoiCTC]([NgayKham], [Status])
GO
CREATE NONCLUSTERED INDEX [_dta_index_KetQuaSoiCTC_10_649365678__K2_K23_K4_K3_1_5_6_9_10_11_12_13_14_15_16_17_18_19_20_21_22] ON [dbo].[KetQuaSoiCTC] 
(
	[PatientGUID] ASC,
	[Status] ASC,
	[NgayKham] ASC,
	[BacSiSoi] ASC
)
INCLUDE ( [KetQuaSoiCTCGUID],
[KetLuan],
[DeNghi],
[AmHo],
[AmDao],
[CTC],
[BieuMoLat],
[MoDem],
[RanhGioiLatTru],
[SauAcidAcetic],
[SauLugol],
[CreatedDate],
[CreatedBy],
[UpdatedDate],
[UpdatedBy],
[DeletedDate],
[DeletedBy]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_649365678_3_2_23] ON [dbo].[KetQuaSoiCTC]([BacSiSoi], [PatientGUID], [Status])
GO
CREATE STATISTICS [_dta_stat_1544105733_5_3_4_2_17] ON [dbo].[KetQuaSieuAm]([LoaiSieuAmGUID], [BacSiSieuAmGUID], [BacSiChiDinhGUID], [PatientGUID], [Status])
GO
CREATE STATISTICS [_dta_stat_1544105733_5_6_17_2_3] ON [dbo].[KetQuaSieuAm]([LoaiSieuAmGUID], [NgaySieuAm], [Status], [PatientGUID], [BacSiSieuAmGUID])
GO
CREATE STATISTICS [_dta_stat_1544105733_6_17_2_3_4_5] ON [dbo].[KetQuaSieuAm]([NgaySieuAm], [Status], [PatientGUID], [BacSiSieuAmGUID], [BacSiChiDinhGUID], [LoaiSieuAmGUID])
GO
CREATE NONCLUSTERED INDEX [_dta_index_KetQuaSieuAm_10_1544105733__K2_K17_K6_K5_K3_K4_1_7_11_12_13_14_15_16] ON [dbo].[KetQuaSieuAm] 
(
	[PatientGUID] ASC,
	[Status] ASC,
	[NgaySieuAm] ASC,
	[LoaiSieuAmGUID] ASC,
	[BacSiSieuAmGUID] ASC,
	[BacSiChiDinhGUID] ASC
)
INCLUDE ( [KetQuaSieuAmGUID],
[LamSang],
[CreatedDate],
[CreatedBy],
[UpdatedDate],
[UpdatedBy],
[DeletedDate],
[DeletedBy]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_1544105733_17_2] ON [dbo].[KetQuaSieuAm]([Status], [PatientGUID])
GO
CREATE STATISTICS [_dta_stat_1544105733_3_4_6_17] ON [dbo].[KetQuaSieuAm]([BacSiSieuAmGUID], [BacSiChiDinhGUID], [NgaySieuAm], [Status])
GO
CREATE STATISTICS [_dta_stat_1704705471_10_2] ON [dbo].[Booking]([CreatedBy], [BookingDate])
GO
CREATE STATISTICS [_dta_stat_1704705471_2_15_10] ON [dbo].[Booking]([BookingDate], [Status], [CreatedBy])
GO
CREATE NONCLUSTERED INDEX [_dta_index_Booking_10_1704705471__K15_K2_K10_1_3_4_5_6_7_8_9_11_12_13_14_16] ON [dbo].[Booking] 
(
	[Status] ASC,
	[BookingDate] ASC,
	[CreatedBy] ASC
)
INCLUDE ( [BookingGUID],
[Company],
[MorningCount],
[AfternoonCount],
[EveningCount],
[Pax],
[BookingType],
[CreatedDate],
[UpdatedDate],
[UpdatedBy],
[DeletedDate],
[DeletedBy],
[InOut]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_1704705471_10_15] ON [dbo].[Booking]([CreatedBy], [Status])
GO
CREATE STATISTICS [_dta_stat_21575115_3_1] ON [dbo].[Contact]([FullName], [ContactGUID])
GO
CREATE NONCLUSTERED INDEX [_dta_index_Contact_5_21575115__K1_3_11_29] ON [dbo].[Contact] 
(
	[ContactGUID] ASC
)
INCLUDE ( [FullName],
[DobStr],
[Gender]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_1051918869_4_18_12_6_8] ON [dbo].[ThongBao]([NgayDuyet1], [Status], [CreatedDate], [NgayDuyet2], [NgayDuyet3])
GO
CREATE STATISTICS [_dta_stat_1051918869_6_18_12] ON [dbo].[ThongBao]([NgayDuyet2], [Status], [CreatedDate])
GO
CREATE STATISTICS [_dta_stat_1051918869_8_18_12_4] ON [dbo].[ThongBao]([NgayDuyet3], [Status], [CreatedDate], [NgayDuyet1])
GO
CREATE STATISTICS [_dta_stat_1051918869_13_18_12_4_6] ON [dbo].[ThongBao]([CreatedBy], [Status], [CreatedDate], [NgayDuyet1], [NgayDuyet2])
GO
CREATE NONCLUSTERED INDEX [_dta_index_ThongBao_5_1051918869__K18_K12_K4_K6_K8_K13_1_2_10_11_14_15_16_17] ON [dbo].[ThongBao] 
(
	[Status] ASC,
	[CreatedDate] ASC,
	[NgayDuyet1] ASC,
	[NgayDuyet2] ASC,
	[NgayDuyet3] ASC,
	[CreatedBy] ASC
)
INCLUDE ( [ThongBaoGUID],
[TenThongBao],
[Path],
[GhiChu],
[UpdatedDate],
[UpdatedBy],
[DeletedDate],
[DeletedBy]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_1051918869_12_4_6_8_13_18] ON [dbo].[ThongBao]([CreatedDate], [NgayDuyet1], [NgayDuyet2], [NgayDuyet3], [CreatedBy], [Status])
GO
CREATE STATISTICS [_dta_stat_1051918869_13_12_4_6] ON [dbo].[ThongBao]([CreatedBy], [CreatedDate], [NgayDuyet1], [NgayDuyet2])
GO
CREATE STATISTICS [_dta_stat_21575115_3_15] ON [dbo].[Contact]([FullName], [Archived])
GO
CREATE STATISTICS [_dta_stat_21575115_15_1_3] ON [dbo].[Contact]([Archived], [ContactGUID], [FullName])
GO
CREATE NONCLUSTERED INDEX [_dta_index_Contact_5_21575115__K1_K15_K3_11_14_18_19_20_21_29_30] ON [dbo].[Contact] 
(
	[ContactGUID] ASC,
	[Archived] ASC,
	[FullName] ASC
)
INCLUDE ( [DobStr],
[IdentityCard],
[HomePhone],
[WorkPhone],
[Mobile],
[Email],
[Gender],
[Address]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE STATISTICS [_dta_stat_213575799_1_2] ON [dbo].[Patient]([PatientGUID], [ContactGUID])
GO















