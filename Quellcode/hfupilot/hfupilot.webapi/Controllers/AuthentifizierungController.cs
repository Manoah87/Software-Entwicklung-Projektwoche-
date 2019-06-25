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
            using (HfupilotContext db = new HfupilotContext())
            {
                var user = new SqlParameter("i_Benutzer", "johndoe");
                var password = new SqlParameter("i_Password", "adsfasd");
                db.AnmeldungRueckmeldungen.FromSql("EXECUTE dbo.pda_Anmelden @i_Benutzer", user,password);
            }
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
