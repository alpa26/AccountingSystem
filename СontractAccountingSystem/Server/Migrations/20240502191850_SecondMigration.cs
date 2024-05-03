using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace СontractAccountingSystem.Server.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "contract_payments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_documents_Number",
                table: "documents",
                column: "Number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_documents_Number",
                table: "documents");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "contract_payments");
        }
    }
}
