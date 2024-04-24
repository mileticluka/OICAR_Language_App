using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContextImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContextImage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameObjectCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameObjectCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IconPath = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordSalt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    ProfilePicturePath = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PublicProfileVisibility = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameObject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObjectCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameObject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameObject_GameObjectCategory_ObjectCategoryId",
                        column: x => x.ObjectCategoryId,
                        principalTable: "GameObjectCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameFillBlank",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    ContextImageId = table.Column<int>(type: "int", nullable: false),
                    Sentence = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameFillBlank", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameFillBlank_ContextImage_ContextImageId",
                        column: x => x.ContextImageId,
                        principalTable: "ContextImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameFillBlank_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameFlashCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    ContextImageId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameFlashCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameFlashCard_ContextImage_ContextImageId",
                        column: x => x.ContextImageId,
                        principalTable: "ContextImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameFlashCard_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamePickSentence",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    ContextImageId = table.Column<int>(type: "int", nullable: false),
                    AnswerSentence = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePickSentence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamePickSentence_ContextImage_ContextImageId",
                        column: x => x.ContextImageId,
                        principalTable: "ContextImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePickSentence_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguageStat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    StatName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageStat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LanguageStat_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LanguageStat_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameObjectLocalized",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    GameObjectId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameObjectLocalized", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameObjectLocalized_GameObject_GameObjectId",
                        column: x => x.GameObjectId,
                        principalTable: "GameObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameObjectLocalized_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameFillBlank_ContextImageId",
                table: "GameFillBlank",
                column: "ContextImageId");

            migrationBuilder.CreateIndex(
                name: "IX_GameFillBlank_LanguageId",
                table: "GameFillBlank",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_GameFlashCard_ContextImageId",
                table: "GameFlashCard",
                column: "ContextImageId");

            migrationBuilder.CreateIndex(
                name: "IX_GameFlashCard_LanguageId",
                table: "GameFlashCard",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_GameObject_ObjectCategoryId",
                table: "GameObject",
                column: "ObjectCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GameObjectLocalized_GameObjectId",
                table: "GameObjectLocalized",
                column: "GameObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_GameObjectLocalized_LanguageId",
                table: "GameObjectLocalized",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePickSentence_ContextImageId",
                table: "GamePickSentence",
                column: "ContextImageId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePickSentence_LanguageId",
                table: "GamePickSentence",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageStat_LanguageId",
                table: "LanguageStat",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageStat_UserId",
                table: "LanguageStat",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameFillBlank");

            migrationBuilder.DropTable(
                name: "GameFlashCard");

            migrationBuilder.DropTable(
                name: "GameObjectLocalized");

            migrationBuilder.DropTable(
                name: "GamePickSentence");

            migrationBuilder.DropTable(
                name: "LanguageStat");

            migrationBuilder.DropTable(
                name: "GameObject");

            migrationBuilder.DropTable(
                name: "ContextImage");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "GameObjectCategory");
        }
    }
}
