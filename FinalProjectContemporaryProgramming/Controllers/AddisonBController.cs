using FinalProjectContemporaryProgramming.Models.DataLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace FinalProjectContemporaryProgramming.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddisonBController : ControllerBase
    {
        public class Matt : AddisonTable
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
        public ActionResult Get([FromQuery] int? Id)
        {
            if (Id.HasValue && Id.Value != 0)
            {
                return IdExists(Id.Value) ? Ok(GetAddisonById(Id.Value)) : StatusCode(404, NotFoundMessage);
            }
            return Ok(DBContext.Context.AddisonTable.Take(5));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public ActionResult Post(
            [FromQuery] string FirstName,
            [FromQuery] string LastName,
            [FromQuery] string FavoriteBreakfeast = null,
            [FromQuery] string FavoriteDinner = null,
            [FromQuery] string FavoriteLunch = null
            )
        {
            var posted = new AddisonTable() { 
                Id = GetNextAvailableID(), 
                FirstName = FirstName, 
                LastName = LastName,
                FavoriteBreakfeast = FavoriteBreakfeast,
                FavoriteDinner = FavoriteDinner,
                FavoriteLunch = FavoriteLunch
            };
            
            DBContext.Context.Add(posted);
            DBContext.Context.SaveChanges();
            return StatusCode(202, posted);
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public ActionResult Update(
            [FromQuery] int id, 
            [FromQuery] string FirstName = null, 
            [FromQuery] string LastName = null, 
            [FromQuery] string FavoriteBreakfeast = null, 
            [FromQuery] string FavoriteDinner = null,
            [FromQuery] string FavoriteLunch = null)
        {
            if (!IdExists(id))
                return StatusCode(404, NotFoundMessage);
            var m = GetAddisonById(id);
            if (FirstName != null)
                m.FirstName = FirstName;
            if (LastName != null)
                m.LastName = LastName;
            if (FavoriteBreakfeast != null)
                m.FavoriteBreakfeast = FavoriteBreakfeast;
            if (FavoriteDinner != null)
                m.FavoriteDinner = FavoriteDinner;
            if (FavoriteLunch != null)
                m.FavoriteLunch = FavoriteLunch;
            DBContext.Context.AddisonTable.Update(m);
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
        public ActionResult<Matt> Delete([FromQuery] int id)
        {
            if (IdExists(id))
            {
                DBContext.Context.AddisonTable.Remove(GetAddisonById(id));
                DBContext.Context.SaveChanges();
                return Ok(new CustomResponse() { Title = "Successfully Deleted", Message = "Row with ID: " + id + " has been deleted." });
            }
            return StatusCode(404, new { id });
        }
        private bool IdExists(int id) => DBContext.Context.AddisonTable.Any(e => e.Id.Equals(id));
        private AddisonTable GetAddisonById(int id)
        {
            return DBContext.Context.AddisonTable.First(e => e.Id == id);
        }
    }
}
