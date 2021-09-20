using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataSource.Models
{
    public partial class MISContext : DbContext
    {
        public MISContext()
        {
        }

        public MISContext(DbContextOptions<MISContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Albums> Albums { get; set; }
        public virtual DbSet<AlbumPicture> AlbumPictures { get; set; }
        public virtual DbSet<AppMenu> AppMenu { get; set; }
        public virtual DbSet<BudgetBugdum> BudgetBugda { get; set; }
        public virtual DbSet<BudgetItem> BudgetItems { get; set; }
        public virtual DbSet<BudgetStep> BudgetSteps { get; set; }
        public virtual DbSet<Bugdum> Bugda { get; set; }
        public virtual DbSet<CommAccount> CommAccounts { get; set; }
        public virtual DbSet<Dept> Depts { get; set; }
        public virtual DbSet<LevStep> LevSteps { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Pictures> Pictures { get; set; }
        public virtual DbSet<Process> Processes { get; set; }
        public virtual DbSet<SystemList> SystemLists { get; set; }
        public virtual DbSet<Tuser> Tusers { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=JOY\\SQLEXPRESS; Database=MIS;Trusted_Connection=True; User ID=joy;Password=Xx98101120");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<Album>(entity =>
            {
                entity.HasKey(e => e.Sn);

                entity.ToTable("Album");

                entity.Property(e => e.Sn).HasColumnName("SN");

                entity.Property(e => e.Crdate)
                    .HasColumnType("datetime")
                    .HasColumnName("crdate");

                entity.Property(e => e.Cruser)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cruser");

                entity.Property(e => e.Download)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("download");

                entity.Property(e => e.Eddate)
                    .HasColumnType("datetime")
                    .HasColumnName("eddate");

                entity.Property(e => e.Eduser)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("eduser");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Remark)
                    .HasMaxLength(500)
                    .HasColumnName("remark");

                entity.Property(e => e.Sctrl)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("sctrl");
            });

           

            modelBuilder.Entity<AlbumPicture>(entity =>
            {
                entity.HasKey(e => new { e.Sn, e.Idnum });

                entity.ToTable("AlbumPicture");

                entity.Property(e => e.Sn).HasColumnName("SN");

                entity.Property(e => e.Idnum).HasColumnName("idnum");

                entity.Property(e => e.Crdate)
                    .HasColumnType("datetime")
                    .HasColumnName("crdate");

                entity.Property(e => e.Cruser)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cruser");

                entity.Property(e => e.Eddate)
                    .HasColumnType("datetime")
                    .HasColumnName("eddate");

                entity.Property(e => e.Eduser)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("eduser");

                entity.Property(e => e.Mtop)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("mtop")
                    .IsFixedLength(true);

                entity.Property(e => e.Path).HasColumnName("path");

                entity.Property(e => e.Picturefile)
                    .IsRequired()
                    .HasColumnName("picturefile");

                entity.Property(e => e.Remark)
                    .HasMaxLength(500)
                    .HasColumnName("remark");

                entity.Property(e => e.Sctrl)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("sctrl");
            });

            modelBuilder.Entity<AppMenu>(entity =>
            {
                entity.ToTable("AppMenu");

                entity.Property(e => e.AppMenuName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Path).HasMaxLength(50);

                entity.Property(e => e.Type)
                    .HasMaxLength(10)
                    .HasColumnName("type");

                entity.HasOne(d => d.SystemList)
                    .WithMany(p => p.AppMenus)
                    .HasForeignKey(d => d.SystemListId)
                    .HasConstraintName("FK_AppMenu_SystemList");
            });

            modelBuilder.Entity<BudgetBugdum>(entity =>
            {
                entity.HasKey(e => e.Uid);

                entity.ToTable("Budget_Bugda");

                entity.Property(e => e.Uid).ValueGeneratedNever();

                entity.Property(e => e.Bugda)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<BudgetItem>(entity =>
            {
                entity.HasKey(e => e.Uid);

                entity.ToTable("Budget_Items");

                entity.Property(e => e.Uid).ValueGeneratedNever();

                entity.Property(e => e.Crdate)
                    .HasColumnType("datetime")
                    .HasColumnName("crdate");

                entity.Property(e => e.Cruser)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cruser");

                entity.Property(e => e.Eddate)
                    .HasColumnType("datetime")
                    .HasColumnName("eddate");

                entity.Property(e => e.Eduser)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("eduser");

                entity.Property(e => e.Items)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Sctrl)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("sctrl");
            });

            modelBuilder.Entity<BudgetStep>(entity =>
            {
                entity.HasKey(e => e.Bno)
                    .HasName("PK_Budget");

                entity.ToTable("Budget_Step");

                entity.Property(e => e.Bno).HasMaxLength(10);

                entity.Property(e => e.Dept).HasMaxLength(10);

                entity.Property(e => e.Month)
                    .IsRequired()
                    .HasMaxLength(6);

                entity.Property(e => e.Sctrl).HasMaxLength(1);
            });

            modelBuilder.Entity<Bugdum>(entity =>
            {
                entity.HasKey(e => e.Bugda);

                entity.Property(e => e.Bugda).HasMaxLength(5);

                entity.Property(e => e.Bugna)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Sctrl)
                    .HasMaxLength(1)
                    .HasColumnName("sctrl");
            });

            modelBuilder.Entity<CommAccount>(entity =>
            {
                entity.HasKey(e => e.Sn);

                entity.ToTable("Comm_Account");

                entity.Property(e => e.Sn).HasColumnName("SN");

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasColumnName("ID");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Dept>(entity =>
            {
                entity.HasKey(e => e.DeptNo);

                entity.ToTable("Dept");

                entity.Property(e => e.DeptNo).HasMaxLength(4);

                entity.Property(e => e.Agent)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Gpdept).HasColumnName("GPdept");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Sctrl)
                    .HasMaxLength(1)
                    .HasColumnName("sctrl");

                entity.Property(e => e.Supervisor)
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LevStep>(entity =>
            {
                entity.HasKey(e => new { e.Bno, e.System, e.Type, e.Lev });

                entity.ToTable("Lev_Step");

                entity.Property(e => e.Bno)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.System)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("system");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("type");

                entity.Property(e => e.Lev)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("lev");

                entity.Property(e => e.Crdate)
                    .HasColumnType("datetime")
                    .HasColumnName("crdate");

                entity.Property(e => e.Cruser)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cruser");

                entity.Property(e => e.Eddate)
                    .HasColumnType("datetime")
                    .HasColumnName("eddate");

                entity.Property(e => e.Eduser)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("eduser");

                entity.Property(e => e.Sctrl)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("sctrl");

                entity.Property(e => e.Sign)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("sign");
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.ToTable("Page");

                entity.Property(e => e.PageId).HasColumnName("pageID");

                entity.Property(e => e.Page1)
                    .HasMaxLength(50)
                    .HasColumnName("page");

                entity.Property(e => e.PageName)
                    .HasMaxLength(50)
                    .HasColumnName("page_name");

                entity.Property(e => e.PageOpen)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("page_open")
                    .IsFixedLength(true);

                entity.Property(e => e.Type)
                    .HasMaxLength(10)
                    .HasColumnName("type")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Penno);

                entity.ToTable("Person");

                entity.Property(e => e.Penno).HasMaxLength(4);

                entity.Property(e => e.Deptno)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .HasColumnName("password");

                entity.Property(e => e.Pname).HasMaxLength(15);
            });

            modelBuilder.Entity<Pictures>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.EditDate).HasColumnType("datetime");

                entity.Property(e => e.IsEnable)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Remark).HasMaxLength(100);

            });

            modelBuilder.Entity<Process>(entity =>
            {
                entity.HasKey(e => new { e.System, e.Type, e.Lev })
                    .HasName("PK_LevStep");

                entity.ToTable("Process");

                entity.Property(e => e.System)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("system");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("type");

                entity.Property(e => e.Lev)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("lev");

                entity.Property(e => e.Sctrl)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("sctrl");

                entity.Property(e => e.Sign)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("sign");
            });

            modelBuilder.Entity<SystemList>(entity =>
            {
                entity.ToTable("SystemList");

                entity.Property(e => e.SystemName)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Tuser>(entity =>
            {
                entity.ToTable("Tuser");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("account");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Village)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("village");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.EditDate).HasColumnType("datetime");

                entity.Property(e => e.Lev).HasDefaultValueSql("((1))");

                entity.Property(e => e.LoginDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Third)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ThirdId)
                    .HasMaxLength(200)
                    .HasColumnName("ThirdID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
