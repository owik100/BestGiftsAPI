using Microsoft.EntityFrameworkCore.Migrations;

namespace BestGiftsAPI.Migrations
{
    public partial class Addauthorcoment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommentAuthor",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Dla niego" },
                    { 2, "Dla niej" },
                    { 3, "Na urodziny" }
                });

            migrationBuilder.InsertData(
                table: "GiftIdeas",
                columns: new[] { "GiftIdeaId", "Author", "Description", "LikesCounter", "Name" },
                values: new object[,]
                {
                    { 1, "Jan", "Można kupić np. na twoje_marzenia.com", 5, "Kolacja we dwoje" },
                    { 2, "Maciek", "Zmienia kolor w zależności od temperatuy napoju", 2, "Kubek termiczny" },
                    { 3, "Paulina", "Własnoręcznie wydziergane skarpety. Czy może być coś lepszego? :)", -3, "Skarpety" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentAuthor", "CommentContent", "GiftIdeaId" },
                values: new object[,]
                {
                    { 1, "Zbyszek", "Super pomysł!", 1 },
                    { 2, "Karolina", "Nudy", 2 },
                    { 3, "Krzychux1200", "Ale to drogie", 2 }
                });

            migrationBuilder.InsertData(
                table: "GiftIdeaCategories",
                columns: new[] { "CategoryId", "GiftIdeaId" },
                values: new object[,]
                {
                    { 3, 1 },
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 3, 2 },
                    { 3, 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "GiftIdeaCategories",
                keyColumns: new[] { "CategoryId", "GiftIdeaId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "GiftIdeaCategories",
                keyColumns: new[] { "CategoryId", "GiftIdeaId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "GiftIdeaCategories",
                keyColumns: new[] { "CategoryId", "GiftIdeaId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "GiftIdeaCategories",
                keyColumns: new[] { "CategoryId", "GiftIdeaId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "GiftIdeaCategories",
                keyColumns: new[] { "CategoryId", "GiftIdeaId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "GiftIdeaCategories",
                keyColumns: new[] { "CategoryId", "GiftIdeaId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "GiftIdeas",
                keyColumn: "GiftIdeaId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GiftIdeas",
                keyColumn: "GiftIdeaId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GiftIdeas",
                keyColumn: "GiftIdeaId",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "CommentAuthor",
                table: "Comments");
        }
    }
}
