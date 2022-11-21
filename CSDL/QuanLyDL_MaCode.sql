USE [QuanLyDL]
GO
/****** Object:  Table [dbo].[BanKiemKe]    Script Date: 11/21/2022 12:02:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BanKiemKe](
	[MaKK] [varchar](10) NOT NULL,
	[MaSP] [varchar](10) NULL,
	[MaNV] [varchar](10) NULL,
	[MaKho] [varchar](10) NULL,
	[TenNV] [nvarchar](50) NULL,
	[NgayLap] [datetime] NULL,
	[SLTon] [int] NULL,
	[SLThucTe] [int] NULL,
	[ChenhLech] [int] NULL,
	[LyDo] [nvarchar](50) NULL,
 CONSTRAINT [PK_BanKiemKe] PRIMARY KEY CLUSTERED 
(
	[MaKK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BaoCao]    Script Date: 11/21/2022 12:02:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaoCao](
	[MaBC] [varchar](10) NOT NULL,
	[MaNV] [varchar](10) NULL,
	[LoaiBC] [nvarchar](50) NULL,
	[NgayGioBC] [datetime] NULL,
	[NoiDungBC] [nvarchar](50) NULL,
 CONSTRAINT [PK_BaoCao] PRIMARY KEY CLUSTERED 
(
	[MaBC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChucVu]    Script Date: 11/21/2022 12:02:56 AM ******/
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
/****** Object:  Table [dbo].[DaiLy]    Script Date: 11/21/2022 12:02:56 AM ******/
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
	[NgayTao] [datetime] NULL,
 CONSTRAINT [PK_DaiLy] PRIMARY KEY CLUSTERED 
