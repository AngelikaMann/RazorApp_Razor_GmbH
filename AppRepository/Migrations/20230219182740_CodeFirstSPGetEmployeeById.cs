using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstRazorApp.Services.Migrations
{
    public partial class CodeFirstSPGetEmployeeById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Create Procedure CodeFirstSPGetEmployeeById
                       @Id int 
                       as
                       Begin
                           Select * from Employees
                            where Id= @Id
                        End";
            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            string procedure = @"Drop procedureCodeFirstSPGetEmployeeById";
            migrationBuilder.Sql(procedure);

        }
    }
}
