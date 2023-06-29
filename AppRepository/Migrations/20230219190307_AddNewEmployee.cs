using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstRazorApp.Services.Migrations
{
    public partial class AddNewEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Create procedure spAddNewExployee
                @Name nvarchar(max),
                @Email nvarchar(max), 
                @PotoPath nvarchar(max),
                @Dept int 
                as
                Begin
                    INSERT INTO Employees(Name,Email,PotoPath,Department)
                    Values(@Name,@Email,@PotoPath,@Dept)
                    End";
            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Drop procedure spAddNewExployee";
            migrationBuilder.Sql(procedure);

        }
    }
}
