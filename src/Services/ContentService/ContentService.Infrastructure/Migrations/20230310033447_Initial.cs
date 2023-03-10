using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Content");

            migrationBuilder.CreateTable(
                name: "FormConfigs",
                schema: "Content",
                columns: table => new
                {
                    FormConfigId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormConfigs", x => x.FormConfigId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Content",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "FieldConfigs",
                schema: "Content",
                columns: table => new
                {
                    FieldConfigId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropsLabel = table.Column<string>(name: "Props_Label", type: "nvarchar(max)", nullable: false),
                    PropsPlaceholder = table.Column<string>(name: "Props_Placeholder", type: "nvarchar(max)", nullable: false),
                    PropsRequired = table.Column<bool>(name: "Props_Required", type: "bit", nullable: false),
                    FormConfigId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldConfigs", x => x.FieldConfigId);
                    table.ForeignKey(
                        name: "FK_FieldConfigs_FormConfigs_FormConfigId",
                        column: x => x.FormConfigId,
                        principalSchema: "Content",
                        principalTable: "FormConfigs",
                        principalColumn: "FormConfigId");
                });

            migrationBuilder.CreateTable(
                name: "JsonSchemaModels",
                schema: "Content",
                columns: table => new
                {
                    JsonSchemaModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormConfigId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JsonSchemaModels", x => x.JsonSchemaModelId);
                    table.ForeignKey(
                        name: "FK_JsonSchemaModels_FormConfigs_FormConfigId",
                        column: x => x.FormConfigId,
                        principalSchema: "Content",
                        principalTable: "FormConfigs",
                        principalColumn: "FormConfigId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contents",
                schema: "Content",
                columns: table => new
                {
                    ContentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JsonSchemaModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Json = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.ContentId);
                    table.ForeignKey(
                        name: "FK_Contents_JsonSchemaModels_JsonSchemaModelId",
                        column: x => x.JsonSchemaModelId,
                        principalSchema: "Content",
                        principalTable: "JsonSchemaModels",
                        principalColumn: "JsonSchemaModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JsonPropertyModels",
                schema: "Content",
                columns: table => new
                {
                    JsonPropertyModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JsonSchemaModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JsonPropertyModels", x => x.JsonPropertyModelId);
                    table.ForeignKey(
                        name: "FK_JsonPropertyModels_JsonSchemaModels_JsonSchemaModelId",
                        column: x => x.JsonSchemaModelId,
                        principalSchema: "Content",
                        principalTable: "JsonSchemaModels",
                        principalColumn: "JsonSchemaModelId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contents_JsonSchemaModelId",
                schema: "Content",
                table: "Contents",
                column: "JsonSchemaModelId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldConfigs_FormConfigId",
                schema: "Content",
                table: "FieldConfigs",
                column: "FormConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_JsonPropertyModels_JsonSchemaModelId",
                schema: "Content",
                table: "JsonPropertyModels",
                column: "JsonSchemaModelId");

            migrationBuilder.CreateIndex(
                name: "IX_JsonSchemaModels_FormConfigId",
                schema: "Content",
                table: "JsonSchemaModels",
                column: "FormConfigId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contents",
                schema: "Content");

            migrationBuilder.DropTable(
                name: "FieldConfigs",
                schema: "Content");

            migrationBuilder.DropTable(
                name: "JsonPropertyModels",
                schema: "Content");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Content");

            migrationBuilder.DropTable(
                name: "JsonSchemaModels",
                schema: "Content");

            migrationBuilder.DropTable(
                name: "FormConfigs",
                schema: "Content");
        }
    }
}
