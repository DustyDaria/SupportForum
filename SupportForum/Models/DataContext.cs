using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SupportForum.Models;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblCategory> TblCategories { get; set; }

    public virtual DbSet<TblCommunication> TblCommunications { get; set; }

    public virtual DbSet<TblForum> TblForums { get; set; }

    public virtual DbSet<TblInstruction> TblInstructions { get; set; }

    public virtual DbSet<TblKeyWord> TblKeyWords { get; set; }

    public virtual DbSet<TblReaction> TblReactions { get; set; }

    public virtual DbSet<TblTopic> TblTopics { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public virtual DbSet<TblValueAttribute> TblValueAttributes { get; set; }

    string? connection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()
        .GetSection("ConnectionStrings")["DefaultConnection"];
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(connection);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CATEGORY_PK");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<TblCommunication>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MSG_PK");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.IdInitiatorNavigation).WithMany(p => p.TblCommunications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MSG_INITIATOR_FK");

            entity.HasOne(d => d.IdParentNavigation).WithMany(p => p.InverseIdParentNavigation).HasConstraintName("MSG_PARENT_FK");

            entity.HasOne(d => d.IdTopicNavigation).WithMany(p => p.TblCommunications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MSG_TOPIK_FK");
        });

        modelBuilder.Entity<TblForum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("FORUM_PK");

            entity.ToTable("TBL_FORUM", tb => tb.HasTrigger("DEL_FORUM_TREE"));

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.TblForums).HasConstraintName("FORUM_CATEGORY_FK");

            entity.HasOne(d => d.IdInitiatorNavigation).WithMany(p => p.TblForums)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FORUM_USER_FK");

            entity.HasOne(d => d.IdParentNavigation).WithMany(p => p.InverseIdParentNavigation).HasConstraintName("FORUM_PARENT_FK");
        });

        modelBuilder.Entity<TblInstruction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("INSTRUCTION_PK");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<TblKeyWord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("KW_PK");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<TblReaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("REACTION_PK");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.IdMsgNavigation).WithMany(p => p.TblReactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("REACTION_MSG_FK");
        });

        modelBuilder.Entity<TblTopic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TOPIC_PK");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.IdForumNavigation).WithMany(p => p.TblTopics)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TOPIC_FORUM_FK");

            entity.HasOne(d => d.IdInitiatorNavigation).WithMany(p => p.TblTopics)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TOPIC_USER_FK");

            entity.HasMany(d => d.IdKeyWords).WithMany(p => p.IdTopics)
                .UsingEntity<Dictionary<string, object>>(
                    "TblTopicKeyWord",
                    r => r.HasOne<TblKeyWord>().WithMany()
                        .HasForeignKey("IdKeyWord")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("TKW_KW_FK"),
                    l => l.HasOne<TblTopic>().WithMany()
                        .HasForeignKey("IdTopic")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("TKW_TOPIC_FK"),
                    j =>
                    {
                        j.HasKey("IdTopic", "IdKeyWord").HasName("PK__TBL_TOPI__321BD9865FE66C29");
                        j.ToTable("TBL_TOPIC_KEY_WORD");
                        j.IndexerProperty<decimal>("IdTopic")
                            .HasColumnType("decimal(18, 0)")
                            .HasColumnName("idTopic");
                        j.IndexerProperty<decimal>("IdKeyWord")
                            .HasColumnType("decimal(18, 0)")
                            .HasColumnName("idKeyWord");
                    });
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("USER_PK");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<TblValueAttribute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("VA_FK");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
