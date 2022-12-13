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
    public class IsabelleWController : ControllerBase
    {
        
        private CustomResponse NotFoundMessage = new CustomResponse()
        {
            Title = "Not Found",
            Message = "There is no row with that ID"
        };

        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Get([FromQuery] int? id)
        {
            if(id.HasValue && id.Value != 0)
            {
                return IdExists(id.Value) ? Ok(GetIsabelleById(id.Value)) : StatusCode(404, NotFoundMessage);
            }
            return Ok(DBContext.Context.IsabelleTable.Take(5));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]

        public ActionResult Post([FromQuery] string FirstName, [FromQuery] string LastName, [FromQuery] string FavCeleb, [FromQuery] string FavMovie, [FromQuery] string FavAnimal)
        {
            var added = new IsabelleTable() { Id = GetNextAvailableID(), FirstName = FirstName, LastName = LastName, FavoriteCeleb = FavCeleb, FavoriteMovie = FavMovie, FavoriteAnimal = FavAnimal };
            DBContext.Context.Add(added);
            DBContext.Context.SaveChanges();
            return StatusCode(StatusCodes.Status202Accepted, added);
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public ActionResult Update([FromQuery] int id, [FromQuery] string FirstName = null, [FromQuery] string LastName = null, [FromQuery] string FavCeleb = null, [FromQuery] string FavMovie = null, [FromQuery] string FavAnimal = null)
        {
            if (!IdExists(id))
                return StatusCode(404,NotFoundMessage);
            Debug.WriteLine("Update by id");
            
            var j = GetIsabelleById(id);
            
            if (FirstName != null)
                j.FirstName = FirstName;
            if (LastName != null)
                j.LastName = LastName;
            if (FavCeleb != null)
                j.FavoriteCeleb = FavCeleb;
            if (FavMovie != null)
                j.FavoriteMovie = FavMovie;
            if (FavAnimal != null)
                j.FavoriteAnimal = FavAnimal;
            DBContext.Context.IsabelleTable.Update(j);
            DBContext.Context.SaveChanges();
            return StatusCode(202, j);
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
        public ActionResult Delete([FromQuery] int id)
        {
            if (IdExists(id))
            {
                DBContext.Context.IsabelleTable.Remove(GetIsabelleById(id));
                DBContext.Context.SaveChanges();
                return Ok(new CustomResponse() {Title = "Successfully Deleted", Message = "Row with ID: " + id + " has been deleted"});
            }
            return StatusCode(404, NotFoundMessage);
        }

        private bool IdExists(int id) => DBContext.Context.IsabelleTable.Any(e => e.Id == id);
        private IsabelleTable GetIsabelleById(int id) => DBContext.Context.IsabelleTable.First(e => e.Id == id);
    }
}
