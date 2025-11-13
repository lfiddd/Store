using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Migrations
{
    /// <inheritdoc />
    public partial class storeAPI8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProdCount",
                table: "Baskets");

            migrationBuilder.AddColumn<int>(
                name: "ProdCount",
                table: "BasketItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProdCount",
                table: "BasketItems");

            migrationBuilder.AddColumn<int>(
                name: "ProdCount",
                table: "Baskets",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
