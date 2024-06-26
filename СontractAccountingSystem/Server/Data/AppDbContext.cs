﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Salazki.Security;
using System.Collections.Generic;
using System.Reflection;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Entities.Interfaces;

namespace СontractAccountingSystem.Server.Data;

public class AppDbContext : IdentityDbContext<User, Role, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public AppDbContext()
    {
    }
    public DbSet<Document> Documents { get; set; }
    public DbSet<DocType> DocTypes { get; set; }
    public DbSet<DocPayType> DocPayTypes { get; set; }
    public DbSet<DocStatus> DocPayStatuses { get; set; }

    public DbSet<Payment> ContractPayments { get; set; }
    public DbSet<PaymentStatus> ContrPayStatus { get; set; }

    public DbSet<Worker> Workers { get; set; }
    public DbSet<KontrAgent> KontrAgents { get; set; }
    public DbSet<KontrAgentType> KontrAgentTypes { get; set; }

    public DbSet<Organization> Organizations { get; set; }

    //public DbSet<Project> Projects { get; set; }
    //public DbSet<ProjectToDocuments> ProjectToDocuments { get; set; }
    public DbSet<RelateDocuments> RelateDocuments { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    public DbSet<LaborHoursCost> LaborHourCost { get; set; }
    public DbSet<WorkedLaborHours> WorkedLaborHours { get; set; }


    public DbSet<Role> UserRoles { get; set; }
    public DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Worker>().ToTable("workers");
        modelBuilder.Entity<DocStatus>().ToTable("doc_statuses");
        modelBuilder.Entity<DocType>().ToTable("document_types");
        modelBuilder.Entity<Document>().ToTable("documents");
        modelBuilder.Entity<Payment>().ToTable("contract_payments");
        modelBuilder.Entity<PaymentStatus>().ToTable("contract_pay_statuses");
        modelBuilder.Entity<KontrAgent>().ToTable("kontr_agents");
        modelBuilder.Entity<KontrAgentType>().ToTable("kontr_agent_types");
        modelBuilder.Entity<Notification>().ToTable("notifications");
        modelBuilder.Entity<Organization>().ToTable("organizations");
        modelBuilder.Entity<DocPayType>().ToTable("doc_pay_types");
        modelBuilder.Entity<LaborHoursCost>().ToTable("labor_hour_cost");
        modelBuilder.Entity<WorkedLaborHours>().ToTable("worked_labor_hour");
        //modelBuilder.Entity<Project>().ToTable("projects");
        //modelBuilder.Entity<ProjectToDocuments>().ToTable("projects_to_documents");
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

        modelBuilder.Entity<User>()
        .HasMany(e => e.Documents)
        .WithMany(e => e.Users)
        .UsingEntity(
            "userdocument",
            r => r.HasOne(typeof(Document)).WithMany().HasForeignKey("DocumentId"),
            l => l.HasOne(typeof(User)).WithMany().HasForeignKey("UserId"),
            j => j.HasKey("UserId", "DocumentId"));


        modelBuilder.Entity<User>()
        .HasMany(e => e.KontrAgents)
        .WithMany(e => e.Users)
        .UsingEntity(
            "userkontragent",
            r => r.HasOne(typeof(KontrAgent)).WithMany().HasForeignKey("KontrAgentId"),
            l => l.HasOne(typeof(User)).WithMany().HasForeignKey("UserId"),
            j => j.HasKey("UserId", "KontrAgentId"));

        modelBuilder.Entity<User>()
        .HasMany(e => e.Organizations)
        .WithMany(e => e.Users)
        .UsingEntity(
            "userorganization",
            l => l.HasOne(typeof(Organization)).WithMany().HasForeignKey("OrganizationId"),
            r => r.HasOne(typeof(User)).WithMany().HasForeignKey("UserId"),
            j => j.HasKey("UserId", "OrganizationId"));

        //modelBuilder.Entity<Document>(user =>
        //{
        //    user.HasIndex(x => x.Number).IsUnique(true);
        //});

        modelBuilder.Entity<User>(user =>
        {
            user.HasIndex(x => x.UserName).IsUnique(true);
        });


        //KontrAgent
        modelBuilder.Entity<KontrAgent>().HasOne(d => d.Type).WithMany()
        .HasForeignKey(x => x.TypeId).OnDelete(DeleteBehavior.Restrict);

        //Document
        modelBuilder.Entity<Document>().HasOne(d => d.Organization).WithMany()
        .HasForeignKey(x => x.OrganizationId).OnDelete(DeleteBehavior.Restrict).IsRequired(false);

        modelBuilder.Entity<Document>().HasOne(d => d.Type).WithMany()
        .HasForeignKey(x => x.TypeId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Document>().HasOne(d => d.DocStatus).WithMany()
        .HasForeignKey(x => x.DocStatusId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Document>().HasOne(d => d.PaymentType).WithMany()
        .HasForeignKey(x => x.PaymentTypeId).OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<Document>().HasOne(d => d.KontrAgent).WithMany()
        .HasForeignKey(x => x.KontrAgentId).OnDelete(DeleteBehavior.Restrict);

        // User 
        modelBuilder.Entity<User>().HasOne(k => k.Role).WithMany()
        .HasForeignKey(r => r.RoleId).OnDelete(DeleteBehavior.Restrict);

        //RelatedDocuments 
        modelBuilder.Entity<RelateDocuments>().HasOne(d => d.Document1).WithMany()
        .HasForeignKey(x => x.Document1Id).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<RelateDocuments>().HasOne(d => d.Document2).WithMany()
        .HasForeignKey(x => x.Document2Id).OnDelete(DeleteBehavior.Cascade);

        //Notification 
        modelBuilder.Entity<Notification>().HasOne(d => d.Project).WithMany()
        .HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Notification>().HasOne(d => d.Document).WithMany()
        .HasForeignKey(x => x.DocumentId).OnDelete(DeleteBehavior.Restrict);

        //ProjectDocuments 
        //modelBuilder.Entity<ProjectToDocuments>().HasOne(d => d.Project).WithMany()
        //.HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Restrict);
        //modelBuilder.Entity<ProjectToDocuments>().HasOne(d => d.Document).WithMany()
        //.HasForeignKey(x => x.DocumentId).OnDelete(DeleteBehavior.Restrict); 

        //LaborHourCost 
        modelBuilder.Entity<LaborHoursCost>().HasOne(d => d.Worker).WithMany()
        .HasForeignKey(x => x.WorkerId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<LaborHoursCost>().HasOne(d => d.Document).WithMany()
        .HasForeignKey(x => x.DocumentId).OnDelete(DeleteBehavior.Cascade);

        //LaborHourWorked 
        modelBuilder.Entity<WorkedLaborHours>().HasOne(d => d.Worker).WithMany()
        .HasForeignKey(x => x.WorkerId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<WorkedLaborHours>().HasOne(d => d.Payment).WithMany()
        .HasForeignKey(x => x.PaymentId).OnDelete(DeleteBehavior.Cascade);

        //ContractPayments 
        modelBuilder.Entity<Payment>().HasOne(d => d.PaymentStatus).WithMany()
        .HasForeignKey(x => x.PaymentStatusId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Payment>().HasOne(d => d.Document).WithMany()
        .HasForeignKey(x => x.DocumentId).OnDelete(DeleteBehavior.Cascade);

        // Identity
        modelBuilder.Entity<IdentityUserRole<string>>()
            .HasKey(x => new { x.UserId, x.RoleId });
        modelBuilder.Entity<IdentityUserLogin<string>>()
            .HasKey(x => new { x.LoginProvider, x.ProviderKey });
        modelBuilder.Entity<IdentityUserToken<string>>()
            .HasKey(x => new { x.UserId, x.LoginProvider, x.Name });

    }
}
