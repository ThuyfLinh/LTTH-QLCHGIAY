use QuanLyCHBanGiay -- Sử dụng database QuanLyCHBanGiay
GO
----------------------DANGNHAP----------------
select * from DANGNHAP
create proc USP_SearchDN(@ten nvarchar(200))
as
begin
	select *
	from DANGNHAP
	where Name like N'%'+@ten+'%'
end

USP_SearchDN N'Nguyen'
---------------------------------------------
delete DANGNHAP
where Name = N'Linh'
---------------------------
update DANGNHAP
set Name =N'NguyenThuyLinh', Pass=N'12345'

where Name = N'Linh'
-------------------------------CTHOADONBAN-----
CREATE   proc [dbo].[USP_searchCTHoaDonBan] @id int,@search nvarchar(100)
as
begin
SELECT CTHOADONBAN.IDGiay,GIAY.TenGiay,CTHOADONBAN.SoLuong,CTHOADONBAN.DonGiaBan,CTHOADONBAN.ChietKhau FROM CTHOADONBAN JOIN GIAY ON CTHOADONBAN.IDGiay = GIAY.IDGiay
where CTHOADONBAN.IDHoaDon=@id and (CTHOADONBAN.IDGiay like N'%' +@search+'%' OR GIAY.TenGiay LIKE N'%'+@search+'%' OR CTHOADONBAN.SoLuong LIKE N'%'+@search+'%' 
	  OR CTHOADONBAN.DonGiaBan LIKE N'%'+@search+'%' OR CTHOADONBAN.ChietKhau LIKE N'%'+@search+'%')
end
--------------------------------------------------------
CREATE   proc [dbo].[USP_delete_CTHDB]
@MaHD INT,@MaGiay int 
as
declare @soluong int
begin 
select @soluong=SoLuong from CTHOADONBAN where IDGiay=@MaGiay
delete CTHOADONBAN where IDHoaDon=@MaHD and IDGiay=@MaGiay
update GIAY set SoLuong=SoLuong+@soluong where IDGiay=@MaGiay
end
----------------------------------------------
CREATE   proc [dbo].[USP_GetListCTHD] @MaHD INT
AS 
BEGIN
SELECT CTHOADONBAN.IDHoaDon, CTHOADONBAN.IDGiay,GIAY.TenGiay,CTHOADONBAN.SoLuong,CTHOADONBAN.DonGiaBan,CTHOADONBAN.ChietKhau FROM CTHOADONBAN JOIN GIAY ON CTHOADONBAN.IDGiay = GIAY.IDGiay
WHERE CTHOADONBAN.IDHoaDon =@MaHD
END
--------------------------------------------
CREATE   proc [dbo].[USP_insert_CTHDB] 
@Mahd INT,
@MaGiay int,
@SoLuong int
as
declare @DonGia DECIMAL(18,2),@ChietKhau float
begin
 select @DonGia=DonGia from GIAY where IDGiay = @MaGiay
 SELECT @ChietKhau=ChietKhau FROM dbo.CTKHUYENMAI WHERE IDGiay=@MaGiay AND IDKhuyenMai = (SELECT IDkhuyenMai FROM dbo.HOADONBAN WHERE IDHoaDon = @Mahd)
 INSERT INTO CTHOADONBAN(IDGiay,IDHoaDon,SoLuong,DonGiaBan,ChietKhau) values (@MaGiay,@Mahd,@SoLuong,@DonGia,@ChietKhau)
 UPDATE dbo.GIAY SET SoLuong=SoLuong-@SoLuong WHERE IDGiay = @MaGiay
END
--------------------HOADONBAN---------------------------------------
CREATE PROC [dbo].[USP_GetListHoaDonBan]
as
BEGIN 
SELECT HOADONBAN.IDHoaDon,NHANVIEN.TenNV,KHACHHANG.TenKhachHang,KHACHHANG.SDT,KHUYENMAI.TenCT,HOADONBAN.Ngay FROM HOADONBAN JOIN KHACHHANG ON HOADONBAN.IDKhachHang = KHACHHANG.IDKhachHang
						JOIN NHANVIEN ON HOADONBAN.IDNhanVien = NHANVIEN.IDNhanVien
						JOIN KHUYENMAI ON HOADONBAN.IDkhuyenMai = KHUYENMAI.IDKhuyenMai
