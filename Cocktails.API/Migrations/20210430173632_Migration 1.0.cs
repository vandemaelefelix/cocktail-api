using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cocktails.API.Migrations
{
    public partial class Migration10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Cocktails",
                columns: table => new
                {
                    CocktailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alcoholic = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cocktails", x => x.CocktailId);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    IngredientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlcoholPercentage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.IngredientId);
                });

            migrationBuilder.CreateTable(
                name: "CocktailCategories",
                columns: table => new
                {
                    CocktailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CocktailCategories", x => new { x.CocktailId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_CocktailCategories_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CocktailCategories_Cocktails_CocktailId",
                        column: x => x.CocktailId,
                        principalTable: "Cocktails",
                        principalColumn: "CocktailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CocktailIngredients",
                columns: table => new
                {
                    CocktailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IngredientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CocktailIngredients", x => new { x.CocktailId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_CocktailIngredients_Cocktails_CocktailId",
                        column: x => x.CocktailId,
                        principalTable: "Cocktails",
                        principalColumn: "CocktailId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CocktailIngredients_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredient",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Old fashioned" },
                    { 2, "Classic" },
                    { 3, "Strong" }
                });

            migrationBuilder.InsertData(
                table: "Ingredient",
                columns: new[] { "IngredientId", "AlcoholPercentage", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("0fed5428-0694-4db8-a896-6b48787a9abb"), 41, "Alcoholic substance drunk by pirates", "Rum" },
                    { new Guid("0330d1dc-2cb7-407c-8ad8-b52d8ec2cb54"), 39, "Alcoholic substance drunk by Russians", "Vodka" },
                    { new Guid("c18fe643-3e99-46b0-8fc3-958554d434e8"), 38, "Alcoholic substance drunk by Alcoholics", "Gin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CocktailCategories_CategoryId",
                table: "CocktailCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CocktailIngredients_IngredientId",
                table: "CocktailIngredients",
                column: "IngredientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CocktailCategories");

            migrationBuilder.DropTable(
                name: "CocktailIngredients");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Cocktails");

            migrationBuilder.DropTable(
                name: "Ingredient");
        }
    }
}
