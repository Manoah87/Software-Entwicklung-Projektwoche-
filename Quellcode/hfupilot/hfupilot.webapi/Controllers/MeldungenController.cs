using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using hfupilot.Models;
using hfupilot.Models.api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hfupilot.webapi.Controllers
{
    [Route("api/[controller]")]
    public class MeldungenController : Controller
    {
        private IConfiguration _configuration;

        public MeldungenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Verspaetung([FromBody]VerspaetungParameter parameter)
        {
            try
            {
                var sessionID = new SqlParameter("i_SessionID", parameter.SessionID);
                var id = new SqlParameter("i_ID", parameter.ID);
                var anzahl = new SqlParameter("i_Anzahl", parameter.Anzal);
                var grund = new SqlParameter("i_Grund", parameter.Grund);

                SqlConnection conn = new SqlConnection(_configuration["ConnectionString"]);
                SqlCommand cmd = new SqlCommand("EXECUTE dbo.pda_Verspaetung @i_SessionID @i_ID @i_Anzahl @i_Grund", conn);
                cmd.Parameters.Add(sessionID);
                cmd.Parameters.Add(id);
                cmd.Parameters.Add(anzahl);
                cmd.Parameters.Add(grund);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                conn.Open();
                da.Fill(ds);
                conn.Close();
                BasisFehlerProperties result;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    result = new BasisFehlerProperties()
                    {
                        Fehler = (int)row["Fehler"],
                        FehlerMeldung = row["Fehlermeldung"].ToString()
                    };
                }
                else
                {
                    result = new BasisFehlerProperties() { Fehler = 999, FehlerMeldung = "Keine Rückmeldung" };
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Abwesenheit([FromBody]VerspaetungParameter parameter)
        {
            try
            {
                var sessionID = new SqlParameter("i_SessionID", parameter.SessionID);
                var id = new SqlParameter("i_ID", parameter.ID);
                var grund = new SqlParameter("i_Grund", parameter.Grund);

                SqlConnection conn = new SqlConnection(_configuration["ConnectionString"]);
                SqlCommand cmd = new SqlCommand("EXECUTE dbo.pda_Abwesenheit @i_SessionID @i_ID @i_Anzahl @i_Grund", conn);
                cmd.Parameters.Add(sessionID);
                cmd.Parameters.Add(id);
                cmd.Parameters.Add(grund);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                conn.Open();
                da.Fill(ds);
                conn.Close();
                BasisFehlerProperties result;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    result = new BasisFehlerProperties()
                    {
                        Fehler = (int)row["Fehler"],
                        FehlerMeldung = row["Fehlermeldung"].ToString()
                    };
                }
                else
                {
                    result = new BasisFehlerProperties() { Fehler = 999, FehlerMeldung = "Keine Rückmeldung" };
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Meldung([FromBody]MeldungParameter parameter)
        {
            try
            {
                var sessionID = new SqlParameter("i_SessionID", parameter.SessionID);
                var id = new SqlParameter("i_ID", parameter.ID);
                var meldung = new SqlParameter("i_Meldung", parameter.Meldung);
                var aktiveBis = new SqlParameter("i_AktivBis", parameter.AktiveBis);
                var art = new SqlParameter("i_Art", parameter.Art);

                SqlConnection conn = new SqlConnection(_configuration["ConnectionString"]);
                SqlCommand cmd = new SqlCommand("EXECUTE dbo.pda_Abwesenheit @i_SessionID @i_ID @i_Meldung @i_AktivBis @i_Art", conn);
                cmd.Parameters.Add(sessionID);
                cmd.Parameters.Add(id);
                cmd.Parameters.Add(meldung);
                cmd.Parameters.Add(aktiveBis);
                cmd.Parameters.Add(art);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                conn.Open();
                da.Fill(ds);
                conn.Close();
                BasisFehlerProperties result;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    result = new BasisFehlerProperties()
                    {
                        Fehler = (int)row["Fehler"],
                        FehlerMeldung = row["Fehlermeldung"].ToString()
                    };
                }
                else
                {
                    result = new BasisFehlerProperties() { Fehler = 999, FehlerMeldung = "Keine Rückmeldung" };
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        // GET api/<controller>/5
        [HttpGet("{sessionID}/{id}")]
        [AllowAnonymous]
        public IActionResult Get(int sessionID, int id)
        {
            try
            {
                var spSessionID = new SqlParameter("i_SessionID", sessionID);
                var spID = new SqlParameter("i_ID", id);
                SqlConnection conn = new SqlConnection(_configuration["ConnectionString"]);
                SqlCommand cmd = new SqlCommand("EXECUTE dbo.pda_Meldungen @i_SessionID, @i_ID", conn);
                cmd.Parameters.Add(spSessionID);
                cmd.Parameters.Add(spID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                conn.Open();
                da.Fill(ds);
                conn.Close();

                Meldungen result;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    result = new Meldungen()
                    {
                        MeldungList = new System.Collections.Generic.List<Meldung>()
                    };

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        result.MeldungList.Add(
                            new Meldung()
                            {
                                Fehler = (int)row["Fehler"],
                                FehlerMeldung = (string)row["Fehlermeldung"],
                                Id = (int)row["ID"],
                                Datum = (string)row["Datum"],
                                Zeit = (string)row["Zeit"],
                                MeldungNachricht = (string)row["Meldung"],
                                Art = (int)row["Art"]
                            }
                        );
                    }
                }
                else
                {
                    result = new Meldungen();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