END
---------------------------------------------------------------------
CREATE   PROC [dbo].[USP_INSERT_HoaDonBan_KHnew]
	@TenNV NVARCHAR(300),
	@TenKH NVARCHAR(300),
	@SDT NVARCHAR(11),
	@TenCT NVARCHAR(200),
	@Ngay Date
AS
BEGIN
	DECLARE @idkh int, @idnv int, @idkm int
	insert into KHACHHANG(TenKhachHang,SDT) values (@TenKH,@SDT)
	select @idkh= IDKhachHang from KHACHHANG where TenKhachHang like N'%'+ @TenKH
	SELECT @idnv= IDNhanVien from NHANVIEN where TenNV like N'%' + @TenNV
	select @idkm = IDKhuyenMai from KHUYENMAI where TenCT LIKE N'%' +@TenCT
	INSERT INTO HOADONBAN(Ngay,IDNhanVien,IDKhachHang,IDkhuyenMai) VALUES (@Ngay,@idnv,@idkh,@idkm)
END
--------------------------------------------------------------------
CREATE   PROC [dbo].[USP_INSERT_HoaDonBan_KHold] 
	@TenNV NVARCHAR(300),
	@TenKH NVARCHAR(300),
	@SDT NVARCHAR(11),
	@TenCT NVARCHAR(200),
	@Ngay Date
AS
BEGIN
	DECLARE @idkh int, @idnv int, @idkm int
	select @idkh= IDKhachHang from KHACHHANG where TenKhachHang like N'%'+ @TenKH
	SELECT @idnv= IDNhanVien from NHANVIEN where TenNV like N'%' + @TenNV
	select @idkm = IDKhuyenMai from KHUYENMAI where TenCT LIKE N'%' +@TenCT
	INSERT INTO HOADONBAN(Ngay,IDNhanVien,IDKhachHang,IDkhuyenMai) VALUES (@Ngay,@idnv,@idkh,@idkm)
END
------------------------------------------------------------------
CREATE   proc [dbo].[USP_searchHoaDonBan] @search nvarchar(100)
as
begin 
select HOADONBAN.IDHoaDon,NHANVIEN.TenNV,KHACHHANG.TenKhachHang,KHACHHANG.SDT,KHUYENMAI.TenCT,HOADONBAN.Ngay FROM HOADONBAN JOIN KHACHHANG ON HOADONBAN.IDKhachHang = KHACHHANG.IDKhachHang
						JOIN NHANVIEN ON HOADONBAN.IDNhanVien = NHANVIEN.IDNhanVien
						JOIN KHUYENMAI ON HOADONBAN.IDkhuyenMai = KHUYENMAI.IDKhuyenMai
where HOADONBAN.IDHoaDon like N'%' +@search+'%' OR NHANVIEN.TenNV LIKE N'%'+@search+'%' OR KHACHHANG.TenKhachHang LIKE N'%'+@search+'%' 
	  OR KHACHHANG.SDT LIKE N'%'+@search+'%' OR KHUYENMAI.TenCT LIKE N'%'+@search+'%' or HOADONBAN.Ngay like N'%'+@search+'%'
end
-----------------------------------------------------------------
CREATE   PROC [dbo].[USP_UPDATE_HoaDonBan] @MaHD INT,@TenNV NVARCHAR(300),@TenKH NVARCHAR(300),@SoDT NVARCHAR(11),@TenCT NVARCHAR(200),@Ngay date
AS 
BEGIN
DECLARE @idnv int, @idkh int,@idkm int
SELECT @idkh = IDKhachHang FROM KHACHHANG WHERE TenKhachHang like N'%' + @TenKH 
SELECT @idnv = IDNhanVien FROM NHANVIEN where TenNV like N'%' + @TenNV
select @idkm =IDKhuyenMai FROM KHUYENMAI WHERE TenCT LIKE N'%' +@TenCT
update KHACHHANG set SDT=@SoDT where IDKhachHang=@idkh
UPDATE HOADONBAN SET IDKhachHang = @idkh, IDNhanVien = @idnv, IDkhuyenMai = @idkm, Ngay = @Ngay where IDHoaDon = @MaHD
END
--------------------------------CTHOADONNHAP-------------------------
CREATE PROCEDURE [dbo].[USP_SEARCHCTHOADONNHAP]
	@SEARCHVALUE NVARCHAR(50)
