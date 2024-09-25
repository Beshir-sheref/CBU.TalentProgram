using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CBU.TalentedApplication.Mvc.Models;

public partial class TalenedSystemContext : DbContext
{
    public TalenedSystemContext()
    {
    }

    public TalenedSystemContext(DbContextOptions<TalenedSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Applicant> Applicants { get; set; }

    public virtual DbSet<ApplicantCriteriaValue> ApplicantCriteriaValues { get; set; }

    public virtual DbSet<ApplicantDocument> ApplicantDocuments { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Criterion> Criteria { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Evaluator> Evaluators { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=BESHIR;Database=TalenedSystem;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Applicant>(entity =>
        {
            entity.ToTable("Applicant");

            entity.HasOne(d => d.Branch).WithMany(p => p.Applicants)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Applicant_Branch");

            entity.HasOne(d => d.User).WithMany(p => p.Applicants)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Applicant_User");
        });

        modelBuilder.Entity<ApplicantCriteriaValue>(entity =>
        {
            entity.ToTable("ApplicantCriteriaValue");

            entity.HasOne(d => d.Applicant).WithMany(p => p.ApplicantCriteriaValues)
                .HasForeignKey(d => d.ApplicantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicantCriteriaValue_Applicant");

            entity.HasOne(d => d.Criteria).WithMany(p => p.ApplicantCriteriaValues)
                .HasForeignKey(d => d.CriteriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicantCriteriaValue_ApplicantCriteriaValue");
        });

        modelBuilder.Entity<ApplicantDocument>(entity =>
        {
            entity.ToTable("ApplicantDocument");

            entity.Property(e => e.DocumentPath).IsUnicode(false);

            entity.HasOne(d => d.Applicant).WithMany(p => p.ApplicantDocuments)
                .HasForeignKey(d => d.ApplicantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicantDocument_Applicant");

            entity.HasOne(d => d.Document).WithMany(p => p.ApplicantDocuments)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicantDocument_Document");
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_branch");

            entity.ToTable("Branch");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Criterion>(entity =>
        {
            entity.Property(e => e.CriteriaName).HasMaxLength(100);

            entity.HasOne(d => d.Branch).WithMany(p => p.Criteria)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Criteria_Branch");

            entity.HasOne(d => d.Role).WithMany(p => p.Criteria)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Criteria_Role");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.ToTable("Document");

            entity.Property(e => e.DocumentName).HasMaxLength(50);

            entity.HasOne(d => d.Branch).WithMany(p => p.Documents)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Document_Branch");
        });

        modelBuilder.Entity<Evaluator>(entity =>
        {
            entity.ToTable("Evaluator");

            entity.HasOne(d => d.Branch).WithMany(p => p.Evaluators)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Evaluator_Branch");

            entity.HasOne(d => d.User).WithMany(p => p.Evaluators)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Evaluator_User");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IdNumber)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
