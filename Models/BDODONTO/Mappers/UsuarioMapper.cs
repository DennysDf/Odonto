using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Odonto.Models.BDODONTO.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Odonto.Models.BDODONTO.Mappers
{
    public class UsuarioMapper : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasOne(c => c.Consultorio)
                .WithMany(c => c.Usuarios)
                .HasForeignKey(c => c.ConsultorioId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(c => c.IsAtivo)
                .HasDefaultValue(true);

    }
    }
}
