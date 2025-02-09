using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    [ExcludeFromCodeCoverage]
    /// <inheritdoc />
    public partial class AddUpdatedAtToEntityBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Contacts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Authentications",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.Sql(@"INSERT INTO public.""Authentications"" (""Id"", ""Email"", ""Password"", ""EmailValidated"", ""ChangePassword"", ""Profile"", ""CreatedAt"", ""UpdatedAt"") VALUES (1, 'rogeriosouzax@hotmail.com', 'EC7117851C0E5DBAAD4EFFDB7CD17C050CEA88CB', false, false, 1, '2024-08-03 20:35:30.531728 +00:00', '-infinity')");
            
            migrationBuilder.Sql(
                @"INSERT INTO public.""Users"" (""Id"", ""FirstName"", ""LastName"", ""Email"", ""Cpf"", ""AuthenticationId"", ""CreatedAt"", ""UpdatedAt"") VALUES (1, 'Rogerio', 'da Silva Souza', 'rogeriosouzax@hotmail.com', '38865053836', 1, '2024-08-03 20:35:30.531365 +00:00', '-infinity')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Authentications");
        }
    }
}
