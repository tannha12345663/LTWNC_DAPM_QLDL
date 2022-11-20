USE [QuanLyDL]
GO
/****** Object:  Table [dbo].[BanKiemKe]    Script Date: 11/19/2022 8:11:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BanKiemKe](
	[MaKK] [varchar](10) NOT NULL,
	[MaNV] [varchar](10) NULL,
	[TenKK] [nvarchar](50) NULL,
	[ChenhLech] [int] NULL,
	[NgayLap] [datetime] NULL,
 CONSTRAINT [PK_BanKiemKe] PRIMARY KEY CLUSTERED 
(
	[MaKK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BaoCao]    Script Date: 11/19/2022 8:11:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaoCao](
	[MaBC] [varchar](10) NOT NULL,
	[MaNV] [varchar](10) NULL,
	[MaThongKe] [varchar](10) NULL,
	[LoaiBC] [nvarchar](50) NULL,
	[NgayGioBC] [date] NULL,
	[NoiDungBC] [nvarchar](50) NULL,
 CONSTRAINT [PK_BaoCao] PRIMARY KEY CLUSTERED 
(
	[MaBC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietDonHang]    Script Date: 11/19/2022 8:11:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietDonHang](
	[MaChiTietDH] [varchar](10) NOT NULL,
	[MaSP] [varchar](10) NULL,
	[MaDH] [varchar](10) NULL,
	[SoLuong] [int] NULL,
	[DonGiaApDung] [int] NULL,
	[ThanhTien] [int] NULL,
 CONSTRAINT [PK_ChiTietDonHang] PRIMARY KEY CLUSTERED 
(
	[MaChiTietDH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietKiemKe]    Script Date: 11/19/2022 8:11:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietKiemKe](
	[MaCTKK] [varchar](10) NOT NULL,
	[MaSP] [varchar](10) NULL,
	[SLTon] [int] NULL,
	[SLThucTe] [int] NULL,
	[ChenhLech] [int] NULL,
	[LyDo] [nvarchar](50) NULL,
	[MaKK] [varchar](10) NULL,
 CONSTRAINT [PK_ChiTietKiemKe] PRIMARY KEY CLUSTERED 
(
	[MaCTKK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChucVu]    Script Date: 11/19/2022 8:11:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChucVu](
	[IdChucVu] [varchar](10) NOT NULL,
	[ChucVu] [nvarchar](50) NULL,
	[ViTri] [nvarchar](50) NULL,
 CONSTRAINT [PK_ChucVu] PRIMARY KEY CLUSTERED 
(
	[IdChucVu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DaiLy]    Script Date: 11/19/2022 8:11:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DaiLy](
	[MaDL] [varchar](10) NOT NULL,
	[MaLoaiDL] [varchar](10) NULL,
	[UserName] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[TenDL] [nvarchar](50) NULL,
	[SDT] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[NgayTiepNhan] [datetime] NULL,
 CONSTRAINT [PK_DaiLy] PRIMARY KEY CLUSTERED 
(
	[MaDL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DonHang]    Script Date: 11/19/2022 8:11:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonHang](
	[MaDH] [varchar](10) NOT NULL,
	[MaDL] [varchar](10) NULL,
	[NgayLap] [datetime] NULL,
	[TrangThai] [nvarchar](50) NULL,
	[TongTien] [int] NULL,
 CONSTRAINT [PK_DonHang] PRIMARY KEY CLUSTERED 
(
	[MaDH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GiaoHang]    Script Date: 11/19/2022 8:11:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GiaoHang](
	[MaGH] [varchar](10) NOT NULL,
	[MaDH] [varchar](10) NULL,
	[MaNV] [varchar](10) NULL,
	[TrangThai] [nvarchar](50) NULL,
	[NgayGiao] [datetime] NULL,
	[SoLuong] [int] NULL,
	[DonGia] [int] NULL,
	[ThanhTien] [int] NULL,
 CONSTRAINT [PK_GiaoHang] PRIMARY KEY CLUSTERED 
(
	[MaGH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kho]    Script Date: 11/19/2022 8:11:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kho](
	[MaKho] [nchar](10) NOT NULL,
	[MaNV] [varchar](10) NULL,
	[MaSP] [varchar](10) NULL,
	[TenKho] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](50) NULL,
	[SLTon] [int] NULL,
 CONSTRAINT [PK_Kho] PRIMARY KEY CLUSTERED 
(
	[MaKho] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiDL]    Script Date: 11/19/2022 8:11:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiDL](
	[MaLoaiDL] [varchar](10) NOT NULL,
	[TenDaiLy] [nvarchar](50) NULL,
 CONSTRAINT [PK_LoaiDL] PRIMARY KEY CLUSTERED 
(
	[MaLoaiDL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 11/19/2022 8:11:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[MaNV] [varchar](10) NOT NULL,
	[IdChucVu] [varchar](10) NULL,
	[UserName] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
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
/****** Object:  Table [dbo].[PhieuCongNo]    Script Date: 11/19/2022 8:11:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuCongNo](
	[MaCongNo] [varchar](10) NOT NULL,
	[MaDH] [varchar](10) NULL,
	[TienNo] [int] NULL,
	[HanTra] [datetime] NULL,
 CONSTRAINT [PK_PhieuCongNo] PRIMARY KEY CLUSTERED 
(
	[MaCongNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 11/19/2022 8:11:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[MaSP] [varchar](10) NOT NULL,
	[TenSP] [nvarchar](50) NULL,
	[DonViTinh] [nvarchar](50) NULL,
	[Gia] [int] NULL,
	[HanSH] [datetime] NULL,
	[NgayXuat] [datetime] NULL,
 CONSTRAINT [PK_SanPham] PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThongKe]    Script Date: 11/19/2022 8:11:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongKe](
	[MaThongKe] [varchar](10) NOT NULL,
	[MaDH] [varchar](10) NULL,
	[NgayLap] [datetime] NULL,
	[DonGia] [int] NULL,
	[SoLuong] [int] NULL,
	[TongTien] [int] NULL,
 CONSTRAINT [PK_ThongKe] PRIMARY KEY CLUSTERED 
(
	[MaThongKe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[ChucVu] ([IdChucVu], [ChucVu], [ViTri]) VALUES (N'NVGH', N'Nhân Viên', N'Giao Hàng')
INSERT [dbo].[ChucVu] ([IdChucVu], [ChucVu], [ViTri]) VALUES (N'NVK', N'Nhân Viên ', N'Kho')
INSERT [dbo].[ChucVu] ([IdChucVu], [ChucVu], [ViTri]) VALUES (N'NVKD', N'Nhân Viên', N'Kinh doanh')
INSERT [dbo].[ChucVu] ([IdChucVu], [ChucVu], [ViTri]) VALUES (N'NVKT', N'Nhân Viên', N'Kế Toán')
GO
INSERT [dbo].[DaiLy] ([MaDL], [MaLoaiDL], [UserName], [Password], [TenDL], [SDT], [DiaChi], [Email], [NgayTiepNhan]) VALUES (N'DL0045', N'DL01', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2022-11-18T00:00:00.000' AS DateTime))
INSERT [dbo].[DaiLy] ([MaDL], [MaLoaiDL], [UserName], [Password], [TenDL], [SDT], [DiaChi], [Email], [NgayTiepNhan]) VALUES (N'DL10', N'DL01', N'Tannha', N'23', N'Trương Tấn Nhã 01', N'0902509292dsad', N'123a, Phan Đình Phùng, P17, Q.Bình Thạnh', N'NhaTruong@gmail.com', CAST(N'2022-11-08T00:00:00.000' AS DateTime))
INSERT [dbo].[DaiLy] ([MaDL], [MaLoaiDL], [UserName], [Password], [TenDL], [SDT], [DiaChi], [Email], [NgayTiepNhan]) VALUES (N'DL18', N'DL02', N'Tannha123456', N'123456789', N'Trương Tấn Nhã 01', N'0902509292', N'123a, Phan Đình Phùng, P17, Q.Bình Thạnh', N'NhaTruong@gmail.com', NULL)
GO
INSERT [dbo].[LoaiDL] ([MaLoaiDL], [TenDaiLy]) VALUES (N'DL01', N'Đại lý lớn')
INSERT [dbo].[LoaiDL] ([MaLoaiDL], [TenDaiLy]) VALUES (N'DL02', N'Đại lý bé')
INSERT [dbo].[LoaiDL] ([MaLoaiDL], [TenDaiLy]) VALUES (N'DL03', N'Đại lý vừa')
INSERT [dbo].[LoaiDL] ([MaLoaiDL], [TenDaiLy]) VALUES (N'DL04', N'Đại lý trung bình')
GO
INSERT [dbo].[NhanVien] ([MaNV], [IdChucVu], [UserName], [Password], [TenNV], [SDT], [DiaChi], [Email]) VALUES (N'NV01', N'NVKD', N'user01', N'123456', N'Tấn Nhã', N'0908455325', N'84a, Phan Đình Phụng, P01, Q.Bình Thạnh', N'abc@gmail.com')
INSERT [dbo].[NhanVien] ([MaNV], [IdChucVu], [UserName], [Password], [TenNV], [SDT], [DiaChi], [Email]) VALUES (N'NV02', N'NVK', N'user02', N'123456', N'Diễm Khang', N'0123456789', N'123, Trần Hưng Đạo', N'abv2@gmail.com')
INSERT [dbo].[NhanVien] ([MaNV], [IdChucVu], [UserName], [Password], [TenNV], [SDT], [DiaChi], [Email]) VALUES (N'NV03', N'NVKT', N'user03', N'123456', N'Diễm Quỳnh', N'0123456789', N'112, Bùi Đình Túy, P05, Q.Bình Chánh', N'diemquynh@gmail.com')
INSERT [dbo].[NhanVien] ([MaNV], [IdChucVu], [UserName], [Password], [TenNV], [SDT], [DiaChi], [Email]) VALUES (N'NV04', N'NVGH', N'user04', N'123456', N'Tấn Nhã giao hàng', N'01237418529', N'99, Lê lợi, P.15,Q.01', N'tannha@gmail.com')
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
ALTER TABLE [dbo].[ChiTietDonHang]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietDonHang_DonHang] FOREIGN KEY([MaDH])
REFERENCES [dbo].[DonHang] ([MaDH])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietDonHang] CHECK CONSTRAINT [FK_ChiTietDonHang_DonHang]
GO
ALTER TABLE [dbo].[ChiTietDonHang]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietDonHang_SanPham] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietDonHang] CHECK CONSTRAINT [FK_ChiTietDonHang_SanPham]
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
