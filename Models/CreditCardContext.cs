using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CreditCardProj.Models;

public partial class CreditCardContext : DbContext
{
    public CreditCardContext()
    {
    }

    public CreditCardContext(DbContextOptions<CreditCardContext> options)
        : base(options)
    {
    }

    //public virtual DbSet<Bank> Banks { get; set; }

    //public virtual DbSet<CreditCard> CreditCards { get; set; }

    //public virtual DbSet<CreditCardTransaction> CreditCardTransactions { get; set; }

    //public virtual DbSet<LateFee> LateFees { get; set; }

    //public virtual DbSet<MonthlyInvoice> MonthlyInvoices { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-854KJI4\\SQLEXPRESS;database=Timetable;Integrated Security=true;Trust Server Certificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Bank>(entity =>
        //{
        //    entity.ToTable("Bank");

        //    entity.Property(e => e.BankId).ValueGeneratedNever();
        //    entity.Property(e => e.BankAccount)
        //        .HasMaxLength(50)
        //        .IsUnicode(false);
        //    entity.Property(e => e.BankActBalance).HasColumnType("decimal(10, 2)");
        //    entity.Property(e => e.BankHolderName)
        //        .HasMaxLength(50)
        //        .IsUnicode(false);
        //    entity.Property(e => e.BankName)
        //        .HasMaxLength(50)
        //        .IsUnicode(false);
        //    entity.Property(e => e.Ifsccode)
        //        .HasMaxLength(50)
        //        .IsUnicode(false)
        //        .HasColumnName("IFSCCode");

        //    entity.HasOne(d => d.CardNoNavigation).WithMany(p => p.Banks)
        //        .HasForeignKey(d => d.CardNo)
        //        .HasConstraintName("fk2");

        //    //entity.HasOne(d => d.User).WithMany(p => p.Banks)
        //    //    .HasForeignKey(d => d.UserId)
        //    //    .HasConstraintName("FK__Bank__UserId__52593CB8");
        //});

        //modelBuilder.Entity<CreditCard>(entity =>
        //{
        //    entity.HasKey(e => e.CardNo).HasName("PK__CreditCa__55FF25F1F7A35E27");

        //    entity.ToTable("CreditCard");

        //    entity.Property(e => e.CardNo).ValueGeneratedNever();
        //    entity.Property(e => e.AvilableCreditLimit).HasColumnType("decimal(10, 2)");
        //    entity.Property(e => e.BankName)
        //        .HasMaxLength(50)
        //        .IsUnicode(false);
        //    entity.Property(e => e.CardholderName)
        //        .HasMaxLength(50)
        //        .IsUnicode(false);
        //    entity.Property(e => e.Cvv).HasColumnName("CVV");
        //    entity.Property(e => e.IsBlocked)
        //        .HasMaxLength(20)
        //        .IsUnicode(false)
        //        .HasColumnName("IsBLocked");
        //});

        //modelBuilder.Entity<CreditCardTransaction>(entity =>
        //{
        //    entity.HasKey(e => e.TransactionId).HasName("PK__CreditCa__55433A6B8DB04DE9");

        //    entity.ToTable("CreditCardTransaction");

        //    entity.Property(e => e.BillerName)
        //        .HasMaxLength(50)
        //        .IsUnicode(false);
        //    entity.Property(e => e.BillingStatus)
        //        .HasMaxLength(50)
        //        .IsUnicode(false);
        //    entity.Property(e => e.TransactionAmt).HasColumnType("decimal(10, 2)");

        //    entity.HasOne(d => d.CardNoNavigation).WithMany(p => p.CreditCardTransactions)
        //        .HasForeignKey(d => d.CardNo)
        //        .HasConstraintName("FK__CreditCar__CardN__6E01572D");
        //});

        //modelBuilder.Entity<LateFee>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("PK__LateFee__3213E83FC3A9E0F1");

        //    entity.ToTable("LateFee");

        //    entity.Property(e => e.Id).HasColumnName("id");
        //    entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
        //    entity.Property(e => e.FeeDescription)
        //        .HasMaxLength(50)
        //        .IsUnicode(false);
        //    entity.Property(e => e.FeeType)
        //        .HasMaxLength(50)
        //        .IsUnicode(false);

        //    entity.HasOne(d => d.CardNoNavigation).WithMany(p => p.LateFees)
        //        .HasForeignKey(d => d.CardNo)
        //        .HasConstraintName("FK__LateFee__CardNo__4F7CD00D");
        //});

        //modelBuilder.Entity<MonthlyInvoice>(entity =>
        //{
        //    entity.HasKey(e => e.InvoiceId).HasName("PK__MonthlyI__D796AAB50443CACF");

        //    entity.ToTable("MonthlyInvoice");

        //    entity.Property(e => e.InvoiceId).ValueGeneratedNever();
        //    entity.Property(e => e.AmtPaid).HasColumnType("decimal(10, 2)");
        //    entity.Property(e => e.DueDate).HasColumnType("datetime");
        //    entity.Property(e => e.InvoiceMonth)
        //        .HasMaxLength(50)
        //        .IsUnicode(false);
        //    entity.Property(e => e.OutstandingAmt).HasColumnType("decimal(10, 2)");
        //    entity.Property(e => e.PaymentDate).HasColumnType("datetime");

        //    entity.HasOne(d => d.CardNoNavigation).WithMany(p => p.MonthlyInvoices)
        //        .HasForeignKey(d => d.CardNo)
        //        .HasConstraintName("FK__MonthlyIn__CardN__619B8048");
        //});

        modelBuilder.Entity<User>(entity =>
        {
            //entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CB9E96286");

            //entity.Property(e => e.UserId).ValueGeneratedNever();
            //entity.Property(e => e.AadharNo)
            //    .HasMaxLength(50)
            //    .IsUnicode(false);
            //entity.Property(e => e.Address)
            //    .HasMaxLength(50)
            //    .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false);
            //entity.Property(e => e.Income)
            //    .HasMaxLength(50)
            //    .IsUnicode(false);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            //entity.Property(e => e.Panno)
            //    .HasMaxLength(50)
            //    .IsUnicode(false)
            //    .HasColumnName("PANno");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);

            //entity.HasOne(d => d.CardNoNavigation).WithMany(p => p.Users)
            //    .HasForeignKey(d => d.CardNo)
            //    .HasConstraintName("FK__Users__CardNo__797309D9");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
