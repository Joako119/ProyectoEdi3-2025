using GestionCompeticiones.Abstractions;
using GestionCompeticiones.DataAccess.MicrosoftIdentity;
using GestionCompeticiones.Entities;
using GestionCompeticiones.Entities.MicrosoftIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.DataAccess
{
  public class DbDataAccess : IdentityDbContext<User, Role, Guid>
        {
            public DbDataAccess(DbContextOptions<DbDataAccess> options) : base(options)
            {
            }
            public virtual DbSet<User> User { get; set; }
        public virtual DbSet<TablaPosiciones> TablaPosiciones { get; set; }
        public virtual DbSet<ResultadoCarrera> ResultadoCarrera { get; set; }
        public virtual DbSet<PilotoEquipo> PilotoEquipo { get; set; }
        public virtual DbSet<Piloto> Piloto { get; set; }
        public virtual DbSet<PersonalFederacion> PersonalFederacion { get; set; }
        public virtual DbSet<Federacion> Federacion { get; set; }
        public virtual DbSet<EstadisticaPiloto> EstadisticaPiloto { get; set; }
        public virtual DbSet<Equipo> Equipo { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Carrera> Carrera { get; set; }
        public virtual DbSet<Campeonato> Campeonato { get; set; }
        public virtual DbSet<AsignacionPersonalFiscalCarrera> AsignacionPersonalFiscalCarrera { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GestionCompeticionesDev;Trusted_Connection=True;");
            }
            optionsBuilder.LogTo(Console.WriteLine).EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

           
            var entityTypes = typeof(DbDataAccess).Assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && typeof(IEntidad).IsAssignableFrom(t));
            foreach (var t in entityTypes)
            {
                modelBuilder.Entity(t);
            }
            modelBuilder.Entity<ResultadoCarrera>(b =>
            {
                b.HasKey(r => r.Id);
                b.HasIndex(r => r.CarreraId);
                b.HasIndex(r => r.PilotoId);

                b.HasOne(r => r.Carrera)
                 .WithMany(c => c.Resultados)
                 .HasForeignKey(r => r.CarreraId)
                 .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(r => r.Piloto)
                 .WithMany(p => p.Resultados)
                 .HasForeignKey(r => r.PilotoId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PilotoEquipo>(b =>
            {
                b.HasKey(pe => pe.Id);

                b.Property(pe => pe.Id)
                 .ValueGeneratedOnAdd();

                b.HasIndex(pe => pe.PilotoId);
                b.HasIndex(pe => pe.EquipoId);

                b.HasOne(pe => pe.Piloto)
                 .WithMany(p => p.HistorialEquipos)
                 .HasForeignKey(pe => pe.PilotoId)
                 .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(pe => pe.Equipo)
                 .WithMany(e => e.Pilotos)
                 .HasForeignKey(pe => pe.EquipoId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TablaPosiciones>(b =>
            {
                b.HasKey(t => t.Id);
                b.HasIndex(t => t.CampeonatoId);
                b.HasIndex(t => t.PilotoId);

                b.HasOne(t => t.Campeonato)
                 .WithMany(c => c.TablaGeneral)
                 .HasForeignKey(t => t.CampeonatoId)
                 .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(t => t.Piloto)
                 .WithMany(p => p.TablaPosiciones) 
                 .HasForeignKey(t => t.PilotoId)
                 .OnDelete(DeleteBehavior.Cascade);
            });
         
            modelBuilder.Entity<EstadisticaPiloto>(b =>
            {
                b.HasKey(ep => ep.Id);
                b.HasOne(ep => ep.Piloto)
                 .WithMany(p => p.Estadisticas)
                 .HasForeignKey(ep => ep.PilotoId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

        
            modelBuilder.Entity<PersonalFederacion>(b =>
            {
                b.HasKey(pf => pf.Id);
                b.HasOne(pf => pf.Federacion)
                 .WithMany(f => f.Personal)
                 .HasForeignKey(pf => pf.FederacionId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

   
            modelBuilder.Entity<AsignacionPersonalFiscalCarrera>(b =>
            {
                b.HasKey(a => a.Id);
                b.HasOne(a => a.PersonalFederacion)
                 .WithMany(p => p.Asignaciones)
                 .HasForeignKey(a => a.PersonalFederacionId)
                 .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(a => a.Carrera)
                 .WithMany(c => c.Fiscales)
                 .HasForeignKey(a => a.CarreraId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbDataAccess).Assembly);


           
        }
    }

}

