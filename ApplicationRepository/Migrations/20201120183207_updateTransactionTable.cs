using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationRepository.Migrations
{
    public partial class updateTransactionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OutputStatus",
                table: "Transactions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OutputStatus",
                table: "Transactions");
        }
    }
}
