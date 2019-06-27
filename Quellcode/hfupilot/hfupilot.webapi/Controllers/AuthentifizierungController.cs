using hfupilot.app.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;
using hfupilot.app.models.api;
using Microsoft.AspNetCore.Authorization;
using System.Web.Http;
using System.Net.Http;

namespace hfupilot.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentifizierungController : ControllerBase
    {
        // GET: api/Authentifizierung
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1xxxx", "value2blub" };
        }

        // GET: api/Authentifizierung/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Authentifizierung
        [HttpPost]
        public string Post([FromBody] string value)
        {
            return value;
        }

        //public string Anmelden(string benutzer, string passwort)
        [HttpPost]
        [AllowAnonymous]
        [Route("anmelden")]
        //Anmelden
        public IActionResult Anmeldung([FromBody]BenutzerLogin value)
        {

            return Ok(new Anmelden() { FehlerMeldung = "gugus" }); // new Anmelden() { FehlerMeldung = "gugus" };
            //try
            //{
            //    var user = new SqlParameter("i_Benutzer", "hf.mpfister3");
            //    var password = new SqlParameter("i_Passwort", "8630!hfu_14");
            //    SqlConnection conn = new SqlConnection("");
            //    SqlCommand cmd = new SqlCommand("EXECUTE dbo.pda_Anmelden @i_Benutzer, @i_Passwort", conn);
            //    cmd.Parameters.Add(user);
            //    cmd.Parameters.Add(password);

            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    DataSet ds = new DataSet();
            //    conn.Open();
            //    da.Fill(ds);
            //    conn.Close();

            //    //var test = db.AnmeldungRueckmeldungen.FromSql("EXECUTE dbo.pda_Anmelden @i_Benutzer, @i_Passwort", user, password).ToList().First(); ;
            //    return Request.CreateResponse()
            //    //return new Anmelden()
            //    //{
            //    //    Fehler = 0,
            //    //    FehlerMeldung = "",
            //    //    Session = 11,
            //    //    Stufe = 32,
            //    //};
            //}
            //catch (Exception ex)
            //{
            //    //return "";
            //    //return new Anmelden()
            //    //{
            //    //    Fehler = 1,
            //    //    FehlerMeldung = ex.ToString(),
            //    //    Session = 0,
            //    //    Stufe = 0,
            //    //};
            //}

        }

        // PUT: api/Authentifizierung/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
