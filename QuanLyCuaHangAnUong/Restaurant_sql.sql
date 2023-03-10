USE [QL_NhaHang]
GO
/****** Object:  Table [dbo].[BAN]    Script Date: 06/24/2022 20:39:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BAN](
	[MaBan] [int] IDENTITY(1,1) NOT NULL,
	[TenBan] [nvarchar](50) NULL,
	[LoaiBan] [nvarchar](50) NULL,
	[TrangThai] [nvarchar](50) NULL,
 CONSTRAINT [PK_BAN] PRIMARY KEY CLUSTERED 
(
	[MaBan] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LOAISP]    Script Date: 06/24/2022 20:39:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LOAISP](
	[MaLoai] [int] IDENTITY(1,1) NOT NULL,
	[TenLoai] [nvarchar](50) NULL,
 CONSTRAINT [PK_LOAISP] PRIMARY KEY CLUSTERED 
(
	[MaLoai] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KHACHHANG]    Script Date: 06/24/2022 20:39:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KHACHHANG](
	[MaKH] [int] IDENTITY(1,1) NOT NULL,
	[TenKH] [nvarchar](50) NULL,
	[SDT] [nchar](10) NULL,
 CONSTRAINT [PK_KHACKHANG] PRIMARY KEY CLUSTERED 
(
	[MaKH] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TAIKHOAN]    Script Date: 06/24/2022 20:39:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TAIKHOAN](
	[MaTK] [int] IDENTITY(1,1) NOT NULL,
	[TenTK] [nvarchar](50) NULL,
	[MK] [nvarchar](50) NULL,
 CONSTRAINT [PK_TAIKHOAN] PRIMARY KEY CLUSTERED 
(
	[MaTK] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[TAIKHOAN] ON
INSERT [dbo].[TAIKHOAN] ([MaTK], [TenTK], [MK]) VALUES (1, N'tin', N'tin123')
INSERT [dbo].[TAIKHOAN] ([MaTK], [TenTK], [MK]) VALUES (2, N'NV001', N'tin123')
SET IDENTITY_INSERT [dbo].[TAIKHOAN] OFF
/****** Object:  StoredProcedure [dbo].[SP_TaiKhoan]    Script Date: 06/24/2022 20:39:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[SP_TaiKhoan]
@TenDN nvarchar(50),
@MK nvarchar(50)
as
begin
	if exists (select * from [dbo].[TAIKHOAN] where TenTK = @TenDN and MK = @MK and TenTK LIKE 'NV%')
        select 0 as code
	else if exists(select * from [dbo].[TAIKHOAN] where TenTK = @TenDN and MK != @MK) 
		select 1 as code
    else select 2 as code
end
GO
/****** Object:  Table [dbo].[SANPHAM]    Script Date: 06/24/2022 20:39:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SANPHAM](
	[MaSP] [int] IDENTITY(1,1) NOT NULL,
	[TenSP] [nvarchar](50) NULL,
	[DonGia] [int] NULL,
	[DonVi] [nchar](3) NULL,
	[MoTa] [nvarchar](max) NULL,
	[HinhAnh] [image] NULL,
	[MaLoaiSP] [int] NOT NULL,
 CONSTRAINT [PK_SANPHAM] PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NHANVIEN]    Script Date: 06/24/2022 20:39:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NHANVIEN](
	[MaNV] [nchar](5) NOT NULL,
	[TenNV] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](max) NULL,
	[NgaySinh] [date] NULL,
	[GioiTinh] [nvarchar](50) NULL,
	[SDT] [nchar](10) NULL,
	[TenTK] [nvarchar](50) NULL,
	[MK] [nvarchar](50) NULL,
	[ChucVu] [nvarchar](50) NULL,
 CONSTRAINT [PK_NHANVIEN] PRIMARY KEY CLUSTERED 
(
	[MaNV] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[NHANVIEN] ([MaNV], [TenNV], [DiaChi], [NgaySinh], [GioiTinh], [SDT], [MaTK], [ChucVu]) VALUES (N'NV001', N'Trần Anh Tôn Tín', N'Q12', CAST(0xB9330B00 AS Date), N'nam', N'0123456789', 2, N'Nhân viên')
/****** Object:  Table [dbo].[HOADON]    Script Date: 06/24/2022 20:39:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOADON](
	[MaHD] [int] IDENTITY(1,1) NOT NULL,
	[NgayBan] [date] NULL,
	[TongTien] [int] NULL,
	[TongTienGiamGia] [int] NULL,
	[TrangThai] [nvarchar](50) NULL,
	[MaNV] [nchar](5) NULL,
	[MaKH] [int] NULL,
	[MaBan] [int] NULL,
 CONSTRAINT [PK_HOADON] PRIMARY KEY CLUSTERED 
(
	[MaHD] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CHITIETDONHANG]    Script Date: 06/24/2022 20:39:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHITIETDONHANG](
	[MaHD] [int] NOT NULL,
	[MaSP] [int] NOT NULL,
	[SoLuong] [int] NULL,
	[DonVi] [nchar](3) NULL,
	[ThanhTien] [int] NULL,
	[Thue] [int] NULL,
	[GhiChu] [nvarchar](max) NULL,
 CONSTRAINT [PK_CHITIETDONHANG] PRIMARY KEY CLUSTERED 
(
	[MaHD] ASC,
	[MaSP] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  ForeignKey [fk_chitietdonhang_mahoadon]    Script Date: 06/24/2022 20:39:25 ******/
