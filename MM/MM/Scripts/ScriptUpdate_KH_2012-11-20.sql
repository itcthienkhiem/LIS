USE MM
GO
UPDATE PatientHistory
SET Tinh_Trang_Gia_Dinh = N'Độc thân'
WHERE Tinh_Trang_Gia_Dinh = N'ĐÔC THÂN' OR
Tinh_Trang_Gia_Dinh = N'Doc than' OR
Tinh_Trang_Gia_Dinh = N'DỘC THÂN' OR
Tinh_Trang_Gia_Dinh = N'DỘC THÂN' OR
Tinh_Trang_Gia_Dinh = N'SINGLE' OR
Tinh_Trang_Gia_Dinh = N'Đ' OR
Tinh_Trang_Gia_Dinh = N' ĐT' OR
Tinh_Trang_Gia_Dinh = N'ĐỘCTHÂN' OR
Tinh_Trang_Gia_Dinh = N'ĐỌCTHÂN' OR
Tinh_Trang_Gia_Dinh = N'ĐTHÂN' OR
Tinh_Trang_Gia_Dinh = N'Độc thân.' OR
Tinh_Trang_Gia_Dinh = N'ĐỘ THÂN' OR
Tinh_Trang_Gia_Dinh = N'Đ T' OR
Tinh_Trang_Gia_Dinh = N'ĐT' OR
Tinh_Trang_Gia_Dinh = N'DT' 
GO
UPDATE PatientHistory
SET Tinh_Trang_Gia_Dinh = N'Có gia đình'
WHERE Tinh_Trang_Gia_Dinh = N' CÓ GĐ' OR
Tinh_Trang_Gia_Dinh = N'CÓ G Đ' OR
Tinh_Trang_Gia_Dinh = N'CÓ' OR
Tinh_Trang_Gia_Dinh = N'CÒ' OR
Tinh_Trang_Gia_Dinh = N'ĐANG CÓ THAI' OR
Tinh_Trang_Gia_Dinh = N'có gia đinh' OR
Tinh_Trang_Gia_Dinh = N'CÓ  GIA ĐÌNH' OR
Tinh_Trang_Gia_Dinh = N'CÓP GĐ' OR
Tinh_Trang_Gia_Dinh = N'ĐÃ CÓ GĐ' OR
Tinh_Trang_Gia_Dinh = N'CÓ GĐ' OR
Tinh_Trang_Gia_Dinh = N'CÒ GĐ' OR
Tinh_Trang_Gia_Dinh = N'CO GD' OR
Tinh_Trang_Gia_Dinh = N'CO GĐ' OR
Tinh_Trang_Gia_Dinh = N'C' OR
Tinh_Trang_Gia_Dinh = N'CÓGĐ' OR
Tinh_Trang_Gia_Dinh = N'Đã kết hôn' OR
Tinh_Trang_Gia_Dinh = N'CÓ GDĐ' OR
Tinh_Trang_Gia_Dinh = N'GĐ' OR
Tinh_Trang_Gia_Dinh = N'Có gia đình.' OR
Tinh_Trang_Gia_Dinh = N'CO 1GĐ' 
GO
UPDATE PatientHistory
SET Tinh_Trang_Gia_Dinh = N'Khác'
WHERE Tinh_Trang_Gia_Dinh IS NULL OR
Tinh_Trang_Gia_Dinh = N'VIÊM MŨI DỊ ỨNG' OR
Tinh_Trang_Gia_Dinh = N'Q.3' OR
Tinh_Trang_Gia_Dinh = N''
