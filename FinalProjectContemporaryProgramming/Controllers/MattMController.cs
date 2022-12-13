using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FinalProjectContemporaryProgramming.Models.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectContemporaryProgramming.Controllers

{
    [ApiController]
    [Route("[controller]")]
    public class MattMController : ControllerBase
    {

        private object NotFoundMessage = new
        {
            Title = "Not Found",
            Message = "There is no row with that ID"
        };
        private ObjectResult CheckForLies(string favGame, string favSport, String favSeason)
        {
            if (favGame != null && favGame != "Call of Duty Warzone")
            {
                return StatusCode(406, new { Title = "Invalid favGame", Message = $"Their Favorite videogame clearly isn't '{favGame}'. If you wish to fill this table with lies then please mark this person as a liar. " });
            }
            else if (favSport != null && favSport != "Football")
            {
                return StatusCode(406, new { Title = "Invalid Favorite Sport", Message = $"Their Favorite Sport clearly isn't '{favSport}'. If you wish to fill this table with lies then please mark this person as a liar. " });
            }
            else if (favSeason != null && favSeason != "Spring")
            {
                return StatusCode(406, new { Title = "Invalid Favorite Season", Message = $"Their Favorite Season clearly isn't '{favSeason}'. if you wish to fill this table with lies then please mark this person as a liar. " });
            }
            return Ok(null);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Get([FromQuery] int? id)
        {
            if(id.HasValue && id.Value != 0)
            {
                return IdExists(id.Value) ? Ok(GetMattById(id.Value)) : StatusCode(404, NotFoundMessage);
            }
            return Ok(DBContext.Context.MattTable.Take(5));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public ActionResult Post([FromQuery] string FirstName, [FromQuery] string LastName, [FromQuery] string FavGame = "MineCraft", [FromQuery] string FavSport = "Soccer", [FromQuery] string FavSeason = "Winter", [FromQuery] bool IsALiar = false)
        {
            if (!IsALiar)
            {
                var islying = CheckForLies(FavGame, FavSport, FavSeason);
                if (islying.StatusCode == 406)
                    return islying;
            }
            var added = new MattTable() { Id = GetNextAvailableID(), FirstName = FirstName, LastName = LastName, FavoriteGame = FavGame, FavoriteSport = FavSport, FavoriteSeason = FavSeason, IAmALiar = IsALiar };
            DBContext.Context.Add(added);
            DBContext.Context.SaveChanges();
            return StatusCode(202, added);
        }
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Update([FromQuery] int id, [FromQuery] string FirstName = null, [FromQuery] string LastName = null, [FromQuery] string FavGame = null, [FromQuery] string FavSport = null, [FromQuery] string FavSeason = null, [FromQuery] bool? IsALiar = null)
        {
            if (!IdExists(id))
                return StatusCode(404, NotFoundMessage);
            var n = GetMattById(id);
            var islying = CheckForLies(FavGame, FavSport, FavSeason);
            if (IsALiar.HasValue)
            {
                if (!IsALiar.Value)
                {
                    if (islying.StatusCode == 406)
                        return islying;
                }
            }
            else if (!n.IAmALiar && islying.StatusCode == 406)
            {
                return islying;
            }
            if (FirstName != null)
                n.FirstName = FirstName;
            if (LastName != null)
                n.LastName = LastName;
            if (FavSeason != null)
                n.FavoriteSeason = FavSeason;
            if (FavSport != null)
                n.FavoriteSport = FavSport;
            if (FavGame != null)
                n.FavoriteGame = FavGame;
            if (IsALiar.HasValue)
                n.IAmALiar = IsALiar.Value;
            DBContext.Context.MattTable.Update(n);
            DBContext.Context.SaveChanges();
            return StatusCode(202, n);
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
                DBContext.Context.MattTable.Remove(GetMattById(id));
                DBContext.Context.SaveChanges();
                return Ok(new { Title = "Successfully Deleted", Message = "Row of ID: " + id + " has been deleted." });
            }
            return StatusCode(404, NotFoundMessage);
        }
        private bool IdExists(int id) => DBContext.Context.MattTable.Any(e => e.Id.Equals(id));
        private MattTable GetMattById(int id) => DBContext.Context.MattTable.First(e => e.Id == id);
    }
}
