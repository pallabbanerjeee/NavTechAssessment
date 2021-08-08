using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NavTechAssesment.DataAccess.Migrations
{
    public partial class AddConfigEntityAndConfigEntityMetadataTableToConfigDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfigEntities",
                columns: table => new
                {
                    Entity_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    EntityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDatetime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(GETUTCDATE())"),
                    DeletedDatetime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigEntities", x => x.Entity_Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfigEntityMetadatas",
                columns: table => new
                {
                    Metadata_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    FieldName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaxLength = table.Column<int>(type: "int", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDatetime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(GETUTCDATE())"),
                    DeletedDatetime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Entity_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigEntityMetadatas", x => x.Metadata_Id);
                    table.ForeignKey(
                        name: "FK_ConfigEntityMetadata_ConfigEntity",
                        column: x => x.Entity_Id,
                        principalTable: "ConfigEntities",
                        principalColumn: "Entity_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfigEntityMetadatas_Entity_Id",
                table: "ConfigEntityMetadatas",
                column: "Entity_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigEntityMetadatas");

            migrationBuilder.DropTable(
                name: "ConfigEntities");
        }
    }
}