AS
BEGIN
	SELECT IDHoaDon,IDGiay,DonGia,SLYeuCau,SLNhan
	FROM CTHOADONNHAP 
	WHERE (IDHoaDon LIKE N'%' + @SEARCHVALUE + '%')
	  OR (IDGiay LIKE N'%' + @SEARCHVALUE + '%') 
		OR (DonGia LIKE N'%' + @SEARCHVALUE + '%') 
		OR (SLYeuCau LIKE N'%' + @SEARCHVALUE + '%') 	
		OR (SLNhan LIKE N'%' + @SEARCHVALUE + '%') 
END

-------------------------KHACHHANG---------------------------
CREATE PROC [dbo].[USP_DeleteKhachHang]
	@makh INT
AS
BEGIN
	DELETE FROM dbo.KHACHHANG WHERE IDKhachHang=@makh
END
---------------------------------------------------
CREATE PROC [dbo].[USP_InsertKhachHang]
    
	@tenkh NVARCHAR(100),
	@sdt CHAR(10)
	AS
	BEGIN
		INSERT dbo.KHACHHANG
		        (  TenKhachHang , SDT )
		VALUES  ( 
		          @tenkh,
		          @sdt 
		          )
	END
--------------------------------------------------------------------
CREATE PROC [dbo].[USP_UpdateKhachHang]
	@makh INT,
	@tenkh NVARCHAR(100),
	@sdt CHAR(10)
AS
BEGIN
	UPDATE dbo.KHACHHANG SET TenKhachHang=@tenkh , SDT = @sdt WHERE IDKhachHang=@makh
END
---------------------------------------------------------------
--------------------------------------------------------------------
CREATE PROC [dbo].[USP_SearchKhachHang]
	@tenkh NVARCHAR(100)
AS
BEGIN
	select *
	from KHACHHANG
	where TenKhachHang like N'%'+@tenkh+'%'
END
---------------THONGKE DOANHTHU-----------------
ALTER FUNCTION [dbo].[f_TinhDoanhThu] ()
RETURNS @doanhthu TABLE (
	IDHoaDon INT,
	TongDoanhThu DECIMAL(15, 2)
	)
AS
BEGIN
	DECLARE @id int 
	DECLARE @tongdoanhthu DECIMAL(15, 2) 
	DECLARE @chietkhau FLOAT 
	DECLARE cursorTinh 
		CURSOR FOR 
		SELECT DISTINCT ThongTin.IDHoaDon,[Tong doanh thu],KHUYENMAI.ChietKhau 
		FROM (SELECT v_ThongTinBan.IDHoaDon,[Tong doanh thu],IDkhuyenMai FROM dbo.v_ViewTongDoanhThu,dbo.v_ThongTinBan
		WHERE v_ThongTinBan.IDHoaDon=v_ViewTongDoanhThu.IDHoaDon) AS ThongTin 
		LEFT JOIN dbo.KHUYENMAI 
		ON KHUYENMAI.IDKhuyenMai=ThongTin.IDkhuyenMai
		OPEN cursorTinh 
		FETCH NEXT FROM cursorTinh INTO @id, @tongdoanhthu, @chietkhau 
		WHILE @@FETCH_STATUS = 0 
		BEGIN
			IF @chietkhau IS NULL 
			INSERT @doanhthu
			        ( IDHoaDon, TongDoanhThu )
			VALUES  ( @id, -- IDHoaDon - int
			          @tongdoanhthu  -- TongDoanhThu - decimal
			          )
			ELSE 
			INSERT @doanhthu
			        ( IDHoaDon, TongDoanhThu )
			VALUES  ( @id, -- IDHoaDon - int
			          @tongdoanhthu-@tongdoanhthu*@chietkhau/100  -- TongDoanhThu - decimal
			          )
			FETCH NEXT FROM cursorTinh INTO @id, @tongdoanhthu, @chietkhau 
		END 
		CLOSE cursorTinh 
		DEALLOCATE cursorTinh 
	RETURN
END
------------------------------------------------------------------------
ALTER FUNCTION [dbo].[f_TinhTongTien] ()
RETURNS @tongtien TABLE (
	IDHoaDon INT,
	TongTien DECIMAL(15, 2)
	)
