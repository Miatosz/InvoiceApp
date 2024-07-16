using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceApp.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ChangeItemsInInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Invoice_InvoiceId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_InvoiceId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Item");

            migrationBuilder.AddColumn<string>(
                name: "Items",
                table: "Invoice",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Items",
                table: "Invoice");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "Item",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_InvoiceId",
                table: "Item",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Invoice_InvoiceId",
                table: "Item",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id");
        }
    }
}
