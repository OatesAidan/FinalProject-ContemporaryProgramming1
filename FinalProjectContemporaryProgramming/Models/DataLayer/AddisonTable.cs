using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FinalProjectContemporaryProgramming.Models.DataLayer
{
    public partial class AddisonTable
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string FavoriteBreakfeast { get; set; }
        [StringLength(50)]
        public string FavoriteDinner { get; set; }
        [StringLength(50)]
        public string FavoriteLunch { get; set; }
        //no length for errors
    }
}
