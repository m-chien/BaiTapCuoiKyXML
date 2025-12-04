IF EXISTS (SELECT * FROM sys.databases WHERE name = 'FootballShop')
BEGIN
    USE master; -- Chuyển sang cơ sở dữ liệu master để có thể xóa được cơ sở dữ liệu khác
    ALTER DATABASE FootballShop SET SINGLE_USER WITH ROLLBACK IMMEDIATE; -- Ngắt mọi kết nối
    DROP DATABASE FootballShop; -- Xóa cơ sở dữ liệu
END
go
create database FootballShop
go
use FootballShop
GO

-- Tạo bảng NguoiDung
CREATE TABLE NguoiDung (
    IDNguoiDung INT PRIMARY KEY IDENTITY(1,1),
    Email VARCHAR(100) NOT NULL UNIQUE,
    password VARCHAR(50) NOT NULL,
    sdt VARCHAR(10),
    DiaChi NVARCHAR(255),
    AvatarURL VARCHAR(255),
    VaiTro VARCHAR(100),
    gioitinh NVARCHAR(3)
);

-- Tạo bảng DanhMuc
CREATE TABLE DanhMuc (
    IDDanhMuc INT PRIMARY KEY IDENTITY(1,1),
    TenDanhmuc NVARCHAR(255) NOT NULL,
    MoTa NVARCHAR(255),
    NgayTao DATE
);

-- Tạo bảng SanPham
CREATE TABLE SanPham (
    IDSanPham INT PRIMARY KEY IDENTITY(1,1),
    IDdanhMuc INT,
    Hang NVARCHAR(100),
    KichThuoc VARCHAR(5),
    mausac NVARCHAR(100),
    mota NVARCHAR(255),
    hinhanh VARCHAR(255),
    SoLuongTonKho INT,
    DonViTinh NVARCHAR(255),
    FOREIGN KEY (IDdanhMuc) REFERENCES DanhMuc(IDDanhMuc)
				    ON UPDATE CASCADE
			ON DELETE CASCADE,
);

-- Tạo bảng HoaDon
CREATE TABLE HoaDon (
    IDHoaDon INT PRIMARY KEY IDENTITY(1,1),
    IdUser INT,
    TongTien DECIMAL(18,2),
    DiaChiGiaoHang NVARCHAR(255),
    NgayDat DATE,
    TrangThai NVARCHAR(100),
    FOREIGN KEY (IdUser) REFERENCES NguoiDung(IDNguoiDung)
				    ON UPDATE CASCADE
			ON DELETE CASCADE,
);

-- Tạo bảng ChiTietHoaDon
CREATE TABLE ChiTietHoaDon (
    IDChiTietHoaDon INT PRIMARY KEY IDENTITY(1,1),
    IdHoaDon INT,
    IdSanPham INT,
    SoLuong INT,
    DonGia DECIMAL(18,2),
    FOREIGN KEY (IdHoaDon) REFERENCES HoaDon(IDHoaDon)
		    ON UPDATE CASCADE
			ON DELETE CASCADE,
    FOREIGN KEY (IdSanPham) REFERENCES SanPham(IDSanPham)
			    ON UPDATE CASCADE
			ON DELETE CASCADE,
);

-- Tạo bảng BinhLuan
CREATE TABLE BinhLuan (
    IDBinhLuan INT PRIMARY KEY IDENTITY(1,1),
    IDNguoiDung INT,
    IdSanPham INT,
    NoiDung NVARCHAR(255),
    NgayBinhLuan DATE,
    TinhTrang NVARCHAR(100),
    FOREIGN KEY (IDNguoiDung) REFERENCES NguoiDung(IDNguoiDung)
				    ON UPDATE CASCADE
			ON DELETE CASCADE,
    FOREIGN KEY (IdSanPham) REFERENCES SanPham(IDSanPham)
			    ON UPDATE CASCADE
			ON DELETE CASCADE,
);

-- =============== THÊM CÁC RÀNG BUỘC CHECK ===============
-- Ràng buộc Vai trò chỉ được Admin hoặc User
ALTER TABLE NguoiDung
ADD CONSTRAINT CK_VaiTro CHECK (VaiTro IN (N'Admin', N'User'));

-- Ràng buộc Trạng thái hóa đơn
ALTER TABLE HoaDon
ADD CONSTRAINT CK_TrangThai CHECK (TrangThai IN (N'Đã thanh toán', N'Chờ thanh toán', N'Hủy thanh toán'));

-- Ràng buộc Số lượng tồn kho phải >= 0
ALTER TABLE SanPham
ADD CONSTRAINT CK_SoLuongTonKho CHECK (SoLuongTonKho >= 0);

-- Ràng buộc Số lượng trong chi tiết hóa đơn phải > 0
ALTER TABLE ChiTietHoaDon
ADD CONSTRAINT CK_SoLuong CHECK (SoLuong > 0);

-- Ràng buộc Đơn giá phải > 0
ALTER TABLE ChiTietHoaDon
ADD CONSTRAINT CK_DonGia CHECK (DonGia > 0);

-- Ràng buộc Tổng tiền phải >= 0
ALTER TABLE HoaDon
ADD CONSTRAINT CK_TongTien CHECK (TongTien >= 0);

-- =============== INSERT DỮ LIỆU MẪU ===============

