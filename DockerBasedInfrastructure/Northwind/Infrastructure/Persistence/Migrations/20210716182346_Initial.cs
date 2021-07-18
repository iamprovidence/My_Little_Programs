using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Northwind.Infrastructure.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "dbo",
                columns: table => new
                {
                    CategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(maxLength: 15, nullable: false),
                    Description = table.Column<string>(type: "ntext", nullable: true),
                    Picture = table.Column<byte[]>(type: "image", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "dbo",
                columns: table => new
                {
                    CustomerID = table.Column<string>(maxLength: 5, nullable: false),
                    CompanyName = table.Column<string>(maxLength: 40, nullable: false),
                    ContactName = table.Column<string>(maxLength: 30, nullable: true),
                    ContactTitle = table.Column<string>(maxLength: 30, nullable: true),
                    Address = table.Column<string>(maxLength: 60, nullable: true),
                    City = table.Column<string>(maxLength: 15, nullable: true),
                    Region = table.Column<string>(maxLength: 15, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 10, nullable: true),
                    Country = table.Column<string>(maxLength: 15, nullable: true),
                    Phone = table.Column<string>(maxLength: 24, nullable: true),
                    Fax = table.Column<string>(maxLength: 24, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "dbo",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(maxLength: 10, nullable: false),
                    Title = table.Column<string>(maxLength: 30, nullable: true),
                    TitleOfCourtesy = table.Column<string>(maxLength: 25, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    HireDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Address = table.Column<string>(maxLength: 60, nullable: true),
                    City = table.Column<string>(maxLength: 15, nullable: true),
                    Region = table.Column<string>(maxLength: 15, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 10, nullable: true),
                    Country = table.Column<string>(maxLength: 15, nullable: true),
                    HomePhone = table.Column<string>(maxLength: 24, nullable: true),
                    Extension = table.Column<string>(maxLength: 4, nullable: true),
                    Photo = table.Column<byte[]>(type: "image", nullable: true),
                    Notes = table.Column<string>(type: "ntext", nullable: true),
                    ReportsTo = table.Column<int>(nullable: true),
                    PhotoPath = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_Employees_Employees",
                        column: x => x.ReportsTo,
                        principalSchema: "dbo",
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                schema: "dbo",
                columns: table => new
                {
                    RegionID = table.Column<int>(nullable: false),
                    RegionDescription = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.RegionID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Shippers",
                schema: "dbo",
                columns: table => new
                {
                    ShipperID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(maxLength: 40, nullable: false),
                    Phone = table.Column<string>(maxLength: 24, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippers", x => x.ShipperID);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                schema: "dbo",
                columns: table => new
                {
                    SupplierID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(maxLength: 40, nullable: false),
                    ContactName = table.Column<string>(maxLength: 30, nullable: true),
                    ContactTitle = table.Column<string>(maxLength: 30, nullable: true),
                    Address = table.Column<string>(maxLength: 60, nullable: true),
                    City = table.Column<string>(maxLength: 15, nullable: true),
                    Region = table.Column<string>(maxLength: 15, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 10, nullable: true),
                    Country = table.Column<string>(maxLength: 15, nullable: true),
                    Phone = table.Column<string>(maxLength: 24, nullable: true),
                    Fax = table.Column<string>(maxLength: 24, nullable: true),
                    HomePage = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierID);
                });

            migrationBuilder.CreateTable(
                name: "Territories",
                schema: "dbo",
                columns: table => new
                {
                    TerritoryID = table.Column<string>(maxLength: 20, nullable: false),
                    TerritoryDescription = table.Column<string>(maxLength: 50, nullable: false),
                    RegionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Territories", x => x.TerritoryID)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Territories_Region",
                        column: x => x.RegionID,
                        principalSchema: "dbo",
                        principalTable: "Regions",
                        principalColumn: "RegionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "dbo",
                columns: table => new
                {
                    OrderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RequiredDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ShippedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ShipVia = table.Column<int>(nullable: true),
                    Freight = table.Column<decimal>(type: "money", nullable: true, defaultValueSql: "((0))"),
                    ShipName = table.Column<string>(maxLength: 40, nullable: true),
                    ShipAddress = table.Column<string>(maxLength: 60, nullable: true),
                    ShipCity = table.Column<string>(maxLength: 15, nullable: true),
                    ShipRegion = table.Column<string>(maxLength: 15, nullable: true),
                    ShipPostalCode = table.Column<string>(maxLength: 10, nullable: true),
                    ShipCountry = table.Column<string>(maxLength: 15, nullable: true),
                    CustomerID = table.Column<string>(maxLength: 5, nullable: true),
                    EmployeeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalSchema: "dbo",
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalSchema: "dbo",
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Shippers",
                        column: x => x.ShipVia,
                        principalSchema: "dbo",
                        principalTable: "Shippers",
                        principalColumn: "ShipperID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "dbo",
                columns: table => new
                {
                    ProductID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(maxLength: 40, nullable: false),
                    QuantityPerUnit = table.Column<string>(maxLength: 20, nullable: true),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: true, defaultValueSql: "((0))"),
                    UnitsInStock = table.Column<short>(nullable: true, defaultValueSql: "((0))"),
                    UnitsOnOrder = table.Column<short>(nullable: true, defaultValueSql: "((0))"),
                    ReorderLevel = table.Column<short>(nullable: true, defaultValueSql: "((0))"),
                    Discontinued = table.Column<bool>(nullable: false),
                    CategoryID = table.Column<int>(nullable: true),
                    SupplierID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalSchema: "dbo",
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalSchema: "dbo",
                        principalTable: "Suppliers",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTerritories",
                schema: "dbo",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(nullable: false),
                    TerritoryID = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTerritories", x => new { x.EmployeeID, x.TerritoryID })
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_EmployeeTerritories_Employees",
                        column: x => x.EmployeeID,
                        principalSchema: "dbo",
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeTerritories_Territories",
                        column: x => x.TerritoryID,
                        principalSchema: "dbo",
                        principalTable: "Territories",
                        principalColumn: "TerritoryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order Details",
                schema: "dbo",
                columns: table => new
                {
                    OrderID = table.Column<int>(nullable: false),
                    ProductID = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: false),
                    Quantity = table.Column<short>(nullable: false, defaultValueSql: "((1))"),
                    Discount = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Details", x => new { x.OrderID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_Order_Details_Orders",
                        column: x => x.OrderID,
                        principalSchema: "dbo",
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Details_Products",
                        column: x => x.ProductID,
                        principalSchema: "dbo",
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ReportsTo",
                schema: "dbo",
                table: "Employees",
                column: "ReportsTo");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTerritories_TerritoryID",
                schema: "dbo",
                table: "EmployeeTerritories",
                column: "TerritoryID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductID",
                schema: "dbo",
                table: "Order Details",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerID",
                schema: "dbo",
                table: "Orders",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EmployeeID",
                schema: "dbo",
                table: "Orders",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShipVia",
                schema: "dbo",
                table: "Orders",
                column: "ShipVia");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                schema: "dbo",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierID",
                schema: "dbo",
                table: "Products",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_Territories_RegionID",
                schema: "dbo",
                table: "Territories",
                column: "RegionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeTerritories",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Order Details",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Territories",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Regions",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Shippers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Suppliers",
                schema: "dbo");
        }
    }
}
