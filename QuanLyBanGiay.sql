CREATE DATABASE QuanLyBanGiay
ON PRIMARY
(
	FILENAME = 'C:\QuanLyBanGiay\QuanLyBanGiay.mdf',
	NAME = QuanLyCuaHangBanGiay
)
LOG ON
(
	FILENAME = 'C:\QuanLyBanGiay\QuanLyBanGiay.ldf',
	NAME = QuanLyBanGiay_log
)
GO

-- Sử dụng Database 10_QuanLyBanGiay
USE QuanLyBanGiay
GO

-- Tạo bảng hóa đơn nhập
-- Thông tin gồm:
-- IDHoaDon kiểu INT, tự động tăng 1
-- NgayNhap kiểu Date
-- IDNhanVien kiểu INT
CREATE TABLE HOADONNHAP(
	IDHoaDon INT IDENTITY,
	NgayNhap DATE,
	IDNhanVien INT
)
GO

-- tạo bảng chi tiết hóa đơn nhập
-- Thông tin gồm:
-- IDHoaDon kiểu INT
-- IDGiay kiểu INT 
-- DonGia kiểu INT
-- SLYeuCau kiểu INT
-- SLNhan kiểu INT
CREATE TABLE CTHOADONNHAP(
	IDHoaDon INT NOT NULL,
	IDGiay INT NOT NULL,
	DonGia DECIMAL(15, 2),
	SLYeuCau INT,
	SLNhan INT
)
GO

-- Tạo bảng giầy
-- IDGiay kiểu INT, tự động tăng
-- TenGiay kiểu nvarchar(10)
-- SoLuong kiểu INT
-- DonGia kiểu INT
CREATE TABLE GIAY(
	IDGiay INT IDENTITY,
	TenGiay NVARCHAR(100),
	SoLuong INT,
	DonGia DECIMAL(15, 2)
)
GO

-- Tạo bảng nhân viên
-- IDNhanVien kiểu INT, tự động tăng
-- TenNV kiểu nvarchar(300)
-- NgaySinh kiểu date
-- DiaChi kiểu nvarchar(100)
CREATE TABLE NHANVIEN(
	IDNhanVien INT IDENTITY,
	TenNV NVARCHAR(300),
	NgSinh DATE,
	DiaChi NVARCHAR(100)
)
GO

-- Tạo bảng quản trị viên
-- IDNhanVien kiểu INT
-- Name kiểu nvarchar(300), not null
-- Pass kiểu nvarchar(20), not null
CREATE TABLE QUANTRIVIEN(
	IDNhanVien INT,
	Name NVARCHAR(300) not null,
	Pass NVARCHAR(20) not null 
)
GO

-- Tạo bảng khách hàng
-- IDKhachHang kiểu int, tự động tăng
-- TenKhachHang kiểu nvarchar(300)
-- SDT kiểu nvarchar(11)
CREATE TABLE KHACHHANG(
	IDKhachHang INT IDENTITY,
	TenKhachHang NVARCHAR(300),
	SDT NVARCHAR(11)
)
GO

-- Tạo bảng hóa đơn bán
-- IDHoaDon kiểu int, tự động tăng
-- Ngay kiểu date,
-- IDKhachHang kiểu int
-- IDNhanVien kiểu int
-- IDKhuyenMai kiểu int
CREATE TABLE HOADONBAN(
	IDHoaDon INT IDENTITY,
	Ngay DATE,
	IDKhachHang INT,
	IDNhanVien INT,
	IDkhuyenMai INT,
)
GO
-- tạo bảng chi tiết hóa đơn
-- IDGiay kiểu int
-- IDHoaDon kiểu int
-- SoLuong kiểu int
-- DonGiaBan kiểu int
-- ChietKhau kiểu float
CREATE TABLE CTHOADONBAN(
	IDGiay INT NOT NULL,
	IDHoaDon INT NOT NULL,
	SoLuong INT,
	DonGiaBan DECIMAL(15, 2),
	ChietKhau FLOAT
)
GO

-- Tạo bảng khuyến mãi
-- IDKhuyeMai kiểu int, tự động tăng
-- TenCT kiểu nvarchar(200)
-- MoTa kiểu navarchar(200),
-- NgayBD kiểu Date
-- NgayKT kiểu Date
-- ChietKhau kiểu Float
CREATE TABLE KHUYENMAI(
	IDKhuyenMai INT IDENTITY,
	TenCT NVARCHAR(200),
	MoTa NVARCHAR(200),
	NgayBD DATE,
	NgayKT DATE,
	ChietKhau FLOAT
)
GO

