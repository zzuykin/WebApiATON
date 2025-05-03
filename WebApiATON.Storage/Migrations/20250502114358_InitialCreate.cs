using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiATON.Storage.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Login = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false),
                    Birthday = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Admin = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    RevokedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RevokedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
       table: "Users",
       columns: new[]
       {
            "Id", "Login", "Password", "Name", "Gender", "Birthday", "Admin",
            "CreatedOn", "CreatedBy", "ModifiedOn", "ModifiedBy", "RevokedOn", "RevokedBy"
       },
       values: new object[]
       {
            Guid.NewGuid(), 
            "admin",        
            "admin",        
            "Administrator",
            2,              
            null,           
            true,           
            DateTime.UtcNow,
            "System",       
            null, null, null, null
       });
        }

        /// <inheritdoc />
        /// 

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
