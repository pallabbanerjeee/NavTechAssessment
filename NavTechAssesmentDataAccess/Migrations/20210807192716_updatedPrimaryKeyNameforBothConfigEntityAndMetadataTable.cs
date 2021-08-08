using Microsoft.EntityFrameworkCore.Migrations;

namespace NavTechAssesment.DataAccess.Migrations
{
    public partial class updatedPrimaryKeyNameforBothConfigEntityAndMetadataTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Metadata_Id",
                table: "ConfigEntityMetadatas",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Entity_Id",
                table: "ConfigEntities",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ConfigEntityMetadatas",
                newName: "Metadata_Id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ConfigEntities",
                newName: "Entity_Id");
        }
    }
}
