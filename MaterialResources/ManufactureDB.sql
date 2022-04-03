USE [master]
GO
/****** Object:  Database [ManufactureDB]    Script Date: 26.03.2022 17:46:12 ******/
CREATE DATABASE [ManufactureDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ManufactureDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ManufactureDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ManufactureDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ManufactureDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ManufactureDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ManufactureDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ManufactureDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ManufactureDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ManufactureDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ManufactureDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ManufactureDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ManufactureDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ManufactureDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ManufactureDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ManufactureDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ManufactureDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ManufactureDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ManufactureDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ManufactureDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ManufactureDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ManufactureDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ManufactureDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ManufactureDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ManufactureDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ManufactureDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ManufactureDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ManufactureDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [ManufactureDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ManufactureDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ManufactureDB] SET  MULTI_USER 
GO
ALTER DATABASE [ManufactureDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ManufactureDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ManufactureDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ManufactureDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ManufactureDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ManufactureDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ManufactureDB] SET QUERY_STORE = OFF
GO
USE [ManufactureDB]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 26.03.2022 17:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 26.03.2022 17:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Timestamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_dbo.Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 26.03.2022 17:46:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Fullname] [nvarchar](50) NOT NULL,
	[RoleId] [int] NOT NULL,
	[Timestamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_dbo.User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'202202261441134_InitialCreate', N'ManufactureEFExample.Model.ManufactureEntities', 0x1F8B0800000000000400ED59DD4E2B3710BEAFD47758ED65C5C91250A51625E7880652A102418473D43BE4EC4E8255AFBDB5BD9C44559FAC177DA4BE42C7FBBFF62E093980DAAA4242893DF3CD8F67C633CE5F7FFC39FAB08E99F7085251C1C7FE7070E87BC0431151BE1AFBA95EBEFBCEFFF0FEEBAF46E751BCF63E9574C7860E39B91AFB0F5A272741A0C20788891AC434944289A51E84220E482482A3C3C3EF83E1300084F011CBF346B729D73486EC0B7E9D081E42A253C2AE44044C15EBB833CF50BD6B12834A480863FF8AF07449429D4A389F9EAF499C3018646CBE77CA28418DE6C096BE4738179A68D4F7E4A382B99682AFE6092E1076B74900E996842928EC38A9C97735E9F0C89814D48C2554982A2DE267020E8F0B1F0536FB5E9EF62B1FA217CFD1DB7A63ACCE3C39F66F0543C36D412713260D518F9333140A6A60B80FBC2E9A832A443092CCDF8137499921197348B524ECC0BB49178C863FC1E64EFC027CCC53C69ACAA2BAB8D75AC0A51B2912907A730BCBC2848BC8F782365F6033566C0D9EDCC00BAE8F8F7CEF1A859305832A161ACE986B21E147E0208986E886680D921B0CC8BCE948B76499FFA5340C3ECC271F5DB6BE04BED20F63FF5B4CA0295D43542E140A7CE414B30F79B44CA143414BE83579A4AB4C5F4B3C86BCF4BD5B60D9A67AA0499E1703B3719F9FFF548AD87CCAA9B3C5FB3B2257A0517161EFCC452A434B83515087D6930197ABB36FC019EEFF036E4BC05D8A15E5AF1F7196D41BA2D46721A337173C4572FE2639660936C9B0FD5C77CDD33C139F9FA76536BA795A66F02E797AAA94086926BA91A8B9D8B601E73CF27A75C80FA0D41ACF0053902698742872EC7FE378A30BAC2A3C35586E7D1B6CE8DB993AE367C04083771AE697ED84A89044EE19A00FA2F60A263748935D84610BA2B05C50AEDD4A40794813C2FA54B618762C1E46A10ADADE398304B8C9FB3E9FEF22B30C54576E056F39689B3F46412360DC7A8F3C1A3940160A340B7651CA3BEE00B4ADB80654919176B018EC39E856C6D4F1DC0A1627D0DABC79783ABCF9B2C5DB30B50D50646563DF4959DBE34F654FA563A99E735E4FA54B83BBD30341DB8C8E9A509D5ADD750779DB5DB6E7414F7F3EBA22498245B7D1AF172BDE3C6FD627EFE6CFEF5EE31C23085547135B695B49C25B93ACC0DA45D1A8E9944AA5CF88260B626AF2248A1DB2AE18ED89A15262230CDD932A23AB24369FDD6C680F2E832EACDA9953B42F36B5206B10EC0377D8B2A98930223B7A91896069CCFB4B523F77DECE36F9F315176114587A3BE5C7F19253A7DB1EDFE93CB2DC79A1F3E8C2DAE13CBAD95EE73C8A6EAF09502CED8E51F76E4D987A7577A4BA196B22D5ABBB2395975613A7EF227BE558730AA44D5249AF0AA55510474571DAFEAAE154AB9CC4F7D03D8F3432956ABE511AE2812118CC7F651346D1DE9A00C3992E41E97CC8F18F0E8747D683C83FE77122502A623BBC50BCF994468D47B7CE61CF1C189A2F01FC91C8F08148674E7912F37963F67FC36BAD71763FB76D9F565F0AD71E465F0AB73D6B667E76FAE70BECE4D763FFB78CE5C4BBF8F93EE73AF06612EBC28977E8FDBE677CED331EE65DE85B4E71EDF6788F7174AF31B0A7677B95D1EFDF31EEB91DF7170C72F9FD37F6A385C013CD23F7CB47BC2ED4371CFE9A16F78E6C1D11BD65447C9921CFED5530421ABFD360642ABAAA21CCAF361CC2566C5434177C29CA10B5342A49AC5277059A441838A75253D396E376084A656F7A9F084B91E43C5E4074C167A94E528D2643BC60AD675113EA4FC9CF26D9B6CEA359923DBCBD8409A826451360C67F48298B2ABDA71D15B707C2E45071899AB3D4E6325D6D2AA46BC177042ADC57A5FE1DE07C83606AC6E7E411F6D10D43EF125624DC942D673FC8F68368BB7D7446C94A92581518353F7EC5188EE2F5FBBF01DA1FB423AE1C0000, N'6.4.4')
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'202202261510139_Final', N'ManufactureEFExample.Model.ManufactureEntities', 0x1F8B0800000000000400ED59DD6EDB3614BE1FB077107439A4569C604017D82D52271E8C357110A745EF025A3A768851A44A52A9BD614FB68B3DD25E6187FA9728C58EE366593104086CF2FCF39C8F3CC77FFFF9D7E0ED2A64CE3D4845051FBAFDDEA1EB00F74540F972E8C67AF1EAB5FBF6CDF7DF0DCE8370E57CCCE98E0D1D72723574EFB48E4E3C4FF9771012D50BA92F85120BDDF345E89140784787873F79FDBE0728C245598E33B88EB9A621245FF0EB48701F221D13762102602A5BC79D5922D5B92421A888F830742F088F17C4D7B184F3F1F98A8411835EC2E63AA78C12B468066CE13A8473A189467B4F3E28986929F87216E1026137EB08906E419882CC8F93927C5B970E8F8C4B5EC9988BF263A545F84881FDE32C465E937DA748BB450C318AE7186DBD365E27911CBAD782A1E34D452723260D51479013291454CF701F386D3407458A602699BF036714334332E4106B49D8817315CF19F57F81F58DF815F890C78C558D457371AFB6804B57524420F5FA1A16990B93C075BC3A9FD7642CD82A3CA98313AE8F8F5CE7129593398322172AC1986921E167E0208986E08A680D921B199044D3D2DED065FEE7DA30F9B09E5C0CD9EA3DF0A5BE1BBA3F62018DE90A827C2133E003A7587DC8A3650C2D063EACF4068B4A693C8A5CF33BCA89445B4D85C5526269AF2F12F189EE9A45AFDB0DDA2144231146B106FB802EC93D5D26AC0DBBB140A5EB5C034B36D51D8DD22AEE998DDB345BC75284E6534A9D2CDEDE10B9048DCE8AE6CE4CC4D26FC46BE09585F06079A4E6EC5A1E86FBFFF2D890A9EFC592F267AF8F2BA2D417218367573C4672FE6F20822986CDE7FA6DA24A8A1B8F47951C3B6C54C9F1661B5439554AF834515D8195546DDD81731E389D36A4E9925B8DB145C0A0114204AA1CBA3F58D1681356C064292CF5BE2EACEF367165CACF808106E7D44F1F3223A27C12D819833108EA2B0845200D16108669A210DC28D7366E51EED388B02E931B0C5B429D31A810DDDC398308B841A9AE986FA3332F2B5B6F21BE11A04DF118789584B16F27E4D1C8013233A07ABD64174FCB8D85BE659796CAF0A3992C46F60C74AD62CA7CAE258B956875DE343D2DDE74B9C15B71B52E20ABCACABE55B2CD883F543D858DB979D6793D542E15EED6087875375A30A138B5B2A3F1D296266F7DBC8EDE677041A208AF884A2F94AD38B3B4111ABD9A3DBE330853199EAF5A1A84C2DA42132231594263D7407100632A953E239ACC89C1EF51105A646D39DA9143B9C64A1ADA279567564E6C3EDBD5506F0A7B6DB2CA608ED1BFD0604172E9340FDC624B3A52C2886C79398D048B43DE0D49DDDC69AB50E54F57B69750B9A1AB622ACBB6AC81D78881056556C42DCCAF9FDE56679BD4E19ECEB64DD61667DBCEF675CE367BE75605644BDBCB285FAD5531E5EAF692CA67685552B9BABDA4FC02ACCAE9BA14BBA5BCB8BCB580BB4952682F00BC01D4830C34374FB22C144D495C0783744F0383A0B3B5D210F60C416FF6998D18457F4B022C0DBAC048A5ADA27B74D83F6A0CC15ECE40CA532A605B4CA59EBDD7A526A21BBBD947B64CD5E90FBF27D2BF23D2EAF69EDA8649F1259BDC763A50364D0F2A7BDC64E4DB38A2DA04A23C23EB0D3DC1D7FC6AE8FE9EB09D3893B34FB7C953F134D67742D2DF12AA03672AB1204F9CFE813351D8B27F8E91F6065FDCCE1F4F3DF7E6D0626FB61EEDDFD6E69C635FB95F1F6324F9B0D1F94FB72957E1EFA1F1EFE517DD2E8383B43F79CEFEBEDE38ED30A8D86940D0F19AFF2A4381FFC620C0EEC59ED0E2A72F90A11BCC059E689AB94F6FFEDBA43EE358A0EA716733DF92D11B8607FB69FFEDD7226648E5D751CC4C4597A508F35B2907BF961B05CD842F449EA20D8B729206BC5D80260126CEA9D4D43459B8ED8352C96CFA236131929C877308267C1A6B04357419C239AB8DF74DAA3FA43F9971D46D1E4CA36424BB0F17D04C8A2EC094BF8B290B0ABBC72D88DB21C2D45006E3E62CB581F3E5BA907429F89682B2F015A57F03D8ADA23035E533720FBBD886A9F71E96C45FE78FFE6E219B0FA21EF6C119254B494295C928F9F12BE67010AEDEFC03E79A8CF324200000, N'6.4.4')
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [Name]) VALUES (1, N'Директор')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (2, N'Администратор')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (3, N'Бухгалтер')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (4, N'Заказчик')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Login], [Password], [Fullname], [RoleId]) VALUES (2, N'fasGJAL0', N'gads34ju4', N'Андрюгова Кристинаfsa', 3)
INSERT [dbo].[User] ([Id], [Login], [Password], [Fullname], [RoleId]) VALUES (3, N'FASYfs26fsafsa', N'g2ds62321fas', N'Ловина Анна', 2)
INSERT [dbo].[User] ([Id], [Login], [Password], [Fullname], [RoleId]) VALUES (4, N'gdsJFGS4', N'j2gf3n46', N'Губернива Полина', 4)
INSERT [dbo].[User] ([Id], [Login], [Password], [Fullname], [RoleId]) VALUES (5, N'sadyFS4', N'j4fg5du6', N'Бофова Елизавета', 4)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IDX_User_Authorization]    Script Date: 26.03.2022 17:46:12 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IDX_User_Authorization] ON [dbo].[User]
(
	[Login] ASC,
	[Password] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoleId]    Script Date: 26.03.2022 17:46:12 ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[User]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_dbo.User_dbo.Role_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_dbo.User_dbo.Role_RoleId]
GO
USE [master]
GO
ALTER DATABASE [ManufactureDB] SET  READ_WRITE 
GO
