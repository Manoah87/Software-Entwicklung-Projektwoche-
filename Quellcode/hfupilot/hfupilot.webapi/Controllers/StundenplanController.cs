using hfupilot.Models;
using hfupilot.Models.api;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hfupilot.webapi.Controllers
{
    [Route("api/[controller]")]
    public class StundenplanController : Controller
    {
        // GET api/<controller>/5
        [HttpGet("/{sessionID}/{filter}")]
        public IActionResult Get(int sessionID, int filter)
        {
            try
            {
                var resultat = new Stundenplan();
                return Ok("test");
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
