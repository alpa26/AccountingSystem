using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace СontractAccountingSystem.Server.Migrations
{
    /// <inheritdoc />
    public partial class ProjectMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contract_pay_statuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contract_pay_statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "doc_pay_types",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doc_pay_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "doc_statuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doc_statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "document_types",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserLogin<string>",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserLogin<string>", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserRole<string>",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserRole<string>", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserToken<string>",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserToken<string>", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "kontr_agent_types",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kontr_agent_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "organizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NormalizedName = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "workers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    SecondName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    StaffPosition = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "kontr_agents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    TypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    INN = table.Column<string>(type: "text", nullable: false),
                    KPP = table.Column<string>(type: "text", nullable: false),
                    ContactPerson = table.Column<string>(type: "text", nullable: false),
                    ContactPhone = table.Column<string>(type: "text", nullable: false),
                    ContactEmail = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kontr_agents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kontr_agents_kontr_agent_types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "kontr_agent_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    SecondName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    NormalizedUserName = table.Column<string>(type: "text", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "text", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_user_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "user_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2024, 5, 4, 0, 0, 0, 0, DateTimeKind.Utc)),
                    DeadlineStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeadlineEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    WorkDescription = table.Column<string>(type: "text", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    TypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocStatusId = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    KontrAgentId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uuid", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_documents_doc_pay_types_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "doc_pay_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_documents_doc_statuses_DocStatusId",
                        column: x => x.DocStatusId,
                        principalTable: "doc_statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_documents_document_types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "document_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_documents_kontr_agents_KontrAgentId",
                        column: x => x.KontrAgentId,
                        principalTable: "kontr_agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_documents_organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_documents_users_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "contract_payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeadlineStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeadlineEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentStatusId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contract_payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_contract_payments_contract_pay_statuses_PaymentStatusId",
                        column: x => x.PaymentStatusId,
                        principalTable: "contract_pay_statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_contract_payments_documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "labor_hour_cost",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkerId = table.Column<Guid>(type: "uuid", nullable: false),
                    HourlyRate = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_labor_hour_cost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_labor_hour_cost_documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_labor_hour_cost_workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2024, 5, 4, 0, 0, 0, 0, DateTimeKind.Utc))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_notifications_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_notifications_documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "related_documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Document1Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Document2Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_related_documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_related_documents_documents_Document1Id",
                        column: x => x.Document1Id,
                        principalTable: "documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_related_documents_documents_Document2Id",
                        column: x => x.Document2Id,
                        principalTable: "documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "worked_labor_hour",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymenttId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkerId = table.Column<Guid>(type: "uuid", nullable: false),
                    HourlyRate = table.Column<decimal>(type: "numeric", nullable: false),
                    WorkedHours = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_worked_labor_hour", x => x.Id);
                    table.ForeignKey(
                        name: "FK_worked_labor_hour_contract_payments_PaymenttId",
                        column: x => x.PaymenttId,
                        principalTable: "contract_payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_worked_labor_hour_workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_contract_payments_DocumentId",
                table: "contract_payments",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_contract_payments_PaymentStatusId",
                table: "contract_payments",
                column: "PaymentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_documents_DocStatusId",
                table: "documents",
                column: "DocStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_documents_EmployeeId",
                table: "documents",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_documents_KontrAgentId",
                table: "documents",
                column: "KontrAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_documents_Number",
                table: "documents",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_documents_OrganizationId",
                table: "documents",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_documents_PaymentTypeId",
                table: "documents",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_documents_TypeId",
                table: "documents",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_kontr_agents_TypeId",
                table: "kontr_agents",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_labor_hour_cost_DocumentId",
                table: "labor_hour_cost",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_labor_hour_cost_WorkerId",
                table: "labor_hour_cost",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_DocumentId",
                table: "notifications",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_ProjectId",
                table: "notifications",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_related_documents_Document1Id",
                table: "related_documents",
                column: "Document1Id");

            migrationBuilder.CreateIndex(
                name: "IX_related_documents_Document2Id",
                table: "related_documents",
                column: "Document2Id");

            migrationBuilder.CreateIndex(
                name: "IX_users_RoleId",
                table: "users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_worked_labor_hour_PaymenttId",
                table: "worked_labor_hour",
                column: "PaymenttId");

            migrationBuilder.CreateIndex(
                name: "IX_worked_labor_hour_WorkerId",
                table: "worked_labor_hour",
                column: "WorkerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityUserLogin<string>");

            migrationBuilder.DropTable(
                name: "IdentityUserRole<string>");

            migrationBuilder.DropTable(
                name: "IdentityUserToken<string>");

            migrationBuilder.DropTable(
                name: "labor_hour_cost");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "related_documents");

            migrationBuilder.DropTable(
                name: "worked_labor_hour");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "contract_payments");

            migrationBuilder.DropTable(
                name: "workers");

            migrationBuilder.DropTable(
                name: "contract_pay_statuses");

            migrationBuilder.DropTable(
                name: "documents");

            migrationBuilder.DropTable(
                name: "doc_pay_types");

            migrationBuilder.DropTable(
                name: "doc_statuses");

            migrationBuilder.DropTable(
                name: "document_types");

            migrationBuilder.DropTable(
                name: "kontr_agents");

            migrationBuilder.DropTable(
                name: "organizations");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "kontr_agent_types");

            migrationBuilder.DropTable(
                name: "user_roles");
        }
    }
}
