using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Migrations
{
    /// <inheritdoc />
    public partial class storeAPI1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Orders_id_order",
                table: "Baskets");

            migrationBuilder.AlterColumn<int>(
                name: "id_order",
                table: "Baskets",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Orders_id_order",
                table: "Baskets",
                column: "id_order",
                principalTable: "Orders",
                principalColumn: "id_order");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Orders_id_order",
                table: "Baskets");

            migrationBuilder.AlterColumn<int>(
                name: "id_order",
                table: "Baskets",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Orders_id_order",
                table: "Baskets",
                column: "id_order",
                principalTable: "Orders",
                principalColumn: "id_order",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
