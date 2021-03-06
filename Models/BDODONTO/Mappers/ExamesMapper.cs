using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Odonto.Models.BDODONTO.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Odonto.Models.BDODONTO.Mappers
{
    public class ExamesMapper : IEntityTypeConfiguration<Exames>
    {
        public void Configure(EntityTypeBuilder<Exames> builder)
        {
            builder.HasOne(c => c.Cliente)
                .WithMany(c => c.Exames)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(c => c.DataCad)
           .HasDefaultValueSql("(getdate())");

            builder.Property(c => c.IsAtivo)
                .HasDefaultValue(true);
        }
    }
}
