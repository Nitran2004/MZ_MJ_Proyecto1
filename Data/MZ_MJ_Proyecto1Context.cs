using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proyecto1_MZ_MJ.Models;

namespace MZ_MJ_Proyecto1.Data
{
    public class MZ_MJ_Proyecto1Context : DbContext
    {
        public MZ_MJ_Proyecto1Context (DbContextOptions<MZ_MJ_Proyecto1Context> options)
            : base(options)
        {
        }

        public DbSet<Proyecto1_MZ_MJ.Models.Habitacion> Habitacion { get; set; } = default!;

        public DbSet<Proyecto1_MZ_MJ.Models.Comentario>? Comentario { get; set; }

        public DbSet<Proyecto1_MZ_MJ.Models.Pago>? Pago { get; set; }

        public DbSet<Proyecto1_MZ_MJ.Models.Queja>? Queja { get; set; }

        public DbSet<Proyecto1_MZ_MJ.Models.Evento>? Evento { get; set; }

    }
}
