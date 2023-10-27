using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReimbursementAPI.Migrations
{
    public partial class AddIsActivatedProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "is_activated",
                table: "tb_m_accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_activated",
                table: "tb_m_accounts");
        }
    }
}
