Create Database [QuanLyDL]
USE [QuanLyDL]
GO
/****** Object:  Table [dbo].[BanKiemKe]    Script Date: 11/16/2022 10:03:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BanKiemKe](
	[MaKK] [char](10) NOT NULL,
	[MaNV] [char](10) NULL,
	[TenKK] [nvarchar](50) NULL,
	[ChenhLech] [int] NULL,
	[NgayLap] [date] NULL,
 CONSTRAINT [PK_BanKiemKe] PRIMARY KEY CLUSTERED 
(
	[MaKK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BaoCao]    Script Date: 11/16/2022 10:03:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaoCao](
	[MaBC] [char](10) NOT NULL,
	[MaNV] [char](10) NULL,
	[MaThongKe] [char](10) NULL,
	[LoaiBC] [nvarchar](50) NULL,
	[NgayGioBC] [date] NULL,
	[NoiDungBC] [nvarchar](50) NULL,
 CONSTRAINT [PK_BaoCao] PRIMARY KEY CLUSTERED 
(
	[MaBC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietDonHangBan]    Script Date: 11/16/2022 10:03:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietDonHangBan](
	[MaChiTietDH] [char](10) NOT NULL,
	[MaSP] [char](10) NULL,
	[MaDH] [char](10) NULL,
	[SoLuong] [int] NULL,
	[DonGiaApDung] [int] NULL,
	[ThanhTien] [int] NULL,
 CONSTRAINT [PK_ChiTietDonHangBan] PRIMARY KEY CLUSTERED 
(
	[MaChiTietDH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietKiemKe]    Script Date: 11/16/2022 10:03:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietKiemKe](
	[MaCTKK] [char](10) NOT NULL,
	[MaSP] [char](10) NULL,
	[SLTong] [int] NULL,
	[SLThucTe] [int] NULL,
	[ChenhLech] [int] NULL,
	[LyDo] [nvarchar](50) NULL,
	[MaKK] [char](10) NULL,
 CONSTRAINT [PK_ChiTietKiemKe] PRIMARY KEY CLUSTERED 
(
	[MaCTKK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChucVu]    Script Date: 11/16/2022 10:03:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChucVu](
	[IdChucVu] [char](10) NOT NULL,
	[ChucVu] [nvarchar](50) NULL,
	[ViTri] [nvarchar](50) NULL,
 CONSTRAINT [PK_ChucVu] PRIMARY KEY CLUSTERED 
(
	[IdChucVu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DaiLy]    Script Date: 11/16/2022 10:03:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DaiLy](
	[MaDL] [char](10) NOT NULL,
	[MaLoaiDL] [char](10) NULL,
	[UserName] [char](20) NULL,
	[Password] [char](20) NULL,
	[TenDL] [nvarchar](50) NULL,
	[SDT] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[NgayTiepNhan] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](50) NULL,
 CONSTRAINT [PK_DaiLy] PRIMARY KEY CLUSTERED 
(
	[MaDL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DonHang]    Script Date: 11/16/2022 10:03:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonHang](
	[MaDH] [char](10) NOT NULL,
	[MaDL] [char](10) NULL,
	[NgayLap] [date] NULL,
	[TrangThai] [nvarchar](50) NULL,
	[TongTien] [int] NULL,
 CONSTRAINT [PK_DonHang] PRIMARY KEY CLUSTERED 
(
	[MaDH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GiaoHang]    Script Date: 11/16/2022 10:03:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GiaoHang](
	[MaGH] [char](10) NOT NULL,
	[MaDH] [char](10) NULL,
	[MaNV] [char](10) NULL,
	[TrangThai] [nvarchar](50) NULL,
	[NgayGiao] [date] NULL,
	[SoLuong] [int] NULL,
	[DonGia] [int] NULL,
	[ThanhTien] [int] NULL,
 CONSTRAINT [PK_GiaoHang] PRIMARY KEY CLUSTERED 
(
	[MaGH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kho]    Script Date: 11/16/2022 10:03:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kho](
	[MaKho] [char](10) NOT NULL,
	[MaNV] [char](10) NULL,
	[MaSP] [char](10) NULL,
	[TenKho] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](50) NULL,
 CONSTRAINT [PK_Kho] PRIMARY KEY CLUSTERED 
(
	[MaKho] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiDL]    Script Date: 11/16/2022 10:03:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiDL](
	[MaLoaiDL] [char](10) NOT NULL,
	[TenDaiLy] [nvarchar](50) NULL,
 CONSTRAINT [PK_LoaiDL] PRIMARY KEY CLUSTERED 
(
	[MaLoaiDL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 11/16/2022 10:03:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[MaNV] [char](10) NOT NULL,
	[IdChucVu] [char](10) NULL,
	[UserName] [char](20) NULL,
	[Password] [char](20) NULL,
	[TenNV] [nvarchar](50) NULL,
	[SDT] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
 CONSTRAINT [PK_NhanVien] PRIMARY KEY CLUSTERED 
(
	[MaNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhieuCongNo]    Script Date: 11/16/2022 10:03:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuCongNo](
	[MaCongNo] [char](10) NOT NULL,
	[MaDL] [char](10) NULL,
	[MaDH] [char](10) NULL,
	[TienNo] [int] NULL,
	[HanTra] [date] NULL,
 CONSTRAINT [PK_PhieuCongNo] PRIMARY KEY CLUSTERED 
(
	[MaCongNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 11/16/2022 10:03:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[MaSP] [char](10) NOT NULL,
	[TenSP] [nvarchar](50) NULL,
	[DonViTinh] [nvarchar](50) NULL,
	[HanSH] [date] NULL,
	[NgayXuat] [date] NULL,
	[GiaSP] [int] NULL,
 CONSTRAINT [PK_SanPham] PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThongKe]    Script Date: 11/16/2022 10:03:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongKe](
	[MaThongKe] [char](10) NOT NULL,
	[MaDH] [char](10) NULL,
	[NgayLap] [date] NULL,
	[DonGia] [int] NULL,
	[SoLuong] [nchar](10) NULL,
	[TongTien] [nchar](10) NULL,
 CONSTRAINT [PK_ThongKe] PRIMARY KEY CLUSTERED 
(
	[MaThongKe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[ChucVu] ([IdChucVu], [ChucVu], [ViTri]) VALUES (N'NVGH      ', N'Nhân viên ', N'Giao hàng')
INSERT [dbo].[ChucVu] ([IdChucVu], [ChucVu], [ViTri]) VALUES (N'NVK       ', N'Nhân viên ', N'Kho hàng')
INSERT [dbo].[ChucVu] ([IdChucVu], [ChucVu], [ViTri]) VALUES (N'NVKD      ', N'Nhân viên', N'Kinh doanh')
INSERT [dbo].[ChucVu] ([IdChucVu], [ChucVu], [ViTri]) VALUES (N'NVKT      ', N'Nhân viên', N'Kế toán')
INSERT [dbo].[ChucVu] ([IdChucVu], [ChucVu], [ViTri]) VALUES (N'TPGH      ', N'Trưởng phòng', N'Giao hàng')
INSERT [dbo].[ChucVu] ([IdChucVu], [ChucVu], [ViTri]) VALUES (N'TPK       ', N'Trưởng phòng', N'Kho hàng')
INSERT [dbo].[ChucVu] ([IdChucVu], [ChucVu], [ViTri]) VALUES (N'TPKD      ', N'Trưởng phòng', N'Kinh doanh')
INSERT [dbo].[ChucVu] ([IdChucVu], [ChucVu], [ViTri]) VALUES (N'TPKT      ', N'Trưởng phòng', N'Kế toán')
GO
INSERT [dbo].[DaiLy] ([MaDL], [MaLoaiDL], [UserName], [Password], [TenDL], [SDT], [Email], [NgayTiepNhan], [DiaChi]) VALUES (N'DL01      ', N'L01       ', N'user                ', N'123456              ', N'Tấn Nhã', N'09458564894589', NULL, NULL, NULL)
INSERT [dbo].[DaiLy] ([MaDL], [MaLoaiDL], [UserName], [Password], [TenDL], [SDT], [Email], [NgayTiepNhan], [DiaChi]) VALUES (N'DL02      ', N'L02       ', N'user2               ', N'123456              ', N'Diễm Khang', N'0956489489', NULL, NULL, NULL)
INSERT [dbo].[DaiLy] ([MaDL], [MaLoaiDL], [UserName], [Password], [TenDL], [SDT], [Email], [NgayTiepNhan], [DiaChi]) VALUES (N'DL03      ', N'L03       ', N'user3               ', N'123456              ', N'Diễm Quỳnh', N'0585121564159', NULL, NULL, NULL)
GO
INSERT [dbo].[LoaiDL] ([MaLoaiDL], [TenDaiLy]) VALUES (N'L01       ', N'Đại lý loại 1')
INSERT [dbo].[LoaiDL] ([MaLoaiDL], [TenDaiLy]) VALUES (N'L02       ', N'Đại lý loại 2')
INSERT [dbo].[LoaiDL] ([MaLoaiDL], [TenDaiLy]) VALUES (N'L03       ', N'Đại lý loại 3')
GO
INSERT [dbo].[NhanVien] ([MaNV], [IdChucVu], [UserName], [Password], [TenNV], [SDT], [DiaChi], [Email]) VALUES (N'NV01      ', N'NVKD      ', N'nv01                ', N'123456              ', N'Tấn Nhã', N'0908455325', N'Tp.Hồ Chí Minh, Q.Bình Tân', N'abc@gmail.com')
INSERT [dbo].[NhanVien] ([MaNV], [IdChucVu], [UserName], [Password], [TenNV], [SDT], [DiaChi], [Email]) VALUES (N'NV02      ', N'NVK       ', N'nv02                ', N'123456              ', N'Diễm Khang', N'1234567899', N'Tp.Hồ Chí Minh, Q.Bình Tân', N'abc12@gmail.com')
INSERT [dbo].[NhanVien] ([MaNV], [IdChucVu], [UserName], [Password], [TenNV], [SDT], [DiaChi], [Email]) VALUES (N'NV03      ', N'NVKT      ', N'nv03                ', N'123456              ', N'Diễm Quỳnh', N'123456789', N'Tp.Hồ Chí Minh, Q.Bình Tân', N'abv@gmail.com')
INSERT [dbo].[NhanVien] ([MaNV], [IdChucVu], [UserName], [Password], [TenNV], [SDT], [DiaChi], [Email]) VALUES (N'NV04      ', N'NVGH      ', N'nv04                ', N'123456              ', N'Tấn Nhã 01', N'0123456879', N'Tp.Hồ Chí Minh, Q.Bình Tân', N'abc123@gmail.com')
INSERT [dbo].[NhanVien] ([MaNV], [IdChucVu], [UserName], [Password], [TenNV], [SDT], [DiaChi], [Email]) VALUES (N'NV05      ', N'TPKD      ', N'nv05                ', N'123456              ', N'Tấn Nhã TP', N'02156489645', N'Tp.Hồ Chí Minh, Q.Bình Tân', N'tpkd@gmail.com')
GO
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonViTinh], [HanSH], [NgayXuat], [GiaSP]) VALUES (N'A01       ', N'Nước ngọt Pepsi', N'Thùng', CAST(N'2023-11-30' AS Date), CAST(N'2022-11-17' AS Date), 750000)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonViTinh], [HanSH], [NgayXuat], [GiaSP]) VALUES (N'A02       ', N'Nước ngọt Pepsi Light', N'Thùng', CAST(N'2023-11-30' AS Date), CAST(N'2022-11-17' AS Date), 745000)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonViTinh], [HanSH], [NgayXuat], [GiaSP]) VALUES (N'A03       ', N'Nước ngọt Pepsi không đường', N'Thùng', CAST(N'2023-11-30' AS Date), CAST(N'2022-11-17' AS Date), 740000)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonViTinh], [HanSH], [NgayXuat], [GiaSP]) VALUES (N'A04       ', N'Nước ngọt Coca ', N'Thùng', CAST(N'2023-11-30' AS Date), CAST(N'2022-11-17' AS Date), 755000)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonViTinh], [HanSH], [NgayXuat], [GiaSP]) VALUES (N'A05       ', N'Nước ngọt Coca Light', N'Thùng', CAST(N'2023-10-30' AS Date), CAST(N'2022-11-20' AS Date), 800000)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonViTinh], [HanSH], [NgayXuat], [GiaSP]) VALUES (N'A06       ', N'Nước ngọt Coca không đường', N'Thùng', CAST(N'2023-05-30' AS Date), CAST(N'2022-11-28' AS Date), 850000)
GO
ALTER TABLE [dbo].[BanKiemKe]  WITH CHECK ADD  CONSTRAINT [FK_BanKiemKe_NhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BanKiemKe] CHECK CONSTRAINT [FK_BanKiemKe_NhanVien]
GO
ALTER TABLE [dbo].[BaoCao]  WITH CHECK ADD  CONSTRAINT [FK_BaoCao_NhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BaoCao] CHECK CONSTRAINT [FK_BaoCao_NhanVien]
GO
ALTER TABLE [dbo].[BaoCao]  WITH CHECK ADD  CONSTRAINT [FK_BaoCao_ThongKe] FOREIGN KEY([MaThongKe])
REFERENCES [dbo].[ThongKe] ([MaThongKe])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BaoCao] CHECK CONSTRAINT [FK_BaoCao_ThongKe]
GO
ALTER TABLE [dbo].[ChiTietDonHangBan]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietDonHangBan_DonHang] FOREIGN KEY([MaDH])
REFERENCES [dbo].[DonHang] ([MaDH])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietDonHangBan] CHECK CONSTRAINT [FK_ChiTietDonHangBan_DonHang]
GO
ALTER TABLE [dbo].[ChiTietDonHangBan]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietDonHangBan_SanPham] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietDonHangBan] CHECK CONSTRAINT [FK_ChiTietDonHangBan_SanPham]
GO
ALTER TABLE [dbo].[ChiTietKiemKe]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietKiemKe_BanKiemKe] FOREIGN KEY([MaKK])
REFERENCES [dbo].[BanKiemKe] ([MaKK])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietKiemKe] CHECK CONSTRAINT [FK_ChiTietKiemKe_BanKiemKe]
GO
ALTER TABLE [dbo].[ChiTietKiemKe]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietKiemKe_SanPham] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietKiemKe] CHECK CONSTRAINT [FK_ChiTietKiemKe_SanPham]
GO
ALTER TABLE [dbo].[DaiLy]  WITH CHECK ADD  CONSTRAINT [FK_DaiLy_LoaiDL] FOREIGN KEY([MaLoaiDL])
REFERENCES [dbo].[LoaiDL] ([MaLoaiDL])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DaiLy] CHECK CONSTRAINT [FK_DaiLy_LoaiDL]
GO
ALTER TABLE [dbo].[DonHang]  WITH CHECK ADD  CONSTRAINT [FK_DonHang_DaiLy] FOREIGN KEY([MaDL])
REFERENCES [dbo].[DaiLy] ([MaDL])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DonHang] CHECK CONSTRAINT [FK_DonHang_DaiLy]
GO
ALTER TABLE [dbo].[GiaoHang]  WITH CHECK ADD  CONSTRAINT [FK_GiaoHang_DonHang] FOREIGN KEY([MaDH])
REFERENCES [dbo].[DonHang] ([MaDH])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GiaoHang] CHECK CONSTRAINT [FK_GiaoHang_DonHang]
GO
ALTER TABLE [dbo].[GiaoHang]  WITH CHECK ADD  CONSTRAINT [FK_GiaoHang_NhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GiaoHang] CHECK CONSTRAINT [FK_GiaoHang_NhanVien]
GO
ALTER TABLE [dbo].[Kho]  WITH CHECK ADD  CONSTRAINT [FK_Kho_NhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Kho] CHECK CONSTRAINT [FK_Kho_NhanVien]
GO
ALTER TABLE [dbo].[Kho]  WITH CHECK ADD  CONSTRAINT [FK_Kho_SanPham] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Kho] CHECK CONSTRAINT [FK_Kho_SanPham]
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD  CONSTRAINT [FK_NhanVien_ChucVu] FOREIGN KEY([IdChucVu])
REFERENCES [dbo].[ChucVu] ([IdChucVu])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NhanVien] CHECK CONSTRAINT [FK_NhanVien_ChucVu]
GO
ALTER TABLE [dbo].[PhieuCongNo]  WITH CHECK ADD  CONSTRAINT [FK_PhieuCongNo_DaiLy] FOREIGN KEY([MaDL])
REFERENCES [dbo].[DaiLy] ([MaDL])
GO
ALTER TABLE [dbo].[PhieuCongNo] CHECK CONSTRAINT [FK_PhieuCongNo_DaiLy]
GO
ALTER TABLE [dbo].[PhieuCongNo]  WITH CHECK ADD  CONSTRAINT [FK_PhieuCongNo_DonHang] FOREIGN KEY([MaDH])
REFERENCES [dbo].[DonHang] ([MaDH])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PhieuCongNo] CHECK CONSTRAINT [FK_PhieuCongNo_DonHang]
GO
ALTER TABLE [dbo].[ThongKe]  WITH CHECK ADD  CONSTRAINT [FK_ThongKe_DonHang] FOREIGN KEY([MaDH])
REFERENCES [dbo].[DonHang] ([MaDH])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ThongKe] CHECK CONSTRAINT [FK_ThongKe_DonHang]
GO