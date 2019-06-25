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

        public virtual DbSet<Stundenplan> StundenplanListe { get; set; }
        public virtual DbSet<AnmeldungRueckmeldung> AnmeldungRueckmeldungen { get; set; }
    }
}
