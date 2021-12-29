using Microsoft.EntityFrameworkCore.Migrations;

namespace BestGiftsAPI.Migrations
{
    public partial class new_seed_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[] { 4, "Walentynki" });

            migrationBuilder.InsertData(
                table: "GiftIdeas",
                columns: new[] { "GiftIdeaId", "Author", "Description", "LikesCounter", "Name" },
                values: new object[] { 4, "Ewelina", "Malinowe, białe, czarne, zielone, a może niebiskie", 17, "Zestaw herbat" });

            migrationBuilder.InsertData(
                table: "GiftIdeas",
                columns: new[] { "GiftIdeaId", "Author", "Description", "LikesCounter", "Name" },
                values: new object[] { 5, "Amadi", "W luym często może być zimno", 0, "Rękawice na zime" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentAuthor", "CommentContent", "GiftIdeaId" },
                values: new object[,]
                {
                    { 4, "Janusz 4000", "Sprzedam opla", 4 },
                    { 5, "Grażynka", "Piękne. Mąż zachwycony", 5 },
                    { 6, "Piotr Fronczewski", "Polecam", 5 }
                });

            migrationBuilder.InsertData(
                table: "GiftIdeaCategories",
                columns: new[] { "CategoryId", "GiftIdeaId" },
                values: new object[,]
                {
                    { 4, 4 },
                    { 4, 5 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "GiftIdeaCategories",
                keyColumns: new[] { "CategoryId", "GiftIdeaId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "GiftIdeaCategories",
                keyColumns: new[] { "CategoryId", "GiftIdeaId" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GiftIdeas",
                keyColumn: "GiftIdeaId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GiftIdeas",
                keyColumn: "GiftIdeaId",
                keyValue: 5);
        }
    }
}
