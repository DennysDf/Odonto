using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Odonto.Models.BDODONTO.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Odonto.Models.BDODONTO.Mappers
{
    public class ProcedimenrosMapper : IEntityTypeConfiguration<Procedimentos>
    {
        public void Configure(EntityTypeBuilder<Procedimentos> builder)
        {
            builder.HasOne(c => c.Cliente)
               .WithMany(c => c.Procedimentos)
               .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(c => c.DataCad)
           .HasDefaultValueSql("(getdate())");

            builder.Property(c => c.IsAtivo)
                .HasDefaultValue(true);
        }
    }
}
