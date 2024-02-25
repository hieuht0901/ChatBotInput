using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CauHoi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdThuTuc = table.Column<int>(type: "int", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHoi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NhomThuTuc",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNhom = table.Column<double>(type: "float", nullable: false),
                    TenNhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhomThuTuc", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SubNhomThuTuc",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubNhomThuTuc", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ThuTucHanhChinh",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Idx = table.Column<double>(type: "float", nullable: false),
                    IdNhomThuTuc = table.Column<int>(type: "int", nullable: true),
                    TenThuTuc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortResponse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkResponse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuTucHanhChinh", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ThuTucHanhChinhCompact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdThuTuc = table.Column<int>(type: "int", nullable: false),
                    DiaDiemTiepNhanShort = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiGianTiepNhanShort = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrinhTuThucHienShort = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CachThucThucHienShort = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiGianGiaiQuyetShort = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LePhiShort = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YeuCauShort = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuTucHanhChinhCompact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThuTucHanhChinhExtend",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Idx = table.Column<double>(type: "float", nullable: false),
                    IdThuTuc = table.Column<int>(type: "int", nullable: false),
                    DiaDiemTiepNhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiGianTiepNhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrinhTuThucHien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CachThucThucHien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiGianGiaiQuyet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LePhi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YeuCau = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDonVi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuTucHanhChinhExtend", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CauHoi");

            migrationBuilder.DropTable(
                name: "NhomThuTuc");

            migrationBuilder.DropTable(
                name: "SubNhomThuTuc");

            migrationBuilder.DropTable(
                name: "ThuTucHanhChinh");

            migrationBuilder.DropTable(
                name: "ThuTucHanhChinhCompact");

            migrationBuilder.DropTable(
                name: "ThuTucHanhChinhExtend");
        }
    }
}
