using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SentiChat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSentimentToMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SentimentResult",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "Sentiment",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sentiment",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "SentimentResult",
                table: "Messages",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);
        }
    }
}
