using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TomasosPizzeria.Data.Migrations.Pizza
{
    /// <inheritdoc />
    public partial class updated_ingredients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Dishes_DishId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_DishId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "DishId",
                table: "Ingredients");

            migrationBuilder.CreateTable(
                name: "DishIngredient",
                columns: table => new
                {
                    DishesDishId = table.Column<int>(type: "int", nullable: false),
                    IngredientsIngredientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishIngredient", x => new { x.DishesDishId, x.IngredientsIngredientId });
                    table.ForeignKey(
                        name: "FK_DishIngredient_Dishes_DishesDishId",
                        column: x => x.DishesDishId,
                        principalTable: "Dishes",
                        principalColumn: "DishId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DishIngredient_Ingredients_IngredientsIngredientId",
                        column: x => x.IngredientsIngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DishIngredient_IngredientsIngredientId",
                table: "DishIngredient",
                column: "IngredientsIngredientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DishIngredient");

            migrationBuilder.AddColumn<int>(
                name: "DishId",
                table: "Ingredients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_DishId",
                table: "Ingredients",
                column: "DishId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Dishes_DishId",
                table: "Ingredients",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "DishId");
        }
    }
}
