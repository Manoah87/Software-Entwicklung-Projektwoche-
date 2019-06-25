﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace hfupilot.getsp
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class iWorld_HFU_AddOnEntities : DbContext
    {
        public iWorld_HFU_AddOnEntities()
            : base("name=iWorld_HFU_AddOnEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual ObjectResult<pda_Abmeldung_Result> pda_Abmeldung(Nullable<int> i_SessionID)
        {
            var i_SessionIDParameter = i_SessionID.HasValue ?
                new ObjectParameter("i_SessionID", i_SessionID) :
                new ObjectParameter("i_SessionID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pda_Abmeldung_Result>("pda_Abmeldung", i_SessionIDParameter);
        }
    
        public virtual ObjectResult<pda_Abwesenheit_Result> pda_Abwesenheit(Nullable<int> i_SessionID, Nullable<int> i_ID, string i_Grund)
        {
            var i_SessionIDParameter = i_SessionID.HasValue ?
                new ObjectParameter("i_SessionID", i_SessionID) :
                new ObjectParameter("i_SessionID", typeof(int));
    
            var i_IDParameter = i_ID.HasValue ?
                new ObjectParameter("i_ID", i_ID) :
                new ObjectParameter("i_ID", typeof(int));
    
            var i_GrundParameter = i_Grund != null ?
                new ObjectParameter("i_Grund", i_Grund) :
                new ObjectParameter("i_Grund", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pda_Abwesenheit_Result>("pda_Abwesenheit", i_SessionIDParameter, i_IDParameter, i_GrundParameter);
        }
    
        public virtual ObjectResult<pda_Anmelden_Result> pda_Anmelden(string i_Benutzer, string i_Passwort)
        {
            var i_BenutzerParameter = i_Benutzer != null ?
                new ObjectParameter("i_Benutzer", i_Benutzer) :
                new ObjectParameter("i_Benutzer", typeof(string));
    
            var i_PasswortParameter = i_Passwort != null ?
                new ObjectParameter("i_Passwort", i_Passwort) :
                new ObjectParameter("i_Passwort", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pda_Anmelden_Result>("pda_Anmelden", i_BenutzerParameter, i_PasswortParameter);
        }
    
        public virtual ObjectResult<pda_Meldung_Result> pda_Meldung(Nullable<int> i_SessionID, Nullable<int> i_ID, string i_Meldung, Nullable<System.DateTime> i_AktivBis, Nullable<int> i_Art)
        {
            var i_SessionIDParameter = i_SessionID.HasValue ?
                new ObjectParameter("i_SessionID", i_SessionID) :
                new ObjectParameter("i_SessionID", typeof(int));
    
            var i_IDParameter = i_ID.HasValue ?
                new ObjectParameter("i_ID", i_ID) :
                new ObjectParameter("i_ID", typeof(int));
    
            var i_MeldungParameter = i_Meldung != null ?
                new ObjectParameter("i_Meldung", i_Meldung) :
                new ObjectParameter("i_Meldung", typeof(string));
    
            var i_AktivBisParameter = i_AktivBis.HasValue ?
                new ObjectParameter("i_AktivBis", i_AktivBis) :
                new ObjectParameter("i_AktivBis", typeof(System.DateTime));
    
            var i_ArtParameter = i_Art.HasValue ?
                new ObjectParameter("i_Art", i_Art) :
                new ObjectParameter("i_Art", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pda_Meldung_Result>("pda_Meldung", i_SessionIDParameter, i_IDParameter, i_MeldungParameter, i_AktivBisParameter, i_ArtParameter);
        }
    
        public virtual int pda_Meldungen(Nullable<int> i_SessionID, Nullable<int> i_ID)
        {
            var i_SessionIDParameter = i_SessionID.HasValue ?
                new ObjectParameter("i_SessionID", i_SessionID) :
                new ObjectParameter("i_SessionID", typeof(int));
    
            var i_IDParameter = i_ID.HasValue ?
                new ObjectParameter("i_ID", i_ID) :
                new ObjectParameter("i_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pda_Meldungen", i_SessionIDParameter, i_IDParameter);
        }
    
        public virtual int pda_Stundenplan(Nullable<int> i_SessionID, Nullable<int> i_Filter)
        {
            var i_SessionIDParameter = i_SessionID.HasValue ?
                new ObjectParameter("i_SessionID", i_SessionID) :
                new ObjectParameter("i_SessionID", typeof(int));
    
            var i_FilterParameter = i_Filter.HasValue ?
                new ObjectParameter("i_Filter", i_Filter) :
                new ObjectParameter("i_Filter", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pda_Stundenplan", i_SessionIDParameter, i_FilterParameter);
        }
    
        public virtual ObjectResult<pda_Verspaetung_Result> pda_Verspaetung(Nullable<int> i_SessionID, Nullable<int> i_ID, Nullable<int> i_Anzahl, string i_Grund)
        {
            var i_SessionIDParameter = i_SessionID.HasValue ?
                new ObjectParameter("i_SessionID", i_SessionID) :
                new ObjectParameter("i_SessionID", typeof(int));
    
            var i_IDParameter = i_ID.HasValue ?
                new ObjectParameter("i_ID", i_ID) :
                new ObjectParameter("i_ID", typeof(int));
    
            var i_AnzahlParameter = i_Anzahl.HasValue ?
                new ObjectParameter("i_Anzahl", i_Anzahl) :
                new ObjectParameter("i_Anzahl", typeof(int));
    
            var i_GrundParameter = i_Grund != null ?
                new ObjectParameter("i_Grund", i_Grund) :
                new ObjectParameter("i_Grund", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pda_Verspaetung_Result>("pda_Verspaetung", i_SessionIDParameter, i_IDParameter, i_AnzahlParameter, i_GrundParameter);
        }
    }
}
