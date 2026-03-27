using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartFYPHandler.Migrations
{
    /// <inheritdoc />
    public partial class AddSearchHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SearchHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Query = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ResultsCount = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SearchHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3732));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3736));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3738));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3739));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3741));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3537), new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3540) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3548), new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3548) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3551), new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3551) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3553), new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3554) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3555), new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3556) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3775), new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3780) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3785), new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3786) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3788), new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3789) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3791), new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3791) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3793), new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3793) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3795), new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3795) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3797), new DateTime(2026, 3, 27, 15, 53, 41, 487, DateTimeKind.Utc).AddTicks(3797) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 41, 696, DateTimeKind.Utc).AddTicks(1518), "$2a$11$UQeKMiijVRCIGDobjUNuC./Y314NuaD/T.DFHgkUoeH6qIrUlgS0O", new DateTime(2026, 3, 27, 15, 53, 41, 696, DateTimeKind.Utc).AddTicks(1522) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 200,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 41, 900, DateTimeKind.Utc).AddTicks(8050), "$2a$11$hX4eigjSl5pSCSizAMF5ru9LE7Qz18YyKGruWd8yiubdjcApfTcTa", new DateTime(2026, 3, 27, 15, 53, 41, 900, DateTimeKind.Utc).AddTicks(8056) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 300,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 53, 42, 123, DateTimeKind.Utc).AddTicks(9066), "$2a$11$VijOPZcWck8uoAJZHYiMGOyZIOEduHX1wKMzeTjTgoDtp57jw1OHG", new DateTime(2026, 3, 27, 15, 53, 42, 123, DateTimeKind.Utc).AddTicks(9071) });

            migrationBuilder.CreateIndex(
                name: "IX_SearchHistories_UserId",
                table: "SearchHistories",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchHistories");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4592));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4598));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4601));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4603));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4605));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4341), new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4344) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4354), new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4355) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4359), new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4360) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4363), new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4363) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4366), new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4366) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4657), new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4666) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4672), new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4673) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4676), new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4677) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4679), new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4680) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4682), new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4683) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4685), new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4685) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4687), new DateTime(2026, 3, 27, 15, 33, 54, 510, DateTimeKind.Utc).AddTicks(4688) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 33, 54, 733, DateTimeKind.Utc).AddTicks(3079), "$2a$11$YmToEyi.UKeB8Z718GTEr.llkBwGGFn5ifiFpMmWsdhoE3TqbEmUS", new DateTime(2026, 3, 27, 15, 33, 54, 733, DateTimeKind.Utc).AddTicks(3088) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 200,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 33, 54, 940, DateTimeKind.Utc).AddTicks(1902), "$2a$11$Q75mXSgfXcsa6NJWr2d5MeJwm0lMTh2zyUJ16OCpLAs4x8DeuRTpG", new DateTime(2026, 3, 27, 15, 33, 54, 940, DateTimeKind.Utc).AddTicks(1909) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 300,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 33, 55, 147, DateTimeKind.Utc).AddTicks(837), "$2a$11$7e/IlFGosfcR/NA9X7BceOYmt.3CRoxABtYOnEoS.yTEqY.tPfv9O", new DateTime(2026, 3, 27, 15, 33, 55, 147, DateTimeKind.Utc).AddTicks(845) });
        }
    }
}
