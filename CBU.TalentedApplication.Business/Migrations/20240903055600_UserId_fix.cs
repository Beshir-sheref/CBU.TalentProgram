using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CBU.TalentedApplication.Business.Migrations
{
    /// <inheritdoc />
    public partial class UserId_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DApplicantDocumentocs");

            migrationBuilder.CreateTable(
                name: "ApplicantDocument",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    DocumentPath = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicantDocument_Applicant",
                        column: x => x.ApplicantId,
                        principalTable: "Applicant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicantDocument_Document",
                        column: x => x.DocumentId,
                        principalTable: "Document",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantDocument_ApplicantId",
                table: "ApplicantDocument",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantDocument_DocumentId",
                table: "ApplicantDocument",
                column: "DocumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicantDocument");

            migrationBuilder.CreateTable(
                name: "DApplicantDocumentocs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    DocPath = table.Column<int>(type: "int", nullable: false),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DApplicantDocumentocs", x => x.Id);
                });
        }
    }
}
