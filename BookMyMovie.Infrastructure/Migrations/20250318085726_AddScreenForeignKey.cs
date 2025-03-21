using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMyMovie.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddScreenForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScreenShowTimes_Theatres_TheatreId",
                table: "ScreenShowTimes");

            migrationBuilder.DropTable(
                name: "ScreenScreenShowTime");

            migrationBuilder.AlterColumn<Guid>(
                name: "TheatreId",
                table: "ScreenShowTimes",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "ScreenId",
                table: "ScreenShowTimes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ScreenId1",
                table: "ScreenShowTimes",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScreenShowTimes_ScreenId",
                table: "ScreenShowTimes",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenShowTimes_ScreenId1",
                table: "ScreenShowTimes",
                column: "ScreenId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ScreenShowTimes_Screens_ScreenId",
                table: "ScreenShowTimes",
                column: "ScreenId",
                principalTable: "Screens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScreenShowTimes_Screens_ScreenId1",
                table: "ScreenShowTimes",
                column: "ScreenId1",
                principalTable: "Screens",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScreenShowTimes_Theatres_TheatreId",
                table: "ScreenShowTimes",
                column: "TheatreId",
                principalTable: "Theatres",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScreenShowTimes_Screens_ScreenId",
                table: "ScreenShowTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_ScreenShowTimes_Screens_ScreenId1",
                table: "ScreenShowTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_ScreenShowTimes_Theatres_TheatreId",
                table: "ScreenShowTimes");

            migrationBuilder.DropIndex(
                name: "IX_ScreenShowTimes_ScreenId",
                table: "ScreenShowTimes");

            migrationBuilder.DropIndex(
                name: "IX_ScreenShowTimes_ScreenId1",
                table: "ScreenShowTimes");

            migrationBuilder.DropColumn(
                name: "ScreenId",
                table: "ScreenShowTimes");

            migrationBuilder.DropColumn(
                name: "ScreenId1",
                table: "ScreenShowTimes");

            migrationBuilder.AlterColumn<Guid>(
                name: "TheatreId",
                table: "ScreenShowTimes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ScreenScreenShowTime",
                columns: table => new
                {
                    ScreenShowTimesId = table.Column<Guid>(type: "uuid", nullable: false),
                    ScreensId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenScreenShowTime", x => new { x.ScreenShowTimesId, x.ScreensId });
                    table.ForeignKey(
                        name: "FK_ScreenScreenShowTime_ScreenShowTimes_ScreenShowTimesId",
                        column: x => x.ScreenShowTimesId,
                        principalTable: "ScreenShowTimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScreenScreenShowTime_Screens_ScreensId",
                        column: x => x.ScreensId,
                        principalTable: "Screens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScreenScreenShowTime_ScreensId",
                table: "ScreenScreenShowTime",
                column: "ScreensId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScreenShowTimes_Theatres_TheatreId",
                table: "ScreenShowTimes",
                column: "TheatreId",
                principalTable: "Theatres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
