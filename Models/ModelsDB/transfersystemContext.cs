using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Models.ModelsDB
{
    public partial class transfersystemContext : DbContext
    {
        private string ConnectionString { get; set; }

        public transfersystemContext(string conn)
        {
            ConnectionString = conn;
        }

        public transfersystemContext(DbContextOptions<transfersystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Availabledeal> Availabledeals { get; set; }
        public virtual DbSet<Desiredplayer> Desiredplayers { get; set; }
        public virtual DbSet<Management> Managements { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Playerspecification> Playerspecifications { get; set; }
        public virtual DbSet<Playerstatistic> Playerstatistics { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Teamstatistic> Teamstatistics { get; set; }
        public virtual DbSet<Userinfo> Userinfos { get; set; }
        public IQueryable<PlayersTeamStat> getplayers() => FromExpression(() => getplayers());

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            
            modelBuilder.HasDbFunction(() => getplayers());
            
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251");

            modelBuilder.Entity<Availabledeal>(entity =>
            {
                entity.ToTable("availabledeals");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.Frommanagementid).HasColumnName("frommanagementid");

                entity.Property(e => e.Playerid).HasColumnName("playerid");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Tomanagementid).HasColumnName("tomanagementid");

                entity.HasOne(d => d.Frommanagement)
                    .WithMany(p => p.AvailabledealFrommanagements)
                    .HasForeignKey(d => d.Frommanagementid)
                    .HasConstraintName("availabledeals_frommanagementid_fkey");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.Availabledeals)
                    .HasForeignKey(d => d.Playerid)
                    .HasConstraintName("availabledeals_playerid_fkey");

                entity.HasOne(d => d.Tomanagement)
                    .WithMany(p => p.AvailabledealTomanagements)
                    .HasForeignKey(d => d.Tomanagementid)
                    .HasConstraintName("availabledeals_tomanagementid_fkey");
            });

            modelBuilder.Entity<Desiredplayer>(entity =>
            {
                entity.ToTable("desiredplayers");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Managementid).HasColumnName("managementid");

                entity.Property(e => e.Playerid).HasColumnName("playerid");

                entity.HasOne(d => d.Management)
                    .WithMany(p => p.Desiredplayers)
                    .HasForeignKey(d => d.Managementid)
                    .HasConstraintName("desiredplayers_managementid_fkey");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.Desiredplayers)
                    .HasForeignKey(d => d.Playerid)
                    .HasConstraintName("desiredplayers_playerid_fkey");
            });

            modelBuilder.Entity<Management>(entity =>
            {
                entity.ToTable("management");

                entity.Property(e => e.Managementid)
                    .ValueGeneratedNever()
                    .HasColumnName("managementid");

                entity.Property(e => e.Analysistid).HasColumnName("analysistid");

                entity.Property(e => e.Managerid).HasColumnName("managerid");

                entity.HasOne(d => d.Analysist)
                    .WithMany(p => p.ManagementAnalysists)
                    .HasForeignKey(d => d.Analysistid)
                    .HasConstraintName("management_analysistid_fkey");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.ManagementManagers)
                    .HasForeignKey(d => d.Managerid)
                    .HasConstraintName("management_managerid_fkey");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("player");

                entity.Property(e => e.Playerid)
                    .ValueGeneratedNever()
                    .HasColumnName("playerid");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("country");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("name");

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.Playerspecifications).HasColumnName("playerspecifications");

                entity.Property(e => e.Playerstatistics).HasColumnName("playerstatistics");

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("position");

                entity.Property(e => e.Teamid).HasColumnName("teamid");

                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.HasOne(d => d.PlayerspecificationsNavigation)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.Playerspecifications)
                    .HasConstraintName("player_playerspecifications_fkey");

                entity.HasOne(d => d.PlayerstatisticsNavigation)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.Playerstatistics)
                    .HasConstraintName("player_playerstatistics_fkey");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.Teamid)
                    .HasConstraintName("player_teamid_fkey");
            });

            modelBuilder.Entity<Playerspecification>(entity =>
            {
                entity.HasKey(e => e.Specificationsid)
                    .HasName("playerspecifications_pkey");

                entity.ToTable("playerspecifications");

                entity.Property(e => e.Specificationsid)
                    .ValueGeneratedNever()
                    .HasColumnName("specificationsid");

                entity.Property(e => e.Defense).HasColumnName("defense");

                entity.Property(e => e.Physical).HasColumnName("physical");

                entity.Property(e => e.Shooting).HasColumnName("shooting");

                entity.Property(e => e.Skating).HasColumnName("skating");
            });

            modelBuilder.Entity<Playerstatistic>(entity =>
            {
                entity.HasKey(e => e.Statisticsid)
                    .HasName("playerstatistics_pkey");

                entity.ToTable("playerstatistics");

                entity.Property(e => e.Statisticsid)
                    .ValueGeneratedNever()
                    .HasColumnName("statisticsid");

                entity.Property(e => e.Averagegametime).HasColumnName("averagegametime");

                entity.Property(e => e.Numberofwashers).HasColumnName("numberofwashers");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("team");

                entity.Property(e => e.Teamid)
                    .ValueGeneratedNever()
                    .HasColumnName("teamid");

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("country");

                entity.Property(e => e.Headcoach)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("headcoach");

                entity.Property(e => e.Managementid).HasColumnName("managementid");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("name");

                entity.Property(e => e.Stadium)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("stadium");

                entity.Property(e => e.Statisticsid).HasColumnName("statisticsid");

                entity.HasOne(d => d.Management)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.Managementid)
                    .HasConstraintName("team_managementid_fkey");

                entity.HasOne(d => d.Statistics)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.Statisticsid)
                    .HasConstraintName("team_statisticsid_fkey");
            });

            modelBuilder.Entity<Teamstatistic>(entity =>
            {
                entity.HasKey(e => e.Statisticsid)
                    .HasName("teamstatistics_pkey");

                entity.ToTable("teamstatistics");

                entity.Property(e => e.Statisticsid)
                    .ValueGeneratedNever()
                    .HasColumnName("statisticsid");

                entity.Property(e => e.League).HasColumnName("league");

                entity.Property(e => e.Numberofmatchesplayed).HasColumnName("numberofmatchesplayed");

                entity.Property(e => e.Numberoftrophies).HasColumnName("numberoftrophies");

                entity.Property(e => e.Placeintheleague).HasColumnName("placeintheleague");
            });

            modelBuilder.Entity<Userinfo>(entity =>
            {
                entity.ToTable("userinfo");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Hash)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("hash");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("login");

                entity.Property(e => e.Permission).HasColumnName("permission");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