ALTER TABLE [dbo].[CHITIETDONHANG]  WITH CHECK ADD  CONSTRAINT [fk_chitietdonhang_mahoadon] FOREIGN KEY([MaHD])
REFERENCES [dbo].[HOADON] ([MaHD])
GO
ALTER TABLE [dbo].[CHITIETDONHANG] CHECK CONSTRAINT [fk_chitietdonhang_mahoadon]
GO
/****** Object:  ForeignKey [fk_chitietdonhang_masp]    Script Date: 06/24/2022 20:39:25 ******/
ALTER TABLE [dbo].[CHITIETDONHANG]  WITH CHECK ADD  CONSTRAINT [fk_chitietdonhang_masp] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SANPHAM] ([MaSP])
GO
ALTER TABLE [dbo].[CHITIETDONHANG] CHECK CONSTRAINT [fk_chitietdonhang_masp]
GO
/****** Object:  ForeignKey [fk_hoadon_maban]    Script Date: 06/24/2022 20:39:25 ******/
ALTER TABLE [dbo].[HOADON]  WITH CHECK ADD  CONSTRAINT [fk_hoadon_maban] FOREIGN KEY([MaBan])
REFERENCES [dbo].[BAN] ([MaBan])
GO
ALTER TABLE [dbo].[HOADON] CHECK CONSTRAINT [fk_hoadon_maban]
GO
/****** Object:  ForeignKey [fk_hoadon_makh]    Script Date: 06/24/2022 20:39:25 ******/
ALTER TABLE [dbo].[HOADON]  WITH CHECK ADD  CONSTRAINT [fk_hoadon_makh] FOREIGN KEY([MaKH])
REFERENCES [dbo].[KHACHHANG] ([MaKH])
GO
ALTER TABLE [dbo].[HOADON] CHECK CONSTRAINT [fk_hoadon_makh]
GO
/****** Object:  ForeignKey [fk_hoadon_manv]    Script Date: 06/24/2022 20:39:25 ******/
ALTER TABLE [dbo].[HOADON]  WITH CHECK ADD  CONSTRAINT [fk_hoadon_manv] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NHANVIEN] ([MaNV])
GO
ALTER TABLE [dbo].[HOADON] CHECK CONSTRAINT [fk_hoadon_manv]
GO
/****** Object:  ForeignKey [fk_nhanvien_matk]    Script Date: 06/24/2022 20:39:25 ******/
ALTER TABLE [dbo].[NHANVIEN]  WITH CHECK ADD  CONSTRAINT [fk_nhanvien_matk] FOREIGN KEY([MaTK])
REFERENCES [dbo].[TAIKHOAN] ([MaTK])
GO
ALTER TABLE [dbo].[NHANVIEN] CHECK CONSTRAINT [fk_nhanvien_matk]
GO
/****** Object:  ForeignKey [fk_sanpham_maloaisp]    Script Date: 06/24/2022 20:39:25 ******/
ALTER TABLE [dbo].[SANPHAM]  WITH CHECK ADD  CONSTRAINT [fk_sanpham_maloaisp] FOREIGN KEY([MaLoaiSP])
REFERENCES [dbo].[LOAISP] ([MaLoai])
GO
ALTER TABLE [dbo].[SANPHAM] CHECK CONSTRAINT [fk_sanpham_maloaisp]
GO
