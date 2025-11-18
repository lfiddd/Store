using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Migrations
{
    /// <inheritdoc />
    public partial class storeAPI3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionLogs_UserAction_UserActionid_action",
                table: "ActionLogs");

            migrationBuilder.DropIndex(
                name: "IX_ActionLogs_UserActionid_action",
                table: "ActionLogs");

            migrationBuilder.DropColumn(
                name: "UserActionid_action",
                table: "ActionLogs");

            migrationBuilder.CreateIndex(
                name: "IX_ActionLogs_id_action",
                table: "ActionLogs",
                column: "id_action");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionLogs_UserAction_id_action",
                table: "ActionLogs",
                column: "id_action",
                principalTable: "UserAction",
                principalColumn: "id_action",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionLogs_UserAction_id_action",
                table: "ActionLogs");

            migrationBuilder.DropIndex(
                name: "IX_ActionLogs_id_action",
                table: "ActionLogs");

            migrationBuilder.AddColumn<int>(
                name: "UserActionid_action",
                table: "ActionLogs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ActionLogs_UserActionid_action",
                table: "ActionLogs",
                column: "UserActionid_action");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionLogs_UserAction_UserActionid_action",
                table: "ActionLogs",
                column: "UserActionid_action",
                principalTable: "UserAction",
                principalColumn: "id_action",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
