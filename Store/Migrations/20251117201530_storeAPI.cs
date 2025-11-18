using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Store.Migrations
{
    /// <inheritdoc />
    public partial class storeAPI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryTypes",
                columns: table => new
                {
                    Id_delivtype = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryTypes", x => x.Id_delivtype);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    Id_status = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.Id_status);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    Id_paytype = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_paytype = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.Id_paytype);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    id_category = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameCategory = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.id_category);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id_role = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameRole = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id_role);
                });

            migrationBuilder.CreateTable(
                name: "UserAction",
                columns: table => new
                {
                    id_action = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    action = table.Column<string>(type: "text", nullable: false),
                    decription = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAction", x => x.id_action);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id_product = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameProduct = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Stock = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateOnly>(type: "date", nullable: false),
                    id_category = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.id_product);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_id_category",
                        column: x => x.id_category,
                        principalTable: "ProductCategories",
                        principalColumn: "id_category",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateOnly>(type: "date", nullable: false),
                    UpdatedAt = table.Column<DateOnly>(type: "date", nullable: false),
                    id_role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id_user);
                    table.ForeignKey(
                        name: "FK_Users_Roles_id_role",
                        column: x => x.id_role,
                        principalTable: "Roles",
                        principalColumn: "id_role",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActionLogs",
                columns: table => new
                {
                    id_log = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    action_date = table.Column<DateOnly>(type: "date", nullable: false),
                    id_user = table.Column<int>(type: "integer", nullable: false),
                    id_action = table.Column<int>(type: "integer", nullable: false),
                    UserActionid_action = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionLogs", x => x.id_log);
                    table.ForeignKey(
                        name: "FK_ActionLogs_UserAction_UserActionid_action",
                        column: x => x.UserActionid_action,
                        principalTable: "UserAction",
                        principalColumn: "id_action",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionLogs_Users_id_user",
                        column: x => x.id_user,
                        principalTable: "Users",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    id_login = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserLogin = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    id_user = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.id_login);
                    table.ForeignKey(
                        name: "FK_Logins_Users_id_user",
                        column: x => x.id_user,
                        principalTable: "Users",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id_order = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    DeliveryAddress = table.Column<string>(type: "text", nullable: false),
                    id_user = table.Column<int>(type: "integer", nullable: false),
                    id_delivtype = table.Column<int>(type: "integer", nullable: false),
                    id_status = table.Column<int>(type: "integer", nullable: false),
                    id_paytype = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id_order);
                    table.ForeignKey(
                        name: "FK_Orders_DeliveryTypes_id_delivtype",
                        column: x => x.id_delivtype,
                        principalTable: "DeliveryTypes",
                        principalColumn: "Id_delivtype",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatuses_id_status",
                        column: x => x.id_status,
                        principalTable: "OrderStatuses",
                        principalColumn: "Id_status",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_PaymentTypes_id_paytype",
                        column: x => x.id_paytype,
                        principalTable: "PaymentTypes",
                        principalColumn: "Id_paytype",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_id_user",
                        column: x => x.id_user,
                        principalTable: "Users",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    id_session = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    token = table.Column<string>(type: "text", nullable: false),
                    id_user = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.id_session);
                    table.ForeignKey(
                        name: "FK_Sessions_Users_id_user",
                        column: x => x.id_user,
                        principalTable: "Users",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    id_basket = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ResultPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    IsOrdered = table.Column<bool>(type: "boolean", nullable: false),
                    id_user = table.Column<int>(type: "integer", nullable: false),
                    id_order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.id_basket);
                    table.ForeignKey(
                        name: "FK_Baskets_Orders_id_order",
                        column: x => x.id_order,
                        principalTable: "Orders",
                        principalColumn: "id_order",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Baskets_Users_id_user",
                        column: x => x.id_user,
                        principalTable: "Users",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BasketItems",
                columns: table => new
                {
                    id_basket_item = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProdCount = table.Column<int>(type: "integer", nullable: false),
                    id_basket = table.Column<int>(type: "integer", nullable: false),
                    id_product = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItems", x => x.id_basket_item);
                    table.ForeignKey(
                        name: "FK_BasketItems_Baskets_id_basket",
                        column: x => x.id_basket,
                        principalTable: "Baskets",
                        principalColumn: "id_basket",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketItems_Products_id_product",
                        column: x => x.id_product,
                        principalTable: "Products",
                        principalColumn: "id_product",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionLogs_id_user",
                table: "ActionLogs",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_ActionLogs_UserActionid_action",
                table: "ActionLogs",
                column: "UserActionid_action");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_id_basket",
                table: "BasketItems",
                column: "id_basket");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_id_product",
                table: "BasketItems",
                column: "id_product");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_id_order",
                table: "Baskets",
                column: "id_order");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_id_user",
                table: "Baskets",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_id_user",
                table: "Logins",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_id_delivtype",
                table: "Orders",
                column: "id_delivtype");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_id_paytype",
                table: "Orders",
                column: "id_paytype");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_id_status",
                table: "Orders",
                column: "id_status");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_id_user",
                table: "Orders",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_Products_id_category",
                table: "Products",
                column: "id_category");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_id_user",
                table: "Sessions",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_id_role",
                table: "Users",
                column: "id_role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionLogs");

            migrationBuilder.DropTable(
                name: "BasketItems");

            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "UserAction");

            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "DeliveryTypes");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
