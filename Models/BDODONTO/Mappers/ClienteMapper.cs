using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Odonto.Models.BDODONTO.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Odonto.Models.BDODONTO.Mappers
{
    public class ClienteMapper : IEntityTypeConfiguration<Clientes>
    {
        public void Configure(EntityTypeBuilder<Clientes> builder)
        {
            builder.HasOne(c => c.Resp)
                .WithMany(c => c.Clientes)
                .HasForeignKey(c => c.IdResp)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(c => c.DataCad)
                .HasDefaultValueSql("(getdate())");

            builder.Property(c => c.IsAtivo)
                .HasDefaultValue(true);
        }
    }
}
