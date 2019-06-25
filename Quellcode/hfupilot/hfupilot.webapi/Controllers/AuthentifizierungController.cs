using hfupilot.models;
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

        [HttpPost]
        [Route("anmelden")]
        public string Anmelden(string benutzer, string passwort)
        {
 
            var user = new SqlParameter("i_Benutzer", "hf.mpfister3");
            var password = new SqlParameter("i_Passwort", "8630!hfu_14");
            SqlConnection conn = new SqlConnection("Server=sql.aplix.ch,14444;Database=iWorld_HFU_AddOn;Trusted_Connection=False;User ID=iWorld_HFU_pda;Password=apl!xPDAHaEfU;");
            SqlCommand cmd = new SqlCommand("EXECUTE dbo.pda_Anmelden @i_Benutzer, @i_Passwort", conn);
            cmd.Parameters.Add(user);
            cmd.Parameters.Add(password);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            conn.Open();
            da.Fill(ds);
            conn.Close();

            //var test = db.AnmeldungRueckmeldungen.FromSql("EXECUTE dbo.pda_Anmelden @i_Benutzer, @i_Passwort", user, password).ToList().First(); ;
            
            return "gugus";
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
