using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerceApi.Migrations
{
    /// <inheritdoc />
    public partial class FixDecimalDataTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "numeric(12,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(6,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "ProductReviews",
                type: "numeric(6,1)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "GeneralQualityRating",
                table: "ProductReviews",
                type: "numeric(6,1)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "CostBenefitRating",
                table: "ProductReviews",
                type: "numeric(6,1)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "numeric(6,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "ProductReviews",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(6,1)");

            migrationBuilder.AlterColumn<int>(
                name: "GeneralQualityRating",
                table: "ProductReviews",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(6,1)");

            migrationBuilder.AlterColumn<int>(
                name: "CostBenefitRating",
                table: "ProductReviews",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(6,1)");
        }
    }
}
