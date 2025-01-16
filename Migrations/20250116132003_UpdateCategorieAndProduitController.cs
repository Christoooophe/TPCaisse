using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPCaisse.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCategorieAndProduitController : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produit_Categorie_CategorieID",
                table: "Produit");

            migrationBuilder.RenameColumn(
                name: "CategorieID",
                table: "Produit",
                newName: "CategorieId");

            migrationBuilder.RenameIndex(
                name: "IX_Produit_CategorieID",
                table: "Produit",
                newName: "IX_Produit_CategorieId");

            migrationBuilder.AlterColumn<int>(
                name: "CategorieId",
                table: "Produit",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Produit_Categorie_CategorieId",
                table: "Produit",
                column: "CategorieId",
                principalTable: "Categorie",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produit_Categorie_CategorieId",
                table: "Produit");

            migrationBuilder.RenameColumn(
                name: "CategorieId",
                table: "Produit",
                newName: "CategorieID");

            migrationBuilder.RenameIndex(
                name: "IX_Produit_CategorieId",
                table: "Produit",
                newName: "IX_Produit_CategorieID");

            migrationBuilder.AlterColumn<int>(
                name: "CategorieID",
                table: "Produit",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Produit_Categorie_CategorieID",
                table: "Produit",
                column: "CategorieID",
                principalTable: "Categorie",
                principalColumn: "ID");
        }
    }
}
