using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acme.BookStore.Migrations
{
    public partial class Addorder_orderline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    orderStatus = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppOrders_AbpUsers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppOrderLine",
                columns: table => new
                {
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quentity = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppOrderLine", x => new { x.OrderId, x.ProductID });
                    table.ForeignKey(
                        name: "FK_AppOrderLine_AppOrders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "AppOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppOrderLine_AppProduct_ProductID",
                        column: x => x.ProductID,
                        principalTable: "AppProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderLine_ProductID",
                table: "AppOrderLine",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrders_CustomerID",
                table: "AppOrders",
                column: "CustomerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppOrderLine");

            migrationBuilder.DropTable(
                name: "AppOrders");
        }
    }
}
