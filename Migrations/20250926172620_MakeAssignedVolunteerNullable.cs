using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisasterRelief.Migrations
{
    /// <inheritdoc />
    public partial class MakeAssignedVolunteerNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_AspNetUsers_DonorId",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerTasks_AspNetUsers_AssignedVolunteerId",
                table: "VolunteerTasks");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "VolunteerTasks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AssignedVolunteerId",
                table: "VolunteerTasks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "DonorId",
                table: "Donations",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_AspNetUsers_DonorId",
                table: "Donations",
                column: "DonorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerTasks_AspNetUsers_AssignedVolunteerId",
                table: "VolunteerTasks",
                column: "AssignedVolunteerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_AspNetUsers_DonorId",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerTasks_AspNetUsers_AssignedVolunteerId",
                table: "VolunteerTasks");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "VolunteerTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssignedVolunteerId",
                table: "VolunteerTasks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DonorId",
                table: "Donations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_AspNetUsers_DonorId",
                table: "Donations",
                column: "DonorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerTasks_AspNetUsers_AssignedVolunteerId",
                table: "VolunteerTasks",
                column: "AssignedVolunteerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
