﻿using hfupilot.Models;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using hfupilot.Models.api;

namespace hfupilot.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentifizierungController : ControllerBase
    {
        private IConfiguration _configuration;


        public AuthentifizierungController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("anmelden")]
        //Anmelden
        public IActionResult Anmeldung([FromBody]BenutzerLogin value)
        {

            try
            {
                var user = new SqlParameter("i_Benutzer", value.Benutzer);
                var password = new SqlParameter("i_Passwort", value.Passwort);
                SqlConnection conn = new SqlConnection(_configuration["ConnectionString"]);
                SqlCommand cmd = new SqlCommand("EXECUTE dbo.pda_Anmelden @i_Benutzer, @i_Passwort", conn);
                cmd.Parameters.Add(user);
                cmd.Parameters.Add(password);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                conn.Open();
                da.Fill(ds);
                conn.Close();
                Anmelden result;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    result = new Anmelden()
                    {
                        Fehler = (int)row["Fehler"],
                        FehlerMeldung = row["Fehlermeldung"].ToString(),
                        Session = (int)row["SessionID"],
                        Stufe = (int)row["Stufe"]
                    };
                }
                else
                {
                    result = new Anmelden();
                }

                return Ok(result); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

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
