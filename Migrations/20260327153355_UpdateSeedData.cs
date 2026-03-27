using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartFYPHandler.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "CreatedAt", "Email", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 27, 15, 33, 54, 733, DateTimeKind.Utc).AddTicks(3079), "admin@gmail.com", "$2a$11$YmToEyi.UKeB8Z718GTEr.llkBwGGFn5ifiFpMmWsdhoE3TqbEmUS", new DateTime(2026, 3, 27, 15, 33, 54, 733, DateTimeKind.Utc).AddTicks(3088) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Department", "DepartmentId", "Email", "FirstName", "GoogleId", "IsActive", "LastName", "PasswordHash", "Role", "StudentId", "UpdatedAt" },
                values: new object[,]
                {
                    { 200, new DateTime(2026, 3, 27, 15, 33, 54, 940, DateTimeKind.Utc).AddTicks(1902), "Computer Science", 1, "teacher@gmail.com", "Dr.", null, true, "Teacher", "$2a$11$Q75mXSgfXcsa6NJWr2d5MeJwm0lMTh2zyUJ16OCpLAs4x8DeuRTpG", 2, "", new DateTime(2026, 3, 27, 15, 33, 54, 940, DateTimeKind.Utc).AddTicks(1909) },
                    { 300, new DateTime(2026, 3, 27, 15, 33, 55, 147, DateTimeKind.Utc).AddTicks(837), "Computer Science", 1, "student@gmail.com", "John", null, true, "Student", "$2a$11$7e/IlFGosfcR/NA9X7BceOYmt.3CRoxABtYOnEoS.yTEqY.tPfv9O", 1, "FA21-BCS-001", new DateTime(2026, 3, 27, 15, 33, 55, 147, DateTimeKind.Utc).AddTicks(845) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 300);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1387));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1391));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1393));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1394));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1396));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1231), new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1233) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1240), new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1240) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1243), new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1243) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1245), new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1246) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1248), new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1248) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1472), new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1472) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1477), new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1477) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1479), new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1480) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1481), new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1482) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1483), new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1489) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1491), new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1491) });

            migrationBuilder.UpdateData(
                table: "ProjectCategories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1493), new DateTime(2026, 3, 25, 14, 16, 0, 167, DateTimeKind.Utc).AddTicks(1493) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Email", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 14, 16, 0, 397, DateTimeKind.Utc).AddTicks(6656), "admin@smartfyp.com", "$2a$11$uMTSblz1Kb2NdU3ra2.YNuA73RfefRbrgtNjS6S5lxbbTVjcSKZ4S", new DateTime(2026, 3, 25, 14, 16, 0, 397, DateTimeKind.Utc).AddTicks(6663) });
        }
    }
}
