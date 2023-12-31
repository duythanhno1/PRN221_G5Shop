USE [G05Shop]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 10/14/2023 7:36:41 AM ******/

GO
CREATE TABLE [dbo].[Cart](
	[ID] [uniqueidentifier] NOT NULL,
	[ProductID] [bigint] NOT NULL,
	[UserId] [nvarchar](450) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 10/14/2023 7:36:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[ShortName] [nvarchar](max) NOT NULL,
	[isDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 10/14/2023 7:36:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[ID] [uniqueidentifier] NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[isDeleted] [bit] NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ProductId] [bigint] NOT NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 10/14/2023 7:36:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[ID] [uniqueidentifier] NOT NULL,
	[OrderID] [uniqueidentifier] NOT NULL,
	[ProductID] [bigint] NOT NULL,
	[Price] [real] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Amount] [real] NOT NULL,
	[isDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 10/14/2023 7:36:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[ID] [uniqueidentifier] NOT NULL,
	[Total] [real] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[isDeleted] [bit] NOT NULL,
	[UserId] [nvarchar](450) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 10/14/2023 7:36:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Price] [real] NOT NULL,
	[imgPath] [nvarchar](max) NULL,
	[isAvailable] [bit] NOT NULL,
	[CategoryID] [bigint] NULL,
	[isDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 10/14/2023 7:36:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleClaim]    Script Date: 10/14/2023 7:36:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleClaim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_RoleClaim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 10/14/2023 7:36:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[ID] [uniqueidentifier] NOT NULL,
	[Amount] [real] NOT NULL,
	[PreviousHash] [nvarchar](max) NOT NULL,
	[HashValue] [nvarchar](max) NOT NULL,
	[PreviousBalance] [decimal](18, 2) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[Status] [bit] NOT NULL,
	[isDeleted] [bit] NOT NULL,
	[OrderID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserClaim]    Script Date: 10/14/2023 7:36:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserClaim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLogin]    Script Date: 10/14/2023 7:36:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogin](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 10/14/2023 7:36:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 10/14/2023 7:36:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[DoB] [datetime2](7) NULL,
	[Address] [nvarchar](max) NULL,
	[Gender] [nvarchar](max) NULL,
	[isDeleted] [bit] NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserToken]    Script Date: 10/14/2023 7:36:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserToken](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserToken] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231013111015_db', N'7.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231013114738_dbconnect', N'7.0.3')
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([ID], [Name], [ShortName], [isDeleted]) VALUES (1, N'Boba and delicacy', N'BAD', 0)
INSERT [dbo].[Category] ([ID], [Name], [ShortName], [isDeleted]) VALUES (2, N'Beverage and liquor', N'BAL', 0)
INSERT [dbo].[Category] ([ID], [Name], [ShortName], [isDeleted]) VALUES (3, N'Meat and supply', N'MAS ', 0)
INSERT [dbo].[Category] ([ID], [Name], [ShortName], [isDeleted]) VALUES (4, N'Milk and detergent', N'MAD', 0)
INSERT [dbo].[Category] ([ID], [Name], [ShortName], [isDeleted]) VALUES (5, N'Fruit and Vegetable', N'FAV', 0)
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
INSERT [dbo].[Orders] ([ID], [Total], [CreatedDate], [isDeleted], [UserId]) VALUES (N'cd2d69f3-64ef-4f17-911b-0544706dab5b', 2, CAST(N'2023-03-11T23:09:51.1231863' AS DateTime2), 0, N'ef7384bc-e743-4bd4-bc5f-eb0ce86d8ee8')
INSERT [dbo].[Orders] ([ID], [Total], [CreatedDate], [isDeleted], [UserId]) VALUES (N'1ea9461b-ff67-45a6-aa9e-0b9f774356ea', 1, CAST(N'2023-03-12T15:06:11.1472115' AS DateTime2), 0, N'ef7384bc-e743-4bd4-bc5f-eb0ce86d8ee8')
INSERT [dbo].[Orders] ([ID], [Total], [CreatedDate], [isDeleted], [UserId]) VALUES (N'980d46ae-c72d-4dd9-a8cb-2b8f7a1f5b7a', 2, CAST(N'2023-03-12T22:46:01.4721757' AS DateTime2), 0, N'ef7384bc-e743-4bd4-bc5f-eb0ce86d8ee8')
INSERT [dbo].[Orders] ([ID], [Total], [CreatedDate], [isDeleted], [UserId]) VALUES (N'd203ee6f-d41c-4372-a965-59f434e0c7bc', 10, CAST(N'2023-03-11T23:12:56.9298829' AS DateTime2), 0, N'ef7384bc-e743-4bd4-bc5f-eb0ce86d8ee8')
INSERT [dbo].[Orders] ([ID], [Total], [CreatedDate], [isDeleted], [UserId]) VALUES (N'97f4039b-2578-40cb-b33e-6e3d26f9aa0d', 200, CAST(N'2023-03-12T15:09:39.3042253' AS DateTime2), 0, N'ef7384bc-e743-4bd4-bc5f-eb0ce86d8ee8')
INSERT [dbo].[Orders] ([ID], [Total], [CreatedDate], [isDeleted], [UserId]) VALUES (N'0d97595e-f919-44ea-8977-6ece3c030730', -10, CAST(N'2023-03-13T07:36:26.0798062' AS DateTime2), 0, N'ef7384bc-e743-4bd4-bc5f-eb0ce86d8ee8')
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (1, N'Avocado ', 1, N'~/client/image/cache/catalog/product/15-315x315.jpg', 1, 1, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (2, N'Cooking Oil ', 2, N'~/client/image/cache/catalog/product/10-315x315.jpg', 1, 3, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (3, N'Milk', 3, N'~/client/image/cache/catalog/product/1-315x315.jpg', 1, 4, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (4, N'Liquor', 10, N'~/client/image/cache/catalog/product/cream-liqueur-pistachio.jpg', 1, 2, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (5, N'Milk Cow', 2, N'~/client/image/cache/catalog/product/milk-central-lechera-asturiana-1-5-l_215035.jpg', 1, 4, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (6, N'Milk Buffalo', 2, N'~/client/image/cache/catalog/product/milk.jpg', 1, 4, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (7, N'Lettuce', 1, N'~/client/image/cache/catalog/product/17-315x315.jpg', 1, 5, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (8, N'Whiskey', 15, N'~/client/image/cache/catalog/product/images (2).jpg', 1, 2, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (9, N'Yogurt France', 2, N'~/client/image/cache/catalog/product/yogurt.webp', 1, 1, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (10, N'Nuts', 4, N'~/client/image/cache/catalog/product/nuts.webp', 1, 1, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (11, N'Cereal', 2, N'~/client/image/cache/catalog/product/milk-central-lechera-asturiana-1-5-l_215035.jpg', 1, 3, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (12, N'Brocoli', 2, N'~/client/image/cache/catalog/product/5-315x315.jpg', 1, 5, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (13, N'Fish', 6, N'~/client/image/cache/catalog/product/fish.jpg', 1, 3, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (14, N'Tuna', 2, N'~/client/image/cache/catalog/product/fish3.webp', 1, 3, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (15, N'Drinks', 2, N'~/client/image/cache/catalog/product/504341.jpg', 1, 4, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (16, N'Cow Meat', 10, N'~/client/image/cache/catalog/product/fish3.webp', 1, 3, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (17, N'Marshmello', 2, N'~/client/image/cache/catalog/product/fruitcandy.webp', 1, 1, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (18, N'Cookies', 3, N'~/client/image/cache/catalog/product/images (4).jpg', 1, 4, 0)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
INSERT [dbo].[Role] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'2c2ff375-55fd-41bf-be0f-f0450b1717a3', N'Administrator', N'ADMINISTRATOR', N'b931f341-3e5a-47e7-bca9-a0a93325eadd')
INSERT [dbo].[Role] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'673e3a95-8fab-4624-a5b1-094634b91760', N'Customer', N'CUSTOMER', N'5e04773f-94b2-4376-b4b4-a9c1a8439a4a')
INSERT [dbo].[Role] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'a512b486-7d22-4988-afa7-36b594ae1624', N'ShopOwner', N'SHOPOWNER', N'0ac67de2-b751-4277-a0ad-3c7b4fb4d518')
GO
INSERT [dbo].[Transaction] ([ID], [Amount], [PreviousHash], [HashValue], [PreviousBalance], [CreatedDate], [Status], [isDeleted], [OrderID]) VALUES (N'ab67cf01-baf1-4d99-b701-328050e52d5a', 2, N'yYyN/kIibtof/HuyIs6wIhaGowojxi5bz/Dog9pCk1g=', N'iD1MqHRzRS4P5V3bipyk4zBfMDJ911R3CtN2uYON4xY=', CAST(0.00 AS Decimal(18, 2)), CAST(N'2023-03-11T16:09:51.2332604' AS DateTime2), 1, 0, N'cd2d69f3-64ef-4f17-911b-0544706dab5b')
INSERT [dbo].[Transaction] ([ID], [Amount], [PreviousHash], [HashValue], [PreviousBalance], [CreatedDate], [Status], [isDeleted], [OrderID]) VALUES (N'6b2ab28e-0a6e-4f1a-90d6-46cf766ec2ba', 2, N'O03eh+oTS93Snngt1Jwl8Va3oA4d3RlROCFFZs0pofQ=', N'NhYNH3f7XAvYH2yLhQhcHiAqr84Ln9I5Uj/Dy5XGnuM=', CAST(213.00 AS Decimal(18, 2)), CAST(N'2023-03-12T15:46:01.4991648' AS DateTime2), 1, 0, N'980d46ae-c72d-4dd9-a8cb-2b8f7a1f5b7a')
INSERT [dbo].[Transaction] ([ID], [Amount], [PreviousHash], [HashValue], [PreviousBalance], [CreatedDate], [Status], [isDeleted], [OrderID]) VALUES (N'd3c38325-8b5d-4708-bd00-519a1534b11e', 1, N'7kC2S+HY758RORS1HKrKe6GEa8YUgc/PHXfSFYOF/7Q=', N'FTMpqPiKaGpP8HxWb06awOw6s7bGU4qrKqs2qQwo/tM=', CAST(12.00 AS Decimal(18, 2)), CAST(N'2023-03-12T08:06:11.1916537' AS DateTime2), 1, 0, N'1ea9461b-ff67-45a6-aa9e-0b9f774356ea')
INSERT [dbo].[Transaction] ([ID], [Amount], [PreviousHash], [HashValue], [PreviousBalance], [CreatedDate], [Status], [isDeleted], [OrderID]) VALUES (N'744ad804-3ce8-4b59-80bb-5d77f20f803a', 200, N'FTMpqPiKaGpP8HxWb06awOw6s7bGU4qrKqs2qQwo/tM=', N'O03eh+oTS93Snngt1Jwl8Va3oA4d3RlROCFFZs0pofQ=', CAST(13.00 AS Decimal(18, 2)), CAST(N'2023-03-12T08:09:39.3545659' AS DateTime2), 1, 0, N'97f4039b-2578-40cb-b33e-6e3d26f9aa0d')
INSERT [dbo].[Transaction] ([ID], [Amount], [PreviousHash], [HashValue], [PreviousBalance], [CreatedDate], [Status], [isDeleted], [OrderID]) VALUES (N'a787e7b3-92c4-48ce-aedc-d60cc8c80f6b', 10, N'iD1MqHRzRS4P5V3bipyk4zBfMDJ911R3CtN2uYON4xY=', N'7kC2S+HY758RORS1HKrKe6GEa8YUgc/PHXfSFYOF/7Q=', CAST(2.00 AS Decimal(18, 2)), CAST(N'2023-03-11T16:12:56.9961436' AS DateTime2), 1, 0, N'd203ee6f-d41c-4372-a965-59f434e0c7bc')
INSERT [dbo].[Transaction] ([ID], [Amount], [PreviousHash], [HashValue], [PreviousBalance], [CreatedDate], [Status], [isDeleted], [OrderID]) VALUES (N'2c002645-4504-41cb-b72e-f880049fab37', -10, N'NhYNH3f7XAvYH2yLhQhcHiAqr84Ln9I5Uj/Dy5XGnuM=', N'+PLy2VncvqUOQEYaNFy2CFlkCwIYL6bCm4a+UPDGlhs=', CAST(215.00 AS Decimal(18, 2)), CAST(N'2023-03-13T00:36:26.2013223' AS DateTime2), 1, 0, N'0d97595e-f919-44ea-8977-6ece3c030730')
GO
SET IDENTITY_INSERT [dbo].[UserClaim] ON 

INSERT [dbo].[UserClaim] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (1, N'edca39f9-bdd6-4887-a67b-707ef8a2bbdf', N'http://schemas.microsoft.com/ws/2008/06/identity/claims/role', N'Customer')
SET IDENTITY_INSERT [dbo].[UserClaim] OFF
GO
INSERT [dbo].[UserLogin] ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId]) VALUES (N'Google', N'112161882286442630305', N'Google', N'ef7384bc-e743-4bd4-bc5f-eb0ce86d8ee8')
GO
INSERT [dbo].[Users] ([Id], [Name], [DoB], [Address], [Gender], [isDeleted], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'511b66b8-dcef-4c48-a112-652771e72a5f', N'erererere', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'NaN', N'F', 1, N'Administrator', N'ADMINISTRATOR', N'voduythanh.aibot@gmail.com', N'VODUYTHANH.AIBOT@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAELQ4U0W+e+WLz59B4laR6c6X3Niz4IBeFQom/bL4vN/bleggDe9d7WqijYrMHuQsTg==', N'PQ6V4BWSN2SSHNL4JESLZZKFJCSBSJOK', N'793c66c8-fc83-4d35-96c7-4d85412d3bee', N'0938143536', 0, 0, NULL, 1, 0)
INSERT [dbo].[Users] ([Id], [Name], [DoB], [Address], [Gender], [isDeleted], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'e979a372-8c80-4b5d-9670-fb89e6ed1ab3', N'erererere', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'NaN', NULL, 1, N'ShopOwner', N'SHOPOWNER', N'voduythanh.cloud@gmail.com', N'VODUYTHANH.CLOUD@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAELQ4U0W+e+WLz59B4laR6c6X3Niz4IBeFQom/bL4vN/bleggDe9d7WqijYrMHuQsTg==', N'IVX7X2LMGGZF4NY3HJQAOXLSDGL5XEW5', N'ca228000-d31b-4380-b845-a44cbd8c7d42', N'0938143536', 0, 0, NULL, 1, 0)
INSERT [dbo].[Users] ([Id], [Name], [DoB], [Address], [Gender], [isDeleted], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'edca39f9-bdd6-4887-a67b-707ef8a2bbdf', N'duythanh', NULL, NULL, NULL, 0, N'booroly', N'BOOROLY', N'thanhvdce160641@fpt.edu.vn', N'THANHVDCE160641@FPT.EDU.VN', 0, N'AQAAAAEAACcQAAAAELQ4U0W+e+WLz59B4laR6c6X3Niz4IBeFQom/bL4vN/bleggDe9d7WqijYrMHuQsTg==', N'KOVCRMB5ZFQRGXG5QZA3QH5TDINUVN3D', N'3a29a760-0f2d-407b-8a89-6bc3d7106a05', NULL, 0, 0, NULL, 1, 0)
GO

