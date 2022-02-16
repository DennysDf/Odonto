using Microsoft.EntityFrameworkCore;
using Odonto.Models.BDODONTO.Entidades;
using Odonto.Models.BDODONTO.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Odonto.Models.BDODONTO.Context
{
    public class DBOdontoContext : DbContext
    {
        public DBOdontoContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Anamnese> Anamneses { get; set; }
        public DbSet<ClienteAnamnese> ClienteAnamneses { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Consultorio> Consultorios { get; set; }
        public DbSet<Exames> Exames { get; set; }
        public DbSet<Procedimentos> Procedimentos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ResetSenha> ResetSenha { get; set; }
        public DbSet<Medicamentos> Medicamentos { get; set; }
        public DbSet<ReceituarioMedicamentos> ReceituarioMedicamentos { get; set; }
        public DbSet<Receituarios> Receituarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AnamneseMapper());
            modelBuilder.ApplyConfiguration(new ClienteAnamneseMapper());
            modelBuilder.ApplyConfiguration(new ClienteMapper());
            modelBuilder.ApplyConfiguration(new ConsultorioMapper());
            modelBuilder.ApplyConfiguration(new ExamesMapper());
            modelBuilder.ApplyConfiguration(new ProcedimenrosMapper());
            modelBuilder.ApplyConfiguration(new UsuarioMapper());
            modelBuilder.ApplyConfiguration(new ResetSenhaMapper());
            modelBuilder.ApplyConfiguration(new MedicamentosMapper());
            modelBuilder.ApplyConfiguration(new ReceituarioMedicamentosMapper());
            modelBuilder.ApplyConfiguration(new ReceituariosMapper());
        }
    }
}
