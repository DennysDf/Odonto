using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Odonto.Models.BDODONTO.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Odonto.Models.BDODONTO.Mappers
{
    public class ReceituarioMedicamentosMapper : IEntityTypeConfiguration<ReceituarioMedicamentos>
    {
        public void Configure(EntityTypeBuilder<ReceituarioMedicamentos> builder)
        {
            builder.HasOne(c => c.Medicamentos)
                .WithMany(c => c.Receituarios)
                .HasForeignKey(c => c.MedicamentoId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(c => c.Receituarios)
               .WithMany(c => c.ReceituarioMedicamentos)
               .HasForeignKey(c => c.RecedituarioId)
               .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