-- tạo bảng khuyến mại sản phẩm
CREATE TABLE CTKHUYENMAI(
	IDKhuyenMai INT NOT NULL,
	IDGiay INT NOT NULL,
	ChietKhau FLOAT
)

GO

-- Tạo khóa chính
ALTER TABLE dbo.HOADONNHAP ADD CONSTRAINT PK_HoaDonNhap PRIMARY KEY(IDHoaDon)
ALTER TABLE dbo.GIAY ADD CONSTRAINT PK_Giay PRIMARY KEY(IDGiay)
ALTER TABLE dbo.NHANVIEN ADD CONSTRAINT PK_NhanVien PRIMARY KEY(IDNhanVien)
ALTER TABLE dbo.HOADONBAN ADD CONSTRAINT PK_HoaDonBan PRIMARY KEY(IDHoaDon)
ALTER TABLE dbo.KHACHHANG ADD CONSTRAINT PK_KhachHang PRIMARY KEY(IDKhachHang)
ALTER TABLE dbo.KHUYENMAI ADD CONSTRAINT PK_KhuyenMai PRIMARY KEY(IDKhuyenMai)
ALTER TABLE dbo.QUANTRIVIEN ADD CONSTRAINT PK_Name PRIMARY KEY(Name)

-- Tạo khóa ngoại
ALTER TABLE dbo.HOADONNHAP ADD CONSTRAINT FK_HOADONNHAP_IDNhanVien FOREIGN KEY(IDNhanVien) REFERENCES dbo.NHANVIEN(IDNhanVien)
ALTER TABLE dbo.CTHOADONNHAP ADD CONSTRAINT FK_CTHOADONNHAP_IDHoaDonBan FOREIGN KEY(IDHoaDon) REFERENCES dbo.HOADONNHAP(IDHoaDon)
ALTER TABLE dbo.CTHOADONNHAP ADD CONSTRAINT FK_CTHOADONNHAP_IDGiay FOREIGN KEY(IDGiay) REFERENCES dbo.GIAY(IDGiay)
ALTER TABLE dbo.HOADONBAN ADD CONSTRAINT FK_HOADONBAN_IDKhachHang FOREIGN KEY(IDKhachHang) REFERENCES dbo.KHACHHANG(IDKhachHang)
ALTER TABLE dbo.HOADONBAN ADD CONSTRAINT FK_HOADONBAN_IDNhanVien FOREIGN KEY(IDNhanVien) REFERENCES dbo.NHANVIEN(IDNhanVien)
ALTER TABLE dbo.HOADONBAN ADD CONSTRAINT FK_HOADONBAN_IDKhuyenMai FOREIGN KEY(IDKhuyenMai) REFERENCES dbo.KHUYENMAI(IDKhuyenMai)
ALTER TABLE dbo.CTHOADONBAN ADD CONSTRAINT FK_CTHOADONBAN_IDHoaDon FOREIGN KEY(IDHoaDon) REFERENCES dbo.HOADONBAN(IDHoaDon)
ALTER TABLE dbo.CTHOADONBAN ADD CONSTRAINT FK_CTHOADONBAN_IDGiay FOREIGN KEY(IDGiay) REFERENCES dbo.GIAY(IDGiay)
ALTER TABLE dbo.CTKHUYENMAI ADD CONSTRAINT FK_CTKHUYENMAI_IDGiay FOREIGN KEY(IDGiay) REFERENCES dbo.GIAY(IDGiay)
ALTER TABLE dbo.CTKHUYENMAI ADD CONSTRAINT FK_CTKHUYENMAI_IDKM FOREIGN KEY(IDKhuyenMai) REFERENCES dbo.KhuyenMai(IDKhuyenMai)
ALTER TABLE dbo.QUANTRIVIEN ADD CONSTRAINT FK_QUANTRIVIEN_IDNV FOREIGN KEY(IDNhanVien) REFERENCES dbo.NhanVien(IDNhanVien)

-- tạo khóa chinh kép
ALTER TABLE dbo.CTHOADONNHAP ADD CONSTRAINT PK_CTHOADONNHAP_IDHOADON_IDGIAY PRIMARY KEY (IDHoaDon,IDGiay)
ALTER TABLE dbo.CTHOADONBAN ADD CONSTRAINT PK_CTHOADONBAN_IDHOADON_IDGIAY PRIMARY KEY (IDHoaDon,IDGiay)
ALTER TABLE dbo.CTKHUYENMAI ADD CONSTRAINT PK_CTKHUYENMAI_IDHOADON_IDGIAY PRIMARY KEY (IDKhuyenMai,IDGiay)


