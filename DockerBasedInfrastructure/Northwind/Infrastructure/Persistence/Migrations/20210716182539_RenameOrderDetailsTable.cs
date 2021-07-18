using Microsoft.EntityFrameworkCore.Migrations;

namespace Northwind.Infrastructure.Persistence.Migrations
{
    public partial class RenameOrderDetailsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Order Details",
                schema: "dbo",
                newName: "OrderDetails",
                newSchema: "dbo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "OrderDetails",
                schema: "dbo",
                newName: "Order Details",
                newSchema: "dbo");
        }
    }
}
