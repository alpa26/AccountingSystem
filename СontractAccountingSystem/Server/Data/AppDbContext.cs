using Microsoft.EntityFrameworkCore;
using Salazki.Security;
using System.Collections.Generic;
using System.Reflection;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Models.Interfaces;

namespace СontractAccountingSystem.Server.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
    public AppDbContext()
    {
    }
    public DbSet<DocPayStatus> DocPayStatuses { get; set; }
    public DbSet<DocType> DocTypes { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<DocPaymentDeadlines> DocPaymentDeadlines { get; set; }
    public DbSet<KontrAgent> KontrAgents { get; set; }
    public DbSet<KontrAgentType> KontrAgentTypes { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<PaymentType> PaymentTypes { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectDocuments> ProjectDocuments { get; set; }
    public DbSet<RelateDocuments> RelateDocuments { get; set; }
    public DbSet<Role> UserRoles { get; set; }
    public DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<DocPayStatus>().ToTable("doc_pay_statuses");
        modelBuilder.Entity<DocType>().ToTable("document_types");
        modelBuilder.Entity<Document>().ToTable("documents");
        modelBuilder.Entity<DocPaymentDeadlines>().ToTable("document_deadlines");
        modelBuilder.Entity<KontrAgent>().ToTable("kontr_agents");
        modelBuilder.Entity<KontrAgentType>().ToTable("kontr_agent_types");
        modelBuilder.Entity<Notification>().ToTable("notifications");
        modelBuilder.Entity<Organization>().ToTable("organizations");
        modelBuilder.Entity<PaymentType>().ToTable("payment_types");
        modelBuilder.Entity<Project>().ToTable("projects");
        modelBuilder.Entity<ProjectDocuments>().ToTable("projects_to_documents");
        modelBuilder.Entity<RelateDocuments>().ToTable("related_documents");
        modelBuilder.Entity<Role>().ToTable("user_roles");
        modelBuilder.Entity<User>().ToTable("users");


        modelBuilder.Entity<Document>(
            x => x.Property(e => e.CreatedDate)
                .HasDefaultValue(DateTime.UtcNow.Date)
        );
        modelBuilder.Entity<Notification>(
            x => x.Property(e => e.Date)
                .HasDefaultValue(DateTime.UtcNow.Date)
        );


        //KontrAgent
        modelBuilder.Entity<KontrAgent>().HasOne(k => k.User).WithMany()
        .HasForeignKey(k => k.UserId);
        modelBuilder.Entity<KontrAgent>().HasOne(d => d.Type).WithMany()
        .HasForeignKey(x => x.TypeId);

        //Document
        modelBuilder.Entity<Document>().HasOne(d => d.Organization).WithMany()
        .HasForeignKey(x => x.OrganizationId);

        modelBuilder.Entity<Document>().HasOne(d => d.Type).WithMany()
        .HasForeignKey(x => x.TypeId);

        modelBuilder.Entity<Document>().HasOne(d => d.PayStatus).WithMany()
        .HasForeignKey(x => x.PayStatusId);

        modelBuilder.Entity<Document>().HasOne(d => d.PaymentType).WithMany()
        .HasForeignKey(x => x.PaymentTypeId);

        modelBuilder.Entity<Document>().HasOne(d => d.Employer).WithMany()
        .HasForeignKey(x => x.EmployerId);

        modelBuilder.Entity<Document>().HasOne(d => d.KontrAgent).WithMany()
        .HasForeignKey(x => x.KontrAgentId);

        // User 
        modelBuilder.Entity<User>().HasOne(k => k.Role).WithMany()
        .HasForeignKey(r => r.RoleId);

        //RelatedDocuments 
        modelBuilder.Entity<RelateDocuments>().HasOne(d => d.Document1).WithMany()
        .HasForeignKey(x => x.Document1Id);
        modelBuilder.Entity<RelateDocuments>().HasOne(d => d.Document2).WithMany()
        .HasForeignKey(x => x.Document2Id);

        //Notification 
        modelBuilder.Entity<Notification>().HasOne(d => d.Project).WithMany()
        .HasForeignKey(x => x.ProjectId);
        modelBuilder.Entity<Notification>().HasOne(d => d.Document).WithMany()
        .HasForeignKey(x => x.DocumentId);

        //ProjectDocuments 
        modelBuilder.Entity<ProjectDocuments>().HasOne(d => d.Project).WithMany()
        .HasForeignKey(x => x.ProjectId);
        modelBuilder.Entity<ProjectDocuments>().HasOne(d => d.Document).WithMany()
        .HasForeignKey(x => x.DocumentId);

        //DocPaymentDeadlines 
        modelBuilder.Entity<DocPaymentDeadlines>().HasOne(d => d.Document).WithMany()
        .HasForeignKey(x => x.DocumentId);

    }
}
