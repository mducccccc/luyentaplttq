-- =============================================
-- SCRIPT TẠO CƠ SỞ DỮ LIỆU VÀ DỮ LIỆU MẪU
-- ĐỀ SỐ 01: QUẢN LÝ NHÂN SỰ (QLNhanSu)
-- =============================================

USE master;
GO

-- 1. TẠO CSDL QLNhanSu NẾU CHƯA CÓ
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'QLNhanSu')
BEGIN
    CREATE DATABASE QLNhanSu;
END
GO

USE QLNhanSu;
GO

-- 2. XÓA BẢNG CŨ NẾU ĐÃ TỒN TẠI (ĐÚNG THỨ TỰ RÀNG BUỘC KHÓA NGOẠI)
IF OBJECT_ID(N'dbo.NhanVien', N'U') IS NOT NULL
    DROP TABLE dbo.NhanVien;
GO

IF OBJECT_ID(N'dbo.PhongBan', N'U') IS NOT NULL
    DROP TABLE dbo.PhongBan;
GO

-- 3. TẠO BẢNG PhongBan
CREATE TABLE PhongBan (
    MaPB NVARCHAR(10) NOT NULL,
    TenPB NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_PhongBan PRIMARY KEY (MaPB)
);
GO

-- 4. TẠO BẢNG NhanVien
CREATE TABLE NhanVien (
    MaNV NVARCHAR(10) NOT NULL,
    HoTen NVARCHAR(50) NOT NULL,
    GioiTinh BIT NOT NULL, -- 1: Nam, 0: Nữ
    NgaySinh DATE NOT NULL,
    MaPB NVARCHAR(10) NOT NULL,
    HeSoLuong FLOAT NOT NULL,
    NgayVaoLam DATE NOT NULL,
    CONSTRAINT PK_NhanVien PRIMARY KEY (MaNV),
    CONSTRAINT FK_NhanVien_PhongBan FOREIGN KEY (MaPB) REFERENCES PhongBan(MaPB)
        ON UPDATE CASCADE
        ON DELETE NO ACTION
);
GO

-- 5. THÊM DỮ LIỆU MẪU BẢNG PhongBan
INSERT INTO PhongBan (MaPB, TenPB) VALUES
(N'KD',  N'Kinh doanh'),
(N'KT',  N'Kế toán'),
(N'KTH', N'Kỹ thuật'),
(N'HC',  N'Hành chính');
GO

-- 6. THÊM DỮ LIỆU MẪU BẢNG NhanVien (CHUẨN THEO ĐỀ THI VÀ HÌNH MINH HỌA)
INSERT INTO NhanVien (MaNV, HoTen, GioiTinh, NgaySinh, MaPB, HeSoLuong, NgayVaoLam) VALUES
(N'NV001', N'Trần Minh Đức', 1, '1990-03-12', N'KD',  3.33, '2015-07-01'),
(N'NV002', N'Lê Thu Hà',     0, '1995-11-25', N'KT',  2.67, '2019-08-15'),
(N'NV003', N'Phạm Quốc Bảo', 1, '1988-06-08', N'KTH', 4.40, '2012-01-02'),
(N'NV004', N'Nguyễn Thị Lan',0, '1997-09-19', N'KD',  2.34, '2021-03-10'),
(N'NV005', N'Đỗ Văn Hùng',   1, '1985-01-30', N'HC',  3.66, '2010-05-20'),
(N'NV006', N'Vũ Thị Hồng',   0, '1992-04-14', N'KD',  3.00, '2017-09-01'),
(N'NV007', N'Hoàng Gia Huy', 1, '1999-12-05', N'KTH', 2.34, '2023-04-04');
GO

-- 7. TRUY VẤN KIỂM TRA DỮ LIỆU
SELECT * FROM PhongBan;
GO

SELECT 
    nv.MaNV,
    nv.HoTen,
    CASE nv.GioiTinh WHEN 1 THEN N'Nam' ELSE N'Nữ' END AS GioiTinh,
    CONVERT(VARCHAR(10), nv.NgaySinh, 103) AS NgaySinh,
    pb.TenPB AS PhongBan,
    nv.HeSoLuong,
    CONVERT(VARCHAR(10), nv.NgayVaoLam, 103) AS NgayVaoLam,
    FORMAT(ROUND(nv.HeSoLuong * 2340000, 0), '#,##0') AS Luong
FROM NhanVien nv
JOIN PhongBan pb ON nv.MaPB = pb.MaPB
ORDER BY nv.MaNV;
GO
