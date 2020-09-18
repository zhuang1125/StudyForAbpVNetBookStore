using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Acme.BookStore.Migrations
{
    public partial class Add_FileManager_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EasyAbpFileManagementFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: true),
                    FileContainerName = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    MimeType = table.Column<string>(nullable: true),
                    FileType = table.Column<int>(nullable: false),
                    SubFilesQuantity = table.Column<int>(nullable: false),
                    ByteSize = table.Column<long>(nullable: false),
                    Hash = table.Column<string>(nullable: true),
                    BlobName = table.Column<string>(nullable: true),
                    OwnerUserId = table.Column<Guid>(nullable: true),
                    Flag = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpFileManagementFiles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EasyAbpFileManagementFiles_BlobName",
                table: "EasyAbpFileManagementFiles",
                column: "BlobName");

            migrationBuilder.CreateIndex(
                name: "IX_EasyAbpFileManagementFiles_Hash",
                table: "EasyAbpFileManagementFiles",
                column: "Hash");

            migrationBuilder.CreateIndex(
                name: "IX_EasyAbpFileManagementFiles_FileName_OwnerUserId_FileContainerName",
                table: "EasyAbpFileManagementFiles",
                columns: new[] { "FileName", "OwnerUserId", "FileContainerName" });

            migrationBuilder.CreateIndex(
                name: "IX_EasyAbpFileManagementFiles_ParentId_OwnerUserId_FileContainerName_FileType",
                table: "EasyAbpFileManagementFiles",
                columns: new[] { "ParentId", "OwnerUserId", "FileContainerName", "FileType" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EasyAbpFileManagementFiles");
        }
    }
}
