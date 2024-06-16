using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataServices.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone_number = table.Column<int>(type: "int", nullable: false),
                    password_hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    display_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bio_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    avatar_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    follower_count = table.Column<int>(type: "int", nullable: true),
                    following_count = table.Column<int>(type: "int", nullable: true),
                    likes_count = table.Column<int>(type: "int", nullable: true),
                    video_count = table.Column<int>(type: "int", nullable: true),
                    is_verify = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Followers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    follower_id = table.Column<int>(type: "int", nullable: false),
                    following_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Followers", x => x.id);
                    table.ForeignKey(
                        name: "FK_Followers_Users_follower_id",
                        column: x => x.follower_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Followers_Users_following_id",
                        column: x => x.following_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    jwt_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_used = table.Column<bool>(type: "bit", nullable: false),
                    is_revoked = table.Column<bool>(type: "bit", nullable: false),
                    added_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    expiry_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    video_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    thumbnail_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    likes_count = table.Column<int>(type: "int", nullable: false),
                    views_count = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Videos_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    video_id = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.id);
                    table.ForeignKey(
                        name: "FK_Comments_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Videos_video_id",
                        column: x => x.video_id,
                        principalTable: "Videos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Favourites",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    video_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favourites", x => x.id);
                    table.ForeignKey(
                        name: "FK_Favourites_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Favourites_Videos_video_id",
                        column: x => x.video_id,
                        principalTable: "Videos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    video_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.id);
                    table.ForeignKey(
                        name: "FK_Likes_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Likes_Videos_video_id",
                        column: x => x.video_id,
                        principalTable: "Videos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_user_id",
                table: "Comments",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_video_id",
                table: "Comments",
                column: "video_id");

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_user_id",
                table: "Favourites",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_video_id",
                table: "Favourites",
                column: "video_id");

            migrationBuilder.CreateIndex(
                name: "IX_Followers_follower_id",
                table: "Followers",
                column: "follower_id");

            migrationBuilder.CreateIndex(
                name: "IX_Followers_following_id",
                table: "Followers",
                column: "following_id");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_user_id",
                table: "Likes",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_video_id",
                table: "Likes",
                column: "video_id");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_user_id",
                table: "RefreshTokens",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_user_id",
                table: "Videos",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Favourites");

            migrationBuilder.DropTable(
                name: "Followers");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
