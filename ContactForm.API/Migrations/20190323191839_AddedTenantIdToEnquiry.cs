using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactForm.API.Migrations
{
    public partial class AddedTenantIdToEnquiry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Enquiries",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Enquiries");
        }
    }
}
