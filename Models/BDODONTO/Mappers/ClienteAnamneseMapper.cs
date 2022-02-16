using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Odonto.Models.BDODONTO.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Odonto.Models.BDODONTO.Mappers
{
    public class ClienteAnamneseMapper : IEntityTypeConfiguration<ClienteAnamnese>
    {
        public void Configure(EntityTypeBuilder<ClienteAnamnese> builder)
        {
            builder.HasOne(c => c.Cliente)
                .WithMany(c => c.ClienteAnamneses)
                .HasForeignKey(c => c.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(c => c.Anamnese)
              .WithMany(c => c.ClienteAnamneses)
              .HasForeignKey(C => C.AnamneseId)
              .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(c => c.DataCad)
           .HasDefaultValueSql("(getdate())");

            builder.Property(c => c.IsAtivo)
                .HasDefaultValue(true);
        }
    }
}
