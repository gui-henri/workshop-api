using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkshopApi.Migrations
{
    /// <inheritdoc />
    public partial class addtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkshopCollaborator",
                columns: table => new
                {
                    CollaboratorsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WorkshopsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkshopCollaborator", x => new { x.CollaboratorsId, x.WorkshopsId });
                    table.ForeignKey(
                        name: "FK_WorkshopCollaborator_Collaborators_CollaboratorsId",
                        column: x => x.CollaboratorsId,
                        principalTable: "Collaborators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkshopCollaborator_Workshop_WorkshopsId",
                        column: x => x.WorkshopsId,
                        principalTable: "Workshop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_WorkshopCollaborator_WorkshopsId",
                table: "WorkshopCollaborator",
                column: "WorkshopsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkshopCollaborator");
        }
    }
}
