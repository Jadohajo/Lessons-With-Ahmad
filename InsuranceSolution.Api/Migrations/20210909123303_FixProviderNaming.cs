using Microsoft.EntityFrameworkCore.Migrations;

namespace InsuranceSolution.Api.Migrations
{
    public partial class FixProviderNaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Providers",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Providers",
                newName: "name");
        }
    }
}
