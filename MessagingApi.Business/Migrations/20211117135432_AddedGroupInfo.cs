using Microsoft.EntityFrameworkCore.Migrations;

namespace MessagingApi.Business.Migrations
{
    public partial class AddedGroupInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxUsers",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Private",
                table: "Groups",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "fdd469e3-a6f2-468e-a348-b51bfd2a4e2f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "b161f35d-34b5-47df-bbc4-3e15ba52d7de");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "7b2711fe-25ac-4e23-a7bd-e9961e1d4d93");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxUsers",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Private",
                table: "Groups");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "006e138e-6af4-498c-80e6-63c35f1ae2b2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "dce50acd-cf2b-484a-b65b-7aaaada24bb7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "8a23a3d6-59f7-42ea-9c8b-25e3d3d6ebd6");
        }
    }
}
