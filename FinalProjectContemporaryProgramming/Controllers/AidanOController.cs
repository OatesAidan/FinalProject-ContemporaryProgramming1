using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FinalProjectContemporaryProgramming.Models.DataLayer;

namespace FinalProjectContemporaryProgramming.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AidanOController : ControllerBase
    {
        public class Main : AidanTable
        {
        }

        private CustomResponse NotFoundMessage = new CustomResponse()
        {
            Title = "Not Found",
            Message = "There is no row with that ID"
        };

        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status302Found)]
        
        public ActionResult Get([FromQuery] int? id)
        {
            if (id.HasValue && id.Value != 0)
            {
                return IdExists(id.Value) ? Ok(GetMainById(id.Value)) : StatusCode(404, NotFoundMessage);
            }
            return Ok(DBContext.Context.AidanTable.Take(5));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]

        public ActionResult Post(
            [FromQuery] string FullName, 
            [FromQuery] DateTime Birthdate, 
            [FromQuery] string CollegeProgram, 
            [FromQuery] string YearInProgram
            )
        {
            var added = new AidanTable() { 
                ID = GetNextAvailableID(), 
                FullName = FullName, 
                Birthdate = Birthdate, 
                CollegeProgram = CollegeProgram, 
                YearInProgram = YearInProgram 
            };

            DBContext.Context.Add(added);
            DBContext.Context.SaveChanges();
            return StatusCode(202, added);
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Update(
            [FromQuery] int id, 
            [FromQuery] string FullName = null, 
            [FromQuery] DateTime? Birthdate = null, 
            [FromQuery] string CollegeProgram = null, 
            [FromQuery] string YearInProgram = null)
        {
            if (!IdExists(id))
                return StatusCode(404, NotFoundMessage);
            var m = GetMainById(id);
            if (FullName != null)
                m.FullName = FullName;
            if (Birthdate != null)
                m.Birthdate = Birthdate;
            if (CollegeProgram != null)
                m.CollegeProgram = CollegeProgram;
            if (YearInProgram != null)
                m.YearInProgram = YearInProgram;
            DBContext.Context.AidanTable.Update(m);
            DBContext.Context.SaveChanges();
            return StatusCode(202, m);
        }
        private int GetNextAvailableID()
        {
            int i = 1;
            while (IdExists(i))
                i++;
            return i;
        }
       
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Main> Delete([FromQuery] int id)
        {
            if (IdExists(id))
            {
                DBContext.Context.AidanTable.Remove(GetMainById(id));
                DBContext.Context.SaveChanges();
                return Ok(new CustomResponse() { Title = "Successfully Deleted", Message = "Row of ID: " + id + " has been deleted." });
            }
            return StatusCode(404, new { id });
        }
        private bool IdExists(int id) => DBContext.Context.AidanTable.Any(e => e.ID.Equals(id));
        private AidanTable GetMainById(int id)
        {
            return DBContext.Context.AidanTable.First(e => e.ID == id);
        }
    }

}