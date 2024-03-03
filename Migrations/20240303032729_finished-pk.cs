using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkshopApi.Migrations
{
    /// <inheritdoc />
    public partial class finishedpk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkshopCollaborator");

            migrationBuilder.CreateTable(
                name: "CollaboratorWorkshop",
                columns: table => new
                {
                    CollaboratorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WorkshopId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorWorkshop", x => new { x.CollaboratorId, x.WorkshopId });
                    table.ForeignKey(
                        name: "FK_CollaboratorWorkshop_Collaborators_CollaboratorId",
                        column: x => x.CollaboratorId,
                        principalTable: "Collaborators",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CollaboratorWorkshop_Workshop_WorkshopId",
                        column: x => x.WorkshopId,
                        principalTable: "Workshop",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorWorkshop_WorkshopId",
                table: "CollaboratorWorkshop",
                column: "WorkshopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollaboratorWorkshop");

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
    }
}
