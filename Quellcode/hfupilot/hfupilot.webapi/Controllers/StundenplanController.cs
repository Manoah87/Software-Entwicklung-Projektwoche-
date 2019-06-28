using hfupilot.Models;
using hfupilot.Models.api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hfupilot.webapi.Controllers
{
    [Route("api/[controller]")]
    public class StundenplanController : Controller
    {
        private IConfiguration _configuration;

        public StundenplanController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET api/<controller>/5
        [HttpGet("{sessionID}/{filter}")]
        [AllowAnonymous]
        public IActionResult Get(int sessionID, int filter)
        {
            try
            {
                var spSessionID = new SqlParameter("i_SessionID", sessionID);
                var spFilter = new SqlParameter("i_Filter", filter);
                SqlConnection conn = new SqlConnection(_configuration["ConnectionString"]);
                SqlCommand cmd = new SqlCommand("EXECUTE dbo.pda_Stundenplan @i_SessionID, @i_Filter", conn);
                cmd.Parameters.Add(spSessionID);
                cmd.Parameters.Add(spFilter);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                conn.Open();
                da.Fill(ds);
                conn.Close();

                Stundenplan result;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    result = new Stundenplan()
                    {
                        StundenplanList = new System.Collections.Generic.List<Termine>()
                    };

                    foreach(DataRow row in ds.Tables[0].Rows)
                    {
                        result.StundenplanList.Add(
                            new Termine()
                            {
                                Id = (int)row["ID"],
                                Datum = (string)row["Datum"],
                                Zeit = (string)row["Zeit"],
                                Titel = (string)row["Titel"],
                                Code = (string)row["Code"],
                                Bezeichnung = (string)row["Bezeichnung"],
                                Zimmer = (string)row["Zimmer"],
                                Lehrperson = (string)row["Lehrperson"]
                            }
                        );
                    }
                }
                else
                {
                    result = new Stundenplan();
                }

                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
