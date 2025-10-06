using GestionCompeticiones.Entities;
using GestionCompeticiones.Entities.GestionCompeticiones.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompeticiones.DataAccess
{
    public class DbDataAccess : IdentityDbContext
    {
        public virtual DbSet<Usuario> Usuario { get; set; }
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
        public DbDataAccess(DbContextOptions<DbDataAccess> options) : base(options) { }
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GestionCompeticionesDev;Trusted_Connection=True;");
            }
            optionsBuilder.LogTo(Console.WriteLine).EnableDetailedErrors();
        }
    }
}
