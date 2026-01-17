using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartFYPHandler.Migrations
{
    /// <inheritdoc />
    public partial class AddFYPEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "ProjectMembers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "ProjectMembers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ProjectMembers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "FYPProjectId",
                table: "ProjectMembers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ProjectMembers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdeaAnalyses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    InputTextHash = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    InputTitle = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    InputAbstract = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    OriginalityScore = table.Column<int>(type: "int", nullable: false),
                    SimilarityMax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ResultCategory = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdeaAnalyses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IndexedDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceType = table.Column<int>(type: "int", nullable: false),
                    SourceEntityId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Embedding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetadataJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndexedDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdeaMatches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdeaAnalysisId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IndexedDocumentId = table.Column<int>(type: "int", nullable: false),
                    SourceType = table.Column<int>(type: "int", nullable: false),
                    Similarity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Snippet = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdeaMatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdeaMatches_IdeaAnalyses_IdeaAnalysisId",
                        column: x => x.IdeaAnalysisId,
                        principalTable: "IdeaAnalyses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdeaMatches_IndexedDocuments_IndexedDocumentId",
                        column: x => x.IndexedDocumentId,
                        principalTable: "IndexedDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FYPProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Semester = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    SupervisorId = table.Column<int>(type: "int", nullable: false),
                    DifficultyLevel = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PerformanceScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinalGrade = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    DepartmentRank = table.Column<int>(type: "int", nullable: true),
                    OverallRank = table.Column<int>(type: "int", nullable: true),
                    Citations = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FYPProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FYPProjects_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FYPProjects_ProjectCategories_ProjectCategoryId",
                        column: x => x.ProjectCategoryId,
                        principalTable: "ProjectCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FYPProjects_Users_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserPreferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PreferredCategories = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EngagementLevel = table.Column<int>(type: "int", nullable: false),
                    AverageRating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPreferences_ProjectCategories_ProjectCategoryId",
                        column: x => x.ProjectCategoryId,
                        principalTable: "ProjectCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserPreferences_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentRankings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    RankPosition = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Semester = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PerformanceScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentRankings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentRankings_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentRankings_FYPProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "FYPProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectEvaluations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    EvaluatorId = table.Column<int>(type: "int", nullable: false),
                    TechnicalScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InnovationScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImplementationScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PresentationScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DocumentationScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OverallScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EvaluationType = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Recommendations = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    EvaluationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectEvaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectEvaluations_FYPProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "FYPProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectEvaluations_Users_EvaluatorId",
                        column: x => x.EvaluatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserInteractions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    InteractionType = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInteractions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInteractions_FYPProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "FYPProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInteractions_Users_UserId",
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
                value: new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1932));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1935));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1936));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1937));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1938));

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Code", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "CS", new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1826), "Computer Science and Software Engineering", "Computer Science", new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1830) },
                    { 2, "SE", new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1834), "Software Engineering and Development", "Software Engineering", new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1835) },
                    { 3, "DS", new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1836), "Data Science and Analytics", "Data Science", new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1836) },
                    { 4, "CYB", new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1838), "Cybersecurity and Information Security", "Cybersecurity", new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1839) },
                    { 5, "IT", new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1840), "Information Technology and Systems", "Information Technology", new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1840) }
                });

            migrationBuilder.InsertData(
                table: "ProjectCategories",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1955), "AI and ML related projects", "Machine Learning", new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1956) },
                    { 2, new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1959), "Web applications and services", "Web Development", new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1959) },
                    { 3, new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1960), "Mobile applications for iOS and Android", "Mobile Development", new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1961) },
                    { 4, new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1962), "Internet of Things projects", "IoT", new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1962) },
                    { 5, new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1963), "Blockchain and cryptocurrency projects", "Blockchain", new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1969) },
                    { 6, new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1970), "Data analysis and visualization", "Data Science", new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1971) },
                    { 7, new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1972), "Security and privacy related projects", "Cybersecurity", new DateTime(2026, 1, 17, 16, 27, 13, 241, DateTimeKind.Utc).AddTicks(1972) }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DepartmentId", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 17, 16, 27, 13, 357, DateTimeKind.Utc).AddTicks(4919), 1, "$2a$11$tPLVYPlKbef.2eJ0qpHO/uLQAp0Y04GkKRZ4uf/9iHB8rG5O5XNoW", new DateTime(2026, 1, 17, 16, 27, 13, 357, DateTimeKind.Utc).AddTicks(4923) });

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMembers_FYPProjectId",
                table: "ProjectMembers",
                column: "FYPProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentRankings_DepartmentId",
                table: "DepartmentRankings",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentRankings_ProjectId",
                table: "DepartmentRankings",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Code",
                table: "Departments",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FYPProjects_DepartmentId",
                table: "FYPProjects",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_FYPProjects_ProjectCategoryId",
                table: "FYPProjects",
                column: "ProjectCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FYPProjects_SupervisorId",
                table: "FYPProjects",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_IdeaMatches_IdeaAnalysisId",
                table: "IdeaMatches",
                column: "IdeaAnalysisId");

            migrationBuilder.CreateIndex(
                name: "IX_IdeaMatches_IndexedDocumentId",
                table: "IdeaMatches",
                column: "IndexedDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_IndexedDocuments_SourceType_Year",
                table: "IndexedDocuments",
                columns: new[] { "SourceType", "Year" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEvaluations_EvaluatorId",
                table: "ProjectEvaluations",
                column: "EvaluatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEvaluations_ProjectId",
                table: "ProjectEvaluations",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInteractions_ProjectId",
                table: "UserInteractions",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInteractions_UserId",
                table: "UserInteractions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPreferences_ProjectCategoryId",
                table: "UserPreferences",
                column: "ProjectCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPreferences_UserId",
                table: "UserPreferences",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMembers_FYPProjects_FYPProjectId",
                table: "ProjectMembers",
                column: "FYPProjectId",
                principalTable: "FYPProjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMembers_FYPProjects_FYPProjectId",
                table: "ProjectMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "DepartmentRankings");

            migrationBuilder.DropTable(
                name: "IdeaMatches");

            migrationBuilder.DropTable(
                name: "ProjectEvaluations");

            migrationBuilder.DropTable(
                name: "UserInteractions");

            migrationBuilder.DropTable(
                name: "UserPreferences");

            migrationBuilder.DropTable(
                name: "IdeaAnalyses");

            migrationBuilder.DropTable(
                name: "IndexedDocuments");

            migrationBuilder.DropTable(
                name: "FYPProjects");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "ProjectCategories");

            migrationBuilder.DropIndex(
                name: "IX_Users_DepartmentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMembers_FYPProjectId",
                table: "ProjectMembers");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ProjectMembers");

            migrationBuilder.DropColumn(
                name: "FYPProjectId",
                table: "ProjectMembers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ProjectMembers");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "ProjectMembers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "ProjectMembers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 31, 23, 13, 15, 832, DateTimeKind.Utc).AddTicks(4110));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 31, 23, 13, 15, 832, DateTimeKind.Utc).AddTicks(4113));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 31, 23, 13, 15, 832, DateTimeKind.Utc).AddTicks(4114));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 31, 23, 13, 15, 832, DateTimeKind.Utc).AddTicks(4115));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 31, 23, 13, 15, 832, DateTimeKind.Utc).AddTicks(4116));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 31, 23, 13, 15, 992, DateTimeKind.Utc).AddTicks(4266), "$2a$11$J7u4nsGfofNLEZmUSE.NFuDSufpa8KArN5cC4wif4wGN7lCESH61u", new DateTime(2025, 7, 31, 23, 13, 15, 992, DateTimeKind.Utc).AddTicks(4272) });
        }
    }
}
