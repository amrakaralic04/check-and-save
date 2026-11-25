using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Identity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_PublicUserEntity_PublicUserEntityId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_ManagerEntity_Store_StoreEntityId",
                table: "ManagerEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ManagerEntity_Users_MarketUserEntityId",
                table: "ManagerEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicUserEntity_Users_MarketUserEntityId",
                table: "PublicUserEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_PublicUserEntity_PublicUserEntityId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PublicUserEntity",
                table: "PublicUserEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ManagerEntity",
                table: "ManagerEntity");

            migrationBuilder.RenameTable(
                name: "PublicUserEntity",
                newName: "PublicUsers");

            migrationBuilder.RenameTable(
                name: "ManagerEntity",
                newName: "Managers");

            migrationBuilder.RenameIndex(
                name: "IX_PublicUserEntity_MarketUserEntityId",
                table: "PublicUsers",
                newName: "IX_PublicUsers_MarketUserEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_ManagerEntity_StoreEntityId",
                table: "Managers",
                newName: "IX_Managers_StoreEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_ManagerEntity_MarketUserEntityId",
                table: "Managers",
                newName: "IX_Managers_MarketUserEntityId");

            migrationBuilder.AlterColumn<string>(
                name: "Lastname",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPublicUser",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "Firstname",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Points",
                table: "PublicUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AvatarLevel",
                table: "PublicUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "StoreEntityId1",
                table: "Managers",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PublicUsers",
                table: "PublicUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Managers",
                table: "Managers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarketUserEntityId = table.Column<int>(type: "int", nullable: false),
                    ActivityType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_Users_MarketUserEntityId",
                        column: x => x.MarketUserEntityId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecurityQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarketUserEntityId = table.Column<int>(type: "int", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecurityQuestions_Users_MarketUserEntityId",
                        column: x => x.MarketUserEntityId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Managers_StoreEntityId1",
                table: "Managers",
                column: "StoreEntityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_Date",
                table: "Activities",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_MarketUserEntityId",
                table: "Activities",
                column: "MarketUserEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityQuestions_MarketUserEntityId",
                table: "SecurityQuestions",
                column: "MarketUserEntityId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_PublicUsers_PublicUserEntityId",
                table: "Favorites",
                column: "PublicUserEntityId",
                principalTable: "PublicUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Store_StoreEntityId",
                table: "Managers",
                column: "StoreEntityId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Store_StoreEntityId1",
                table: "Managers",
                column: "StoreEntityId1",
                principalTable: "Store",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Users_MarketUserEntityId",
                table: "Managers",
                column: "MarketUserEntityId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicUsers_Users_MarketUserEntityId",
                table: "PublicUsers",
                column: "MarketUserEntityId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_PublicUsers_PublicUserEntityId",
                table: "Reviews",
                column: "PublicUserEntityId",
                principalTable: "PublicUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_PublicUsers_PublicUserEntityId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Store_StoreEntityId",
                table: "Managers");

            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Store_StoreEntityId1",
                table: "Managers");

            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Users_MarketUserEntityId",
                table: "Managers");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicUsers_Users_MarketUserEntityId",
                table: "PublicUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_PublicUsers_PublicUserEntityId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "SecurityQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PublicUsers",
                table: "PublicUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Managers",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_StoreEntityId1",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "StoreEntityId1",
                table: "Managers");

            migrationBuilder.RenameTable(
                name: "PublicUsers",
                newName: "PublicUserEntity");

            migrationBuilder.RenameTable(
                name: "Managers",
                newName: "ManagerEntity");

            migrationBuilder.RenameIndex(
                name: "IX_PublicUsers_MarketUserEntityId",
                table: "PublicUserEntity",
                newName: "IX_PublicUserEntity_MarketUserEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Managers_StoreEntityId",
                table: "ManagerEntity",
                newName: "IX_ManagerEntity_StoreEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Managers_MarketUserEntityId",
                table: "ManagerEntity",
                newName: "IX_ManagerEntity_MarketUserEntityId");

            migrationBuilder.AlterColumn<string>(
                name: "Lastname",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<bool>(
                name: "IsPublicUser",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Firstname",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "Points",
                table: "PublicUserEntity",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "AvatarLevel",
                table: "PublicUserEntity",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PublicUserEntity",
                table: "PublicUserEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ManagerEntity",
                table: "ManagerEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_PublicUserEntity_PublicUserEntityId",
                table: "Favorites",
                column: "PublicUserEntityId",
                principalTable: "PublicUserEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ManagerEntity_Store_StoreEntityId",
                table: "ManagerEntity",
                column: "StoreEntityId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ManagerEntity_Users_MarketUserEntityId",
                table: "ManagerEntity",
                column: "MarketUserEntityId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicUserEntity_Users_MarketUserEntityId",
                table: "PublicUserEntity",
                column: "MarketUserEntityId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_PublicUserEntity_PublicUserEntityId",
                table: "Reviews",
                column: "PublicUserEntityId",
                principalTable: "PublicUserEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
