using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace hfupilot.models.Models
{
    public partial class iWorld_HFU_AddOnContext : DbContext
    {
        public iWorld_HFU_AddOnContext()
        {
        }

        public iWorld_HFU_AddOnContext(DbContextOptions<iWorld_HFU_AddOnContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Benutzer> Benutzer { get; set; }
        public virtual DbSet<Datenplan> Datenplan { get; set; }
        public virtual DbSet<Durchfuehrungen> Durchfuehrungen { get; set; }
        public virtual DbSet<LoginLogFile> LoginLogFile { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<PdaMeldungen> PdaMeldungen { get; set; }
        public virtual DbSet<PdaPersonen> PdaPersonen { get; set; }
        public virtual DbSet<PdaSessions> PdaSessions { get; set; }
        public virtual DbSet<Personen> Personen { get; set; }
        public virtual DbSet<PersonenDatenplan> PersonenDatenplan { get; set; }
        public virtual DbSet<PersonenDurchfuehrung> PersonenDurchfuehrung { get; set; }
        public virtual DbSet<PersonenPruefung> PersonenPruefung { get; set; }
        public virtual DbSet<Pruefungen> Pruefungen { get; set; }
        public virtual DbSet<ZParameter> ZParameter { get; set; }

        // Unable to generate entity type for table 'dbo.tbl_ShowDatenplan'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tbl_ShowModule'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tbl_ShowPraesenzen'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tbl_ShowNoten'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tbl_ShowPruefungen'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tbl_ShowTeilnehmer'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=sql.aplix.ch,14444;Database=iWorld_HFU_AddOn;Trusted_Connection=False;User ID=iWorld_HFU_pda;Password=apl!xPDAHaEfU;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Benutzer>(entity =>
            {
                entity.Property(e => e.BenutzerId).HasColumnName("BenutzerID");

                entity.Property(e => e.Anmeldename).HasMaxLength(50);

                entity.Property(e => e.ExtId).HasColumnName("ExtID");

                entity.Property(e => e.Fehlermeldung)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Passwort).HasMaxLength(50);

                entity.Property(e => e.PersonId).HasColumnName("PersonID");
            });

            modelBuilder.Entity<Datenplan>(entity =>
            {
                entity.Property(e => e.DatenplanId).HasColumnName("DatenplanID");

                entity.Property(e => e.Datum).HasColumnType("date");

                entity.Property(e => e.DurchfuehrungId).HasColumnName("DurchfuehrungID");

                entity.Property(e => e.ExtId).HasColumnName("ExtID");

                entity.Property(e => e.Zimmer).HasMaxLength(50);
            });

            modelBuilder.Entity<Durchfuehrungen>(entity =>
            {
                entity.HasKey(e => e.DurchfuehrungId)
                    .HasName("PK_Moduldurchfuehrung");

                entity.Property(e => e.DurchfuehrungId).HasColumnName("DurchfuehrungID");

                entity.Property(e => e.AnzahlLektionen).HasColumnType("decimal(19, 1)");

                entity.Property(e => e.DatumEnde).HasColumnType("date");

                entity.Property(e => e.DatumStart).HasColumnType("date");

                entity.Property(e => e.ExtId).HasColumnName("ExtID");

                entity.Property(e => e.ModulId).HasColumnName("ModulID");

                entity.Property(e => e.Titel).HasMaxLength(20);
            });

            modelBuilder.Entity<LoginLogFile>(entity =>
            {
                entity.HasKey(e => e.LoginLogId);

                entity.Property(e => e.LoginLogId).HasColumnName("LoginLogID");

                entity.Property(e => e.BenutzerId).HasColumnName("BenutzerID");

                entity.Property(e => e.BenutzerPw)
                    .HasColumnName("BenutzerPW")
                    .HasMaxLength(50);

                entity.Property(e => e.Benutzername).HasMaxLength(50);

                entity.Property(e => e.Datum).HasColumnType("datetime");

                entity.Property(e => e.ExtId).HasColumnName("ExtID");

                entity.Property(e => e.Fehlermeldung)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LetzteAktivitaet).HasMaxLength(50);
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.HasKey(e => e.ModulId);

                entity.Property(e => e.ModulId).HasColumnName("ModulID");

                entity.Property(e => e.Beschreibung).HasMaxLength(100);

                entity.Property(e => e.Bezeichnung).HasMaxLength(100);

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.ExtId).HasColumnName("ExtID");
            });

            modelBuilder.Entity<PdaMeldungen>(entity =>
            {
                entity.HasKey(e => e.MeldungId);

                entity.ToTable("pdaMeldungen");

                entity.Property(e => e.MeldungId).HasColumnName("MeldungID");

                entity.Property(e => e.DatenplanId).HasColumnName("DatenplanID");

                entity.Property(e => e.DatumZeit).HasColumnType("datetime");

                entity.Property(e => e.GemeldetAm).HasColumnType("datetime");

                entity.Property(e => e.KontaktId)
                    .HasColumnName("KontaktID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Meldung)
                    .HasMaxLength(512)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PdaPersonen>(entity =>
            {
                entity.HasKey(e => e.PersonId);

                entity.ToTable("pdaPersonen");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.Benutzer)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.KontaktId)
                    .HasColumnName("KontaktID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Passwort)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Stammklasse)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Vorname)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PdaSessions>(entity =>
            {
                entity.HasKey(e => e.SessionId);

                entity.ToTable("pdaSessions");

                entity.Property(e => e.SessionId).HasColumnName("SessionID");

                entity.Property(e => e.DatumLetzteAktivitaet).HasColumnType("datetime");

                entity.Property(e => e.DatumLogin).HasColumnType("datetime");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");
            });

            modelBuilder.Entity<Personen>(entity =>
            {
                entity.HasKey(e => e.PersonId);

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.Arbeitgeber).HasMaxLength(100);

                entity.Property(e => e.EmailG)
                    .HasColumnName("EMailG")
                    .HasMaxLength(100);

                entity.Property(e => e.EmailP)
                    .HasColumnName("EMailP")
                    .HasMaxLength(100);

                entity.Property(e => e.ExtId).HasColumnName("ExtID");

                entity.Property(e => e.Geschlecht).HasMaxLength(10);

                entity.Property(e => e.KontaktId)
                    .HasColumnName("KontaktID")
                    .HasMaxLength(50);

                entity.Property(e => e.LandG).HasMaxLength(50);

                entity.Property(e => e.LandP).HasMaxLength(50);

                entity.Property(e => e.Nachname).HasMaxLength(100);

                entity.Property(e => e.OrtG).HasMaxLength(100);

                entity.Property(e => e.OrtP).HasMaxLength(100);

                entity.Property(e => e.PersonStatus).HasMaxLength(100);

                entity.Property(e => e.Plzg)
                    .HasColumnName("PLZG")
                    .HasMaxLength(10);

                entity.Property(e => e.Plzp)
                    .HasColumnName("PLZP")
                    .HasMaxLength(10);

                entity.Property(e => e.Stammklasse).HasMaxLength(100);

                entity.Property(e => e.StrasseNrG).HasMaxLength(150);

                entity.Property(e => e.StrasseNrP).HasMaxLength(150);

                entity.Property(e => e.TelG).HasMaxLength(100);

                entity.Property(e => e.TelMobileP).HasMaxLength(100);

                entity.Property(e => e.TelP).HasMaxLength(100);

                entity.Property(e => e.Vorname).HasMaxLength(100);
            });

            modelBuilder.Entity<PersonenDatenplan>(entity =>
            {
                entity.HasKey(e => e.PersonDatenplanId);

                entity.Property(e => e.PersonDatenplanId).HasColumnName("PersonDatenplanID");

                entity.Property(e => e.DatenplanId).HasColumnName("DatenplanID");

                entity.Property(e => e.ExtId).HasColumnName("ExtID");

                entity.Property(e => e.IstVerspaetetUm).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Kommentar).HasMaxLength(100);

                entity.Property(e => e.PersonId).HasColumnName("PersonID");
            });

            modelBuilder.Entity<PersonenDurchfuehrung>(entity =>
            {
                entity.HasKey(e => e.PersonenDurchfuehrungsId)
                    .HasName("PK_Modulbesuch");

                entity.Property(e => e.PersonenDurchfuehrungsId).HasColumnName("PersonenDurchfuehrungsID");

                entity.Property(e => e.Besuchsmodus).HasMaxLength(100);

                entity.Property(e => e.Besuchsstatus).HasMaxLength(100);

                entity.Property(e => e.DurchfuehrungId).HasColumnName("DurchfuehrungID");

                entity.Property(e => e.ExtId).HasColumnName("ExtID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");
            });

            modelBuilder.Entity<PersonenPruefung>(entity =>
            {
                entity.HasKey(e => e.PersonPruefungId);

                entity.Property(e => e.PersonPruefungId).HasColumnName("PersonPruefungID");

                entity.Property(e => e.ExtId).HasColumnName("ExtID");

                entity.Property(e => e.Kommentar).HasMaxLength(100);

                entity.Property(e => e.Note).HasColumnType("decimal(6, 1)");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.PruefungId).HasColumnName("PruefungID");

                entity.Property(e => e.Punkte).HasColumnType("decimal(6, 1)");
            });

            modelBuilder.Entity<Pruefungen>(entity =>
            {
                entity.HasKey(e => e.PruefungId);

                entity.Property(e => e.PruefungId).HasColumnName("PruefungID");

                entity.Property(e => e.Bezeichnung).HasMaxLength(100);

                entity.Property(e => e.DatenplanId).HasColumnName("DatenplanID");

                entity.Property(e => e.ExtId).HasColumnName("ExtID");

                entity.Property(e => e.IstMlz).HasColumnName("IstMLZ");

                entity.Property(e => e.MaxPunkte).HasColumnType("decimal(6, 1)");
            });

            modelBuilder.Entity<ZParameter>(entity =>
            {
                entity.HasKey(e => e.ParameterId);

                entity.ToTable("z_Parameter");

                entity.Property(e => e.ParameterId).HasColumnName("ParameterID");

                entity.Property(e => e.Bezeichnung).HasMaxLength(100);

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.ExtId).HasColumnName("ExtID");

                entity.Property(e => e.Wert)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
        }
    }
}