-- Insert dữ liệu vào bảng NguoiDung
INSERT INTO NguoiDung (Email, password, sdt, DiaChi, AvatarURL, VaiTro, gioitinh) VALUES
(N'ad', N'1', N'0901234567', N'123 Nguyễn Huệ, Đà Nẵng', N'avatar1.jpg', N'Admin', N'Nam'),
(N'user1@gmail.com', N'user123', N'0912345678', N'456 Lê Lợi, Đà Nẵng', N'avatar2.jpg', N'User', N'Nữ'),
(N'user2@gmail.com', N'user456', N'0923456789', N'789 Trần Phú, Đà Nẵng', N'avatar3.jpg', N'User', N'Nam'),
(N'user3@gmail.com', N'user789', N'0934567890', N'321 Hùng Vương, Đà Nẵng', N'avatar4.jpg', N'User', N'Nữ'),
(N'user4@yahoo.com', N'user000', N'0945678901', N'654 Hoàng Diệu, Đà Nẵng', N'avatar5.jpg', N'User', N'Nam');

-- Insert dữ liệu vào bảng DanhMuc
INSERT INTO DanhMuc (TenDanhmuc, MoTa, NgayTao) VALUES
(N'Áo đá banh', N'Các loại áo đá banh CLB và ĐTQG', '2024-01-15'),
(N'Quần đá banh', N'Quần đá banh nam nữ', '2024-01-16'),
(N'Giày đá bóng', N'Giày sân cỏ tự nhiên và nhân tạo', '2024-01-17'),
(N'Phụ kiện bóng đá', N'Găng tay, tất, bóng', '2024-01-18'),
(N'Áo khoác thể thao', N'Áo khoác CLB và đội tuyển', '2024-01-19');

-- Insert dữ liệu vào bảng SanPham
INSERT INTO SanPham (IDdanhMuc, Hang, KichThuoc, mausac, mota, hinhanh, SoLuongTonKho, DonViTinh) VALUES
(1, N'Adidas', N'M', N'Đỏ', N'Áo đá banh Đội tuyển Việt Nam sân nhà', N'aovn1.jpg', 60, N'Cái'),
(1, N'Nike', N'L', N'Xanh dương', N'Áo đá banh CLB Chelsea mùa 2024', N'chelsea2024.jpg', 45, N'Cái'),
(2, N'Puma', N'XL', N'Đen', N'Quần đá banh nam chất liệu thoáng khí', N'quandabanh1.jpg', 80, N'Cái'),
(3, N'Nike', N'42', N'Trắng', N'Giày đá bóng sân nhân tạo Nike Tiempo', N'tiempo.jpg', 20, N'Đôi'),
(4, N'Adidas', N'Free', N'Đen', N'Găng tay thủ môn Adidas Pro', N'gangtay1.jpg', 35, N'Cặp'),
(5, N'Adidas', N'XL', N'Xám', N'Áo khoác thể thao Real Madrid', N'aokhoacRM.jpg', 25, N'Cái');

-- Insert dữ liệu vào bảng HoaDon
INSERT INTO HoaDon (IdUser, TongTien, DiaChiGiaoHang, NgayDat, TrangThai) VALUES
(2, 1200000, N'456 Lê Lợi, Đà Nẵng', '2024-02-01', N'Đã thanh toán'),
(3, 850000, N'789 Trần Phú, Đà Nẵng', '2024-02-05', N'Chờ thanh toán'),
(4, 1500000, N'321 Hùng Vương, Đà Nẵng', '2024-02-10', N'Đã thanh toán'),
(5, 450000, N'654 Hoàng Diệu, Đà Nẵng', '2024-02-20', N'Chờ thanh toán'),
(2, 900000, N'456 Lê Lợi, Đà Nẵng', '2024-02-20', N'Hủy thanh toán');

-- Insert dữ liệu vào bảng ChiTietHoaDon
INSERT INTO ChiTietHoaDon (IdHoaDon, IdSanPham, SoLuong, DonGia) VALUES
(1, 1, 2, 250000),
(2, 2, 1, 300000),
(2, 4, 1, 450000),
(3, 3, 2, 600000),
(4, 5, 1, 300000);

-- Insert dữ liệu vào bảng BinhLuan
INSERT INTO BinhLuan (IDNguoiDung, IdSanPham, NoiDung, NgayBinhLuan, TinhTrang) VALUES
(2, 1, N'Áo Việt Nam chất lượng, mặc đá rất thoải mái', '2024-02-02', N'Đã duyệt'),
(3, 2, N'Màu đẹp, form chuẩn đúng Nike', '2024-02-06', N'Đã duyệt'),
(4, 3, N'Quần nhẹ, thoáng khí, chạy thoải mái', '2024-02-11', N'Đã duyệt'),
(5, 4, N'Giày bám sân tốt, đá rất đã', '2024-02-16', N'Chờ duyệt'),
(2, 6, N'Áo khoác xịn, giữ nhiệt tốt', '2024-02-21', N'Đã duyệt');

-- =============== KIỂM TRA DỮ LIỆU ===============
SELECT * FROM NguoiDung;
SELECT * FROM DanhMuc;
SELECT * FROM SanPham;
SELECT * FROM HoaDon;
SELECT * FROM ChiTietHoaDon;
SELECT * FROM BinhLuan;

-- =============== KIỂM TRA CÁC RÀNG BUỘC ===============
PRINT N'Danh sách các ràng buộc CHECK:';
SELECT 
    TABLE_NAME,
    CONSTRAINT_NAME
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
WHERE CONSTRAINT_TYPE = 'CHECK';