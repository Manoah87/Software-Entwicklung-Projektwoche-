using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace hfupilot.models
{
    public class HfupilotContext : DbContext
    {
        public HfupilotContext()
        {
        }

        public HfupilotContext(DbContextOptions<HfupilotContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=sql.aplix.ch,14444;Database=iWorld_HFU_AddOn;Trusted_Connection=False;User ID=iWorld_HFU_pda;Password=apl!xPDAHaEfU;");
            }
        }

        public virtual DbSet<Stundenplan> StundenplanListe { get; set; }
        public virtual DbSet<AnmeldungRueckmeldung> AnmeldungRueckmeldungen { get; set; }
    }
}
