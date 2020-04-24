using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderManagementDao_Mysql.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authority_Account",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Account = table.Column<string>(type: "varchar(200)", nullable: false),
                    Pass = table.Column<string>(type: "varchar(200)", nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authority_Account", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Authority_PermissionBase",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Area = table.Column<string>(type: "nvarchar(400)", nullable: false),
                    Control = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authority_PermissionBase", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Authority_Role",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authority_Role", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Authority_RelatedAccountRole",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccountID = table.Column<int>(nullable: false),
                    RoleID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authority_RelatedAccountRole", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Authority_RelatedAccountRole_Authority_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Authority_Account",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Authority_RelatedAccountRole_Authority_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Authority_Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Authority_RelatedRoleBasePer",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BasePerID = table.Column<int>(nullable: false),
                    RoleID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authority_RelatedRoleBasePer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Authority_RelatedRoleBasePer_Authority_PermissionBase_BasePe~",
                        column: x => x.BasePerID,
                        principalTable: "Authority_PermissionBase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Authority_RelatedRoleBasePer_Authority_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Authority_Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authority_Account_Account",
                table: "Authority_Account",
                column: "Account");

            migrationBuilder.CreateIndex(
                name: "IX_Authority_PermissionBase_Action",
                table: "Authority_PermissionBase",
                column: "Action");

            migrationBuilder.CreateIndex(
                name: "IX_Authority_PermissionBase_Area",
                table: "Authority_PermissionBase",
                column: "Area");

            migrationBuilder.CreateIndex(
                name: "IX_Authority_PermissionBase_Control",
                table: "Authority_PermissionBase",
                column: "Control");

            migrationBuilder.CreateIndex(
                name: "IX_Authority_RelatedAccountRole_AccountID",
                table: "Authority_RelatedAccountRole",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Authority_RelatedAccountRole_RoleID",
                table: "Authority_RelatedAccountRole",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Authority_RelatedRoleBasePer_BasePerID",
                table: "Authority_RelatedRoleBasePer",
                column: "BasePerID");

            migrationBuilder.CreateIndex(
                name: "IX_Authority_RelatedRoleBasePer_RoleID",
                table: "Authority_RelatedRoleBasePer",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Authority_Role_RoleName",
                table: "Authority_Role",
                column: "RoleName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authority_RelatedAccountRole");

            migrationBuilder.DropTable(
                name: "Authority_RelatedRoleBasePer");

            migrationBuilder.DropTable(
                name: "Authority_Account");

            migrationBuilder.DropTable(
                name: "Authority_PermissionBase");

            migrationBuilder.DropTable(
                name: "Authority_Role");
        }
    }
}