AS
BEGIN
	DECLARE @id int 
	DECLARE @thanhtien DECIMAL(15, 2) 
	DECLARE @chietkhau FLOAT 
	DECLARE cursorTinh 
		CURSOR FOR 
		SELECT IDHoaDon,[Thành tiền],ChietKhau FROM dbo.v_ThongTinBan
		OPEN cursorTinh 
		FETCH NEXT FROM cursorTinh INTO @id, @thanhtien, @chietkhau 
		WHILE @@FETCH_STATUS = 0 
		BEGIN
			IF @chietkhau IS NULL 
			INSERT @tongtien
			        ( IDHoaDon, TongTien )
			VALUES  ( @id, -- IDHoaDon - int
			          @thanhtien  -- TongTien - decimal
			          )
			ELSE 
			INSERT @tongtien
			        ( IDHoaDon, TongTien )
			VALUES  ( @id, -- IDHoaDon - int
			          @thanhtien-@thanhtien*@chietkhau/100  -- TongTien - decimal
			          )
			FETCH NEXT FROM cursorTinh INTO @id, @thanhtien, @chietkhau 
		END 
		CLOSE cursorTinh 
		DEALLOCATE cursorTinh 
	RETURN
END
---------------------------------------------------------------------
CREATE VIEW v_ThongTinBan AS
SELECT HOADONBAN.IDHoaDon,IDGiay,SoLuong,(SoLuong * DonGiaBan) AS [Thành tiền],ChietKhau,dbo.HOADONBAN.IDkhuyenMai 
FROM dbo.HOADONBAN JOIN dbo.CTHOADONBAN ON CTHOADONBAN.IDHoaDon = HOADONBAN.IDHoaDon
GO
-------------------------------------------------------------------------
CREATE VIEW v_ThongTinHoaDon AS
SELECT IDHoaDon,TenNV,Ngay FROM dbo.HOADONBAN LEFT JOIN dbo.NHANVIEN ON NHANVIEN.IDNhanVien = HOADONBAN.IDNhanVien
GO
-----------------------------------------------------------------------
CREATE VIEW v_TongSL
AS
SELECT IDHoaDon,SUM(SoLuong) AS SoLuongSP FROM dbo.v_ThongTinBan GROUP BY (IDHoaDon)
GO
---------------------------------------------------------------------------
CREATE VIEW v_ViewTongDoanhThu
AS
SELECT IDHoaDon,SUM(TongTien) AS [Tong doanh thu] FROM dbo.f_TinhTongTien() GROUP BY IDHoaDon
GO
---------------------------------------------------
ALTER   PROC [dbo].[USP_ThongKeDoanhThu]
@tungay DATE,
@denngay DATE
AS
BEGIN
	SELECT v_ThongTinHoaDon.IDHoaDon,TenNV,SoLuongSP,TongDoanhThu FROM dbo.v_ThongTinHoaDon,dbo.v_TongSL,dbo.f_TinhDoanhThu() 
	WHERE v_ThongTinHoaDon.IDHoaDon=v_TongSL.IDHoaDon AND dbo.f_TinhDoanhThu.IDHoaDon=v_ThongTinHoaDon.IDHoaDon AND Ngay > @tungay AND Ngay <@denngay
END
----------------NHANVIEN---------------
Insert NHANVIEN(TenNV,NgSinh,DiaChi) values (
                N'" + hoten + "',
                '2019/02/01',
                N'" + diachi + "'
                )
				select * from NHANVIEN
-------------------DELETE NHANVIEN--------EXEC dbo.USP_DeleteNhanVien @manv -------------
alter proc USP_DeleteNhanVien(@manv int)
as
begin
	update DANGNHAP
	set IDNhanVien = null
	where IDNhanVien = @manv
	update HOADONNHAP
	set IDNhanVien = null
	where IDNhanVien = @manv
	update HOADONBAN
	set IDNhanVien = null
	where IDNhanVien = @manv
	delete NHANVIEN
	where IDNhanVien= @manv
end
EXEC dbo.USP_DeleteNhanVien 18
select * from NHANVIEN
-----------------EXEC dbo.USP_SearchNhanVien @search-------------------------
create proc USP_SearchNhanVien(@search nvarchar(100))
as
begin
	select *
	from NHANVIEN
	where TenNV like N'%'+@search+'%'
