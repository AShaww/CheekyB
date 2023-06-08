using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CheekyData.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    RatingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RatingName = table.Column<string>(type: "nvarchar(52)", maxLength: 52, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.RatingId);
                });

            migrationBuilder.CreateTable(
                name: "ScrapedNews",
                columns: table => new
                {
                    NewsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScrapedNews", x => x.NewsId);
                });

            migrationBuilder.CreateTable(
                name: "SkillType",
                columns: table => new
                {
                    SkillTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillType", x => x.SkillTypeId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Archived = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LoginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArchivedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    GoogleUserId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    SkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkillTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.SkillId);
                    table.ForeignKey(
                        name: "FK_Skill_SkillType_SkillTypeId",
                        column: x => x.SkillTypeId,
                        principalTable: "SkillType",
                        principalColumn: "SkillTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkillTypeRating",
                columns: table => new
                {
                    SkillTypeId = table.Column<int>(type: "int", nullable: false),
                    RatingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillTypeRating", x => new { x.SkillTypeId, x.RatingId });
                    table.ForeignKey(
                        name: "FK_SkillTypeRating_Rating_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Rating",
                        principalColumn: "RatingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillTypeRating_SkillType_SkillTypeId",
                        column: x => x.SkillTypeId,
                        principalTable: "SkillType",
                        principalColumn: "SkillTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ToDo",
                columns: table => new
                {
                    ToDoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToDoTitle = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ToDoMessage = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ToDoDateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDo", x => x.ToDoId);
                    table.ForeignKey(
                        name: "FK_ToDo_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSkills",
                columns: table => new
                {
                    SkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RatingId = table.Column<int>(type: "int", nullable: false),
                    LastEvaluated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSkills", x => new { x.SkillId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserSkills_Rating_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Rating",
                        principalColumn: "RatingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSkills_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSkills_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Rating",
                columns: new[] { "RatingId", "RatingName" },
                values: new object[,]
                {
                    { 1, "1 - Awareness" },
                    { 2, "2 - Novice" },
                    { 3, "3 - Professional" },
                    { 4, "4 - Expert" },
                    { 5, "5 - Leading-edge expert" },
                    { 6, "1 - Beginner" },
                    { 7, "2 - Want to improve" },
                    { 8, "3 - Proffesional" },
                    { 9, "4 - Expert" },
                    { 10, "5 - Leading-edge expert" }
                });

            migrationBuilder.InsertData(
                table: "ScrapedNews",
                columns: new[] { "NewsId", "ImageUrl", "PageUrl", "Title" },
                values: new object[] { new Guid("fde34b5e-5b1a-46d6-9fe7-b6896cf86517"), "Pretend IMG URL", "Pretend Page URL", "First Random Title for News" });

            migrationBuilder.InsertData(
                table: "SkillType",
                columns: new[] { "SkillTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "Core" },
                    { 2, "Technical" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "ArchivedOn", "Email", "FirstName", "GoogleUserId", "LoginDate", "Surname" },
                values: new object[] { new Guid("830e9471-9d6e-4557-8bf5-ec89d375d933"), null, "AmirShaw@hotmail.co.uk", "Amir", null, null, "Shaw" });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "SkillId", "SkillName", "SkillTypeId" },
                values: new object[,]
                {
                    { new Guid("ba5706bc-7e50-441d-93e1-8e14f7d09c76"), "Skill 1", 1 },
                    { new Guid("ebdfc7bb-fba6-42a9-b51a-e04772449baa"), "Skill 2", 2 },
                    { new Guid("fdf21334-a2e2-4d7d-9b53-9377cd648186"), "Skill 3", 1 }
                });

            migrationBuilder.InsertData(
                table: "ToDo",
                columns: new[] { "ToDoId", "ToDoDateModified", "ToDoMessage", "ToDoTitle", "UserId" },
                values: new object[,]
                {
                    { new Guid("59887cb4-62be-4d64-a7cf-70a608c84d7b"), new DateTime(2023, 6, 8, 20, 39, 1, 688, DateTimeKind.Utc).AddTicks(7765), "Well there was one day the ended and the new day started", "Something about something 2", new Guid("830e9471-9d6e-4557-8bf5-ec89d375d933") },
                    { new Guid("5fce3a3a-a421-4830-a49b-f8813d6d4fb9"), new DateTime(2023, 6, 8, 20, 39, 1, 688, DateTimeKind.Utc).AddTicks(7778), "Well there was one day the ended and the new day started", "Something about something", new Guid("830e9471-9d6e-4557-8bf5-ec89d375d933") },
                    { new Guid("f783e4e6-f492-4ecf-8362-fd4834ab37d7"), new DateTime(2023, 6, 8, 20, 39, 1, 688, DateTimeKind.Utc).AddTicks(7768), "Well there was one day the ended and the new day started", "Something about something 3", new Guid("830e9471-9d6e-4557-8bf5-ec89d375d933") },
                    { new Guid("fde34b5e-5b1a-46d6-9fe7-b6896cf86517"), new DateTime(2023, 6, 8, 20, 39, 1, 688, DateTimeKind.Utc).AddTicks(7760), "Well there was one day the ended and the new day started", "Something about something 1", new Guid("830e9471-9d6e-4557-8bf5-ec89d375d933") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skill_SkillTypeId",
                table: "Skill",
                column: "SkillTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillTypeRating_RatingId",
                table: "SkillTypeRating",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_ToDo_UserId",
                table: "ToDo",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSkills_RatingId",
                table: "UserSkills",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSkills_UserId",
                table: "UserSkills",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScrapedNews");

            migrationBuilder.DropTable(
                name: "SkillTypeRating");

            migrationBuilder.DropTable(
                name: "ToDo");

            migrationBuilder.DropTable(
                name: "UserSkills");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "SkillType");
        }
    }
}
