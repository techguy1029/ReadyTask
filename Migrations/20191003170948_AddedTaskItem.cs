using Microsoft.EntityFrameworkCore.Migrations;

namespace ReadyTask.Migrations
{
    public partial class AddedTaskItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_AspNetUsers_AssigneduserId",
                table: "TaskItems");

            migrationBuilder.DropColumn(
                name: "AssignUserId",
                table: "TaskItems");

            migrationBuilder.RenameColumn(
                name: "AssigneduserId",
                table: "TaskItems",
                newName: "AssignedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskItems_AssigneduserId",
                table: "TaskItems",
                newName: "IX_TaskItems_AssignedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_AspNetUsers_AssignedUserId",
                table: "TaskItems",
                column: "AssignedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_AspNetUsers_AssignedUserId",
                table: "TaskItems");

            migrationBuilder.RenameColumn(
                name: "AssignedUserId",
                table: "TaskItems",
                newName: "AssigneduserId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskItems_AssignedUserId",
                table: "TaskItems",
                newName: "IX_TaskItems_AssigneduserId");

            migrationBuilder.AddColumn<int>(
                name: "AssignUserId",
                table: "TaskItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_AspNetUsers_AssigneduserId",
                table: "TaskItems",
                column: "AssigneduserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
