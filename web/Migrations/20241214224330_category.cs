using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web.Migrations
{
    /// <inheritdoc />
    public partial class category : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budget_Catrgory_CategoryId",
                table: "Budget");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Catrgory_CategoryId",
                table: "Transaction");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Transaction",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Budget",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Budget_Catrgory_CategoryId",
                table: "Budget",
                column: "CategoryId",
                principalTable: "Catrgory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Catrgory_CategoryId",
                table: "Transaction",
                column: "CategoryId",
                principalTable: "Catrgory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budget_Catrgory_CategoryId",
                table: "Budget");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Catrgory_CategoryId",
                table: "Transaction");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Transaction",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Budget",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Budget_Catrgory_CategoryId",
                table: "Budget",
                column: "CategoryId",
                principalTable: "Catrgory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Catrgory_CategoryId",
                table: "Transaction",
                column: "CategoryId",
                principalTable: "Catrgory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
