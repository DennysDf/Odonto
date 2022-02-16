using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Odonto.Models.BDODONTO.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Odonto.Models.BDODONTO.Mappers
{
    public class MedicamentosMapper : IEntityTypeConfiguration<Medicamentos>
    {
        public void Configure(EntityTypeBuilder<Medicamentos> builder)
        {
        }
    }
}