(
	[MaDL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DanhSachGiaoHang]    Script Date: 11/21/2022 12:02:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DanhSachGiaoHang](
	[MaGH] [int] IDENTITY(1,1) NOT NULL,
	[MaDH] [varchar](10) NULL,
	[MaNV] [varchar](10) NULL,
	[DiemGiao] [nvarchar](50) NULL,
	[TrangThai] [nvarchar](50) NULL,
	[NgayGiao] [datetime] NULL,
	[TienThu] [int] NULL,
	[TenNV] [nvarchar](50) NULL,
	[XacNhan] [nvarchar](50) NULL,
 CONSTRAINT [PK_DanhSachGiaoHang] PRIMARY KEY CLUSTERED 
(
	[MaGH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DonHang]    Script Date: 11/21/2022 12:02:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonHang](
	[MaDH] [varchar](10) NOT NULL,
	[MaSP] [varchar](10) NULL,
	[MaDL] [varchar](10) NULL,
	[MaNV] [varchar](10) NULL,
	[SoLuong] [int] NULL,
	[DonGiaApDung] [int] NULL,
	[ThanhTien] [int] NULL,
	[NgayLap] [datetime] NULL,
	[TrangThai] [nvarchar](50) NULL,
	[DiemGiao] [nvarchar](50) NULL,
	[TenNV] [nvarchar](50) NULL,
	[TinhTrangThanhToan] [nvarchar](50) NULL,
 CONSTRAINT [PK_DonHang] PRIMARY KEY CLUSTERED 
(
	[MaDH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kho]    Script Date: 11/21/2022 12:02:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kho](
	[MaKho] [varchar](10) NOT NULL,
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
/****** Object:  Table [dbo].[LoaiDL]    Script Date: 11/21/2022 12:02:56 AM ******/
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
/****** Object:  Table [dbo].[NhanVien]    Script Date: 11/21/2022 12:02:56 AM ******/
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
/****** Object:  Table [dbo].[PhieuCongNo]    Script Date: 11/21/2022 12:02:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuCongNo](
	[MaCongNo] [int] IDENTITY(1,1) NOT NULL,
	[MaDL] [varchar](10) NULL,
	[MaDH] [varchar](10) NULL,
	[TienNo] [int] NULL,
	[HanTra] [datetime] NULL,
 CONSTRAINT [PK_PhieuCongNo] PRIMARY KEY CLUSTERED 
(
	[MaCongNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 11/21/2022 12:02:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[MaSP] [varchar](10) NOT NULL,
	[TenSP] [nvarchar](50) NULL,
	[DonViTinh] [nvarchar](50) NULL,
	[Gia] [int] NULL,
	[HanSD] [datetime] NULL,
	[NgayXuat] [datetime] NULL,
 CONSTRAINT [PK_SanPham] PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThongKe]    Script Date: 11/21/2022 12:02:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongKe](
	[Ngay] [datetime] NULL,
	[MaDH] [varchar](10) NULL,
	[NgayLap] [datetime] NULL,
	[DonGia] [int] NULL,
	[SoLuong] [int] NULL,
	[TongTien] [int] NULL,
	[MaThongKe] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_ThongKe] PRIMARY KEY CLUSTERED 
(
	[MaThongKe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[BaoCao] ([MaBC], [MaNV], [LoaiBC], [NgayGioBC], [NoiDungBC]) VALUES (N'BC01', N'NV01', N'Báo cáo đơn hàng CD02', CAST(N'2022-11-20T00:00:00.000' AS DateTime), N'Sản phẩm bị lỗi')
GO
INSERT [dbo].[ChucVu] ([IdChucVu], [ChucVu], [ViTri]) VALUES (N'NVGH', N'Nhân Viên', N'Giao Hàng')
INSERT [dbo].[ChucVu] ([IdChucVu], [ChucVu], [ViTri]) VALUES (N'NVK', N'Nhân Viên', N'Kho hàng')
INSERT [dbo].[ChucVu] ([IdChucVu], [ChucVu], [ViTri]) VALUES (N'NVKD', N'Nhân Viên', N'Kinh Doanh')
INSERT [dbo].[ChucVu] ([IdChucVu], [ChucVu], [ViTri]) VALUES (N'NVKT', N'Nhân Viên', N'Kế Toán')
GO
INSERT [dbo].[DaiLy] ([MaDL], [MaLoaiDL], [UserName], [Password], [TenDL], [SDT], [DiaChi], [Email], [NgayTao]) VALUES (N'MDL01', N'DL01', N'user01', N'123456', N'Tấn Nhã', N'0908455325', N'84a, PDC, P01, Q.Bình Thạnh', N'avcx@gmail.com', CAST(N'2022-11-20T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[DonHang] ([MaDH], [MaSP], [MaDL], [MaNV], [SoLuong], [DonGiaApDung], [ThanhTien], [NgayLap], [TrangThai], [DiemGiao], [TenNV], [TinhTrangThanhToan]) VALUES (N'DH06', N'SP001', N'MDL01', N'NV01', 25, 100000, 2500000, CAST(N'2022-12-09T00:00:00.000' AS DateTime), N'Chưa xét duyệt', N'123, Nguyễn Trãi', N'Tấn Nhã Kinh doanh', N'Chưa thanh toán')
INSERT [dbo].[DonHang] ([MaDH], [MaSP], [MaDL], [MaNV], [SoLuong], [DonGiaApDung], [ThanhTien], [NgayLap], [TrangThai], [DiemGiao], [TenNV], [TinhTrangThanhToan]) VALUES (N'DH10', N'SP001', N'MDL01', N'NV01', 7, 120000, NULL, NULL, N'Chưa xét duyệt', N'123, Nguyễn Văn Kha', N'Tấn Nhã Kinh doanh', N'Đã thanh toán')
INSERT [dbo].[DonHang] ([MaDH], [MaSP], [MaDL], [MaNV], [SoLuong], [DonGiaApDung], [ThanhTien], [NgayLap], [TrangThai], [DiemGiao], [TenNV], [TinhTrangThanhToan]) VALUES (N'DH112', N'SP002', N'MDL01', N'NV01', 75, 100000, 7500000, CAST(N'2022-11-20T00:00:00.000' AS DateTime), NULL, N'123, Nguyễn Văn Kha', N'Tấn Nhã Kinh doanh', N'Chưa thanh toán')
INSERT [dbo].[DonHang] ([MaDH], [MaSP], [MaDL], [MaNV], [SoLuong], [DonGiaApDung], [ThanhTien], [NgayLap], [TrangThai], [DiemGiao], [TenNV], [TinhTrangThanhToan]) VALUES (N'DH13', N'SP001', N'MDL01', N'NV01', 100, 750000, 75000000, CAST(N'2022-11-20T00:00:00.000' AS DateTime), NULL, N'123, Nguyễn Văn Kha', N'Tấn Nhã Kinh doanh', N'Đã thanh toán')
INSERT [dbo].[DonHang] ([MaDH], [MaSP], [MaDL], [MaNV], [SoLuong], [DonGiaApDung], [ThanhTien], [NgayLap], [TrangThai], [DiemGiao], [TenNV], [TinhTrangThanhToan]) VALUES (N'DH18', N'SP001', N'MDL01', N'NV01', 5, 10000, 50000, CAST(N'2022-11-20T00:00:00.000' AS DateTime), NULL, N'123, Lê Trực', N'Tấn Nhã Kinh doanh', N'Chưa thanh toán')
GO
INSERT [dbo].[LoaiDL] ([MaLoaiDL], [TenDaiLy]) VALUES (N'DL01', N'Đại lý lớn')
INSERT [dbo].[LoaiDL] ([MaLoaiDL], [TenDaiLy]) VALUES (N'DL02', N'Đại lý bé')
INSERT [dbo].[LoaiDL] ([MaLoaiDL], [TenDaiLy]) VALUES (N'DL03', N'Đại lý vừa')
INSERT [dbo].[LoaiDL] ([MaLoaiDL], [TenDaiLy]) VALUES (N'DL04', N'Đại lý nhỏ')
GO
INSERT [dbo].[NhanVien] ([MaNV], [IdChucVu], [UserName], [Password], [TenNV], [SDT], [DiaChi], [Email]) VALUES (N'NV01', N'NVKD', N'user01', N'123456', N'Tấn Nhã Kinh doanh', N'09026489465', N'84a, Phan Đình Phùng , P01, Q.01', N'abc@gmail.com')
INSERT [dbo].[NhanVien] ([MaNV], [IdChucVu], [UserName], [Password], [TenNV], [SDT], [DiaChi], [Email]) VALUES (N'NV02', N'NVK', N'user02', N'123456', N'Diễm Khang', N'090456465489', N'123, Lê Trực, Q.01', N'diemkhang@gmail.com')
INSERT [dbo].[NhanVien] ([MaNV], [IdChucVu], [UserName], [Password], [TenNV], [SDT], [DiaChi], [Email]) VALUES (N'NV03', N'NVGH', N'user03', N'123456', N'Tấn Nhã Giao hàng', N'0905421564564', N'746, Điện Biên Phủ', N'tannha@gmail.com')
GO
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonViTinh], [Gia], [HanSD], [NgayXuat]) VALUES (N'SP001', N'Nước ngọt Pepsi', N'Thùng', 1250000, CAST(N'2022-12-08T00:00:00.000' AS DateTime), CAST(N'2022-11-20T00:00:00.000' AS DateTime))
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonViTinh], [Gia], [HanSD], [NgayXuat]) VALUES (N'SP002', N'Nước ngọt Coca Cola', N'Lon', 7500, CAST(N'2022-12-24T00:00:00.000' AS DateTime), CAST(N'2022-11-20T00:00:00.000' AS DateTime))
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [DonViTinh], [Gia], [HanSD], [NgayXuat]) VALUES (N'SP01', N'Nướt ngọt number One', N'Thùng', 900000, CAST(N'2022-12-30T00:00:00.000' AS DateTime), CAST(N'2022-11-20T00:00:00.000' AS DateTime))
GO
ALTER TABLE [dbo].[BanKiemKe]  WITH CHECK ADD  CONSTRAINT [FK_BanKiemKe_NhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BanKiemKe] CHECK CONSTRAINT [FK_BanKiemKe_NhanVien]
GO
ALTER TABLE [dbo].[BanKiemKe]  WITH CHECK ADD  CONSTRAINT [FK_BanKiemKe_SanPham] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BanKiemKe] CHECK CONSTRAINT [FK_BanKiemKe_SanPham]
GO
ALTER TABLE [dbo].[BaoCao]  WITH CHECK ADD  CONSTRAINT [FK_BaoCao_NhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BaoCao] CHECK CONSTRAINT [FK_BaoCao_NhanVien]
GO
ALTER TABLE [dbo].[DaiLy]  WITH CHECK ADD  CONSTRAINT [FK_DaiLy_LoaiDL] FOREIGN KEY([MaLoaiDL])
REFERENCES [dbo].[LoaiDL] ([MaLoaiDL])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DaiLy] CHECK CONSTRAINT [FK_DaiLy_LoaiDL]
GO
ALTER TABLE [dbo].[DanhSachGiaoHang]  WITH CHECK ADD  CONSTRAINT [FK_DanhSachGiaoHang_DonHang] FOREIGN KEY([MaDH])
REFERENCES [dbo].[DonHang] ([MaDH])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DanhSachGiaoHang] CHECK CONSTRAINT [FK_DanhSachGiaoHang_DonHang]
GO
ALTER TABLE [dbo].[DanhSachGiaoHang]  WITH CHECK ADD  CONSTRAINT [FK_DanhSachGiaoHang_NhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[DanhSachGiaoHang] CHECK CONSTRAINT [FK_DanhSachGiaoHang_NhanVien]
GO
ALTER TABLE [dbo].[DonHang]  WITH CHECK ADD  CONSTRAINT [FK_DonHang_DaiLy] FOREIGN KEY([MaDL])
REFERENCES [dbo].[DaiLy] ([MaDL])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DonHang] CHECK CONSTRAINT [FK_DonHang_DaiLy]
GO
ALTER TABLE [dbo].[DonHang]  WITH CHECK ADD  CONSTRAINT [FK_DonHang_NhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DonHang] CHECK CONSTRAINT [FK_DonHang_NhanVien]
GO
ALTER TABLE [dbo].[DonHang]  WITH CHECK ADD  CONSTRAINT [FK_DonHang_SanPham] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DonHang] CHECK CONSTRAINT [FK_DonHang_SanPham]
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
