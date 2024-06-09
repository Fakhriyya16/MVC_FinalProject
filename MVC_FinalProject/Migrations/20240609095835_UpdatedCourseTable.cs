using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_FinalProject.Migrations
{
    public partial class UpdatedCourseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "Courses",
                newName: "EndDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Courses",
                newName: "EndTime");
        }
    }
}
