using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Odonto.Models.BDODONTO.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Odonto.Models.BDODONTO.Mappers
{
    public class ReceituariosMapper : IEntityTypeConfiguration<Receituarios>
    {
        public void Configure(EntityTypeBuilder<Receituarios> builder)
        {
            builder.HasOne(c => c.Clientes)
                .WithMany(c => c.Receituarios)
                .HasForeignKey(c => c.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(c => c.IsAtivo)
                .HasDefaultValue(true);

            builder.Property(c => c.DataCad)
                .HasDefaultValueSql("(getdate())");
        }
    }
}