end

EXEC dbo.USP_SearchNhanVien N'Nguyễn'

------------------------------------------
---------------------KHUYENMAI-----------------------
-----insertKM--
create proc USP_InsertKM(@ten nvarchar(100), @mota nvarchar(200),@ngaybd date, @ngaykt date, @chietkhau float)
as
begin
	insert into KHUYENMAI(TenCT,MoTa,NgayBD,NgayKT,ChietKhau)
	values (@ten,@mota,@ngaybd,@ngaykt,@chietkhau)
end

exec USP_InsertKM N'linh',null,'6/9/2019 12:00:00 AM','2019/02/10',10
-----updateKM---
create proc USP_UpdateKM(@idkm int, @ten nvarchar(100), @mota nvarchar(200),@ngaybd date, @ngaykt date, @chietkhau float)
as
begin
	update KHUYENMAI
	set TenCT=@ten, MoTa=@mota,NgayBD=@ngaybd,NgayKT=@ngaykt,ChietKhau=@chietkhau
	where IDKhuyenMai=@idkm
end

EXEC USP_UpdateKM 59,N'ac',null,'2019/05/01','2019/05/03',10
select * from KHUYENMAI where IDKhuyenMai=59
-------------------------
Update KHUYENMAI set TenCT = 
                N'xyz',
                MoTa = N'" + mota + "',
                NgayBD = '2019/02/01',
                NgayKT = '2019/02/01',
                ChietKhau = 10 
                 where IDKhuyenMai = 59
-------searchKM-----------
create proc USP_SearchKM(@ten nvarchar(200))
as
begin
	select *
	from KHUYENMAI
	where TenCT like N'%'+@ten+'%'
end

exec USP_SearchKM tine
--------------------------------CHITIETKHUYENMAI------------

create proc USP_ListCTKM(@ma int)
as
begin
SELECT IDKHUYENMAI,IDGIAY,CHIETKHAU FROM CTKHUYENMAI WHERE IDKHUYENMAI = @ma
end

exec USP_ListCTKM 25
        --------------------------
	-------insert-----
create proc USP_ThemCTKM(@makm int, @magiay int, @ck float)
as
begin
	insert into CTKHUYENMAI(IDKhuyenMai,IDGiay,ChietKhau)
	values (@makm,@magiay,@ck)
end

EXEC USP_ThemCTKM 6,3,10


	------update-----
create PROC USP_UpdateCT(@idkm int,@idgiay int, @chietkhau float)
as
begin
	update CTKHUYENMAI
	set  ChietKhau=@chietkhau
	where IDKhuyenMai=@idkm
	and IDGiay = @idgiay
end

EXEC USP_UpdateCT 6,2,10
	----------delete-------------
create proc USP_DeleteCTKM(@maGiay int, @maKM int)
as
begin
	delete CTKHUYENMAI
	where IDGiay = @maGiay
	and IDKhuyenMai=@maKM
end
select * from CTKHUYENMAI
exec USP_DeleteCTKM 1,6

----------------------GIAY------------------
------search-------------
create proc USP_SearchGiay(@ten nvarchar(200))
as
begin
	select IDGiay,TenGiay,SoLuong,DonGia
	from GIAY
	where TenGiay like N'%'+@ten+'%'
end

exec USP_SearchGiay Nike

-------------update-------------------

create proc USP_UpdateGiay(@maGiay int, @ten nvarchar(200),@soluong int, @dongia decimal(15,2))
as
begin
	update Giay
	set TenGiay = @ten, SoLuong = @soluong , DonGia= @dongia
	where IDGiay = @maGiay
end

exec USP_UpdateGiay 1,N'Nike 34',100,200000

------------delete------------------
alter proc USP_DeleteGiay(@id int)
as
begin
	delete CTKHUYENMAI
	where IDGiay=@id
	update CTHOADONBAN
	set IDGiay=null
	where IDGiay=@id
	update CTHOADONNHAP
	set IDGiay=null
	where IDGiay=@id
	delete GIAY
	where IDGiay=@id
end
select * from GIAY
exec USP_DeleteGiay 25
-----------------------------
select * from Giay

insert Giay(TenGiay,SoLuong,DonGia)
values (N'Vans',10,100000)
