using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FinalProjectContemporaryProgramming.Models.DataLayer
{
    public partial class IsabelleTable
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
        public string FavoriteCeleb { get; set; }
        [StringLength(50)]
        public string FavoriteMovie { get; set; }
        [Column("FavoriteMovie")]
        [StringLength(50)]
        public string FavoriteAnimal { get; set; }
    }
}
